import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { AuthenticationService } from '../../services/authService/authentication.service';
import { ErrorMessages } from 'src/app/helpers/Constants';
import { SnackbarService } from 'src/app/services/snackBar/snackbar.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  public loginForm!: FormGroup;
  token: string | undefined;
  firstStep!: boolean | false;
  isLoading: boolean = false;

  constructor(
    private authenticationService: AuthenticationService,
    private snackbarService: SnackbarService
  ) {
    this.token = undefined;
  }

  ngOnInit() {
    this.loginForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
      captchToken: new FormControl('', Validators.required),
    });
  }

  public onSubmit() {
    const invalid = [];
    const controls = this.loginForm.controls;
    for (const name in controls) {
      if (controls[name].invalid) {
        invalid.push(name);
      }
    }

    if (invalid.length != 0) {
      this.snackbarService.open(ErrorMessages.FormValidation);
    } else {
      this.isLoading = true;
      this.authenticationService.login(this.loginForm.value);
      this.isLoading = false;
    }
  }
}
