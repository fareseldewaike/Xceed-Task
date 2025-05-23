using AutoMapper;
using ECommerce.ErrorsHandling;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductCatalog.BLL.Services;
using ProductCatalog.DAL.Entities;
using ProductCatalog.DTOs;
using System.Security.Claims;

namespace ProductCatalog.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper; 

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                await AllCategoriesAsync();
                var products = await _productService.GetAllProductsAsync();
                return View("GetAllProducts", products);
            }
            catch (Exception)
            {
                return StatusCode(500, new ApiResponse(500, "error occurred while retrieving products"));
            }
        }
            [HttpGet]
        public async Task<IActionResult> GetAllProductsWithStillInOffer()
        {
            try
            {
                await AllCategoriesAsync();
                var products = await _productService.GetAllProductsWithStillInOfferAsync();
                if (products == null)
                {
                    return NotFound(new ApiResponse(400));
                }
                return View(products);
            }
            catch (Exception)
            {
                return StatusCode(500, new ApiResponse(500, "error while retrieving products in offer"));
            }
        }
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound(new ApiResponse(400));
                }
                return View(product);
            }
            catch (Exception)
            {
                return StatusCode(500, new ApiResponse(500, "Error while retrieving product."));
            }
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct()
        {
            await AllCategoriesAsync();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(ProductToAddViewModel productdto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product = _mapper.Map<Product>(productdto);

                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    product.CreatedByUserId = userId;

                    await _productService.AddProductAsync(product);
                    return RedirectToAction("GetAllProducts");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Something went wrong. Please try again.");
                    return View(productdto);
                }
            }
            await AllCategoriesAsync();
            return View(productdto);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var existingProduct = await _productService.GetProductByIdAsync(id);
            if (existingProduct == null) return NotFound(new ApiResponse(400));
            await AllCategoriesAsync();
            var productDto = _mapper.Map<ProductToAddViewModel>(existingProduct);

            return View(productDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, ProductToAddViewModel productdto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingProduct = await _productService.GetProductByIdAsync(id);
                    if (existingProduct == null)
                        return NotFound(new ApiResponse(400));

                    existingProduct.Name = productdto.Name;
                    existingProduct.Price = productdto.Price;
                    existingProduct.StartDate = productdto.StartDate;
                    existingProduct.DurationInDays = productdto.DurationInDays;
                    existingProduct.CategoryId = productdto.CategoryId;

                    await _productService.UpdateProductAsync(existingProduct);
                    return RedirectToAction("GetAllProducts");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Error while updating the product.");
                }
            }
            await AllCategoriesAsync();
            return View(productdto);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                    return NotFound(new ApiResponse(400));

                await _productService.DeleteProductAsync(id);
                return RedirectToAction("GetAllProducts");
            }
            catch (Exception)
            {
                return StatusCode(500, new ApiResponse(500, "Error while deleting the product."));
            }
        }
        [HttpGet]
        public async Task<IActionResult> FilterAllByCategory(int categoryid)
        {
            try
            {
                await AllCategoriesAsync();
                var products = await _productService.GetProductsByCategoryAsync(categoryid);
                return View("GetAllProducts", products);
            }
            catch (Exception)
            {
                return StatusCode(500, new ApiResponse(500, "Error while filtering products by category."));
            }
        }
        [HttpGet]
        public async Task<IActionResult> FilterOfferedByCategory(int categoryid)
        {
            try
            {
                await AllCategoriesAsync();
                var products = await _productService.GetProductsByCategoryStillInOfferAsync(categoryid);
                return View("GetAllProductsWithStillInOffer", products);
            }
            catch (Exception)
            {
                return StatusCode(500, new ApiResponse(500, "Error while filtering offered products by category."));
            }
        }

        private async Task AllCategoriesAsync()
        {
            var categories = await _productService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }
    }
}
