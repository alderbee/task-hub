import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './userManagement/login/login.component';
import { RegisterComponent } from './userManagement/register/register.component';
import { AuthGuard } from './helpers/auth.guard';
import { DashboardComponent } from './share/dashboard/dashboard.component';
import { TaskManagerComponent } from './taskManagement/task-manager/task-manager.component';

const routes: Routes = [
  { path: '', component: TaskManagerComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
