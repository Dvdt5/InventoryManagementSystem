using AutoMapper;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;
using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly WarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public WarehouseController(WarehouseRepository warehouseRepository, IMapper mapper)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var warehouses = await _warehouseRepository.GetAllAsync();
            var warehouseModels = _mapper.Map<List<WarehouseModel>>(warehouses);
            return View(warehouseModels);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(WarehouseModel warehouseModel)
        {
            if (!ModelState.IsValid)
            {
                return View(warehouseModel);
            }
            
            warehouseModel.Created = DateTime.Now;
            warehouseModel.Updated = DateTime.Now;

            var warehouse = _mapper.Map<Warehouse>(warehouseModel);
            await _warehouseRepository.AddAsync(warehouse);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(id);
            var warehouseModel = _mapper.Map<WarehouseModel>(warehouse);
            return View(warehouseModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(WarehouseModel warehouseModel)
        {
            if (!ModelState.IsValid)
            {
                return View(warehouseModel);
            }
            warehouseModel.Updated = DateTime.Now;
            var warehouse = _mapper.Map<Warehouse>(warehouseModel);
            await _warehouseRepository.UpdateAsync(warehouse);
            return RedirectToAction("Index");
        }
    }
}
