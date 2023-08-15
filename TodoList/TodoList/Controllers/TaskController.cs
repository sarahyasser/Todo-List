using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TodoList.Models;
namespace TodoList.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private static List<Task> tasks = new List<Task>
        {
            new Task { TaskId = 1, Description = "Task 1", DueDate = new DateTime(2023, 8, 31), IsCompleted = false },
            new Task { TaskId = 2, Description = "Task 2", DueDate = new DateTime(2023, 9, 15), IsCompleted = true }
        };

        [HttpPost("api/addTask")]
        public IActionResult CreateTask([FromBody] Task newTask)
        {
            if (newTask == null)
            {
                return BadRequest("Invalid task data.");
            }

            int newTaskId = tasks.Count + 1;

            newTask.TaskId = newTaskId;
            newTask.IsCompleted = false;
            tasks.Add(newTask);

            // Return the newly created task along with the generated task id
            return CreatedAtAction(nameof(GetTaskById), new { id = newTaskId }, newTask);

        }

        //[HttpGet("{/api/getTask/{id}")]
        public IActionResult GetTaskById(int id)
        {
            Task task = tasks.Find(t => t.TaskId == id);
            if (task == null)
            {
                return NotFound("Task not found.");
            }

            return Ok(task);
        }

        [HttpGet("api/tasks")]
        public IActionResult GetAllTasks()
        {
            return Ok(tasks);
        }

        [HttpPut("/api/completeTask/{id}")]
        public IActionResult CompleteTask(int id)
        {
            Task task = tasks.Find(t => t.TaskId == id);
            if (task == null)
            {
                return NotFound("Task not found.");
            }

            task.IsCompleted = true;
            return Ok(task);
        }
    }
}
