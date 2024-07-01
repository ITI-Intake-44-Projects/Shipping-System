import { error } from 'console';
import { EmployeeService } from '../employee.service';
import { Employee } from './../../../Models/Employee';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  employees : Employee[] = []

  p: number = 1; // current page

  constructor(private employeeService : EmployeeService) {


   }



  ngOnInit(): void {

    this.employeeService.getAll().subscribe({
      next:(data:Employee[])=>{
        console.log(data)
        this.employees= data
      },
      error:(error)=>{
        console.log(error)
      }
    })
  }

  toggleStatus(employee: any): void {
    employee.status = employee.status === 'Active' ? 'Inactive' : 'Active';
    // Optionally, make an API call to save the status change
  }
}
