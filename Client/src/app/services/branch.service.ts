import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class BranchService {
  private apiURL = 'https://localhost:44389/api/Branches';

  constructor(private http: HttpClient) {}

  getBranches(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiURL}`, {
      withCredentials: true,
    });
  }

  createBranch(representative: any): Observable<any> {
    return this.http.post<any>(`${this.apiURL}`, representative, {
      withCredentials: true,
    });
  }

  deleteBranch(representativeId: string): Observable<any> {
    return this.http.delete<any>(`${this.apiURL}/${representativeId}`, {
      withCredentials: true,
    });
  }

  updateBranch(representativeId: string, representative: any): Observable<any> {
    return this.http.put<any>(`${this.apiURL}/${representativeId}`, representative, {
      withCredentials: true,
    });
  }

}
