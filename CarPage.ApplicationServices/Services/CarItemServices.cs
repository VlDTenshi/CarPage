using CarPage.Core.Domain;
using CarPage.Core.Dto;
using CarPage.Core.ServiceInterface;
using CarPage.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPage.ApplicationServices.Services
{
    public class CarItemServices : ICarItemServices
    {
        private readonly CarContext _context;
        private readonly IFileServices _fileServices;

        public CarItemServices
            (
                CarContext context,
                IFileServices fileServices
            )
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<CarItem> Create(CarItemDto dto)
        {
            CarItem caritem = new CarItem();
            caritem.Id = Guid.NewGuid();
            caritem.Name = dto.Name;
            caritem.ShortDesc = dto.ShortDesc;
            caritem.LongDesc = dto.LongDesc;
            caritem.Price = dto.Price;
            caritem.isFavorite = dto.isFavorite;
            caritem.available = dto.available;
            caritem.CreatedAt = DateTime.Now;
            caritem.UpdatedAt = DateTime.Now;

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, caritem);
            }
            await _context.CarItems.AddAsync(caritem);
            await _context.SaveChangesAsync();

            return caritem;
        }
        public async Task<CarItem> Update(CarItemDto dto)
        {
            var domain = new CarItem()
            {
                Id = dto.Id,
                Name = dto.Name,
                ShortDesc = dto.ShortDesc,
                LongDesc = dto.LongDesc,
                Price = dto.Price,
                isFavorite = dto.isFavorite,
                available = dto.available,

                CreatedAt = dto.CreatedAt,
                UpdatedAt = DateTime.Now,
            };
            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, domain);
            }
            _context.CarItems.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }
        public async Task<CarItem> Delete(Guid id)
        {
            var caritemId = await _context.CarItems
                .FirstOrDefaultAsync(x => x.Id == id);
            var photos = await _context.FileToDatabases1
                .Where(x => x.CarItemId == id)
                .Select(y => new FileToDatabaseDto
                {
                    Id = y.Id,
                    ImageTitle = y.ImageTitle,
                    CarItemId = y.CarItemId
                }).ToArrayAsync();

            await _fileServices.RemovePhotosFromDatabase(photos);

            _context.CarItems.Remove(caritemId);
            await _context.SaveChangesAsync();

            return caritemId;
        }
        public async Task<CarItem> GetAsync(Guid id)
        {
            var result = await _context.CarItems
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}
