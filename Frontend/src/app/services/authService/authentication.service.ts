import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { ErrorMessages } from '../../helpers/Constants';
import { LoginResponse } from '../../model/loginReponse';
import { environment } from 'src/environments/environment';
import { BehaviorSubject } from 'rxjs';
import { SnackbarService } from '../snackBar/snackbar.service';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private tokenKey = 'token';
  private userId = 'userId';

  private authStatusListener = new BehaviorSubject<boolean>(false);

  private isAuthenticated = false;

  constructor(
    private http: HttpClient,
    private router: Router,
    private snackbar: SnackbarService
  ) {
    const token = this.getToken();
    console.log('Token on service init:', token);
    if (token) {
      this.isAuthenticated = true;
      console.log('User is authenticated on service init');
      this.authStatusListener.next(true);
    } else {
      this.isAuthenticated = false;
      console.log('User is not authenticated on service init');
      this.authStatusListener.next(false);
    }
  }

  public login(formData: {
    username: string;
    password: string;
    captchToken: string;
  }): void {
    const { username, password, captchToken } = formData;
    const loginPayload = { username, password, captchToken };

    this.http
      .post<LoginResponse>(
        environment.apiUrl + '/UserManagement/Login',
        loginPayload
      )
      .subscribe(
        (res: LoginResponse) => {
          if (res.token) {
            localStorage.setItem(this.tokenKey, res.token);
            localStorage.setItem(this.userId, JSON.stringify(res.userId));
            this.authStatusListener.next(true);
            this.isAuthenticated = true;
            this.router.navigate(['']);
          } else {
            this.snackbar.open(ErrorMessages.UnknownError);
            this.router.navigate(['/login']);
          }
        },
        (err) => {
          this.snackbar.open(err?.error?.title || ErrorMessages.UnknownError);
        }
      );
  }

  public register(formData: {
    userName: any;
    email: any;
    password: any;
    captchToken: any;
  }): void {
    const { userName, email, password, captchToken } = formData;
    const registrationPayload = {
      userName,
      email,
      password,
      captchToken,
    };

    this.http
      .post<LoginResponse>(
        environment.apiUrl + '/UserManagement/Registration',
        registrationPayload
      )
      .subscribe(
        (res: LoginResponse) => {
          if (res.token) {
            localStorage.setItem(this.tokenKey, res.token);
            localStorage.setItem(this.userId, JSON.stringify(res.userId));
            this.authStatusListener.next(true);
            this.isAuthenticated = true;
            this.router.navigate(['']);
          } else {
            this.snackbar.open(ErrorMessages.UnknownError);
            this.router.navigate(['/login']);
          }
        },
        (err) => {
          this.snackbar.open(err?.error?.title || ErrorMessages.UnknownError);
        }
      );
  }

  public logout(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.userId);
    this.authStatusListener.next(false);
    this.isAuthenticated = false;
    this.router.navigate(['/login']);
  }

  public isLoggedIn(): boolean {
    const token = localStorage.getItem(this.tokenKey);

    return token != null && token.length > 0;
  }

  public getToken(): string | null {
    return this.isLoggedIn() ? localStorage.getItem(this.tokenKey) : null;
  }

  public getAuthStatusListener() {
    this.authStatusListener.next(this.isAuthenticated);
    return this.authStatusListener.asObservable();
  }
}
