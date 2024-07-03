import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class WeightService {
  private apiUrl = 'https://localhost:5000/api/WeightOptions'; // Replace with your actual API URL

  constructor(private http: HttpClient) {}

  addWeightSettings(data: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, data).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse) {
    console.error('حدث خطأ:', error);
    return throwError('خطأ في إضافة إعدادات الوزن');
  }
}
