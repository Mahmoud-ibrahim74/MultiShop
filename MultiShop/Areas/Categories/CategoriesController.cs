using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.GenericRepository.Base;
using MultiShop.Models;

namespace MultiShop.Areas.Categories
{
    [Area("Categories")]
    [Authorize(Roles = clsRoles.roleAdmin)]
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        // GET: Categories/Categories
        public async Task<IActionResult> Index()
        {
            return _unitOfWork._UCategories != null ?
                        View(await _unitOfWork._UCategories.FindAllAsync()) :
                        Problem("Entity set 'AppDbContext.categories'  is null.");
        }

        // GET: Categories/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _unitOfWork._UCategories == null)
            {
                return NotFound();
            }

            var categories = await _unitOfWork._UCategories.FindByColumnsAsync(m => m.CategoryId == id);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // GET: Categories/Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDescription,CategoryImg,CategoriesDate,clientFile")] Models.Categories categories)
        {

            if (ModelState.IsValid)
            {
                if (categories.clientFile != null)
                {
                    //string _uploadPath = Path.Combine(_hosting.WebRootPath, "images");
                    //file_name = categories.clientFile.FileName;
                    //string fullPath = Path.Combine(_uploadPath, file_name); 
                    //categories.clientFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                    //categories.ImagePath = file_name;


                    MemoryStream stream = new MemoryStream();
                    categories.clientFile.CopyTo(stream);
                    categories.CategoryImg = stream.ToArray();
                    stream.Close();
                    stream.Dispose();
                    _unitOfWork._UCategories.InsertOne(categories);
                    return LocalRedirect("/Home/Index");
                }

            }
            return View(categories);

        }

        // GET: Categories/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _unitOfWork._UCategories == null)
            {
                return NotFound();
            }

            var categories = await _unitOfWork._UCategories.FindByIdAsync(id);
            if (categories == null)
            {
                return NotFound();
            }
            return View(categories);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,CategoryDescription,CategoryImg,CategoriesDate,clientFile")] Models.Categories categories)
        {
            if (ModelState.IsValid)
            {
                if (categories.clientFile != null)
                {

                    MemoryStream stream = new MemoryStream();
                    categories.clientFile.CopyTo(stream);
                    categories.CategoryImg = stream.ToArray();
                    stream.Close();
                    stream.Dispose();


                    _unitOfWork._UCategories.UpdateOne(categories);
                    return RedirectToAction("Index");

                }

            }
            return View(categories);

        }


        // GET: Categories/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _unitOfWork._UCategories.FindAll() == null)
            {
                return NotFound();
            }

            var categories = await _unitOfWork._UCategories.FindByColumnsAsync(m => m.CategoryId == id);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // POST: Categories/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_unitOfWork._UCategories == null)
            {
                return Problem("Entity set 'AppDbContext.categories'  is null.");
            }
            var categories = await _unitOfWork._UCategories.FindByIdAsync(id);
            if (categories != null)
            {
               await _unitOfWork._UCategories.DeleteOneAsync(categories);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CategoriesExists(int id)
        {
            return (_unitOfWork._UCategories.FindAll()?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
