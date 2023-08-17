import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  private apiUrl = 'http://localhost:30537/api';

  constructor(private http: HttpClient) {}

  getTasks(): Observable<any> {
    return this.http.get(`${this.apiUrl}/tasks`);
  }
  completeTask(taskId: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/completeTask/${taskId}`, null);
  }
  getCompletedTasks(): Observable<any> {
    return this.http.get(`${this.apiUrl}/completedTasks`);
  }
  addTask(taskData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/addTask`, taskData);
  }
  deleteTask(taskId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/deleteTask/${taskId}`);
  }


}
