
namespace Salon.Services.Implementation
{

    using Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Salon.Data.Models;
    using Salon.Services.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Threading.Tasks;

    public class SalonService : ISalonServices
    {
        private readonly SalonDbContext db;
        private readonly UserManager<User> userManager;

        public SalonService(SalonDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;

        }

        public IEnumerable<SalonViewModel> AllSalon()
        {

            var salon = db.Salons;

            return salon.Select(s => new SalonViewModel
            {
                Name = s.Name,
                City = s.City,
                Country = s.Country,
                Id = s.Id,
                Products = s.Products
            }).ToList(); ;
        }

        public void Create(Salons salon, string name)
        {
            var user = db.Users.Include(s => s.Salon).FirstOrDefault(n => n.Email == name);
            user.Salon.Add(salon);
            db.SaveChanges();
        }


        public Salons FindSalon(int id)
        {
            var salonFromDb = this.db.Salons.FirstOrDefault(s => s.Id == id);
            return salonFromDb;


        }
        public void Edit(Salons salon, int id)
        {
            try
            {
                var salonFromDb = this.db.Salons.FirstOrDefault(s => s.Id == id);
                salonFromDb.Name = salon.Name;
                salonFromDb.City = salon.City;
                salonFromDb.Country = salon.Country;
                salon.Products = salon.Products;

                db.SaveChanges();
            }

            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("Ne vlizai tuk");
            }
        }


        public void Delete(int id)
        {
            var salon = db.Salons.SingleOrDefault(s => s.Id == id);

        }

        public void Delete(int id, string str)
        {
            var salon = db.Salons.SingleOrDefault(s => s.Id == id);
            db.Salons.Remove(salon);
            db.SaveChanges();
        }

        public Salons Details(int id)
        {
            var salon = db.Salons.Include("Products").SingleOrDefault(s => s.Id == id);

            return (salon);
        }


        public void AddProduct(AddProductView product, int id)
        {
            var result = this.db.Salons.Include(p => p.Products).SingleOrDefault(s => s.Id == id);

            Product tempProdduct = new Product()
            {
                Name = product.Name,
                Price = product.Price,
                Discount = product.Discount
            };

            result.Products.Add(tempProdduct);

            db.SaveChanges();

        }

        public List<SearchByProductViewModel> SearchProduct(string product)
        {
            var result = this.db.Salons.Include(p => p.Products);

            List<SearchByProductViewModel> searchByProducts = new List<SearchByProductViewModel>(); ;
            foreach (var sal in result)
            {
                foreach (var currrentProduct in sal.Products)
                {
                    if (currrentProduct.Name == product)
                    {
                        var prod = new SearchByProductViewModel();
                        prod.Id = sal.Id;
                        prod.SalonName = sal.Name;
                        prod.ProductName = currrentProduct.Name;
                        searchByProducts.Add(prod);
                    }
                }

            }

            return searchByProducts;
        }

        public IEnumerable<SalonViewModel> MySalons(string name)
        {

            var mySalon = db.Users.Include(s => s.Salon).FirstOrDefault(n => n.Email == name);

            return mySalon.Salon.Select(s => new SalonViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Products = s.Products,
                City = s.City,
                Country = s.Country
            });
        }

        public IEnumerable<Event> Book()
        {
            return null;
        }


        //Anonymouse
        public ProductWithWorkers GetProductWithWorkers(int id)
        {
            //var product = this.db.Products.Include(s => s.Workers).Where(p => p.Id == id);

            ProductWithWorkers prdoductWorker = new ProductWithWorkers();
            prdoductWorker.Id = id;

            var productWorkers = this.db.WorkerProduct.Include(p => p.Product).ThenInclude(w => w.Workers).Where(p => p.Product.Id == id);

            foreach (var prod in productWorkers)
            {
                //TO DO fix override name
                prdoductWorker.ProductName = prod.Product.Name;

                prdoductWorker.productId = prod.Product.Id;

                var worker = this.db.Worker.SingleOrDefault(w => w.Id == prod.WorkerId);
                var user = userManager.FindByIdAsync(worker.userId).Result;
                var workerName = user.Email;
                prdoductWorker.Workers.Add(workerName);
                prdoductWorker.selectWorker.Add(new SelectListItem
                {
                    Value = workerName,
                    Text = workerName
                });
            }


            // foreach (var worker in workers)
            // {
            // 
            //     var user = userManager.FindByIdAsync(worker.WorkerName).Result;
            // 
            //     ProductWithWorkers productWithWorkers = new ProductWithWorkers();
            //     productWithWorkers.ProductName = worker.ProductName;
            //     productWithWorkers.Workers.Add(user.Email);
            //     //TO DO optimize fix bug wih product name
            //     allWorkers.Add(productWithWorkers);
            //         
            // }
            //

            return prdoductWorker;
        }

        //Salon Role
        public ProductDetails ProductDetails(int id)
        {
            var productWorkers = this.db.WorkerProduct.Include(p => p.Product).ThenInclude(w => w.Workers).Where(p => p.Product.Id == id);
            var productDetails = new ProductDetails();

            if (productWorkers.Any())

            {

                foreach (var item in productWorkers)
                {
                    var worker = this.db.Worker.SingleOrDefault(w => w.Id == item.WorkerId);
                    var product = this.db.Products.SingleOrDefault(w => w.Id == item.ProductId);

                    productDetails.Id = product.Id;
                    productDetails.Name = product.Name;
                    productDetails.Price = product.Price;
                    productDetails.Discount = product.Discount;
                    //Greska v AddWorker
                    var user = userManager.FindByIdAsync(worker.userId).Result;
                    var workerName = user.Email;
                    productDetails.Workers.Add(workerName);
                }
            }
            else
            {
                var product = this.db.Products.FirstOrDefault(p => p.Id == id);

                productDetails.Id = product.Id;
                productDetails.Name = product.Name;
                productDetails.Price = product.Price;
                productDetails.Discount = product.Discount;
            }


            return productDetails;
        }


        public SearchByUser SearchByUser(string email, int id)
        {
            SearchByUser searchResult = new SearchByUser();

            var product = this.db.Products.SingleOrDefault(p => p.Id == id);
            var user = userManager.FindByEmailAsync(email).Result;
            searchResult.productId = product.Id;
            searchResult.Name = product.Name;
            searchResult.Price = product.Price;
            searchResult.Discount = product.Discount;
            searchResult.User = user;
            searchResult.Roles = userManager.GetRolesAsync(user).Result;

            return searchResult;
        }

        public void AddWorker(string id, string role, int productId)
        {
            var user = this.userManager.FindByIdAsync(id).Result;

            if (!(userManager.IsInRoleAsync(user, "Worker").Result))
            {
                this.userManager.AddToRoleAsync(user, role).Wait();
            }
            var workerFromDb = this.db.Worker.FirstOrDefault(w => w.userId == id);
            WorkerProduct workerProduct = new WorkerProduct();

            if (workerFromDb == null)
            {
                Worker worker = new Worker();
                worker.userId = id;
                worker.Email = user.Email;
                this.db.Worker.Add(worker);
                workerProduct.Worker = worker;
            }
            else
            {
                workerProduct.Worker = workerFromDb;
            }
            //TO DO fix bug with dublicate ....
            var product = this.db.Products.FirstOrDefault(p => p.Id == productId);
            workerProduct.Product = product;
            this.db.WorkerProduct.Add(workerProduct);
            db.SaveChanges();
        }

        public void AddProductWorker()
        {

        }


        public async Task<IEnumerable<Event>> AddEvent(BookView appointment)
        {

            var currentworker = this.db.Worker.FirstOrDefault(w => w.Email == appointment.WorkerEmail);
            var product = this.db.Products.FirstOrDefault(p => p.Id == appointment.ProductId);



            var step = product.Duration / 15;


            var dateToken = appointment.DateEvent.Split('-');

            int year = int.Parse(dateToken[0]);
            int month = int.Parse(dateToken[1]);
            int day = int.Parse(dateToken[2]);

            DateTime dateTime = new DateTime(year, month, day);

            var dayOfWeek = dateTime.DayOfWeek.ToString();

            var workTime = this.db.WorkTimes.Where(d => d.Day == dayOfWeek).FirstOrDefault(w => w.WorkerId == currentworker.Id);

            string[] startEvent = (appointment.StartEvent).Split(':');
            int hours = int.Parse(startEvent[0]);
            int min = int.Parse(startEvent[1]);

            string[] endEvent = (workTime.EndEvent).Split(':');
            int endHours = int.Parse(endEvent[0]);
            int endMin = int.Parse(endEvent[0]);
            
            int stepCounter = 0;

            while (stepCounter < step)
            {
                min += 15;

                if (min >= 60)
                {
                    hours++;
                    min = 0;
                }

                stepCounter++;
            }

            string res = hours + ":" + min;

            appointment.EndEvent = res;

           

            var currentEvent = new Event()
            {
                ClientName = appointment.ClientName,
                StartEvent = appointment.StartEvent,
               
                DateEvent = appointment.DateEvent,
                EndEvent = appointment.EndEvent
            };


            var allEventForDay = this.db.Events.Where(e => e.DateEvent == appointment.DateEvent && currentworker.Email == appointment.WorkerEmail);







            this.db.Events.Add(currentEvent);
            await db.SaveChangesAsync();

            return allEventForDay;
        }




        public IEnumerable<Event> GetAllEvent(string currentDay, string worker)
        {

            var dayEvents = this.db.Events.Where(e => e.DateEvent == currentDay && worker == e.Worker.Email).ToList();

            return dayEvents;
        }

    }
}
