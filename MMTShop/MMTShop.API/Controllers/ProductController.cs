using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MMTShop.Data.Entities.Products;
using MMTShop.Data.Repositories;

namespace MMTShop.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IDataRepository _repository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IDataRepository repository,ILogger<ProductController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Api to get all data
        /// </summary>
        /// <param name="page">one (1) based page index</param>
        /// <param name="size">page size</param>
        /// <returns></returns>
        /// 

        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var data = await _repository.Products.GetAsync(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int size = 10, bool onlyActive = true)
        {
            try
            {
                var data = await _repository.Products.GetAllAsync(page, size, onlyActive);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }
        [HttpGet("categoryId")]
        public async Task<IActionResult> GetbyCayegory(int categoryId, int page = 1, int size = 10)
        {
            try
            {
                var data = await _repository.Products.GetAllSP(categoryId,page, size);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }
        [HttpGet("Featured")]
        public async Task<IActionResult> GetFeatured()
        {
            try
            {
                var data = await _repository.Products.GetFeaturedAsy();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            try
            {
                if (product == null)
                {
                    return NotFound();
                }
                product.IsActive = true;
                await _repository.Products.AddAsync(product);
                var rows = await _repository.SaveAsync();
                if (rows > 0)
                {
                    return Ok(product);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Product product)
        {
            try
            {
                _repository.Products.Update(product);

                int rows = await _repository.SaveAsync();
                if (rows > 0)
                {
                    return Ok(product);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            try
            {
                var data = await _repository.Products.GetAsync(Id);
                if (data == null)
                {
                    return NotFound();
                }
                data.IsActive = false;
                _repository.Products.Update(data);
                var rows = await _repository.SaveAsync();
                if (rows > 0)
                {
                    return Ok(data);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }
    }
}
