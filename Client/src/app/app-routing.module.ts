import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from './features/auth/auth.guard';

const routes: Routes = [
  // { path: '', loadChildren: () => import('./features/home/home.module').then(m => m.HomeModule) },
  {path:'governate',loadChildren:()=>import('./features/governate/governate.module').then(m=>m.GovernateModule)},
  {path:'city',loadChildren:()=>import('./features/city/city.module').then(m=>m.CityModule)},
  {path:'order',loadChildren:()=>import('./features/order/order.module').then(m=>m.OrderModule)},


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
