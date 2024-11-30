import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs/internal/Subscription';
import { AuthenticationService } from 'src/app/services/authService/authentication.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit, OnDestroy {
  userIsAuthenticated: boolean = false;
  private authStatusSub!: Subscription;

  constructor(
    private authenticationService: AuthenticationService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.authStatusSub = this.authenticationService
      .getAuthStatusListener()
      .subscribe((isAuthenticated: boolean) => {
        this.userIsAuthenticated = isAuthenticated;
        this.cdr.detectChanges();
      });
  }
  ngOnDestroy(): void {
    if (this.authStatusSub) {
      this.authStatusSub.unsubscribe();
    }
  }

  logout(): void {
    this.authenticationService.logout();
  }
}
