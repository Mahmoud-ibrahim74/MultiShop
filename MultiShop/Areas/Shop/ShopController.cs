using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.GenericRepository.Base;

namespace MultiShop.Areas.Shop
{
    [Authorize]
    [Area("Shop")]
    public class ShopController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShopController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;  
        }
        private async void print()
        {
            while (true)
            {
                await Task.Delay(1000);
                Console.WriteLine("*********************************************" + TempData["filter"]);
            }
        }
        // GET: Shop/Shop
        public async Task<IActionResult> Index()
        {
            
            return _unitOfWork._UProduct != null ?
                   View(await _unitOfWork._UProduct.FindAllAsync()) :
                   Problem("Entity set '_productRepository'  is null.");
        }



        // GET: Shop/Shop/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _unitOfWork._UProduct == null)
            {
                return NotFound();
            }

            var products = await _unitOfWork._UProduct
                .FindByColumnsAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Shop/Shop/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public void FilterCheckBox(string filters)
        {
            TempData["filter"] = filters;
            //if(filterType == "price")
            //var filter  = await _productRepository.FindByColumnsAsync(x => x.ProductPrice > 0 && x.ProductPrice <= 100 && x.ProductCategories == categ_id)
        }


    }
}
