
namespace Salon.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Salon.Data.Models;
    using Salon.Services;
    using Salon.Services.Models;
    using System;
    using System.Threading.Tasks;

    public class SalonController : Controller
    {
      
        private readonly ISalonServices salonSvc;
        

        public SalonController(ISalonServices salon)
        {
            this.salonSvc = salon;
           
        }

         [HttpGet]
        public IActionResult All()
        {

            return View(salonSvc.AllSalon());
        }
        [HttpGet]
        [Authorize(Roles = "Salon")]
        public IActionResult Create ()
        {
            
            return View(new Salons());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Salon")]
        public IActionResult Create(Salons salon)
        {
            if (ModelState.IsValid)
            {

                var userName = HttpContext.User.Identity.Name;
                this.salonSvc.Create(salon , userName);
                return RedirectToAction(nameof(All));
            }
            return View(salon);
        }

        [HttpGet]
        [Authorize(Roles = "Salon")]
        public IActionResult Edit(int  id )
        {

            var currentSalon = this.salonSvc.FindSalon(id);
            return View(currentSalon);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Salon")]
        public IActionResult Edit(Salons salons, int id)
        {

            if (ModelState.IsValid)
            {

                salonSvc.Edit(salons, id);
                return RedirectToAction(nameof(All));
            }

            return View(salons);
        }

        [HttpGet]
        [Authorize(Roles = "Salon")]
        public IActionResult Delete(int id)
        {

            var currentSalon = this.salonSvc.FindSalon(id);

            return View(currentSalon);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Salon")]
        public IActionResult Delete(int id, string str)
        {
            if (ModelState.IsValid)
            {
                this.salonSvc.Delete(id, str);
                return RedirectToAction(nameof(All));
            }

            return RedirectToAction(nameof(All));

          
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var currentSalon = this.salonSvc.Details(id);
            TempData["productId"] = id;
            return View(currentSalon);
        }

        [HttpGet]
        public IActionResult AddProduct(int id)
        {
            return View(new AddProductView () { SalonId = id });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Salon")]
        public IActionResult AddProduct(AddProductView product,int id)
        {
            if (ModelState.IsValid)
            {
                this.salonSvc.AddProduct(product, id);
                return RedirectToAction(nameof(MySalon));
            }

           return View(product);
        }

        [HttpGet]
        public IActionResult SearchByProduct(string product)
        {
            var result = this.salonSvc.SearchProduct(product);

            return View(result);
        }

        [HttpGet]
        [Authorize(Roles = "Salon")]
        public IActionResult MySalon()
        {
            var userName = HttpContext.User.Identity.Name;

            var mySalons = this.salonSvc.MySalons(userName);

            return View(mySalons);
        }

        [HttpGet]
        public IActionResult ProductWithWorkers(int id)
        {
            var productWorkers = this.salonSvc.GetProductWithWorkers(id);

            TempData["productId"] = productWorkers.productId;
           
            return View(productWorkers);
        }

        
        [HttpGet]
        public IActionResult Book (string worker)
        {

            BookView bookView = new BookView();
           
           
           
            if (TempData["productId"] != null)
            {
                var prod = TempData["productId"].ToString();
           
                int  productId  = int.Parse(prod);
                bookView.ProductId = productId;
            }
           
            bookView.WorkerEmail = worker;

            return View("Book",bookView);
        }

        [HttpGet]
        public IActionResult ProductDetails(int id)
        {
            var product = this.salonSvc.ProductDetails(id);

            return View(product);
        }

       [HttpGet]
        public IActionResult SearchByUser(string email,int id)
        {
            var temp = Request.QueryString;
            var product = this.salonSvc.SearchByUser(email, id);

            return View(product);
        }

      

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddWorker(string id , string role,int productId)
        {
           

           if (ModelState.IsValid)
            {
                this.salonSvc.AddWorker(id, role, productId);
                return RedirectToAction(nameof(MySalon));
            }

            return RedirectToAction(nameof(MySalon));
        
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody] BookView appointment)
        {

            if (ModelState.IsValid)
            {
               

             await this.salonSvc.AddEvent(appointment);

              
                return new JsonResult ("Success");
            }
           

            return new JsonResult("error");
        }

        public IActionResult GetEvents(string currentDay, string selectedWorker)
        {
          
            var currnetEvents = salonSvc.GetAllEvent(currentDay, selectedWorker);

            return new JsonResult(currnetEvents);
        }

    }

  
}
