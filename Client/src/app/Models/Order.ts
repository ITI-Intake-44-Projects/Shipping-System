import { OrderStatus, OrderType, PaymentType } from "./Enums";
import { ProductOrder } from "./ProductOrder";
export interface Order {
    id: number;
    customerName?: string;
    customerPhone1?: string;
    customerPhone2?: string;
    customerEmail?: string;
    villageOrStreet?: string;
    villageDeliver?: boolean;
    orderCost?: number;
    totalWeight?: number;
    notes?: string;
    orderStatus?: OrderStatus;
    orderType: OrderType;
    paymentType: PaymentType;
    totalCost?: number;
    shippingCost?: number;
    orderDate?: Date;
    branchId?: number;
    shippingId?: number;
    merchantId?: string;
    merchantName?:string;
    representativeId?: string;
    governateId?: number;
    governateName:string,
    cityId?: number;
    cityName:string ;
    productOrders: ProductOrder[];
}