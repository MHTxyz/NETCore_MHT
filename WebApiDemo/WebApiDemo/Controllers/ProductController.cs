using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiDemo.Dtos;
using WebApiDemo.Repositories;
using WebApiDemo.Services;

namespace WebApiDemo.Controllers
{
    //[Route("api/Product")]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMailService _mailService;
        private readonly IProductRepository _productRepository;

        public ProductController(ILogger<ProductController> logger, IMailService mailService, IProductRepository productRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            //var res = new JsonResult(ProductService.Current.Products) {
            //    StatusCode = 200
            //};

            //return (res);

            //return Ok(ProductService.Current.Products);

            var products = _productRepository.GetProducts();
            var result = AutoMapper.Mapper.Map<IEnumerable<ProductWithoutMaterialDto>>(products);
            //var result = new List<ProductWithoutMaterialDto>();
            //foreach (var p in products)
            //{
            //    result.Add(new ProductWithoutMaterialDto
            //    {
            //        Id = p.Id,
            //        Name = p.Name,
            //        Price = p.Price,
            //        Description = p.Description,
            //    });
            //}

            return Ok(result);
        }

        //[HttpGet("GetProduct/{id}",Name = "GetProduct")]
        //[HttpGet("getProduct/{id}")]
        [HttpGet,Route("{id}",Name ="GetProduct")]
        public IActionResult GetProduct(int id, bool includeMaterial = false)
        {
            #region MyRegion
            /*
            try
            {
                throw new Exception("抛出个异常！！！");

                var product = ProductService.Current.Products.SingleOrDefault(p => p.Id == id);
                if (product == null)
                {
                    _logger.LogInformation($"Id为{id}的产品没有被找到");
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"查找Id为{id}的产品时出现了错误！！", ex);
                return StatusCode(500, "处理请求的时候发生了错误！");
            }
            */
            #endregion

            var product = _productRepository.GetProduct(id, includeMaterial);
            if (product == null)
            {
                return NotFound();
            }

            if (includeMaterial)
            {
                var productWithMaterial = AutoMapper.Mapper.Map<ProductDto>(product);
                //var productWithMaterial = new ProductDto
                //{
                //    Id = product.Id,
                //    Name = product.Name,
                //    Price = product.Price,
                //    Description = product.Description,
                //};
                //foreach (var m in product.Materials)
                //{
                //    productWithMaterial.Materials.Add(new MaterialDto
                //    {
                //        Id = m.Id,
                //        Name =m.Name,
                //    });
                //}

                return Ok(productWithMaterial);
            }

            var onlyProduct = AutoMapper.Mapper.Map<ProductWithoutMaterialDto>(product);
            //var onlyProduct = new ProductDto
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    Price = product.Price,
            //    Description = product.Description,
            //};

            return Ok(onlyProduct);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductCreation product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (product.Name=="产品")
            {
                ModelState.AddModelError("Name", "产品的名称不可以是'产品'二字");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var maxId = ProductService.Current.Products.Max(p => p.Id);
            //var newProduct = new Product
            //{
            //    Id = ++maxId,
            //    Name = product.Name,
            //    Price = product.Price,
            //    Description = product.Description,
            //};
            //ProductService.Current.Products.Add(newProduct);
            var newProduct = AutoMapper.Mapper.Map<Entities.Product>(product);
            _productRepository.AddProduct(newProduct);
            if (!_productRepository.Save())
            {
                return StatusCode(500, "新增产品失败");
            }
            var dto = AutoMapper.Mapper.Map<ProductWithoutMaterialDto>(newProduct);

            return CreatedAtRoute("GetProduct", new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]//整体更新
        public IActionResult Put(int id,[FromBody]ProductModification product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (product.Name == "产品")
            {
                ModelState.AddModelError("Name", "产品名称不能是'产品'二字");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var model = ProductService.Current.Products.SingleOrDefault(p => p.Id == id);
            var model = _productRepository.GetProduct(id,false);
            if (model == null)
            {
                return NotFound();
            }

            //model.Name = product.Name;
            //model.Price = product.Price;
            //model.Description = product.Description;

            //model = AutoMapper.Mapper.Map<Entities.Product>(product);//没生效？Execute a mapping from the source object to a new destination object.
            AutoMapper.Mapper.Map(product, model);//Execute a mapping from the source object to the existing destination object.
            if (!_productRepository.Save())
            {
                return StatusCode(500, "整体更新失败");
            }
            //return Ok(model);

            return NoContent();
        }

        [HttpPatch("{id}")]//部分更新
        public IActionResult Patch(int id, [FromBody] Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<ProductModification> patchDoc)
        {
            /**
             [
	            {
		            "op":"replace",
		            "path":"/name",
		            "value":"Patched New Name"

                },
	            {
		            "op":"replace",
		            "path":"/description",
		            "value":"Patched New description"
	            }	
            ]
             **/

            if (patchDoc == null)
            {
                return BadRequest();
            }

            //var model = ProductService.Current.Products.SingleOrDefault(p => p.Id == id);
            var model = _productRepository.GetProduct(id, false);
            if (model == null)
            {
                return NotFound();
            }

            //var toPatch = new ProductModification
            //{
            //    Name = model.Name,
            //    Price = model.Price,
            //    Description = model.Description,
            //};
            var toPatch = AutoMapper.Mapper.Map<ProductModification>(model);// to a new destination object
            patchDoc.ApplyTo(toPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (toPatch.Name == "产品")
            {
                ModelState.AddModelError("Name","产品不能有'产品'二字");
            }
            TryValidateModel(toPatch);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //model.Name = toPatch.Name;
            //model.Price = toPatch.Price;
            //model.Description = toPatch.Description;
            AutoMapper.Mapper.Map(toPatch, model);
            if (!_productRepository.Save())
            {
                return StatusCode(500, "部分更新失败");
            }

            //return Ok(model);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //var model = ProductService.Current.Products.SingleOrDefault(p => p.Id == id);
            var model = _productRepository.GetProduct(id, false);
            if (model == null)
            {
                return NotFound();
            }

            //var sucess = ProductService.Current.Products.Remove(model);
            _productRepository.DeleteProduct(model);
            if (!_productRepository.Save())
            {
                return StatusCode(500, "删除产品失败");
            }
            _mailService.Send("Product Deleted", $"Id为{id}的产品被删除了");

            return NoContent();
        }
    }
}