using AutoMapper;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;
using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(ProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            var productModels = _mapper.Map<List<ProductModel>>(products);
            return View(productModels);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.CategoryId = 1; // Default CategoryId for simplicity
            var product = _mapper.Map<Product>(model);
            await _productRepository.AddAsync(product);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}
