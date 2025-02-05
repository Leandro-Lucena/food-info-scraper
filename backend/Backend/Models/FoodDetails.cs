namespace Backend.Models
{
    /// <summary>
    /// Represents the details of a food item.
    /// </summary>
    public class FoodDetails
    {
        /// <summary>
        /// Gets or sets the food code.
        /// </summary>
        public string FoodCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the component of the food.
        /// </summary>
        public string Component { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the units of the component.
        /// </summary>
        public string Units { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the value per 100 grams.
        /// </summary>
        public decimal ValuePer100g { get; set; }

        /// <summary>
        /// Gets or sets the standard deviation of the value.
        /// </summary>
        public decimal StandardDeviation { get; set; }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        public decimal MinValue { get; set; }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        public decimal MaxValue { get; set; }

        /// <summary>
        /// Gets or sets the data count.
        /// </summary>
        public int DataCount { get; set; }

        /// <summary>
        /// Gets or sets the food references.
        /// </summary>
        public string FoodReferences { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the data type.
        /// </summary>
        public string DataType { get; set; } = string.Empty;
    }
}
