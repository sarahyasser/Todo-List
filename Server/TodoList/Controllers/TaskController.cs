using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TodoList.Models;
using System.Linq;
namespace TodoList.Controllers
{
    public class TaskController : Controller
    {
   
        private static List<Task> tasks = new List<Task>
        {
            new Task { TaskId = 1, Description = "Task 1", DueDate = new DateTime(2023, 8, 31) },
            new Task { TaskId = 2, Description = "Task 2", DueDate = new DateTime(2023, 9, 15) }
        };
        private static Stack<Task> completedTasks = new Stack<Task>() ;

        [HttpPost("api/addTask")]
        public IActionResult CreateTask([FromBody] Task newTask)
        {
            int newTaskId = createID();
            newTask.TaskId = newTaskId;
            tasks.Add(newTask);

            // Return the newly created task along with the generated task id
            return CreatedAtAction(nameof(GetTaskById), new { id = newTaskId }, newTask);

        }
        private int createID()
        {
            return tasks.Count + 1;
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
            List<Task> sortedTasks = SortTasksByDueDate(tasks);
            return Ok(sortedTasks);
        }

        private List<Task> SortTasksByDueDate(List<Task> taskList)
        {
            List<Task> sortedTasks = taskList.OrderBy(t => t.DueDate).ToList();
            return sortedTasks;
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
            return Ok(completedTasks);
        }

        [HttpDelete("api/deleteTask/{id}")]
        public IActionResult deleteTask(int id)
        {
            Task task = tasks.Find(t => t.TaskId == id);
            if (task == null)
            {
                return NotFound("Task not found.");
            }
            tasks.Remove(task);
            return Ok(task);
        }
    }
}
