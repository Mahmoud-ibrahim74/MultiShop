using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.GenericRepository.Base;
using MultiShop.Models;
using System.Diagnostics;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace MultiShop.Areas.Products
{
    [Authorize(Roles = clsRoles.roleAdmin)]
    [Area("Products")]
    public class ProductsController : Controller
    {
        private readonly IHostingEnvironment _hosting;
        private readonly IUnitOfWork _unitOfWork;
        private string Image1 { get; set; } = string.Empty;
        private string Image2 { get; set; } = string.Empty;
        private string Image3 { get; set; } = string.Empty;

        public ProductsController(IHostingEnvironment _hosting, IUnitOfWork unitOfWork )
        {
            this._hosting = _hosting;
            this._unitOfWork = unitOfWork;
        }

        // GET: Products/Products
        public async Task<IActionResult> Index()
        {
            return _unitOfWork != null ?
                        View(await _unitOfWork._UProduct.FindAllAsync()) :
                        Problem("Entity set 'AppDbContext.products'  is null.");
        }

        // GET: Products/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _unitOfWork._UProduct == null)
            {
                return NotFound();
            }

            var products = await _unitOfWork._UProduct.FindByIdAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Products/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public void SetTempData(string tempDataValue)
        {
            TempData["SelectedOption"] = tempDataValue;
        }

        // POST: Products/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductDescription,ProductSize,ProductColor,ProductPrice,PrimaryImg,img2_name,img3_name,ProductCreation,clientFile,ProductCategories")] Models.Products products)
        {
            if (ModelState.IsValid)
            {
                int selectedOption = Convert.ToInt32(TempData["SelectedOption"]);
                string[] file_names = new string[3];
                if (products.clientFile != null && selectedOption != 0)
                {
                    string root_path = Path.Combine(_hosting.WebRootPath, @"img/ProductsImages");
                    for (int i = 0; i < 3; i++)
                    {
                        string FileNameWithGuid = Guid.NewGuid().ToString() + products.clientFile[i].FileName;
                        string full_path = Path.Combine(root_path, FileNameWithGuid);

                        using (var fileCreate = new FileStream(full_path, FileMode.Create))
                        {
                            products.clientFile[i].CopyTo(fileCreate);
                        }
                        file_names[i] = FileNameWithGuid;
                    }

                    products.PrimaryImg = file_names[0];
                    products.img2_name = file_names[1];
                    products.img3_name = file_names[2];
                    products.ProductCategories = selectedOption;

                    products.clientFile.Clear();
                    await _unitOfWork._UProduct.InsertOneAsync(products);
                    TempData["ProductAdded"] = "Product Added";
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(products);

        }


        // GET: Products/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _unitOfWork._UProduct == null)
            {
                return NotFound();
            }
            var products = await _unitOfWork._UProduct.FindByIdAsync(id);
            this.Image1 = products.PrimaryImg;
            this.Image2 = products.img2_name;
            this.Image3 = products.img3_name;
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductDescription,ProductSize,ProductColor,ProductPrice,PrimaryImg,img2_name,img3_name,ProductCreation,clientFile,ProductCategories")] Models.Products products)
        {
            if (id != products.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    int selectedOption = Convert.ToInt32(TempData["SelectedOption"]);
                    string[] file_names = new string[3];
                    if (products.clientFile != null && selectedOption != 0)
                    {
                        string root_path = Path.Combine(_hosting.WebRootPath, @"img\ProductsImages");
                        var products_img = _unitOfWork._UProduct.SelectNoTracking(x => x.ProductId == id);
                        this.Image1 = products_img.PrimaryImg;
                        this.Image2 = products_img.img2_name;
                        this.Image3 = products_img.img3_name;
                        DeleteOldFiles();
                        for (int i = 0; i < 3; i++)
                        {
                            string FileNameWithGuid = Guid.NewGuid() + products.clientFile[i].FileName;
                            string full_path = Path.Combine(root_path, FileNameWithGuid);
                            using (var fileCreate = new FileStream(full_path, FileMode.Create))
                            {
                                products.clientFile[i].CopyTo(fileCreate);
                            }
                            file_names[i] = FileNameWithGuid;

                        }

                        products.PrimaryImg = file_names[0];
                        products.img2_name = file_names[1];
                        products.img3_name = file_names[2];
                        products.ProductCategories = selectedOption;
                        await _unitOfWork._UProduct.UpdateOneAsync(products);
                        TempData["ProductUpdated"] = "Product Updated Sucessfully";

                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork._UProduct.ItemExists(e => e.ProductId == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // GET: Products/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _unitOfWork._UProduct == null)
            {
                return NotFound();
            }

            var products = await _unitOfWork._UProduct.FindByColumnsAsync(x => x.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        // POST: Products/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_unitOfWork._UProduct == null)
            {
                return Problem("Entity set 'AppDbContext.products'  is null.");
            }
            var products = await _unitOfWork._UProduct.FindByIdAsync(id);
            if (products != null)
            {
                this.Image1 = products.PrimaryImg;
                this.Image2 = products.img2_name;
                this.Image3 = products.img3_name;
                await _unitOfWork._UProduct.DeleteOneAsync(products);
                DeleteOldFiles();
                TempData["ProductDeleted"] = "Product Deleted Sucessfully";
            }
            return RedirectToAction(nameof(Index));

        }
        private void DeleteOldFiles()
        {
            try
            {
                string root_path = Path.Combine(_hosting.WebRootPath, @"img\ProductsImages");
                string primaryImg1 = Path.Combine(root_path, Image1);
                string Img2_file = Path.Combine(root_path, Image2);
                string Img3_file = Path.Combine(root_path, Image3);

                if (System.IO.File.Exists(primaryImg1))
                {
                    System.IO.File.Delete(primaryImg1);
                    System.IO.File.Delete(Img2_file);
                    System.IO.File.Delete(Img3_file);
                }
                else
                {
                    Debug.WriteLine("File doesn't exists");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error *********************" + ex.Message);
            }

        }


    }
}
