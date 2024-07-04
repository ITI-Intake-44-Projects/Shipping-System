import { LoginDTO } from './interfaces/login-dto';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { ResponseDTO } from './interfaces/response-dto';
import { ForgetPasswordDTO } from './interfaces/forget-password-dto';
import { Router } from '@angular/router';
import { ResetPasswordDTO } from './interfaces/reset-password-dto';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiURL = environment.apiUrl;
  private httpOptions = {
    headers: new HttpHeaders({
      'Accept': 'application/json',
      'Content-Type': 'application/json',
    })
  };

  constructor(private http:HttpClient, private router: Router) { }

  login(loginCredentials:LoginDTO): Observable<ResponseDTO> {
    return this.http.post<ResponseDTO>(`${this.apiURL}Account/Login`, loginCredentials, this.httpOptions).pipe(map(response=>{
      localStorage.setItem('token', response.token);
      localStorage.setItem('role', response.role);
      return response;
    }));
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    this.router.navigate(['/login'])
  }

  getRole(): string | null {
    if(this.isLoggedIn())
      return localStorage.getItem('role');
    return "";
  }

  isLoggedIn(): boolean {
    // if (typeof window !== 'undefined' && typeof localStorage !== 'undefined') {
    //   const token = localStorage.getItem('token');
    //   return token != null;
    // }
    // return false;
    return !!localStorage.getItem('token');
  }

  forgetPassword(credentials:ForgetPasswordDTO): Observable<ResponseDTO> {
    return this.http.post<ResponseDTO>(`${this.apiURL}Account/ForgetPassword`, { credentials },  this.httpOptions);
  }

  resetPassword(data: ResetPasswordDTO): Observable<ResponseDTO> {
    return this.http.post<ResponseDTO>(`${this.apiURL}/Account/resetPassword`, data);
  }
}
