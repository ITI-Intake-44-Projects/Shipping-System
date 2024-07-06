import { Order } from './../../../Models/Order';
import { OrderStatus } from './../../../Models/Enums';
import { Component, OnInit } from '@angular/core';
import { OrderService } from '../order.service';
@Component({
  selector: 'app-all-orders',
  templateUrl: './all-orders.component.html',
  styleUrl: './all-orders.component.css'
})
export class AllOrdersComponent implements OnInit{

  orderStatus= OrderStatus 

  orders : Order[] = [] 
  constructor(
    private orderService:OrderService
  ) {
    // this.orderStatus = OrderStatus
    
  }
  ngOnInit(): void {
    this.orderService.getOrders(1,10).subscribe({
      next:(data:Order[])=>{
        this.orders = data
        console.log(data)
      }
    })

  }


  filterOrder(status:OrderStatus){

    

  }

  deleteOrder(id : number){

    this.orderService.deleteItem(id).subscribe({
      next:(data:any)=>{
        console.log(data)
      }
    })
  }
}
