using Microsoft.AspNetCore.Mvc;
using MultiShop.GenericRepository.Base;

namespace MultiShop.ViewComponents
{
    [ViewComponent(Name = "ReviewsCompnents")]
    public class ReviewsCompnents : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReviewsCompnents(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork; 
        }
        public async Task<IViewComponentResult> InvokeAsync(int categ)
        {
            return View("Index", await _unitOfWork._UReview.FindByColumnsListAsync(x => x.prod_categ == categ));
        }

    }
}
