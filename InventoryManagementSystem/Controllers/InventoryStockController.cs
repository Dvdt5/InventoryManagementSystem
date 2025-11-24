using AutoMapper;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;
using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagementSystem.Controllers
{
    public class InventoryStockController : Controller
    {
        private readonly InventoryStockRepository _inventoryStockRepository;
        private readonly ProductRepository _productRepository;
        private readonly WarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public InventoryStockController(InventoryStockRepository inventoryStockRepository, IMapper mapper, ProductRepository productRepository, WarehouseRepository warehouseRepository)
        {
            _inventoryStockRepository = inventoryStockRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _warehouseRepository = warehouseRepository;
        }

        public async Task<IActionResult> Index()
        {
            var inventoryStocks = await _inventoryStockRepository.GetAllAsync();
            var inventoryStockModels = _mapper.Map<List<InventoryStockModel>>(inventoryStocks);
            return View(inventoryStockModels);
        }

        public async Task<IActionResult> Add()
        {
            var products = await _productRepository.GetAllAsync();
            var productModels = _mapper.Map<List<ProductModel>>(products);

            var warehouses = await _warehouseRepository.GetAllAsync();
            var warehouseModels = _mapper.Map<List<WarehouseModel>>(warehouses);

            var productSelectList = productModels.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            var warehouseSelectList = warehouseModels.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            ViewBag.Products = productSelectList;
            ViewBag.Warehouses = warehouseSelectList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(InventoryStockModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var product = await _productRepository.GetByIdAsync(model.ProductId);
            var warehouse = await _warehouseRepository.GetByIdAsync(model.WarehouseId);

            model.Created = DateTime.Now;
            model.Updated = DateTime.Now;
            model.LastUpdated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            model.ProductName = product.Name;
            model.WarehouseName = warehouse.Name;

            var inventoryStock = _mapper.Map<InventoryStock>(model);
            await _inventoryStockRepository.AddAsync(inventoryStock);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var inventoryStock = await _inventoryStockRepository.GetByIdAsync(id);
            var inventoryStockModel = _mapper.Map<InventoryStockModel>(inventoryStock);

            var products = await _productRepository.GetAllAsync();
            var productModels = _mapper.Map<List<ProductModel>>(products);

            var warehouses = await _warehouseRepository.GetAllAsync();
            var warehouseModels = _mapper.Map<List<WarehouseModel>>(warehouses);

            var productSelectList = productModels.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            var warehouseSelectList = warehouseModels.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            ViewBag.Products = productSelectList;
            ViewBag.Warehouses = warehouseSelectList;

            return View(inventoryStockModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(InventoryStockModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Updated = DateTime.Now;
            model.LastUpdated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var inventoryStock = _mapper.Map<InventoryStock>(model);
            await _inventoryStockRepository.UpdateAsync(inventoryStock);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var inventoryStock = await _inventoryStockRepository.GetByIdAsync(id);
            var inventoryStockModel = _mapper.Map<InventoryStockModel>(inventoryStock);
            return View(inventoryStockModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(InventoryStockModel model)
        {
            var inventoryStock = _mapper.Map<InventoryStock>(model);
            await _inventoryStockRepository.DeleteAsync(inventoryStock.Id);
            return RedirectToAction("Index");
        }
    }
}
