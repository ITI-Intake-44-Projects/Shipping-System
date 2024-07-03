import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GovernateComponentComponent } from './governate-component/governate-component.component';
import { GovernateRoutingModule } from './governate-routing.module';
import { PaginationComponent } from './pagination/pagination.component';
import { GovernateComponent } from './governate/governate.component';


@NgModule({
  declarations: [
    GovernateComponentComponent,
    PaginationComponent,
    GovernateComponent
  ],
  imports: [
    CommonModule,
    GovernateRoutingModule,
  ]
})
export class GovernateModule { }
