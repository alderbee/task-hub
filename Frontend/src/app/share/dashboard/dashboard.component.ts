import { Component } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authService/authentication.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent {
  ngOnInit(): void {}

  constructor(private authenticationService: AuthenticationService) {}

  logout(): void {
    this.authenticationService.logout();
  }
}
