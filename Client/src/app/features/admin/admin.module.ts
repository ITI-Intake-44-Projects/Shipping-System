import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrivilegesListComponent } from './privileges-list/privileges-list.component';
import { AddPrivilegeComponent } from './add-privilege/add-privilege.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AdminRoutingModule } from './admin-routing.module';

@NgModule({
  declarations: [
    PrivilegesListComponent,
    AddPrivilegeComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    AdminRoutingModule,
    RouterModule.forChild([
      { path: 'add-privilege', component: AddPrivilegeComponent },
      { path: 'privilege-list', component: PrivilegesListComponent }
    ]),
  ]
})
export class AdminModule { }
