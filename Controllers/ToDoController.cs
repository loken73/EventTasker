using System.Linq;
using System.Threading.Tasks;
using EventTasker.Data;
using EventTasker.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;

namespace EventTasker.Controllers
{
    [Authorize]
    public class ToDoController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public ToDoController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        [HttpGet]
        [Authorize(Roles = "ToDosForAll")]
        public IActionResult Index ()
        {
            var tasks = _context.ToDos.OrderBy(t => t.StartTime).Include(u => u.User).ToList();
            
            return View(tasks);

            /*TimeSpan time = new TimeSpan();
            String.Format*/
        }

        public IActionResult MyToDos()
        {
            var userNow = _userManager.GetUserAsync(HttpContext.User);
            //var userNowId = userNow.Id.ToString();
            var name = userNow.Result.UserName;

            var todos = _context.ToDos.OrderBy(t => t.StartTime).Where(u => u.User.UserName == name).Include(u => u.User).ToList();

            var nameSplit = name.Split('@');

            ViewData["Name"] = Upper.UppercaseFirst(nameSplit[0]);

            return View(todos);
        }

        [Authorize(Roles = "ToDosForAll")]
        public IActionResult CreateToDo ()
        {
            var names = _context.Users.Select(u => u.UserName).ToList();
            ViewData["Users"] = names;
            return View();
        }

        [HttpPost]
        public IActionResult CreateToDo (CreateToDoViewModel model)
        {
            var fullUserName = String.Concat(model.User, "@eventtasker.com");

            fullUserName.ToLower();

            var task = new ToDo()
            {
                Title = model.TaskTitle,
                Description = model.TaskDescription,
                StartTime = DateTime.Now,
                IsComplete = false,
            };

            task.User = _context.Users.Where(u => u.UserName == fullUserName).FirstOrDefault();

            //task.User = new ApplicationUser();

            //task.User.Id = userId;

            //var currentUser = _context.Users.Where(u => u.UserName == model.User);
            

            _context.ToDos.Add(task);
            _context.SaveChanges();

            return Redirect("CreateToDo");
        }
    }
}