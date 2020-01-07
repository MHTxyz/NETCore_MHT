using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Dtos;
using WebApiDemo.Repositories;
using WebApiDemo.Services;

namespace WebApiDemo.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/product")]// 和主Model的Controller前缀一样
    public class MaterialController : Controller
    {
        private readonly IProductRepository _productRepository;

        public MaterialController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("{productId}/materials")]
        public IActionResult GetMaterials(int productId)
        {
            //var product = ProductService.Current.Products.SingleOrDefault(p => p.Id == productId);
            //if (product == null)
            //{
            //    return NotFound();
            //}

            //return Ok(product.Materials);

            var productExist = _productRepository.ProductExist(productId);
            if (!productExist)
            {
                return NotFound();
            }

            var materials = _productRepository.GetMaterialsForProduct(productId);
            if (materials== null || materials.Count() == 0)
            {
                return NotFound();
            }

            var result = AutoMapper.Mapper.Map<IEnumerable<MaterialDto>>(materials);
            //var result = materials.Select(m => new MaterialDto
            //{
            //    Id = m.Id,
            //    Name = m.Name,
            //})
            //.ToList();

            return Ok(result);
        }

        [HttpGet("{productId}/materials/{id}")]
        public IActionResult GetMaterial(int productId, int id)
        {
            //var product = ProductService.Current.Products.SingleOrDefault(p => p.Id == productId);
            //if (product == null)
            //{
            //    return NotFound();
            //}

            //var material = product.Materials.SingleOrDefault(m => m.Id == id);
            //if (material == null)
            //{
            //    return NotFound();
            //}

            //return Ok(material);

            var productExist = _productRepository.ProductExist(productId);
            if (!productExist)
            {
                return NotFound();
            }

            var material = _productRepository.GetMaterialForProduct(productId, id);
            if (material == null)
            {
                return NotFound();
            }

            var result = AutoMapper.Mapper.Map<MaterialDto>(material);
            //var result = new MaterialDto
            //{
            //    Id= material.Id,
            //    Name= material.Name,
            //};

            return Ok(result);
        }
    }
}