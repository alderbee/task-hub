import { Component, TemplateRef, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { TaskModel } from 'src/app/model/taskModel';
import { TaskService } from 'src/app/services/taskService/task.service';
import { TaskOperationComponent } from '../task-operation/task-operation.component';
import {
  ErrorMessages,
  Messages,
  OperationType,
} from 'src/app/helpers/Constants';
import { SnackbarService } from 'src/app/services/snackBar/snackbar.service';

@Component({
  selector: 'app-task-manager',
  templateUrl: './task-manager.component.html',
  styleUrls: ['./task-manager.component.scss'],
})
export class TaskManagerComponent {
  displayedColumns: string[] = [
    'title',
    'content',
    'status',
    'priorityId',
    'startDate',
    'endDate',
    'Delete',
  ];

  dataSource: MatTableDataSource<TaskModel>;
  selectedTask!: TaskModel;
  successfulyAddedNewtask: boolean = false;
  isLoading: boolean = true;
  deleteTaskTitle!: string;

  opAddNewTask = OperationType.AddNewTask;
  opUpdateTask = OperationType.UpdateExistingTask;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild('deleteDialog') deleteDialogTemplate!: TemplateRef<any>;

  constructor(
    private taskService: TaskService,
    private dialog: MatDialog,
    private snackbar: SnackbarService
  ) {
    this.isLoading = true;
    this.dataSource = new MatTableDataSource<TaskModel>([]);
    this.GetTask();
    this.isLoading = false;
  }

  GetTask() {
    this.taskService.getTasks().subscribe(
      (tasks) => {
        this.dataSource.data = tasks;
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isLoading = false;
      },
      (error) => {
        if (error?.error) {
          this.onRowClick(undefined, this.opAddNewTask);
          this.snackbar.open(error?.error);
          this.isLoading = false;
        } else {
          this.onRowClick(undefined, this.opAddNewTask);
          this.snackbar.open(ErrorMessages.UnknownError);
          this.isLoading = false;
        }
      }
    );
  }

  onRowClick(row?: TaskModel, operation?: string) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    dialogConfig.data = {
      selectedTask: row,
      operation: operation,
    };

    const dialogRef = this.dialog.open(TaskOperationComponent, dialogConfig);
    dialogRef.afterClosed().subscribe((data) => {
      if (data != null && data != undefined) {
        if (operation == OperationType.AddNewTask) {
          this.AddNewTask(data);
        } else {
          data.taskId = row?.taskId;
          this.UpdateTask(data);
        }
      } else {
        this.snackbar.open(Messages.NoDataSaved);
      }
    });
  }

  AddNewTask(data: any) {
    this.isLoading = true;
    this.taskService.addNewTask(data).subscribe(
      (tasks) => {
        this.successfulyAddedNewtask = tasks;
        this.GetTask();
      },
      (error) => {
        this.snackbar.open(ErrorMessages.AddNewTaskError);
        this.isLoading = false;
      }
    );
  }

  UpdateTask(data: any) {
    this.isLoading = true;
    this.taskService.updateTask(data).subscribe(
      (tasks) => {
        this.successfulyAddedNewtask = tasks;
        this.GetTask();
      },
      (error) => {
        this.snackbar.open(ErrorMessages.AddNewTaskError);
        this.isLoading = false;
      }
    );
  }

  onDeleteClick(row: TaskModel, event: MouseEvent) {
    event.stopPropagation();
    this.deleteTaskTitle = row?.title;

    const dialogRef = this.dialog.open(this.deleteDialogTemplate, {
      width: '300px',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.deleteTask(row);
      }
    });
  }

  deleteTask(task: TaskModel) {
    this.isLoading = true;

    this.taskService.deleteTask(task.taskId).subscribe(
      () => {
        this.snackbar.open(Messages.TaskDeleted);
        this.GetTask();
      },
      (error) => {
        this.snackbar.open(ErrorMessages.DeleteTaskMessage);
        this.isLoading = false;
      }
    );
  }
}
