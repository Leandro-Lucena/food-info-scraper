using Microsoft.AspNetCore.Mvc;
using Backend.Services;

namespace Backend.Controllers
{
    /// <summary>
    /// Controller for scraping operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ScraperController : ControllerBase
    {
        private readonly Scraper _scraper;
        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        /// <summary>
        /// Constructor for the controller.
        /// </summary>
        /// <param name="scraper">The scraper service.</param>
        public ScraperController(Scraper scraper)
        {
            _scraper = scraper;
        }

        /// <summary>
        /// Endpoint to update foods by extraction.
        /// </summary>
        /// <returns>Result of the operation.</returns>
        [HttpGet("update-foods")]
        public async Task<IActionResult> UpdateFoodsAsync()
        {
            if (!_semaphore.Wait(0))
            {
                return StatusCode(429, new { message = "Update already in progress. Please wait." }); // 429 (Too Many Requests)
            }

            try
            {
                bool success = await _scraper.ScrapeFoodsAsync();

                if (success)
                {
                    return Ok(new
                    {
                        message = "Foods updated successfully."
                    });
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while updating foods.",
                    error = ex.Message
                });
            }
            finally
            {
                _semaphore.Release();
            }
        }

    }
}
