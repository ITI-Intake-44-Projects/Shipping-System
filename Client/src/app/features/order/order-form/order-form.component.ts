import { Component } from '@angular/core';

@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styleUrl: './order-form.component.css'
})
export class OrderFormComponent {

  selectedTab: string = 'description';

  constructor() {


  }

  ngOnInit(): void {

    // this.activateRoute.params.subscribe({
    //   next:(params)=>{
    //     this.productId = params['id']
    //   }
    // })

  }

  selectTab(tab: string): void {
    this.selectedTab = tab;
  }

  onSubmit(event : Event){


  }

  
 
}

