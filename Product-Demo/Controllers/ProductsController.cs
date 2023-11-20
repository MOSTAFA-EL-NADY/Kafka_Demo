using Microsoft.AspNetCore.Mvc;
using Product_Demo.Models;
using System.Text.Json;

namespace Product_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly string TopicName = "productTopic";
        private readonly KafkaProducer _kafkaProducer;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
            _kafkaProducer = new KafkaProducer();
        }

        [HttpPost]
        public ActionResult PostProduct(Product product)
        {
            var prodcuctJson = JsonSerializer.Serialize(product);
            _kafkaProducer.ProduceMessage(TopicName, prodcuctJson);

            return Ok("product Added");
        }
    }
}
