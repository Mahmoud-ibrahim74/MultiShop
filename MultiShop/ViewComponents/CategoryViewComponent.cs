using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.ShopDbContext;
using System;

namespace MultiShop.ViewComponents
{
    [ViewComponent(Name = "Categories")]
    public class CategoryViewComponent : ViewComponent
    {
        private readonly AppDbContext _appDbContext;
        public CategoryViewComponent(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<int> CategCount = new List<int>();
            foreach (var item in _appDbContext.categories)
            {
                var res = _appDbContext.products.Count(x => x.ProductCategories == item.CategoryId); // get counts of each category
                CategCount.Add(res);
            }
            ViewBag.CategCount = CategCount;
            return View("Index", _appDbContext.categories.ToList());
        }

    }
}
