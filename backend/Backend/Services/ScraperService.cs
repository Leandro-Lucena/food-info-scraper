using System.Globalization;
using Backend.Data;
using Backend.Models;
using HtmlAgilityPack;

namespace Backend.Services
{
    /// <summary>
    /// Provides methods to scrape food data from web pages and save it to a database.
    /// </summary>
    public class Scraper
    {
        private readonly string _foodsUrl;
        private readonly string _foodDetailUrl;
        private readonly Repository _repository;

        /// <summary>
        /// Constructor for the Scraper service.
        /// </summary>
        /// <param name="foodsUrl">The URL to the foods list page.</param>
        /// <param name="foodDetailUrl">The URL to the food detail page template.</param>
        /// <param name="repository">The repository to store the scraped information.</param>
        public Scraper(string foodsUrl, string foodDetailUrl, Repository repository)
        {
            _foodsUrl = foodsUrl;
            _foodDetailUrl = foodDetailUrl;
            _repository = repository;
        }

        /// <summary>
        /// Scrapes the foods list page and saves the food details and basic food information into the database.
        /// </summary>
        /// <returns>True if the operation was successful, false otherwise.</returns>
        /// <remarks>
        /// The scraper will iterate through all pages of the foods list and save
        /// the information in batches of 10 foods and 10 food details.
        /// </remarks>
        public async Task<bool> ScrapeFoodsAsync()
        {
            try
            {
                using HttpClient client = new();
                List<Food> foodsBatch = new();
                List<FoodDetails> foodDetailsBatch = new();
                int currentPage = 1;

                while (true)
                {
                    string url = $"{_foodsUrl}{currentPage}";
                    string pageContent = await client.GetStringAsync(url);
                    HtmlDocument doc = new();
                    doc.LoadHtml(pageContent);

                    var rows = doc.DocumentNode.SelectNodes("//table//tr");

                    if (rows == null || rows.Count == 1)
                    {
                        Console.WriteLine($"No more data found on page {currentPage}. Ending the scraping process.");
                        break;
                    }

                    foreach (var row in rows)
                    {
                        var columns = row.SelectNodes("./td");

                        if (columns != null && columns.Count >= 4)
                        {
                            string code = columns[0].InnerText.Trim();
                            string name = columns[1].InnerText.Trim();
                            string scientificName = columns[2].InnerText.Trim();
                            string foodGroup = columns[3].InnerText.Trim();

                            foodsBatch.Add(new Food
                            {
                                Code = code,
                                Name = name,
                                ScientificName = scientificName,
                                FoodGroup = foodGroup
                            });

                            var details = await GetFoodDetailsAsync(client, code);
                            if (details != null)
                            {
                                foodDetailsBatch.AddRange(details);
                            }
                        }
                    }

                    if (foodsBatch.Any())
                    {
                        _repository.UpdateFoods(foodsBatch);
                        foodsBatch.Clear();
                    }

                    if (foodDetailsBatch.Any())
                    {
                        _repository.SaveFoodDetails(foodDetailsBatch);
                        foodDetailsBatch.Clear();
                    }

                    Console.WriteLine($"Page {currentPage} scraped");
                    currentPage++;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error scraping foods: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Scrapes the food details page for a given food code, returning a list of FoodDetails objects.
        /// </summary>
        /// <param name="client">The client to use for making the HTTP request.</param>
        /// <param name="foodCode">The code of the food item to retrieve details for.</param>
        /// <returns>The list of FoodDetails objects, or an empty list if an error occurred.</returns>
        private async Task<List<FoodDetails>> GetFoodDetailsAsync(HttpClient client, string foodCode)
        {
            try
            {
                string additionalUrl = $"{_foodDetailUrl}{foodCode}";
                string additionalPageContent = await client.GetStringAsync(additionalUrl);
                HtmlDocument additionalDoc = new();
                additionalDoc.LoadHtml(additionalPageContent);

                var tableRows = additionalDoc.DocumentNode.SelectNodes("//table[@id='tabela1']//tr");

                List<FoodDetails> detailsList = new();

                if (tableRows != null)
                {
                    foreach (var row in tableRows)
                    {
                        var columns = row.SelectNodes("./td");

                        if (columns != null && columns.Count == 9)
                        {
                            detailsList.Add(new FoodDetails
                            {
                                FoodCode = foodCode,
                                Component = columns[0].InnerText.Trim(),
                                Units = columns[1].InnerText.Trim(),
                                ValuePer100g = ParseDecimal(columns[2].InnerText),
                                StandardDeviation = ParseDecimal(columns[3].InnerText),
                                MinValue = ParseDecimal(columns[4].InnerText),
                                MaxValue = ParseDecimal(columns[5].InnerText),
                                DataCount = ParseInt(columns[6].InnerText),
                                FoodReferences = columns[7].InnerText.Trim(),
                                DataType = columns[8].InnerText.Trim()
                            });
                        }
                    }
                }
                return detailsList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error scraping food details for product {foodCode}: {ex.Message}");
                return new List<FoodDetails>();
            }
        }

        /// <summary>
        /// Parses a decimal value from a string, returning 0 if the string is null, empty, or contains a hyphen.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <returns>The parsed decimal, or 0 if the string is invalid.</returns>

        internal static decimal ParseDecimal(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || input == "-")
                return 0;
            return decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out var result) ? result : 0;
        }

        /// <summary>
        /// Parses an integer value from a string, returning 0 if the string is null, empty, or contains a hyphen.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <returns>The parsed integer, or 0 if the string is invalid.</returns>
        internal static int ParseInt(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || input == "-")
                return 0;
            return int.TryParse(input, out var result) ? result : 0;
        }

    }
}
