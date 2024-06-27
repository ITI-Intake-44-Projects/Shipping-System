// import { Component } from '@angular/core';
// import { Router } from '@angular/router';
// import { AuthService } from '../auth.service';
// import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// @Component({
//   selector: 'app-login',
//   templateUrl: './login.component.html',
//   styleUrl: './login.component.css'
// })
// export class LoginComponent {
//   loginForm: FormGroup = new FormGroup({});

//   constructor(private authService: AuthService, private router: Router, private fb: FormBuilder) {}
//   ngOnInit() {
//     this.loginForm = this.fb.group({
//       email: ['', [Validators.required, Validators.email]],
//       password: ['', [Validators.required, Validators.minLength(6)]]
//     });
//   }

//   get getEmail() {
//     return this.loginForm.get('email');
//   }

//   get getPassword() {
//     return this.loginForm.get('password');
//   }

//   validateFormFields(formGroup: FormGroup) {
//     Object.keys(formGroup.controls).forEach(field => {
//       const control = formGroup.get(field);
//       if (control instanceof FormGroup) {
//         this.validateFormFields(control);
//       } else {
//         control.markAsTouched({ onlySelf: true });
//       }
//     });
//   }

//   onSubmit() {
//     if (this.loginForm.valid) {
//       console.log('Login data:', this.loginForm.value);
//       console.log('email:', this.getEmail?.value, 'password: ', this.getPassword?.value);
//       //this.authService.login(this.getEmail?.value, this.getPassword?.value)
//       //   .subscribe(
//       //     response=>{
//       //       this.router.navigate(['/']);
//       //     },
//       //     error=>{
//       //       console.error('Login failed:', error);
//       //     }
//       // );
//     } else {
//       this.validateFormFields(this.loginForm);
//     }
//   }
// }


import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup = new FormGroup({});
  showPassword: boolean = false;
  
  constructor(private authService: AuthService, private router: Router, private fb: FormBuilder) {}

  ngOnInit() {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  validateFormFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if (control instanceof FormGroup) {
        this.validateFormFields(control);
      } else {
        control?.markAsTouched({ onlySelf: true });
      }
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      console.log('Login data:', this.loginForm.value);
      console.log('email:', this.email?.value, 'password: ', this.password?.value);
      /*
      this.authService.login(this.email?.value, this.password?.value)
        .subscribe(
          response => {
            this.router.navigate(['/']);
          },
          error => {
            console.error('Login failed:', error);
          }
        );
      */
    } else {
      this.validateFormFields(this.loginForm);
    }
  }
}
