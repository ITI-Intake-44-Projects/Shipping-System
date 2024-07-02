import { AuthService } from './../../auth/auth.service';
import { error } from 'console';
import { EmployeeService } from '../employee.service';
import { Component, OnInit } from '@angular/core';
import { Employee } from '../../../Models/Employee';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  employees : Employee[]  = []

  p: number = 1; // current page

  userId : string = '' 
  constructor(private employeeService : EmployeeService,private authService : AuthService) {


  }


  ngOnInit(): void {

    this.employeeService.getAll().subscribe({
      next:(data:Employee[] )=>{
        console.log(data)
        this.employees= data
      },
      error:(error)=>{
        console.log(error)
      }
    })

    this.authService.getUserDetails().subscribe({
      next:(data:any)=>{
        console.log(data)
        this.userId = data.id
        console.log(this.userId)
      }
    })
  }

  toggleStatus(employee: any): void {
    employee.status = employee.status === 'Active' ? 'Inactive' : 'Active';
    // Optionally, make an API call to save the status change
  }
}
