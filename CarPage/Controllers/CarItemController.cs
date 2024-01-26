using CarPage.Core.Dto;
using CarPage.Core.ServiceInterface;
using CarPage.Data;
using CarPage.Models.Caritems;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarPage.Controllers
{
    public class CarItemController : Controller
    {
        private readonly CarContext _context;
        private readonly ICarItemServices _carItemServices;
        private readonly IFileServices _fileServices;

        public CarItemController(CarContext context, ICarItemServices carItemServices, IFileServices fileServices)
        {
            _context = context;
            _carItemServices = carItemServices;
            _fileServices = fileServices;
        }

        public IActionResult Index()
        {
            var result = _context.CarItems
                .Select(x => new CarItemViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ShortDesc = x.ShortDesc,
                    LongDesc = x.LongDesc,
                    Price = x.Price,
                    isFavorite = x.isFavorite,
                    available = x.available,
                });
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            CarItemCreateUpdateViewModel caritem = new CarItemCreateUpdateViewModel();
            return View("CreateUpdate", caritem);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CarItemCreateUpdateViewModel vm)
        {
            var dto = new CarItemDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                ShortDesc = vm.ShortDesc,
                LongDesc = vm.LongDesc,
                Price = vm.Price,
                isFavorite = vm.isFavorite ?? false,
                available = vm.available ?? false,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    CarItemId = x.CarItemId
                }).ToArray()

            };
            var result = await _carItemServices.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var caritem = await _carItemServices.GetAsync(id);
            if (caritem == null)
            {
                return NotFound();
            }
            var photos = await _context.FileToDatabases1
                .Where(x => x.CarItemId == id)
                .Select(y => new ImageToDatabaseViewModel
                {
                    CarItemId = y.CarItemId,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new CarItemDetailsViewModel();

            vm.Id = caritem.Id;
            vm.Name = caritem.Name;
            vm.ShortDesc = caritem.ShortDesc;
            vm.LongDesc = caritem.LongDesc;
            vm.Price = caritem.Price;
            vm.isFavorite = caritem.isFavorite;
            vm.available = caritem.available;
            vm.CreatedAt = caritem.CreatedAt;
            vm.UpdatedAt = caritem.UpdatedAt;
            vm.Image.AddRange(photos);

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var caritem = await _carItemServices.GetAsync(id);
            if (caritem == null)
            {
                return NotFound();
            }
            var photos = await _context.FileToDatabases1
                .Where(x => x.CarItemId == id)
                .Select(y => new ImageToDatabaseViewModel
                {
                    CarItemId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64, {0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();
            var vm = new CarItemCreateUpdateViewModel();

            vm.Id = caritem.Id;
            vm.Name = caritem.Name;
            vm.ShortDesc = caritem.ShortDesc;
            vm.LongDesc = caritem.LongDesc;
            vm.Price = caritem.Price;
            vm.isFavorite = caritem.isFavorite;
            vm.available = caritem.available;
            vm.CreatedAt = caritem.CreatedAt;
            vm.UpdatedAt = caritem.UpdatedAt;
            vm.Image.AddRange(photos);

            return View("CreateUpdate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CarItemCreateUpdateViewModel vm)
        {
            var dto = new CarItemDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                ShortDesc = vm.ShortDesc,
                LongDesc = vm.LongDesc,
                Price = vm.Price,
                isFavorite = vm.isFavorite ?? false,
                available = vm.available ?? false,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = DateTime.Now,
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    CarItemId = x.CarItemId,
                }).ToArray()
            };
            var result = await _carItemServices.Update(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index), vm);
            }
            return RedirectToAction(nameof(Index), vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var caritem = await _carItemServices.GetAsync(id);
            if (caritem == null)
            {
                return NotFound();
            }
            var photos = await _context.FileToDatabases1
                .Where(x => x.CarItemId == id)
                .Select(y => new ImageToDatabaseViewModel
                {
                    CarItemId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64, {0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();
            var vm = new CarItemDeleteViewModel();

            vm.Id = caritem.Id;
            vm.Name = caritem.Name;
            vm.ShortDesc = caritem.ShortDesc;
            vm.LongDesc = caritem.LongDesc;
            vm.Price = caritem.Price;
            vm.isFavorite = caritem.isFavorite;
            vm.available = caritem.available;
            vm.CreatedAt = caritem.CreatedAt;
            vm.UpdatedAt = caritem.UpdatedAt;
            vm.ImageToDatabase.AddRange(photos);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var caritemId = await _carItemServices.Delete(id);
            if (caritemId == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> RemoveImage(ImageToDatabaseViewModel file)
        {
            var dto = new FileToDatabaseDto()
            {
                Id = file.ImageId
            };
            var image = await _fileServices.RemoveImageFromDatabase(dto);

            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
