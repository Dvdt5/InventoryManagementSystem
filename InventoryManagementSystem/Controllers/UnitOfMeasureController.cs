using AutoMapper;
using InventoryManagementSystem.Repositories;
using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class UnitOfMeasureController : Controller
    {
        private readonly UnitOfMeasureRepository _unitOfMeasureRepository;
        private readonly IMapper _mapper;

        public UnitOfMeasureController(UnitOfMeasureRepository unitOfMeasureRepository, IMapper mapper)
        {
            _unitOfMeasureRepository = unitOfMeasureRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var units = await _unitOfMeasureRepository.GetAllAsync();
            var unitModels = _mapper.Map<List<UnitOfMeasureModel>>(units);

            return View(unitModels);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UnitOfMeasureModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Created = DateTime.Now;
            model.Updated = DateTime.Now;

            var unit = _mapper.Map<Models.UnitOfMeasure>(model);

            await _unitOfMeasureRepository.AddAsync(unit);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var UnitOfMeasure = await _unitOfMeasureRepository.GetByIdAsync(id);
            var UnitOfMeasureModel = _mapper.Map<UnitOfMeasureModel>(UnitOfMeasure);

            return View(UnitOfMeasureModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UnitOfMeasureModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Updated = DateTime.Now;
            var unit = _mapper.Map<Models.UnitOfMeasure>(model);

            await _unitOfMeasureRepository.UpdateAsync(unit);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var unit = await _unitOfMeasureRepository.GetByIdAsync(id);
            var unitModel = _mapper.Map<UnitOfMeasureModel>(unit);
            return View(unitModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UnitOfMeasureModel unitModel)
        {
            await _unitOfMeasureRepository.DeleteAsync(unitModel.Id);
            return RedirectToAction("Index");
        }

    }
}
