using Backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    /// <summary>
    /// Controller for handling food-related requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FoodController : ControllerBase
    {
        private readonly Repository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FoodController"/> class.
        /// </summary>
        /// <param name="repository">The repository instance used for accessing food data.</param>
        public FoodController(Repository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all foods stored in the database.
        /// </summary>
        /// <returns>All foods.</returns>
        [HttpGet]
        public IActionResult GetFoods()
        {
            try
            {
                var foods = _repository.GetAllFoods();
                return Ok(new
                {
                    Success = true,
                    Message = "Foods retrieved successfully.",
                    Data = foods
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "An error occurred while retrieving foods.",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Gets food details by food code.
        /// </summary>
        /// <param name="foodCode">The code of the food.</param>
        /// <returns>Food details.</returns>
        [HttpGet("{foodCode}")]
        public IActionResult GetFoodDetails(string foodCode)
        {
            try
            {
                var (foodName, foodDetails) = _repository.GetFoodDetailsByCode(foodCode);

                if (string.IsNullOrEmpty(foodName) || foodDetails.Count == 0)
                {
                    return NotFound(new
                    {
                        Success = false,
                        Message = $"No details found for food code: {foodCode}."
                    });
                }

                return Ok(new
                {
                    Success = true,
                    Message = "Food details retrieved successfully.",
                    FoodName = foodName,
                    Data = foodDetails
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "An error occurred while retrieving food details.",
                    Error = ex.Message
                });
            }
        }

    }
}
