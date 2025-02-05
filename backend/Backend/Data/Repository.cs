using System.Data.SQLite;
using Backend.Models;

namespace Backend.Data
{
    /// <summary>
    /// Provides methods to interact with the SQLite database for food-related data.
    /// </summary>
    public class Repository
    {
        private readonly string _connectionString;


        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class with a database connection string.
        /// </summary>
        /// <param name="connectionString">The connection string to the SQLite database.</param>
        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Updates food list in the database.
        /// </summary>
        /// <param name="foods">The list of foods to update.</param>
        public void UpdateFoods(IEnumerable<Food> foods)
        {
            try
            {
                using var connection = new SQLiteConnection(_connectionString);
                connection.Open();

                using var transaction = connection.BeginTransaction();

                string insertQuery = @"
                    INSERT OR REPLACE INTO Food (Code, Name, ScientificName, FoodGroup)
                    VALUES (@code, @name, @scientificName, @foodGroup)";

                using var command = new SQLiteCommand(insertQuery, connection);

                var codeParam = new SQLiteParameter("@code");
                var nameParam = new SQLiteParameter("@name");
                var scientificNameParam = new SQLiteParameter("@scientificName");
                var foodGroupParam = new SQLiteParameter("@foodGroup");

                command.Parameters.Add(codeParam);
                command.Parameters.Add(nameParam);
                command.Parameters.Add(scientificNameParam);
                command.Parameters.Add(foodGroupParam);

                foreach (var food in foods)
                {
                    codeParam.Value = food.Code;
                    nameParam.Value = food.Name;
                    scientificNameParam.Value = food.ScientificName;
                    foodGroupParam.Value = food.FoodGroup;

                    command.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Saves a list of food details into the database.
        /// </summary>
        /// <param name="foodDetailsList">The list of food details to be saved.</param>
        /// <remarks>
        /// If the provided list is null or empty, the method returns immediately.
        /// Each food detail from the list is inserted or replaced in the FoodDetails table.
        /// </remarks>
        public void SaveFoodDetails(List<FoodDetails> foodDetailsList)
        {
            if (foodDetailsList == null || foodDetailsList.Count == 0)
                return;

            try
            {
                using var connection = new SQLiteConnection(_connectionString);
                connection.Open();

                using var transaction = connection.BeginTransaction();

                string insertQuery = @"
            INSERT OR REPLACE INTO FoodDetails 
            (FoodCode, Component, Units, ValuePer100g, StandardDeviation, MinValue, MaxValue, DataCount, FoodReferences, DataType)
            VALUES 
            (@foodCode, @component, @units, @valuePer100g, @standardDeviation, @minValue, @maxValue, @dataCount, @foodReferences, @dataType)";

                using var command = new SQLiteCommand(insertQuery, connection);

                var foodCodeParam = command.Parameters.Add("@foodCode", System.Data.DbType.String);
                var componentParam = command.Parameters.Add("@component", System.Data.DbType.String);
                var unitsParam = command.Parameters.Add("@units", System.Data.DbType.String);
                var valuePer100gParam = command.Parameters.Add("@valuePer100g", System.Data.DbType.Decimal);
                var standardDeviationParam = command.Parameters.Add("@standardDeviation", System.Data.DbType.Decimal);
                var minValueParam = command.Parameters.Add("@minValue", System.Data.DbType.Decimal);
                var maxValueParam = command.Parameters.Add("@maxValue", System.Data.DbType.Decimal);
                var dataCountParam = command.Parameters.Add("@dataCount", System.Data.DbType.Int32);
                var foodReferencesParam = command.Parameters.Add("@foodReferences", System.Data.DbType.String);
                var dataTypeParam = command.Parameters.Add("@dataType", System.Data.DbType.String);

                foreach (var foodDetails in foodDetailsList)
                {
                    foodCodeParam.Value = foodDetails.FoodCode;
                    componentParam.Value = foodDetails.Component;
                    unitsParam.Value = foodDetails.Units;
                    valuePer100gParam.Value = foodDetails.ValuePer100g;
                    standardDeviationParam.Value = foodDetails.StandardDeviation;
                    minValueParam.Value = foodDetails.MinValue;
                    maxValueParam.Value = foodDetails.MaxValue;
                    dataCountParam.Value = foodDetails.DataCount;
                    foodReferencesParam.Value = foodDetails.FoodReferences;
                    dataTypeParam.Value = foodDetails.DataType;

                    command.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }


        /// <summary>
        /// Gets all foods stored in the database.
        /// </summary>
        /// <returns>List of all foods.</returns>
        public List<Food> GetAllFoods()
        {
            var foods = new List<Food>();

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM Food";

                using var command = new SQLiteCommand(selectQuery, connection);
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    foods.Add(new Food
                    {
                        Code = reader.GetString(0),
                        Name = reader.GetString(1),
                        ScientificName = reader.GetString(2),
                        FoodGroup = reader.GetString(3)
                    });
                }
            }

            return foods;
        }

        /// <summary>
        /// Retrieves the name and details of a food item by its code from the database.
        /// </summary>
        /// <param name="foodCode">The code of the food item to retrieve details for.</param>
        /// <returns>
        /// A tuple containing the food name as a string and a list of FoodDetails.
        /// The list contains detailed information about the food components.
        /// </returns>
        /// <remarks>
        /// If no name or details are found for the specified food code, the food name will be an empty string
        /// and the details list will be empty.
        /// </remarks>
        public (string FoodName, List<FoodDetails> FoodDetailsList) GetFoodDetailsByCode(string foodCode)
        {
            var foodDetailsList = new List<FoodDetails>();
            string foodName = string.Empty;

            try
            {
                using var connection = new SQLiteConnection(_connectionString);
                connection.Open();

                string nameQuery = "SELECT Name FROM Food WHERE Code = @foodCode";
                using (var nameCommand = new SQLiteCommand(nameQuery, connection))
                {
                    nameCommand.Parameters.AddWithValue("@foodCode", foodCode);
                    var result = nameCommand.ExecuteScalar();
                    if (result != null)
                    {
                        foodName = result?.ToString() ?? string.Empty;
                    }
                }

                string selectQuery = "SELECT * FROM FoodDetails WHERE FoodCode = @foodCode";

                using var command = new SQLiteCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@foodCode", foodCode);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    foodDetailsList.Add(new FoodDetails
                    {
                        FoodCode = reader.GetString(0),
                        Component = reader.GetString(1),
                        Units = reader.GetString(2),
                        ValuePer100g = reader.GetDecimal(3),
                        StandardDeviation = reader.GetDecimal(4),
                        MinValue = reader.GetDecimal(5),
                        MaxValue = reader.GetDecimal(6),
                        DataCount = reader.GetInt32(7),
                        FoodReferences = reader.GetString(8),
                        DataType = reader.GetString(9)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving food details: " + ex.Message);
            }

            return (foodName, foodDetailsList);
        }

    }
}
