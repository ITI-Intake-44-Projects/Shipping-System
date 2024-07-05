import { CityService } from './../../city/city.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators,ReactiveFormsModule } from '@angular/forms';
import { OrderService } from '../order.service';
import { ActivatedRoute } from '@angular/router';
import { NgModule } from '@angular/core';
import { Order } from '../../../Models/Order';
import { GovernateServiceService } from '../../governate/governate-service.service';
import { City } from '../../../Models/City';
import { Governate } from '../../../Models/Governate';
import { OrderType, PaymentType } from '../../../Models/Enums';

@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styleUrl: './order-form.component.css'
})


export class OrderFormComponent implements OnInit {
  modalOpen: boolean = false;
  editFlag: boolean = false;
  orderForm: FormGroup;
  orderId: number | null = null;
  products: any[] = [];
  selectedTab: string = 'customer';

  orderTypes = OrderType 
  paymetTypes = PaymentType 


  cities : City[] = [];
  governates : Governate[] = [];


  constructor(
    private fb: FormBuilder,
    private orderService: OrderService,
    private route: ActivatedRoute,
    private cityService:CityService,
    private governateService:GovernateServiceService
  ) {
    this.orderForm = this.fb.group({
      id: [null],
      customerName: ['', Validators.required],
      customerPhone1: ['', Validators.required],
      customerPhone2: [''],
      customerEmail: ['', [Validators.email]],
      villageOrStreet: [''],
      villageDeliver: [false],
      orderCost: [null],
      totalWeight: [null],
      notes: [''],
      orderStatus: [null],
      orderType: [null],
      paymentType: [null],
      totalCost: [null],
      shippingCost: [null],
      orderDate: [null],
      branch_Id: [null],
      shipping_Id: [null],
      merchant_Id: [''],
      representative_Id: [''],
      governate_Id: [null],
      city_Id: [null],
      productOrders: this.fb.array([]) // Initialize the form array
    });
  }

  ngOnInit() {

    this.selectedTab ='customer';

    this.cityService.getAll().subscribe({
      next:(data:City[])=>{
          this.cities= data
      }
    })

    this.governateService.getAll().subscribe({
      next:(data:Governate[])=>{
          this.governates= data
      }
    })

    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.orderId = +id;
        this.loadOrder(this.orderId);
      }
    });

    this.getEnumKeys(this.orderTypes)
  }

  loadOrder(id: number) {
    this.orderService.getOrderById(id).subscribe((order: Order) => {
      this.orderForm.patchValue(order);
      this.products = order.productOrders || [];
    });
  }

  selectTab(tab: string) {
    this.selectedTab = tab;
  }

  openModal(product?: any) {
    if (product) {
      this.editFlag = true;
      // Set form values for editing the product
    } else {
      this.editFlag = false;
      // Reset form values for adding a new product
    }
    this.modalOpen = true;
  }

  closeModal() {
    this.modalOpen = false;
    this.editFlag = false;

  }

  addOrEditProduct() {
    if (this.editFlag) {
      // Update the existing product
    } else {
      // Add a new product
    }
    this.closeModal();
  }

  deleteProduct(id: number) {
    this.products = this.products.filter(p => p.id !== id);
  }

  handleSubmit() {
    if (this.orderForm.valid) {
      const orderData: Order = this.orderForm.value;
      console.log(this.orderForm)
      // if (this.orderId) {
      //   this.orderService.editItem(this.orderId, orderData).subscribe(() => {
      //     // Handle successful update
      //   });
      // } else {
      //   this.orderService.addItem(orderData).subscribe(() => {
      //     // Handle successful creation
      //   });
      // }
    }
  }

  getEnumKeys<T extends object>(enumType: T): (keyof T)[] {
    return Object.keys(enumType).filter(key => isNaN(Number(key as any))) as (keyof T)[];
  }
}