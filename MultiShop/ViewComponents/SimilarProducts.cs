using Microsoft.AspNetCore.Mvc;
using MultiShop.GenericRepository.Base;
using MultiShop.ShopDbContext;

namespace MultiShop.ViewComponents
{
    [ViewComponent(Name = "SimilarProducts")]
    public class SimilarProducts : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public SimilarProducts(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IViewComponentResult> InvokeAsync(int categ)
        {
            // get products from last week
            return View("Index", await _unitOfWork._UProduct.FindByColumnsListLimitAsync(x => x.ProductCategories == categ,5));
        }

    }
}
