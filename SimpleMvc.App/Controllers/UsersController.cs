namespace SimpleMvc.App.Controllers
{
    using SimpleMcv.Framework.Attributes.Methods;
    using SimpleMcv.Framework.Controllers;
    using SimpleMcv.Framework.Interfaces;
    using BindingModels;
    using SimpleMvc.Domain;
    using SimpleMvc.Data;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel model)
        {
            if (!this.IsValidModel(model))
            {
                return View();
            }

            var user = new User
            {
                Username = model.Username,
                Password = model.Password
            };

            using (var db = new SimpleAppDb())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }

            return View();
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserBindingModel model)
        {
            using (var db = new SimpleAppDb())
            {
                var foundUser = db.Users.FirstOrDefault(u => u.Username == model.Username);

                if (foundUser == null)
                {
                    return RedirectToAction("/home/login");
                }

                db.SaveChanges();
                this.SignIn(foundUser.Username);
            }

            return RedirectToAction("home/index");
        }

        [HttpGet]
        public IActionResult All()
        {
            if (!this.User.IsAuthenticated)
            {
                return RedirectToAction("/users/login");
            }

            Dictionary<int, string> users = new Dictionary<int, string>();

            using (var db = new SimpleAppDb())
            {
                users = db.Users.ToDictionary(u => u.Id, u => u.Username);
            }

            this.Model["users"] =
                users.Any() ? string.Join(string.Empty, users.Select(u => $"<li><a href=\"/users/profile?id={u.Key}\">{u.Value}</a></li>")) : string.Empty;

            return View();
        }

        [HttpGet]
        public IActionResult Profile(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return RedirectToAction("/users/login");
            }

            using (var db = new SimpleAppDb())
            {
                var currentUser = db.Users.Include(u => u.Notes).FirstOrDefault(u => u.Id == id);
                var currentUserNotes = currentUser.Notes.ToList();
                
                this.Model["username"] = currentUser.Username;
                this.Model["userid"] = currentUser.Id.ToString();
                this.Model["notes"] = 
                    currentUserNotes.Any() 
                    ? string.Join(string.Empty, currentUserNotes.Select(n => $"<li><strong>{n.Title}</strong> {n.Content}</li>")) 
                    : "No Notes";
            }

            return View();
        }

        [HttpPost]
        public IActionResult Profile(AddNoteBindingModel model)
        {
            if (!this.IsValidModel(model))
            {
                return View();
            }

            var note = new Note
            {
                UserId = model.UserId,
                Title = model.Title,
                Content = model.Content
            };

            using (var db = new SimpleAppDb())
            {
                db.Notes.Add(note);
                db.SaveChanges();
            }

            return this.Profile(model.UserId);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            this.SignOut();

            return RedirectToAction("/home/index");
        }
    }
}
