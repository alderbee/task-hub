import { Component, Inject } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

import {
  ErrorMessages,
  Messages,
  OperationType,
  PriorityOptions,
  StatusOptions,
} from 'src/app/helpers/Constants';
import { TaskModel } from 'src/app/model/taskModel';
import { SnackbarService } from 'src/app/services/snackBar/snackbar.service';

@Component({
  selector: 'app-task-operation',
  templateUrl: './task-operation.component.html',
  styleUrls: ['./task-operation.component.scss'],
})
export class TaskOperationComponent {
  form!: FormGroup;
  taskName: string = Messages.EditTask;
  isUpdateTask = true;

  priorityOptions = PriorityOptions;
  statusOptions = StatusOptions;

  constructor(
    private fb: FormBuilder,
    private snackbar: SnackbarService,
    private dialogRef: MatDialogRef<TaskOperationComponent>,
    @Inject(MAT_DIALOG_DATA)
    public data: { selectedTask: TaskModel; operation: string }
  ) {
    this.isUpdateTask = true;
    if (this.data?.operation == OperationType.UpdateExistingTask.toString()) {
      this.taskName = Messages.EditTask + this.data?.selectedTask.title;
      this.isUpdateTask = false;
    } else {
      this.taskName = Messages.AddNewTask;
    }

    this.form = this.fb.group({
      title: [
        this.data?.selectedTask?.title,
        [
          Validators.required,
          Validators.minLength(1),
          Validators.maxLength(25),
        ],
      ],
      content: [this.data?.selectedTask?.content, [Validators.required]],
      startDate: [
        this.data?.selectedTask?.startDate,
        [Validators.required, this.startDateValidator],
      ],
      endDate: [
        this.data?.selectedTask?.endDate,
        [Validators.required, this.endDateValidator],
      ],
      priorityId: [this.data?.selectedTask?.priorityId, [Validators.required]],
      status: [this.data?.selectedTask?.status, [Validators.required]],
    });
  }

  save() {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    } else {
      this.form.markAllAsTouched();
      this.snackbar.open(ErrorMessages.AddNewTaskValidatorMessage);
    }
  }

  close() {
    this.dialogRef.close();
  }
  startDateValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const date = new Date(control.value);
      return isNaN(date.getTime()) ? { invalidDate: true } : null;
    };
  }

  endDateValidator(startDateControl: AbstractControl): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const startDate = startDateControl.value;
      const endDate = control.value;

      if (startDate && endDate) {
        return new Date(endDate) < new Date(startDate)
          ? { endDateBeforeStartDate: true }
          : null;
      }
      return null;
    };
  }
}
