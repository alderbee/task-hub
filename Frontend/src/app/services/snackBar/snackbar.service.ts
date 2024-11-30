import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root',
})
export class SnackbarService {
  private readonly defaultAction = 'Splash';

  constructor(private snackbar: MatSnackBar) {}

  open(
    message: string,
    action: string = this.defaultAction,
    duration: number = 2500
  ) {
    this.snackbar.open(message, action, {
      duration,
      horizontalPosition: 'center',
      verticalPosition: 'top',
    });
  }
}
