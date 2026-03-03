using AutoMapper;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;
using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagementSystem.Controllers
{
    public class InventoryTransactionController : Controller
    {
        private readonly InventoryTransactionRepository _InventoryTransactionRepository;
        private readonly ProductRepository _ProductRepository;
        private readonly WarehouseRepository _WarehouseRepository;
        private readonly IMapper _mapper;

        public InventoryTransactionController(InventoryTransactionRepository ınventoryTransactionsRepository, IMapper mapper, ProductRepository productRepository, WarehouseRepository warehouseRepository)
        {
            _InventoryTransactionRepository = ınventoryTransactionsRepository;
            _mapper = mapper;
            _ProductRepository = productRepository;
            _WarehouseRepository = warehouseRepository;
        }

        public async Task<IActionResult> Index()
        {
            var inventoryTransactions = await _InventoryTransactionRepository.GetAllAsync();
            var inventoryTransactionsModel = _mapper.Map<List<InventoryTransactionModel>>(inventoryTransactions);
            return View(inventoryTransactionsModel);
        }

        public async Task<IActionResult> Add()
        {
            var products = await _ProductRepository.GetAllAsync();
            var productModels = _mapper.Map<List<ProductModel>>(products);

            var warehouses = await _WarehouseRepository.GetAllAsync();
            var warehouseModels = _mapper.Map<List<WarehouseModel>>(warehouses);

            var productsSelectList = productModels.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            var warehousesSelectList = warehouseModels.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            ViewBag.Products = productsSelectList;
            ViewBag.Warehouses = warehouseModels;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(InventoryTransactionModel inventoryTransactionModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inventoryTransactionModel);
            }
            inventoryTransactionModel.Created = DateTime.Now;
            inventoryTransactionModel.Updated = DateTime.Now;

            var transactionTypeShort = "";

            switch (inventoryTransactionModel.TransactionType)
            {
                case "Inbound":
                    transactionTypeShort = "PO";
                    break;

                case "Outbound":
                    transactionTypeShort = "SO";
                    break;

                case "Adjustment":
                    transactionTypeShort = "ADJ";
                    break;
            }

            
            

            var inventoryTransaction = _mapper.Map<InventoryTransaction>(inventoryTransactionModel);

            await _InventoryTransactionRepository.AddAsync(inventoryTransaction);

            inventoryTransaction.ReferenceNumber = $"{transactionTypeShort}-{DateTime.UtcNow.Year}-{inventoryTransaction.Id:D6}";

            await _InventoryTransactionRepository.SaveChangesCustomAsync(inventoryTransaction.Id);


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var inventoryTransaction = await _InventoryTransactionRepository.GetByIdAsync(id);
            var inventoryTransactionModel = _mapper.Map<InventoryTransactionModel>(inventoryTransaction);

            var products = await _ProductRepository.GetAllAsync();
            var productModels = _mapper.Map<List<ProductModel>>(products);

            var warehouses = await _WarehouseRepository.GetAllAsync();
            var warehouseModels = _mapper.Map<List<WarehouseModel>>(warehouses);

            var productsSelectList = productModels.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            var warehousesSelectList = warehouseModels.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            ViewBag.Products = productsSelectList;
            ViewBag.Warehouses = warehouseModels;

            return View(inventoryTransactionModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(InventoryTransactionModel inventoryTransactionModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inventoryTransactionModel);
            }

            inventoryTransactionModel.Updated = DateTime.Now;

            var inventoryTransaction = _mapper.Map<InventoryTransaction>(inventoryTransactionModel);

            await _InventoryTransactionRepository.UpdateAsync(inventoryTransaction);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await _InventoryTransactionRepository.GetByIdAsync(id);
            var transactionModel = _mapper.Map<InventoryTransactionModel>(transaction);

            return View(transactionModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(InventoryTransactionModel transactionModel)
        {
            await _InventoryTransactionRepository.DeleteAsync(transactionModel.Id);
            return RedirectToAction("Index");
        }
    }
}
