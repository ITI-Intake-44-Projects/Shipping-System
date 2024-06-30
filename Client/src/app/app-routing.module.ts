import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from './features/auth/auth.guard';
import { AddEmployeeComponent } from './features/employee-module/add-employee/add-employee.component';
import { EmployeeListComponent } from './features/employee-module/employee-list/employee-list.component';

const routes: Routes = [
  { path: '', loadChildren: () => import('./features/home/home.module').then(m => m.HomeModule), canActivate:[authGuard] },
  { path: 'home', loadChildren: () => import('./features/home/home.module').then(m => m.HomeModule), canActivate:[authGuard] },
  { path: 'auth', loadChildren: () => import('./features/auth/auth.module').then(m => m.AuthModule) },
  { path: '**', loadChildren: () => import('./shared/modules/shared.module').then(m => m.SharedModule) },
  { path: 'emp',loadChildren:()=>import('./features/employee-module/employee.module').then(m=>m.EmployeeModule)},
  // { path: 'employees', component: EmployeeListComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
