import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable } from 'rxjs';
import { TaskModel } from '../../model/taskModel';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { AddTask } from '../../model/addTask';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  private userId = localStorage.getItem('userId');

  constructor(private snackbar: MatSnackBar, private https: HttpClient) {}

  public getTasks(): Observable<TaskModel[]> {
    if (!this.userId) {
      throw new Error('User ID is missing from local storage.');
    }

    const params = new HttpParams().set('userId', this.userId);

    return this.https.get<TaskModel[]>(
      `${environment.apiUrl}/TaskManagement/GetTask`,
      { params }
    );
  }

  public addNewTask(task: AddTask): Observable<boolean> {
    if (!this.userId) {
      throw new Error('User ID is missing from local storage.');
    }
    task.userId = Number(this.userId);
    return this.https.post<boolean>(
      `${environment.apiUrl}/TaskManagement/AddTask`,
      task
    );
  }

  public updateTask(task: TaskModel): Observable<boolean> {
    if (!this.userId) {
      throw new Error('User ID is missing from local storage.');
    }
    task.userId = Number(this.userId);
    return this.https.put<boolean>(
      `${environment.apiUrl}/TaskManagement/UpdateTask`,
      task
    );
  }

  public deleteTask(taskId: number): Observable<boolean> {
    if (!this.userId) {
      throw new Error('User ID is missing from local storage.');
    }

    const params = new HttpParams().set('taskId', taskId);

    return this.https.delete<boolean>(
      `${environment.apiUrl}/TaskManagement/RemoveTask`,
      { params }
    );
  }
}
