import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { adminGuard } from './admin.guard';
import { AdminModule } from './admin.module';

const routes: Routes = [
  {
    path: '', component: AdminModule, canActivate: [adminGuard], children:[ ]
  }
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
