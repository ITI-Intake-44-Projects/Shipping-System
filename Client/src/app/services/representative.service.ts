import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class RepresentativeService {
  private apiURL = 'https://localhost:44389/api/Representatives'; // Replace with your API base URL

  constructor(private http: HttpClient) {}

  getRepresentatives(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiURL}`, {
      withCredentials: true,
    });
  }

  registerRepresentative(representative: any): Observable<any> {
    return this.http.post<any>(`${this.apiURL}`, representative, {
      withCredentials: true,
    });
  }

  deleteRepresentative(representativeId: string): Observable<any> {
    return this.http.delete<any>(`${this.apiURL}/${representativeId}`, {
      withCredentials: true,
    });
  }

  updateRepresentative(representativeId: string, representative: any): Observable<any> {
    return this.http.put<any>(`${this.apiURL}/${representativeId}`, representative, {
      withCredentials: true,
    });
  }
  
  getGovernorates(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiURL}/governorates`, {
      withCredentials: true,
    });
  }
  getBranches(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiURL}/branches`, {
      withCredentials: true,
    });
  }
}
