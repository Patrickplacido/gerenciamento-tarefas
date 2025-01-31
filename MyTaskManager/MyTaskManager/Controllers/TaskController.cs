using Microsoft.AspNetCore.Mvc;
using MyTaskManager.Data;
using MyTaskManager.Services;

namespace MyTaskManager.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public TasksController(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public IActionResult Index(string status = null!, DateTime? startDate = null, DateTime? endDate = null)
        {
            var userId = _userService.GetUserIdFromToken();

            var tasksQuery = _context.Tasks.Where(t => t.UserId == userId).AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                tasksQuery = tasksQuery.Where(t => t.Status == status);
            }

            if (startDate.HasValue)
            {
                tasksQuery = tasksQuery.Where(t => t.DueDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                tasksQuery = tasksQuery.Where(t => t.DueDate <= endDate.Value);
            }

            var tasks = tasksQuery.ToList();

            return View(tasks);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Models.Task task)
        {
            if (ModelState.IsValid)
            {
                var userId = _userService.GetUserIdFromToken();

                task.UserId = userId;

                _context.Add(task);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        public IActionResult Edit(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(int id, Models.Task task)
        {
            var userId = _userService.GetUserIdFromToken();

            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                task.UserId = userId;
                _context.Update(task);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
