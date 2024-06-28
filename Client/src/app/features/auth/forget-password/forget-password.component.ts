import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import e from 'express';

@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrl: './forget-password.component.css'
})
export class ForgetPasswordComponent {
  email: string = '';

  constructor(private authService: AuthService) {}

  onSubmit() {
    const forgetPasswordCredentials = {
      email: this.email
    }
    this.authService.forgetPassword(forgetPasswordCredentials)
    .subscribe(
      response => {
        console.log('Password reset email sent:', response);
      }, 
      error => {
        console.error('Password reset failed:', error);
      }
    );
  }
}
