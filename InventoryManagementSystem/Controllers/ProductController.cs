using AutoMapper;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;
using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagementSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductController(ProductRepository productRepository, IMapper mapper, CategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            var productModels = _mapper.Map<List<ProductModel>>(products);

            foreach(var productModel in productModels)
            {
                if (productModel.CategoryId.HasValue)
                {
                    var category = await _categoryRepository.GetByIdAsync(productModel.CategoryId.Value);
                    productModel.CategoryName = category?.Name;
                }
            }

            return View(productModels);
        }

        public async Task<IActionResult> Add()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoryModels = _mapper.Map<List<CategoryModel>>(categories);
            var categoriesSelectList = categoryModels.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.Categories = categoriesSelectList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Created = DateTime.Now;
            model.Updated = DateTime.Now;
            var product = _mapper.Map<Product>(model);
            await _productRepository.AddAsync(product);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            var productModel = _mapper.Map<ProductModel>(product);


            var categories = await _categoryRepository.GetAllAsync();
            var categoryModels = _mapper.Map<List<CategoryModel>>(categories);
            var categoriesSelectList = categoryModels.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });


            ViewBag.Categories = categoriesSelectList;
            return View(productModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            model.Updated = DateTime.Now;
            var product = _mapper.Map<Product>(model);
            await _productRepository.UpdateAsync(product);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            var productModel = _mapper.Map<ProductModel>(product);

            return View(productModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductModel model)
        {
            await _productRepository.DeleteAsync(model.Id);
            return RedirectToAction("Index");
        }
    }
}
