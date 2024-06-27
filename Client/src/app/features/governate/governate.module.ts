import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GovernateComponentComponent } from './governate-component/governate-component.component';
import { GovernateRoutingModule } from './governate-routing.module';
import { GovernateComponent } from './governate/governate.component';
import { SharedModule } from '../../shared/modules/shared/shared.module';


@NgModule({
  declarations: [
    GovernateComponentComponent,
    GovernateComponent
  ],
  imports: [
    CommonModule,
    GovernateRoutingModule,
    SharedModule
  ],
  exports:[
  ]
})
export class GovernateModule { }
