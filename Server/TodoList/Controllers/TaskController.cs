using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TodoList.Models;
using System.Linq;
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
            new Task { TaskId = 2, Description = "Task 2", DueDate = new DateTime(2023, 9, 15), IsCompleted = false }
        };
        private static Stack<Task> completedTasks = new Stack<Task>() ;

        [HttpPost("api/addTask")]
        public IActionResult CreateTask([FromBody] Task newTask)
        {
            if (newTask == null)
            {
                return BadRequest("Invalid task data.");
            }

            int newTaskId = tasks.Count + 1;

            //DateTime newDate = DateTime.Parse(newTask.DueDate);
           // newTask.DueDate = newDate;
            newTask.TaskId = newTaskId;
            newTask.IsCompleted = false;
            tasks.Add(newTask);

            // Return the newly created task along with the generated task id
            return CreatedAtAction(nameof(GetTaskById), new { id = newTaskId }, newTask);

        }

        [HttpGet("api/tasks/{id}")]
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

        [HttpPut("api/completeTask/{id}")]
        public IActionResult CompleteTask(int id)
        {
            Task task = tasks.Find(t => t.TaskId == id);
            if (task == null)
            {
                return NotFound("Task not found.");
            }

            task.IsCompleted = true;
            completedTasks.Push(task);
            tasks.Remove(task);
            return Ok(task);
        }
        [HttpGet("api/completedTasks")]
        public IActionResult GetCompletedTasks()
        {
           // List<Task> completedTasks = tasks.Where(t => t.IsCompleted).ToList();
            return Ok(completedTasks);
        }
    }
}
