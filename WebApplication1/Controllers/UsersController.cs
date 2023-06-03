using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UsersController : Controller
    {
        //Server=localhost;Database=master;Trusted_Connection=True;
        // GET: Users
        public ActionResult Index()
         {
            List<User> lstUsers = Models.User.GetAllUsers();

            return View(lstUsers);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            User user = new User();
            user.GetUserByID(id);
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                if (user != null)
                {
                    bool isSuccess = user.CreateUser(user);
                    if (isSuccess)
                        return RedirectToAction("Index");
                    else
                    {
                        ViewBag.errormsg("Error");
                        return View(user);
                    }

                }
            }
            catch
            {
                
            }
            return View();
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            User user = new User();
            user.GetUserByID(id);
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(User user)
        {
            try
            {
                if (user != null)
                {
                    bool isSuccess = user.EditUser(user);
                    if (isSuccess)
                        return RedirectToAction("Index");
                    else return View(user);

                }


            }
            catch
            {

            }
            return View();
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            User user = new User();
            user.GetUserByID(id);
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, User user)
        {
            try
            {
                bool isSuccess=user.DeleteUser(id);
                if (isSuccess)
                    return RedirectToAction("Index");
                else return View(user);
            }
            catch
            {
            return View(user);

            }
        }
    }
}
