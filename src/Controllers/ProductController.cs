using CerealAPI;
using CerealAPI.src.Data;
using CerealAPI.src.Models;
using CerealAPI.src.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CerealAPI.src.Controllers
{
    /// <summary>
    /// Class exposing the product endpoints. Implements the RESTful API.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
      
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        /// <summary>
        /// Get endpoint which returns full list or filtered list
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(200)]
        public IActionResult Get([FromQuery] string query)
        {
            

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }   
            
            var queryDict = Utils.ParseQuery(query);
            ICollection<Product> products = null;

            if (queryDict.Count == 0)
            {
                products  = _productRepository.GetAll();
            }
            else
            {
                products = _productRepository.GetAllFiltered(queryDict);
            }

            if(products.Count == 0)
            {
                return NotFound();
            }

            return Ok(products);
        }


        /// <summary>
        /// Get endpoint that retursn the product with id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!_productRepository.Exists(id))
            {
                return NotFound(id);
            }

            Product product = _productRepository.Get(id);

            return Ok(product);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(201)]
        public IActionResult Post([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var p = _productRepository.Create(product);
            return CreatedAtAction(nameof(Get), new { id = p.Id }, p);
        }

        /// <summary>
        /// Post endpoint to modify product if id exists, or create a new product if id does not
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns>The newly created/modified product and redirects to get/id endpoint</returns>
        [Authorize]
        [HttpPost("{id}")]        
        [ProducesResponseType(201)]        
        public IActionResult Post(int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // This is in conflict with REST response codes. Should return 409 Conflict for duplicate            
            if (_productRepository.Exists(id))
            {
                var p = _productRepository.Update(product);
                return CreatedAtAction(nameof(Get), new { id = p.Id }, p);
            }
            else
            {
                var p = _productRepository.Create(product);
                return CreatedAtAction(nameof(Get), new { id = p.Id }, p);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_productRepository.Exists(id))
            {
                return NotFound(id);
            }
            product.Id = id;
            var p = _productRepository.Update(product);
            return Ok(p);
        }

        /// <summary>
        /// Deletes product with key id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ok if sucessful</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_productRepository.Exists(id))
            {
                return NotFound(id);
            }
            if(!_productRepository.Delete(id))
            {
                return StatusCode(500, "An error occurred while deleting the product.");
            }
            
            return Ok(id);
        }
    }
}
