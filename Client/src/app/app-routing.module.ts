import { VillageCostModule } from './features/village-cost/village-cost.module';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from './features/auth/auth.guard';

const routes: Routes = [
  // { path: '', loadChildren: () => import('./features/auth/auth.module').then(m => m.AuthModule), canActivate: [authGuard] },
  { path: 'auth', loadChildren: () => import('./features/auth/auth.module').then(m => m.AuthModule)},
  { path: 'weight', loadChildren: () => import('./features/weight/weight.module').then(m=>m.WeightModule)},
  { path: 'village-cost', loadChildren: () => import('./features/village-cost/village-cost.module').then(m=>m.VillageCostModule)}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
