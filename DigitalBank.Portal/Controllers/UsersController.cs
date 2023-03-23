namespace DigitalBank.Portal.Controllers
{
    using Azure;
    using DigitalBank.Portal.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using RestSharp;
    using System.Text.Json;

    public class UsersController : Controller
    {

        private IUserInfoRestClient _restClient;
        static readonly IUserInfoRestClient RestClient = new UserInfoRestClient();

       
        public UsersController(IUserInfoRestClient restClient)
        {
            _restClient = restClient;

        }

        // GET: Users
        public async Task<IActionResult> Index()
        {  
             return View(RestClient.GetAll());
        }

        // GET: Users/Details/5
      
        public async Task<IActionResult> Details(int id)
        {
            return View(RestClient.GetById(id));
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Birthdate,Sex")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                RestClient.Post(userInfo);
                return RedirectToAction(nameof(Index));
            }
            return View(userInfo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromBody] UserInfo userInfo, string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            RestClient.Put(userInfo);

           
            return View(userInfo);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Birthdate,Sex")] UserInfo userInfo)
        {
            if (id != userInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    RestClient.Put(userInfo);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                   
                        throw ex;
                  
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userInfo);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            RestClient.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {           
            RestClient.Delete(id);

            return RedirectToAction(nameof(Index));
        }

       
    }
}
