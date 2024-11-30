import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authService/authentication.service';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { ErrorMessages, Messages } from 'src/app/helpers/Constants';
import { SnackbarService } from 'src/app/services/snackBar/snackbar.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  public registerForm!: FormGroup;
  token: string | undefined;
  passwordReuirementMessage = Messages.PasswordRequirementMessage;
  isLoading: boolean = false;

  constructor(
    private authenticationService: AuthenticationService,
    private snackbar: SnackbarService
  ) {
    this.token = undefined;
  }

  ngOnInit() {
    this.registerForm = new FormGroup({
      userName: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [
        Validators.required,
        Validators.pattern(
          '(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-zd$@$!%*?&].{8,}'
        ),
      ]),
      captchToken: new FormControl('', Validators.required),
    });
  }

  public onSubmit() {
    const invalid = [];
    const controls = this.registerForm.controls;
    for (const name in controls) {
      if (controls[name].invalid) {
        invalid.push(name);
      }
    }

    if (invalid.length != 0) {
      this.snackbar.open(ErrorMessages.FormValidation);
    } else {
      this.isLoading = true;
      this.authenticationService.register(this.registerForm.value);
      this.isLoading = false;
    }
  }
}
