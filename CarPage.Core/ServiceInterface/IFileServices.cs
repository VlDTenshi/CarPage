using CarPage.Core.Domain;
using CarPage.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPage.Core.ServiceInterface
{
    public interface IFileServices
    {
        void UploadFilesToDatabase(CarItemDto dto, CarItem domain);
        Task<FileToDatabase1> RemoveImageFromDatabase(FileToDatabaseDto dto);
        Task<FileToDatabase1> RemovePhotosFromDatabase(FileToDatabaseDto[] dto);
    }
}
