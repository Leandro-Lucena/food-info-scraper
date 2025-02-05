using System.Data.SQLite;

namespace Backend.Data
{

    /// <summary>
    /// Provides methods to initialize the SQLite database by creating necessary tables.
    /// </summary>
    public class DatabaseInitializer
    {
        private readonly string _connectionString;
        /// <summary>
        /// Initializes a new instance of the class with a database connection string.
        /// </summary>
        /// <param name="connectionString">The connection string to the SQLite database.</param>
        public DatabaseInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }
        /// <summary>
        /// Initializes the database by creating necessary tables if they do not exist.
        /// </summary>
        /// <remarks>
        /// This method ensures the creation of the "Food" and "FoodDetails" tables in the database.
        /// The "Food" table contains basic information about each food item, while the "FoodDetails"
        /// table stores detailed component information associated with each food code.
        /// </remarks>
        public void Initialize()
        {
            try
            {
                var directoryPath = Path.GetDirectoryName(_connectionString.Split('=')[1]);
                if (!string.IsNullOrEmpty(directoryPath))
                {
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                        Console.WriteLine($"Directory created at: {directoryPath}");
                    }
                }

                using var connection = new SQLiteConnection(_connectionString);
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Food (
                        Code TEXT PRIMARY KEY,
                        Name TEXT,
                        ScientificName TEXT,
                        FoodGroup TEXT
                    )";

                using var command = new SQLiteCommand(createTableQuery, connection);
                command.ExecuteNonQuery();

                string createFoodDetailsTableQuery = @"
                    CREATE TABLE IF NOT EXISTS FoodDetails (
                        FoodCode TEXT,
                        Component TEXT,
                        Units TEXT,
                        ValuePer100g REAL,
                        StandardDeviation REAL,
                        MinValue REAL,
                        MaxValue REAL,
                        DataCount INTEGER,
                        FoodReferences TEXT,
                        DataType TEXT,
                        PRIMARY KEY (FoodCode, Component)
                    )";

                using var createFoodDetailsTableCommand = new SQLiteCommand(createFoodDetailsTableQuery, connection);
                createFoodDetailsTableCommand.ExecuteNonQuery();
                Console.WriteLine("Database initialized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing database: {ex.Message}");
            }
        }
    }
}
