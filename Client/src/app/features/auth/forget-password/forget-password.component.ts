import { Component } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrl: './forget-password.component.css'
})
export class ForgetPasswordComponent {
  email: string = '';

  constructor(private authService: AuthService) {}

  onSubmit() {
    this.authService.forgetPassword(this.email)
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
