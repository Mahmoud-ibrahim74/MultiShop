using Microsoft.AspNetCore.Mvc;
using MultiShop.GenericRepository.Base;
using MultiShop.ShopDbContext;

namespace MultiShop.ViewComponents
{
    [ViewComponent(Name = "CategoreyDropDown")]
    public class CategDropDown : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategDropDown(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("Index", await _unitOfWork._UCategories.FindAllAsync());
        }

    }
}
