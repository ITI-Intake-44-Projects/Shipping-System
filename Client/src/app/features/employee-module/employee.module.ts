// employee.module.ts

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
// Remove NgxPaginationModule import if present
import { EmployeeListComponent } from './employee-list/employee-list.component';

@NgModule({
  declarations: [
    EmployeeListComponent
  ],
  imports: [
    CommonModule,
    FormsModule
    // Remove NgxPaginationModule from imports
  ],
  exports: [
    EmployeeListComponent
  ]
})
export class EmployeeModule { }
