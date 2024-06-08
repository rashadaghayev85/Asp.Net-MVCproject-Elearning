using MVCproject_Elearning.Models;
using MVCproject_Elearning.ViewModels.Abouts;
using MVCproject_Elearning.ViewModels.Informations;
using System.Composition;

namespace MVCproject_Elearning.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Information> Informations { get; set; }

        public IEnumerable<AboutVM> Abouts { get; set; }
        public Category CategoryFirst { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        //public IEnumerable<BlogVM> Blogs { get; set; }
        //public IEnumerable<Expert> Experts { get; set; }

        //public IEnumerable<Models.Product> Products { get; set; }
    }
}
