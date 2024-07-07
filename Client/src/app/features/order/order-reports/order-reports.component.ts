import { Component, OnInit } from '@angular/core';
import { OrderService } from '../order.service';
import { OrderStatus } from '../../../Models/Enums';
import { Order } from '../../../Models/Order';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-order-reports',
  templateUrl: './order-reports.component.html',
  styleUrls: ['./order-reports.component.css']
})

export class OrderReportsComponent implements OnInit {
  orders: Order[] = [];
  selectedStatus: OrderStatus = OrderStatus.New; // def choice
  startDate: Date = new Date();
  endDate: Date = new Date();
  displayedColumns: string[] = ['index', 'id', 'orderStatus', 'merchantId', 'customerName', 'customerPhone1', 'governateId', 'cityId', 'orderCost', 'totalCost', 'shippingCost', 'orderDate'];
  totalOrders: number = 0;
  itemsPerPage: number = 10;
  currentPage: number = 1;

  statuses: OrderStatus[] = [
    OrderStatus.New,
    OrderStatus.Pending,
    OrderStatus.DeliveredToRepresentitive,
    OrderStatus.DeliveredToCustomer,
    OrderStatus.UnReachable,
    OrderStatus.Postponed,
    OrderStatus.DeliveredPartially,
    OrderStatus.CustomerCanceled,
    OrderStatus.RejectedWithPaying,
    OrderStatus.RejectedWithPartialPaying,
    OrderStatus.RejectedFromEmployee
  ];

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders(): void {
    this.orderService.getOrders(this.currentPage, this.itemsPerPage).subscribe((data) => {
      this.orders = data;
      this.totalOrders = data.length;
    });
  }

  filterOrders(): void {
    this.orderService.filterOrderByStatusAndDate(this.selectedStatus, this.startDate, this.endDate).subscribe((data) => {
      this.orders = data;
    });
  }

  onPageChange(event: PageEvent): void {
    this.currentPage = event.pageIndex + 1;
    this.itemsPerPage = event.pageSize;
    this.getOrders();
  }
}
