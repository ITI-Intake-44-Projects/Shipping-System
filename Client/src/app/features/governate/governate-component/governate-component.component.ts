import { Component, ElementRef, ViewChild } from '@angular/core';
import {PageEvent, MatPaginatorModule} from '@angular/material/paginator';

@Component({
  selector: 'app-governate-component',
  templateUrl: './governate-component.component.html',
  styleUrl: './governate-component.component.css'
})
export class GovernateComponentComponent {

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
