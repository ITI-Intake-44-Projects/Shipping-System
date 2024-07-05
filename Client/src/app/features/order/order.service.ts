import { Order } from './../../Models/Order';
import { Inject, Injectable } from '@angular/core';
import { ApiService } from '../../shared/services/api.service';
import { HttpClient,HttpParams } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ProductOrder } from '../../Models/ProductOrder';
import { OrderStatus,OrderType,PaymentType } from '../../Models/Enums';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class OrderService extends ApiService<Order> {

  constructor(http:HttpClient , @Inject('apiUrl') protected apiUrl:string )
  {
    super(http,environment.apiUrl+'order')
  }


  getOrders(pageNumber: number, pageSize: number): Observable<Order[]> {
    let params = new HttpParams();
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<Order[]>(this.apiUrl, { params });
  }

  getOrderById(id: number): Observable<Order> {
    return this.http.get<Order>(`${this.apiUrl}/${id}`);
  }

  // postOrder(order: Order): Observable<Order> {
  //   return this.http.post<Order>(this.apiUrl, order);
  // }

  // putOrder(id: number, order: Order): Observable<void> {
  //   return this.http.put<void>(`${this.apiUrl}/${id}`, order);
  // }

  // deleteOrder(id: number): Observable<void> {
  //   return this.http.delete<void>(`${this.apiUrl}/${id}`);
  // }

  getOrdersByCustomerName(name: string): Observable<Order[]> {
    let params = new HttpParams();
    params = params.append('name', name);
    return this.http.get<Order[]>(`${this.apiUrl}/customerName`, { params });
  }

  getMerchantOrders(merchantId: string): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}/merchantOrders/${merchantId}`);
  }

  getRepresentativeOrders(representativeId: string): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}/representativeOrders/${representativeId}`);
  }

  assignRepresentative(orderId: number, representativeId: string): Observable<void> {
    let params = new HttpParams();
    params = params.append('orderId', orderId.toString());
    params = params.append('representativeId', representativeId);
    return this.http.put<void>(`${this.apiUrl}/assignRepresentative`, {}, { params });
  }

  filterOrderByStatus(status: OrderStatus): Observable<Order[]> {
    let params = new HttpParams();
    params = params.append('status', status);
    return this.http.get<Order[]>(`${this.apiUrl}/filterByStatus`, { params });
  }

  filterOrderByStatusAndDate(status: OrderStatus, startDate: Date, endDate: Date): Observable<Order[]> {
    let params = new HttpParams();
    params = params.append('status', status);
    params = params.append('startDate', startDate.toISOString());
    params = params.append('endDate', endDate.toISOString());
    return this.http.get<Order[]>(`${this.apiUrl}/filterByStatusAndDate`, { params });
  }


}
