using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WeddingBells.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WeddingBells.Controllers
{
    public class WeddingController : Controller
    {
        private WeddingContext dbContext;

    // here we can "inject" our context service into the constructor
    public WeddingController(WeddingContext context)
    {
        dbContext = context;
    }
        // GET: /Home/
        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            List<Wedding> AllTheInfo = dbContext.Weddings
                                    .Include(r => r.RSVP)
                                    .ThenInclude(g => g.Guest)
                                    .ToList();
            return View(AllTheInfo);
        }
        [HttpGet]
        [Route("new")]
        public IActionResult New()
        {
            return View();
        }
        [HttpPost("create")]
        public IActionResult Create(Wedding newWedding)
        {
            if(ModelState.IsValid)
            {
                //Save your user object to the database
                dbContext.Add(newWedding);
                dbContext.SaveChanges();
                return Redirect("/dashboard"); //This doesn't exist yet
            }
            // other code
            else
            {
                return View("New");
            }
        }
    }
}