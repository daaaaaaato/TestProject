using Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProject.Models;

namespace TestProject.Controllers
{
    public class HomeController : Controller
    {
        private BloggerEntities db = new BloggerEntities();

        [Session]
        public ActionResult Index()
        {
            var posts = db.GetPosts().ToList();
            return View(posts);
        }

        [Session]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Session]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Session]
        [Route("posts")]
        public ActionResult Posts()
        {
            var posts = db.GetPosts().ToList();
            return View(posts);
        }

        [Session]
        [HttpGet]
        [Route("posts/add")]
        public ActionResult AddPosts()
        {
            var posts = db.GetPosts().ToList();
            ViewBag.News = posts;
            var model = new PostModel();
            return View(model);
        }

        [Session]
        [HttpPost]
        [Route("posts/add")]
        public ActionResult AddPosts(PostModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Post
                {
                    Name = model.Name,
                    Description = model.Description
                };
                db.Posts.Add(entity);
                db.SaveChanges();
                return Redirect("/posts");
            }
            return View(model);
        }

        [Session]
        [HttpGet]
        [Route("posts/edit/{id}")]
        public ActionResult EditPosts(int id)
        {
            var posts = db.Posts.FirstOrDefault(d => d.ID == id);

            if (posts == null)
                return HttpNotFound();
            ViewBag.Posts = posts;
            return View(posts);
        }




        [Session]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Route("posts/edit/{id}")]
        public ActionResult EditPosts(int id, PostModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = db.Posts.FirstOrDefault(d => d.ID == model.ID);
                entity.ID = model.ID;
                entity.Name = model.Name;
                entity.Description = model.Description;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/posts");
            }
            return View(model);
        }


        [Session]
        [Route("posts/delete/{id}")]
        public ActionResult DeletePosts(int id)
        {
            Post posts = db.Posts.Find(id);
            db.Posts.Remove(posts);
            db.SaveChanges();
            return Redirect("/posts");

        }


        

    }
}