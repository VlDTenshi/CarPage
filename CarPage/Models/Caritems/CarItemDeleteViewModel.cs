namespace CarPage.Models.Caritems
{
    public class CarItemDeleteViewModel
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
        public List<ImageToDatabaseViewModel> ImageToDatabase { get; set; } = new List<ImageToDatabaseViewModel>();
    }
}
