import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { environment } from '../environments/environment';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AuthModule } from './features/auth/auth.module';
import { SharedModule } from './shared/modules/shared.module';
import { AddEmployeeComponent } from './features/employee-module/add-employee/add-employee.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { EmployeeListComponent } from './features/employee-module/employee-list/employee-list.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { EditEmployeeComponent } from './features/employee-module/edit-employee/edit-employee.component';



@NgModule({
  declarations: [AppComponent, AddEmployeeComponent, EmployeeListComponent, EditEmployeeComponent],
  imports: [
    BrowserModule, AppRoutingModule, HttpClientModule, FormsModule, 
    AuthModule, SharedModule,    ReactiveFormsModule, NgbModule, // Add ReactiveFormsModule here
    NgxPaginationModule

  ],
  providers: [
    provideClientHydration(),
    { provide: 'apiUrl', useValue: environment.apiUrl }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
