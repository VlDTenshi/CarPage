using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPage.Core.Domain
{
    public class CarItem
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public ushort Price { get; set; }
        public bool? isFavorite { get; set; }
        public bool? available { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
