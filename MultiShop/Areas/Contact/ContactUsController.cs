using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.GenericRepository.Base;
using MultiShop.Models;

namespace MultiShop.Areas.Contact
{
    [Area("Contact")]
    [Authorize]
    public class ContactUsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactUsController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        // GET: Contact/ContactUs
        public async Task<IActionResult> Index()
        {
            return _unitOfWork._UContactUs != null ?
                        View(await _unitOfWork._UContactUs.FindAllAsync()) :
                        Problem("Entity set 'AppDbContext.ContactUs'  is null.");
        }

        // GET: Contact/ContactUs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _unitOfWork._UContactUs == null)
            {
                return NotFound();
            }

            var contactUs = await _unitOfWork._UContactUs.FindByColumnsAsync(m => m.MailId == id);
            if (contactUs == null)
            {
                return NotFound();
            }

            return View(contactUs);
        }

        // GET: Contact/ContactUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contact/ContactUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MailId,Name,Email,Subject,Message")] ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork._UContactUs.InsertOneAsync(contactUs);
                TempData["contactAdded"] = "Thanks,We are Suporting Soon !!";
                return LocalRedirect("/Home/Index");

            }
            return View(contactUs);
        }


    }
}
