import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GovernateComponentComponent } from './governate-component/governate-component.component';
import { GovernateRoutingModule } from './governate-routing.module';
import { GovernateComponent } from './governate/governate.component';
import { SharedModule } from '../../shared/modules/shared/shared.module';
import { FormGroup, FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    GovernateComponentComponent,
    GovernateComponent
  ],
  imports: [
    CommonModule,
    GovernateRoutingModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule
  ],
  exports:[
  ]
})
export class GovernateModule { }
