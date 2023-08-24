import { Component, OnInit } from '@angular/core';
import {TaskService } from 'src/app/task.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit{
  tasks: any[] = []; // Initialize tasks
  completedTasks:any[] = []; //initialize completed tasks
  newTaskDescription: string = ''; 
  newTaskDueDate: string = ''; 

  constructor(private TaskService: TaskService){ }

  ngOnInit(): void {
    
    this.fetchTasks(); // Fetch tasks when the component initializes
    this.fetchCompletedTasks();
  }
  updateContent(){
    this.fetchTasks();
    this.fetchCompletedTasks()
  }
  

  fetchTasks(): void {
    this.TaskService.getTasks().subscribe(
      (tasks) => {
        console.log(tasks);
        this.tasks = tasks; // Assign fetched tasks to the tasks array
      },
      (error) => {
        console.log(error);
      }
    );
  }

  fetchCompletedTasks() {
    this.TaskService.getCompletedTasks().subscribe(
      (completedTasks) => {
        console.log(completedTasks);
        this.completedTasks = completedTasks; // Assign fetched tasks to the tasks array
      },
      (error) => {
        console.log(error);
      }
    );
  }

  reloadCurrentPage() {
    window.location.reload();
  }

  onCheckboxClick(task: any): void {
    if (!task.isCompleted) {
      this.completeTask(task.taskId);
    }
  }

  completeTask(taskId: number): void {
    this.TaskService.completeTask(taskId).subscribe(
      (completedTask) => {
        console.log('Task completed:', completedTask);
        this.updateContent();
      },
      (error) => {
        console.log(error);
      }
    );
  }
  addTask() {
    if (!this.newTaskDescription) {
      alert('Please enter task description');
      return;
    }
    if (!this.newTaskDueDate) {
      alert('Please enter due date');
      return;
    }

    const newTask = {
      Description: this.newTaskDescription,
      DueDate: this.newTaskDueDate,
    };

    this.TaskService.addTask(newTask).subscribe(response => {
      console.log('Task added successfully:', response);
      alert('Task added successfully');
      this.updateContent();
    }, error => {
      console.error('Error adding task:', error);
    });
    this.newTaskDescription = '';
    this.newTaskDueDate = '';
  }
  deleteTask(task: any): void {
    this.TaskService.deleteTask(task.taskId).subscribe(
      (response) => {
        console.log('Task deleted successfully:', response);
        alert('Task deleted successfully');
        this.updateContent();
      },
      (error) => {
        console.error('Error deleting task:', error);
      }
    );
  }

}
