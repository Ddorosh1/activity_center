using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using activity_center.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace activity_center.Controllers
{
  public class DashboardController : Controller
  {
    private activity_centerContext dbContext;

    public DashboardController(activity_centerContext context)
    {
      dbContext = context;
    }

    [Route("dashboard")]
    [HttpGet]
    public IActionResult Dashboard()
    {
      int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
          return RedirectToAction("Index", "Home");

      List<Models.Activity> AllActivities = dbContext.Activity
      .Include(a => a.Participants)
      .ThenInclude(a => a.User)
      .OrderBy(x => x.Date)
      .ToList();
      ViewBag.UserId = userId;
      User LoggedInUser = dbContext.Users
      .FirstOrDefault(a => a.UserId == userId);
      ViewBag.Name = LoggedInUser.FirstName;
      return View("Dashboard", AllActivities);
    }

    [Route("create/activity")]
    [HttpGet]
    public IActionResult CreateActivity()
    {
      int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
          return RedirectToAction("Index", "Home");

      return View("NewActivity");
    }

    [Route("add/activity")]
    [HttpPost]
    public IActionResult AddActivity(Models.Activity NewActivity)
    {
      int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
          return RedirectToAction("Index", "Home");
      User oneUser = dbContext.Users
      .FirstOrDefault(a => a.UserId == userId);
      if(NewActivity.Date < DateTime.Now)
      {
        ModelState.AddModelError("Date", "Date must be in the future");
      }
      if(ModelState.IsValid)
      {
        NewActivity.UserId = (int)userId;
        NewActivity.Coordinator = oneUser.FirstName;
        dbContext.Add(NewActivity);
        dbContext.SaveChanges();
        return RedirectToRoute(new
        {
          Controller = "Dashboard",
          action = "ViewActivity",
          id = NewActivity.ActivityId
        });
      }
      return View("NewActivity");
    }

    [Route("view/activity/{id}")]
    [HttpGet]
    public IActionResult ViewActivity(int id)
    {
      int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
          return RedirectToAction("Index", "Home");

      Models.Activity oneActivity = dbContext.Activity
      .Include(a => a.Participants)
      .ThenInclude(b => b.User)
      .FirstOrDefault(c => c.ActivityId == id);
      ViewBag.UserId = (int)userId;
      return View("ViewActivity", oneActivity);
    }

    [Route("join/activity/{id}")]
    [HttpGet]
    public IActionResult JoinActivity(int id)
    {
      int? userId = HttpContext.Session.GetInt32("UserId");
      if (userId == null)
        return RedirectToAction("Index", "Home");

        var oneActivity = dbContext.Activity.FirstOrDefault(a => a.ActivityId == id);
        Association newAssociation = new Association()
        {
          UserId = (int)userId,
          ActivityId = id
        };
        dbContext.Associations.Add(newAssociation);
        oneActivity.Participants.Add(newAssociation);
        dbContext.SaveChanges();
        return RedirectToAction("Dashboard");
    }

    [Route("leave/activity/{id}")]
    [HttpGet]
    public IActionResult LeaveActivity(int id)
    {
      int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
          return RedirectToAction("Index", "Home");

      var oneAssociation = dbContext.Associations
      .FirstOrDefault(a => a.ActivityId == id && a.UserId == userId);
      dbContext.Associations.Remove(oneAssociation);
      dbContext.SaveChanges();
      return RedirectToAction("Dashboard");
    }

    [Route("/delete/activity/{id}")]
    [HttpGet]
    public IActionResult DeleteActivity(int id)
    {
      int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
          return RedirectToAction("Index", "Home");

      var oneActivity = dbContext.Activity
      .FirstOrDefault(a => a.ActivityId == id);
      dbContext.Activity.Remove(oneActivity);
      dbContext.SaveChanges();
      return RedirectToAction("Dashboard");
    }
  }


}