import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TaskItem } from '../models/task-item';

@Injectable({ providedIn: 'root' })
export class TaskService {
  constructor(private http: HttpClient) {}

  getTasksForBoard(boardId: number): Observable<TaskItem[]> {
    return this.http.get<TaskItem[]>(`/api/tasks/board/${boardId}`);
  }

  addTask(task: TaskItem): Observable<TaskItem> {
    return this.http.post<TaskItem>('/api/tasks', task);
  }

  updateTask(task: TaskItem): Observable<TaskItem> {
    return this.http.put<TaskItem>(`/api/tasks/${task.id}`, task);
  }

  deleteTask(taskId: number): Observable<void> {
    return this.http.delete<void>(`/api/tasks/${taskId}`);
  }
}
