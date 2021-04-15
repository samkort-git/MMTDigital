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
    public class CategoryController : ControllerBase
    {
        private readonly IDataRepository _repository;
        private readonly ILogger<ProductController> _logger;

        public CategoryController(IDataRepository repository,ILogger<ProductController> logger)
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
                var data = await _repository.Categories.GetAsync(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int size = 10, bool onlyActive = false)
        {
            try
            {
                var data = await _repository.Categories.GetAllAsync(page, size, onlyActive);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpGet("Active")]
        public async Task<IActionResult> GetActive(int page = 1, int size = 10, bool onlyActive = true)
        {
            try
            {
                var data = await _repository.Categories.GetAllSP(page, size);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(Category category)
        {
            try
            {
                if (category == null)
                {
                    return NotFound();
                }
                category.IsActive = true;
                await _repository.Categories.AddAsync(category);
                var rows = await _repository.SaveAsync();
                if (rows > 0)
                {
                    return Ok(category);
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
        public async Task<IActionResult> Put([FromBody] Category category)
        {
            try
            {
                _repository.Categories.Update(category);

                int rows = await _repository.SaveAsync();
                if (rows > 0)
                {
                    return Ok(category);
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
                var data = await _repository.Categories.GetAsync(Id);
                if (data == null)
                {
                    return NotFound();
                }
                data.IsActive = false;
                _repository.Categories.Update(data);
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
