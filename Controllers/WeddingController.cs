using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WeddingBells.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UpdatedLogReg.Models;

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
            ViewBag.UserId = HttpContext.Session.GetInt32("userid");
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
                newWedding.UserId = (int)HttpContext.Session.GetInt32("userid");
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

        [HttpGet]
        [Route("wedding/{id}")]
        public IActionResult WeddingView(int id)
        {
            Wedding selectedWedding = dbContext.Weddings
                                    .Where (i => i.WeddingId == id)
                                    .Include(r => r.RSVP)
                                    .ThenInclude(g => g.Guest)
                                    .FirstOrDefault();
            return View(selectedWedding);
        }

        [HttpGet]
        [Route("wedding/{id}/rsvp")]
        public IActionResult RSVP(int id)
        {
            Wedding selectedWedding = dbContext.Weddings
                                    .Where (i => i.WeddingId == id)
                                    .Include(r => r.RSVP)
                                    .ThenInclude(g => g.Guest)
                                    .FirstOrDefault();
            
            UserObject curentUser = dbContext.Users
                                    .Where (i => i.UserId == HttpContext.Session.GetInt32("userid"))
                                    .Include(r => r.RSVP)
                                    .ThenInclude(w => w.Wedding)
                                    .FirstOrDefault();
            
            if (selectedWedding.RSVP.Any(i => i.Guest.UserId == (int)HttpContext.Session.GetInt32("userid")))
            {
                RSVP thisRSVP = dbContext.RSVPs
                                .Where(i => i.WeddingID == selectedWedding.WeddingId)
                                .Where(j => j.UserId == curentUser.UserId)
                                .FirstOrDefault();
                dbContext.RSVPs.Remove(thisRSVP);
            }
            else
            {
                RSVP newRSVP = new RSVP();
                newRSVP.WeddingID = selectedWedding.WeddingId;
                newRSVP.UserId = curentUser.UserId;
                dbContext.Add(newRSVP);
            }
            dbContext.SaveChanges();
            return Redirect("/dashboard");
        }

        [HttpGet]
        [Route("wedding/{id}/delete")]
        public IActionResult Delete (int id)
        {
            Wedding selectedWedding = dbContext.Weddings
                                    .Where (i => i.WeddingId == id)
                                    .Include(r => r.RSVP)
                                    .ThenInclude(g => g.Guest)
                                    .FirstOrDefault();
            
            dbContext.Weddings.Remove(selectedWedding);
            return Redirect("/dashboard");
        }

    }
}