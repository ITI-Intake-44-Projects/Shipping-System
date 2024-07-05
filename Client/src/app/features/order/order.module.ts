// import { NgModule } from '@angular/core';
// import { CommonModule } from '@angular/common';
// import { FormsModule, ReactiveFormsModule } from '@angular/forms';
// import { OrderRoutingModule } from './order-routing.module';
// import { SharedModule } from '../../shared/modules/shared/shared.module';

// import { MatFormFieldModule } from '@angular/material/form-field';
// import { MatSelectModule } from '@angular/material/select';
// import { MatInputModule } from '@angular/material/input';
// import { MatButtonModule } from '@angular/material/button';
// import { MatDatepickerModule } from '@angular/material/datepicker';
// import { MatNativeDateModule } from '@angular/material/core';

// import { OrderFormComponent } from './order-form/order-form.component';
// import { OrderReportsComponent } from './order-reports/order-reports.component';

// @NgModule({
//   declarations: [
//     OrderFormComponent,
//     OrderReportsComponent
//   ],
//   imports: [
//     CommonModule,
//     OrderRoutingModule,
//     FormsModule,
//     ReactiveFormsModule,
//     SharedModule,
//     MatFormFieldModule,
//     MatSelectModule,
//     MatInputModule,
//     MatButtonModule,
//     MatDatepickerModule,
//     MatNativeDateModule
//   ]
// })
// export class OrderModule { }

































import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { OrderRoutingModule } from './order-routing.module';
import { OrderFormComponent } from './order-form/order-form.component';
import { OrderReportsComponent } from './order-reports/order-reports.component';
import { SharedModule } from '../../shared/modules/shared/shared.module';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';

@NgModule({
  declarations: [
    OrderFormComponent,
    OrderReportsComponent
  ],
  imports: [
    CommonModule,
    OrderRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatTableModule,
    MatPaginatorModule
  ]
})
export class OrderModule { }
