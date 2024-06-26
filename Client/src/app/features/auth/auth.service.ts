import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiURL = environment.apiUrl;
  constructor(private http:HttpClient) { }

  login(username: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.apiURL}login`, { username, password })
    .pipe(map(response=>{
      localStorage.setItem('token', response.token);
      return response;
    }));
  }

  logout(): void {
    localStorage.removeItem('token');
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  forgetPassword(email:string): Observable<any> {
    return this.http.post<any>(`${this.apiURL}forget-password`, { email })
  }

}
