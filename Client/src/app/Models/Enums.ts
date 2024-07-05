export enum OrderStatus {
    New = 'New',
    Pending = 'Pending',
    DeliveredToRepresentitive = 'DeliveredToRepresentitive',
    DeliveredToCustomer = 'DeliveredToCustomer',
    UnReachable = 'UnReachable',
    Postponed = 'Postponed',
    DeliveredPartially = 'DeliveredPartially',
    CustomerCanceled = 'CustomerCanceled',
    RejectedWithPaying = 'RejectedWithPaying',
    RejectedWithPartialPaying = 'RejectedWithPartialPaying',
    RejectedFromEmployee = 'RejectedFromEmployee'
  }
  
  export enum OrderType {
    Normal = 'Normal',
    PickUp = 'PickUp'
  }
  
  export enum PaymentType {
    PayOnDeliver = 'PayOnDeliver',
    Deposit = 'Deposit',
    PackageForPackage = 'PackageForPackage'
  }