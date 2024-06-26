import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  constructor(private authService: AuthService, private router: Router) {}
  onSubmit() {
    this.authService.login(this.username, this.password)
      .subscribe(
        response=>{
          this.router.navigate(['/']);
        },
        error=>{
          console.error('Login failed:', error);
        }
    );
  }
}
