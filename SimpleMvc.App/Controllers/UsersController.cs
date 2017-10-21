namespace SimpleMvc.App.Controllers
{
    using SimpleMcv.Framework.Attributes.Methods;
    using SimpleMcv.Framework.Controllers;
    using SimpleMcv.Framework.Interfaces;
    using BindingModels;
    using SimpleMvc.Domain;
    using SimpleMvc.Data;
    using SimpleMcv.Framework.Interfaces.Generic;
    using System.Collections.Generic;
    using System.Linq;
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

        public IActionResult<AllUsernamesViewModel> All()
        {
            List<string> usernames = null;
            using (var db = new SimpleAppDb())
            {
                usernames = db.Users.Select(u => u.Username).ToList();
            }

            var viewModel = new AllUsernamesViewModel
            {
                Usernames = usernames
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult<UserProfileViewModel> Profile(int id)
        {
            using (var db = new SimpleAppDb())
            {
                var user = db.Users.Include(u=> u.Notes).FirstOrDefault(u=> u.Id == id);
                var viewModel = new UserProfileViewModel
                {
                    UserId = user.Id,
                    Username = user.Username,
                    Notes = user.Notes
                            .Select(x => new NoteViewModel
                            {
                                Title = x.Title,
                                Content = x.Content
                            })
                };

                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult<UserProfileViewModel> Profile(AddNoteBindingModel model)
        {
            using (var db = new SimpleAppDb())
            {
                var user = db.Users.Find(model.UserId);
                var note = new Note
                {
                    Title = model.Title,
                    Content = model.Content
                };

                user.Notes.Add(note);
                db.SaveChanges();
            }

            return Profile(model.UserId);
        }
    }
}
