import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotFoundComponent } from '../../components/not-found/not-found.component';
import { PaginationComponent } from '../../components/pagination/pagination.component';

@NgModule({
  declarations: [ NotFoundComponent, PaginationComponent ],
  imports: [CommonModule],
  exports: [ NotFoundComponent, PaginationComponent ]
})
export class SharedModule { }
