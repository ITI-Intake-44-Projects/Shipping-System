import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CityRoutingModule } from './city-routing.module';
import { CityTableComponent } from './city-table/city-table.component';
import { CityComponent } from './city/city.component';
import { GovernateModule } from '../governate/governate.module';
import { SharedModule } from '../../shared/modules/shared/shared.module';


@NgModule({
  declarations: [
    CityTableComponent,
    CityComponent,

  ],
  imports: [
    CommonModule,
    CityRoutingModule,
    SharedModule
  ]
})
export class CityModule { }
