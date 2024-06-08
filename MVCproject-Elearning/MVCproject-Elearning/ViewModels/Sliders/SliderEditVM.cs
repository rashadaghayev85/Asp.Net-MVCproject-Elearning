namespace MVCproject_Elearning.ViewModels.Sliders
{
    public class SliderEditVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public IFormFile NewImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
