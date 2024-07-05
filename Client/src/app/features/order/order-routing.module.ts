import { OrderFormComponent } from './order-form/order-form.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrderReportsComponent } from './order-reports/order-reports.component';

const routes: Routes = [
  { path: 'add', component:OrderFormComponent },
  { path: 'reports', component: OrderReportsComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrderRoutingModule { }
