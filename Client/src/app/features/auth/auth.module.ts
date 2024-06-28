import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';
import { ForgetPasswordComponent } from './forget-password/forget-password.component';
import { AuthRoutingModule } from './auth-routing.module';

@NgModule({
  declarations: [ LoginComponent, LogoutComponent, ForgetPasswordComponent ],
  imports: [ CommonModule, ReactiveFormsModule, FormsModule, AuthRoutingModule ],
  exports: [ LoginComponent, LogoutComponent, ForgetPasswordComponent]
})
export class AuthModule { }
