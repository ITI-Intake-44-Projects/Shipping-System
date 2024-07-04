import { Governate } from './../../Models/Governate';
import { Observable } from 'rxjs';
import { Inject, Injectable } from '@angular/core';
import { ApiService } from '../../shared/services/api.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GovernateServiceService extends ApiService<Governate>{

  constructor(http:HttpClient , @Inject('apiUrl') protected apiUrl:string ) 
  {
    super(http,environment.apiUrl+'governate')
  }

  searchByName(name : string):Observable<Governate>{

    return this.http.get<Governate>(`${this.apiUrL}/${name}`)
  }


}