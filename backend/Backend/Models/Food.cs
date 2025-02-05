namespace Backend.Models
{
    /// <summary>
    /// Represents a food item with its details.
    /// </summary>
    public class Food
    {
        /// <summary>
        /// Gets or sets the code of the food.
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the food.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the scientific name of the food.
        /// </summary>
        public string ScientificName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the food group of the food.
        /// </summary>
        public string FoodGroup { get; set; } = string.Empty;
    }
}
