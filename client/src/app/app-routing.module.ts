import { VillageCostModule } from './features/village-cost/village-cost.module';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from './features/auth/auth.guard';

const routes: Routes = [
  {path:'governate',loadChildren:()=>import('./features/governate/governate.module').then(m=>m.GovernateModule)},
  {path:'city',loadChildren:()=>import('./features/city/city.module').then(m=>m.CityModule)},
  {path:'order',loadChildren:()=>import('./features/order/order.module').then(m=>m.OrderModule)},
  { path: 'merchants', loadChildren: () => import('./features/merchant/merchants.module').then(m => m.MerchantsModule) },
  { path: 'employee', loadChildren: () => import('./features/employee/employee.module').then(m => m.EmployeeModule) },
  { path: 'weight', loadChildren: () => import('./features/weight/weight.module').then(m=>m.WeightModule)},
  { path: 'village-cost', loadChildren: () => import('./features/village-cost/village-cost.module').then(m=>m.VillageCostModule)},
  { path: 'auth', loadChildren: () => import('./features/auth/auth.module').then(m => m.AuthModule)},
  { path: 'admin', loadChildren: () => import('./features/admin/admin.module').then(m => m.AdminModule)},
  // , canActivate: [authGuard]
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }