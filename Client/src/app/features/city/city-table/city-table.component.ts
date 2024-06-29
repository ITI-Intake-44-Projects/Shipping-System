import { City } from '../../../Models/City';
import { Component, ElementRef, OnInit, ViewChild,Input } from '@angular/core';
import {PageEvent, MatPaginatorModule} from '@angular/material/paginator';
import { FormGroup, FormsModule, ReactiveFormsModule ,FormBuilder,Validators} from '@angular/forms';
import { Router } from '@angular/router';
import { CityService } from '../city.service';
@Component({
  selector: 'app-city-table',
  templateUrl: './city-table.component.html',
  styleUrl: './city-table.component.css'
})
export class CityTableComponent implements OnInit {

  modalOpen : boolean = false

  cities : City[] | null = null 

  cityForm!:FormGroup

  editFlag: boolean = false

  cityName : string = ''

  constructor(private cityService : CityService,private formBuilder:FormBuilder,private router:Router) {

  }
  ngOnInit(): void {

    this.cityService.getAll().subscribe({
      next:(data:City[])=>{
        console.log(data)
        this.cities = data 
      }
    })

    this.cityForm = this.formBuilder.group({
      id : [Number] ,
      name : ['',Validators.required],
      normalCost:[Number,Validators.required],
      pickupCost :[Number,Validators.required],
      governate_Id :[Number]
    })
  }

  cityHandler(){

    if(this.editFlag){
      
      console.log("clicked")
      let city : City ={
        id : this.cityForm.get('id')?.value ,
        name : this.cityForm.get('name')?.value  ,
        normalCost : this.cityForm.get('normalCost')?.value,
        pickupCost : this.cityForm.get('pickupCost')?.value,
        governate_Id : this.cityForm.get('governate_Id')?.value  
      }

      console.log("edited ",city.id , city)
      this.cityService.editItem(city.id , city).subscribe({
        next:(data:any)=>{
          const currentUrl = this.router.url;
          this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
            this.router.navigate([currentUrl]);
          });
        }
        ,
        error:(error)=>{

          console.log(error)
        }
      })
    }
    else {
      this.addCity()
    }
  }

  search(){

    console.log(this.cityName)

    this.cityService.searchByName(this.cityName).subscribe({
      next:(data:City)=>{
        this.cities = []
        this.cities.push(data)
      }
      ,
      error:(error)=>{
        const currentUrl = this.router.url;
        this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
          this.router.navigate([currentUrl]);
        });
      }
    })
  }

  addCity() {

    
    let city : City ={
      id : 0,
      name : '',
      normalCost:0,
      pickupCost :0,
      governate_Id : 0 
    };

    if (this.cityForm.invalid){
      return 
    }

    city = this.cityForm.value
    
    this.cityService.addItem(city).subscribe({
      next:(data:any)=>{
        const currentUrl = this.router.url;
        this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
          this.router.navigate([currentUrl]);
        });
      }
    })
  }


  editCity(city:City){

    this.editFlag = true;
    this.openModal()
    this.cityForm = this.formBuilder.group({
      id : [city.id] ,
      name : [city.name,Validators.required],
      normalCost:[city.normalCost,Validators.required],
      pickupCost :[city.pickupCost,Validators.required],
      governate_Id :[city.governate_Id]
    })

  }

 deleteCity(cityId : number){
    this.cityService.deleteItem(cityId).subscribe({
      next:(data)=>{
        console.log(data)
        const currentUrl = this.router.url;
        this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
          this.router.navigate([currentUrl]);
        });
      }
      ,
      error:(error)=>{
        console.log(error)
      }
    })
 }

  openModal() {
    this.modalOpen = true;
    
  }

  closeModal() {
    this.modalOpen = false;
  }
}
