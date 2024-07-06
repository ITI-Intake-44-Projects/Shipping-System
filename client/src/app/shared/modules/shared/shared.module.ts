import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
//import { NavbarComponent } from '../../components/navbar/navbar.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { NotFoundComponent } from '../../components/not-found/not-found.component';
import { PaginationComponent } from '../../components/pagination/pagination.component';
@NgModule({
  declarations: [FooterComponent, NotFoundComponent, PaginationComponent],
  imports: [CommonModule],
  exports: [ FooterComponent,PaginationComponent]
})
export class SharedModule { }
