import { Component } from '@angular/core';

@Component({
  selector: 'app-city-table',
  templateUrl: './city-table.component.html',
  styleUrl: './city-table.component.css'
})
export class CityTableComponent {

  modalOpen : boolean = false

  constructor() {

  }
  openModal() {
    this.modalOpen = true;
    
  }

  closeModal() {
    this.modalOpen = false;
  }
}
