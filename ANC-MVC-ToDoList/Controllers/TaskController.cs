using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Domain.ViewModels.Task;

namespace ANC_MVC_ToDoList.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskViewModel model)
        {
            return Ok();
        }
    }
}