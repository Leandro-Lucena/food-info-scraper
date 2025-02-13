# Food Data Visualization

## Performance Highlight 🚀

This application delivers high-performance food data extraction and processing. Using C# and HtmlAgilityPack, it scrapes 5,729 pages, extracting and saving 215,494 rows into the database—all in under 5 minutes! This efficiency showcases the power of ASP.NET Core in handling large-scale web scraping and data processing.

## Overview

This is an application that allows detailed visualization of food data. The project also includes an automated scraper for extracting food data from external sources.

## Technologies Used

- C#
- ASP.NET Core
- SQLite
- HtmlAgilityPack (for scraping)
- React (TypeScript)
- Material UI
- Docker Compose

## Project Structure

### Backend:

- **Controllers:**

  - `FoodController`: Handles food operations.
    - `GetFoods()`: Returns all foods from the database.
    - `GetFoodDetails(string foodCode)`: Returns the details of a specific food.
  - `ScraperController`: Handles scraping operations.
    - `UpdateFoodsAsync()`: Updates foods from automated extraction.

- **Data:**

  - `DatabaseInitializer`: Initializes the database and creates the tables.
    - `Initialize()`: Creates the "Food" and "FoodDetails" tables if they do not exist.
  - **Repository**: Responsible for interacting with the database.
    - `UpdateFoods(IEnumerable<Food>)`: Updates foods in the database.
    - `SaveFoodDetails(List<FoodDetails>)`: Saves food details.
    - `GetAllFoods()`: Returns all foods.
    - `GetFoodDetailsByCode(string foodCode)`: Returns food details by code.

- **Models:**

  - `Food`: Represents a food item.
  - `FoodDetails`: Nutritional details of a food item.

- **Services:**

  - `Scraper`: Performs scraping of food data.
    - `ScrapeFoodsAsync()`: Retrieves the list of foods and stores them in the database.
    - `GetFoodDetailsAsync(HttpClient client, string foodCode)`: Retrieves details of a food item.

- **Program**:
  - Configures and initializes the backend.

### Frontend:

- **Technologies:**

  - React (TypeScript)
  - Material UI

  The application displays an interface with two main pages:

  - **Home Page**: Displays a table with all foods and an update button. The button triggers a scraping process that takes about 5 minutes to retrieve and save the data in the SQLite database. After the update, the data is persisted in the SQLite database and the food list is automatically loaded into the table.

  - **Details Page**: When a food item is clicked in the table, the user is redirected to a new page displaying the nutritional details of the food item. There is a back button that redirects to the previous page.

### Docker Compose:

- The backend and frontend are containerized using Docker Compose.
- The SQLite database is persisted using Docker volumes.

## Steps for Configuration and Execution

### 1. Clone the repository:

First, clone the repository using the following command:

```bash
git clone https://github.com/Leandro-Lucena/food-info-scraper.git
cd food-info-scraper
```

### 2. Configure the .env in the frontend:

In the `/frontend` directory, rename the `.env.example` file to `.env`.

```bash
mv .env.example .env
```

### 3. Requirements to Run Docker Compose:

- **Windows:**

  - Ensure Docker Desktop is installed.
  - Enable virtualization in the BIOS and enable WSL2 (Windows Subsystem for Linux 2) in Docker Desktop.

- **Linux/Mac:**
  - Have Docker Engine and Docker Compose installed.
  - Check if you have permissions to run Docker (on Linux, you may need to use `sudo` for Docker commands).

### 4. Start the containers with Docker Compose:

In the project's root directory, run the following command:

```bash
docker-compose up
```

This command will build and start the containers for the backend and frontend. The server will be available at http://localhost:3000

##

The structure was carefully organized to provide a scalable and easy-to-maintain solution, with a focus on simplicity and good development practices. I'm available to answer questions or provide more information about the project.
