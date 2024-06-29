import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from './features/auth/auth.guard';
import { LoginComponent } from './features/auth/login/login.component';

const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('./features/home/home.module').then((m) => m.HomeModule),
    canActivate: [authGuard],
  },
  {
    path: 'home',
    loadChildren: () =>
      import('./features/home/home.module').then((m) => m.HomeModule),
    canActivate: [authGuard],
  },
  {
    path: 'auth',
    loadChildren: () =>
      import('./features/auth/auth.module').then((m) => m.AuthModule),
    canActivate: [authGuard],
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'governate',
    loadChildren: () =>
      import('./features/governate/governate.module').then(
        (m) => m.GovernateModule
      ),
  },
  {
    path: 'city',
    loadChildren: () =>
      import('./features/city/city.module').then((m) => m.CityModule),
  },
  {
    path: 'representative',
    loadChildren: () =>
      import('./features/representative/representative.module').then((m) => m.RepresentativeModule),
  },
  {
    path: 'branch',
    loadChildren: () =>
      import('./features/branch/branch.module').then((m) => m.BranchModule),
  },
  {
    path: '**',
    loadChildren: () =>
      import('./shared/modules/shared.module').then((m) => m.SharedModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
