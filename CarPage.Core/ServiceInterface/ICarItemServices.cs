using CarPage.Core.Domain;
using CarPage.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPage.Core.ServiceInterface
{
    public interface ICarItemServices
    {
        Task<CarItem> Create(CarItemDto dto);
        Task<CarItem> Update(CarItemDto dto);
        Task<CarItem> Delete(Guid id);
        Task<CarItem> GetAsync(Guid id);
    }
}
