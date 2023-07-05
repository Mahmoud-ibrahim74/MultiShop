using Microsoft.AspNetCore.Mvc;
using MultiShop.GenericRepository.Base;
using MultiShop.ShopDbContext;

namespace MultiShop.ViewComponents
{
    [ViewComponent(Name = "CatergoryCards")]
    public class CatergoryCards : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public CatergoryCards(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<int> CategCount = new List<int>();
            foreach (var item in _unitOfWork._UCategories.FindAll())
            {
                var res = _unitOfWork._UProduct.FindAll().Count(x => x.ProductCategories == item.CategoryId); // get counts of each category
                CategCount.Add(res);
            }
            ViewBag.CategCount = CategCount;
            return View( "Index", await _unitOfWork._UCategories.FindAllAsync());
        }

    }
}
