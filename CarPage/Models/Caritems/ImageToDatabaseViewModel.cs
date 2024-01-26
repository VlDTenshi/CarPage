namespace CarPage.Models.Caritems
{
    public class ImageToDatabaseViewModel
    {
        public Guid ImageId { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public string Image { get; set; }
        public Guid? CarItemId { get; set; }
    }
}
