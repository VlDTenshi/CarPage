namespace CarPage.Models.Caritems
{
    public class CarItemDetailsViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public ushort Price { get; set; }
        public bool? isFavorite { get; set; }
        public bool? available { get; set; }
        public List<ImageToDatabaseViewModel> Image { get; set; } = new List<ImageToDatabaseViewModel>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
