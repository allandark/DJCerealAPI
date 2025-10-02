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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }



        private List<ProductFilterEntity> ParseQuery(string query)
        {
            List<ProductFilterEntity> filter = new List<ProductFilterEntity>();
            if(query == null)
            {
                return filter;
            }
            var entreis = query.Split('&');
            foreach(var entry in entreis)
            {
                foreach(var op in Utils.FilterOps)
                {
                    if (entry.Contains(op))
                    {

                        var values = entry.Split(op);
                        string category = "";
                        string value = values[1];
                        if (Utils.ProductMemberNames.TryGetValue(values[0].ToLower(), out category))
                        {
                            if(category == "Mfr")
                            {
                                if (! ProductFactory.ManufacturerStrings.TryGetValue(values[1].ToLower(), out value))
                                {
                                    Console.WriteLine(string.Format("Invalid manufacturer: {0}", value));
                                    filter.Clear();
                                    return filter;
                                }
                            }
                            else if( category == "Type")
                            {
                                if(!ProductFactory.TypeStrings.TryGetValue(values[1].ToLower(), out value))
                                {
                                    Console.WriteLine(string.Format("Invalid type: {0}", value));
                                    filter.Clear();
                                    return filter;
                                }

                            }
                            filter.Add(new ProductFilterEntity(category, op, value));
                        }
                        else
                        {
                            Console.WriteLine(string.Format("Category does not exist: {0}!", values[0]));
                            filter.Clear();
                            return filter;
                        }
                        
   
                    }
                }

            }
            return filter;
        }

        
        [HttpGet()]
        [ProducesResponseType(200)]
        public IActionResult Get([FromQuery] string query)
        {
            

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }   
            
            var queryDict = ParseQuery(query);
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

        
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult Get(int id)
        {
            Product product = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!_productRepository.Exists(id))
            {
                return NotFound(id);
            }

            product = _productRepository.Get(id);

            return Ok(product);            
        }

        
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
