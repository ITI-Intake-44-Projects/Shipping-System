import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from './features/auth/auth.guard';

const routes: Routes = [
  // { path: '', loadChildren: () => import('./features/home/home.module').then(m => m.HomeModule) },
  {path:'governate',loadChildren:()=>import('./features/governate/governate.module').then(m=>m.GovernateModule)},
  {path:'city',loadChildren:()=>import('./features/city/city.module').then(m=>m.CityModule)},
  {path:'order',loadChildren:()=>import('./features/order/order.module').then(m=>m.OrderModule)},


 // { path: '', loadChildren: () => import('./features/auth/auth.module').then(m => m.AuthModule), canActivate: [authGuard] },
 //{ path: 'auth', loadChildren: () => import('./features/auth/auth.module').then(m => m.AuthModule)},
 { path: 'merchants', loadChildren: () => import('./features/merchant/merchants.module').then(m => m.MerchantsModule) },
  { path: '**', loadChildren: () => import('./shared/modules/shared.module').then(m => m.SharedModule) }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
