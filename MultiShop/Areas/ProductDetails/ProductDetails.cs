using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.GenericRepository.Base;
using MultiShop.Models;

namespace MultiShop.Areas.ProductDetails
{
    [Area("ProductDetails")]
    [Authorize]
    public class ProductDetails : Controller
    {
        ViewModels.ProductDetailsViewModel productDetails = new ViewModels.ProductDetailsViewModel();
        private readonly IUnitOfWork _unitOfWork;

        public ProductDetails(IUnitOfWork unitOfWork)
        {
           this._unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> IndexId(int? id)
        {
            if (id == null || _unitOfWork._UCategories == null)
            {
                return NotFound();
            }
            var product = await _unitOfWork._UProduct.FindByIdAsync(id);
            productDetails.products = product;
            var review = await _unitOfWork._UReview.FindByIdAsync(id);
            productDetails.reviews = review;
            TempData["id"] = id;
            if (product == null)
            {
                return NotFound();
            }
            return View(productDetails);
        }
        [HttpPost]
        public async Task<IActionResult> AddReviews(ReviewsProducts reviews)
        {
            reviews.prod_categ = _unitOfWork._UProduct.FindById((int?)TempData["id"]).ProductCategories;
            await _unitOfWork._UReview.InsertOneAsync(reviews);
            TempData["reviewAdd"] = "Thanks for Your Opinion !!";
            return RedirectToAction("IndexId", new { id = TempData["id"]});
        }


    }
}
