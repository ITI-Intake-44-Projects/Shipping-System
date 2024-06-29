import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PrivilegeDTO } from './interfaces/privilege-dto';
import { PrivilegeResponseDTO } from './interfaces/privilege-response-dto';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PrivilegeService {
  private apiUrl = `${environment.apiUrl}privileges`;

  constructor(private http: HttpClient) {}

  getPrivileges(): Observable<PrivilegeResponseDTO[]> {
    return this.http.get<PrivilegeResponseDTO[]>(this.apiUrl);
  }

  addPrivilege(privilege: PrivilegeDTO): Observable<PrivilegeResponseDTO> {
    return this.http.post<PrivilegeResponseDTO>(this.apiUrl, privilege);
  }

  updatePrivilege(privilege: PrivilegeDTO): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${privilege.id}`, privilege);
  }

  deletePrivilege(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
