using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PoqAssignment.Application.DTO;
using PoqAssignment.Application.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace PoqAssignment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MockyProductsController : Controller
    {
        private readonly ILogger<MockyProductsController> _logger;
        private readonly MockyProductsService _mockyProductsService;

        public MockyProductsController(MockyProductsService mockyProductsService,
            ILogger<MockyProductsController> logger)
        {
            _mockyProductsService = mockyProductsService;
            _logger = logger;
        }

        [HttpPost("filter")]
        [HttpPost]
        [SwaggerOperation(Summary = "Retrieve all products",
            Description = "This endpoint retrieves all products from mocky.io")]
        public IActionResult GetAll([FromQuery] FilterByUser filter = null)
        {
            if (!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            _logger.LogInformation("Received request for retrieving Mocky products");
            var result = _mockyProductsService.GetAll(filter);

            return Json(result);
        }
    }
}