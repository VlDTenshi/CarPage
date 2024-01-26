using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPage.Core.Domain
{
    public class FileToDatabase1
    {
        public Guid Id { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public Guid? CarItemId { get; set; }
    }
}
