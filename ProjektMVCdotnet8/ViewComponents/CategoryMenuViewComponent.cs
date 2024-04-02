using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Interfaces;

namespace ProjektMVCdotnet8.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryMenuViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories =  _categoryRepository.GetAllCategories();
            return View(categories);
        }
    }
}
