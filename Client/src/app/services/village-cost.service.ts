import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class VillageCostService {

  private apiUrl = 'https://localhost:5000/api/VillageCost'; // Replace with your actual API URL

  constructor(private http: HttpClient) { }

  addVillageCost(data: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, data)
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    console.error('Error occurred:', error);
    return throwError('Error adding village cost');
  }
  
}
