import { City } from '../../Models/City';
import { Observable } from 'rxjs';
import { Inject, Injectable } from '@angular/core';
import { ApiService } from '../../shared/services/api.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class CityService extends ApiService<City>{

  constructor(http:HttpClient , @Inject('apiUrl') protected apiUrl:string ) 
  {
    super(http,environment.apiUrl+'City')
  }

  searchByName(name : string):Observable<City>{
    return this.http.get<City>(`${this.apiUrL}/${name}`)
  }

}
