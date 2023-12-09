using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using ToDoList.Domain.Filters.Task;
using ToDoList.Domain.Helpers;
using ToDoList.Domain.ViewModels.Task;
using ToDoList.Service.Interfaces;

namespace ANC_MVC_ToDoList.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CalculateCompletedTasks()
        {
            var response = await _taskService.CalculateCompletedTasks();
            if (response.StatusCode == ToDoList.Domain.Enum.StatusCode.OK)
            {
                var csvService = new CsvBaseService<IEnumerable<TaskViewModel>>();

                CultureInfo englishCulture = new CultureInfo("en-US");
                var formattedDate = DateTime.Now.ToString("MMMM d", englishCulture);

                var uploadFile = csvService.UploadFile(response.Data);
                return File(uploadFile, "text/csv", $"Statistic for {formattedDate}.csv");
            }
            return BadRequest(new { description = response.Description });
        }

        public async Task<IActionResult> GetCompletedTasks()
        {
            var result = await _taskService.GetCompletedTasks();
            return Json(new { data = result.Data });    
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskViewModel model)
        {
            var response = await _taskService.Create(model);
            if (response.StatusCode == ToDoList.Domain.Enum.StatusCode.OK)
            {
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }

        [HttpPost]
        public async Task<IActionResult> EndTask(long id)
        {
            var response = await _taskService.EndTask(id);
            if (response.StatusCode == ToDoList.Domain.Enum.StatusCode.OK)
            {
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }

        [HttpPost]
        public async Task<IActionResult> TaskHandler(TaskFilter filter)
        {
            var response = await _taskService.GetTasks(filter);
            return Json(new {data = response.Data});
        }
    }
}