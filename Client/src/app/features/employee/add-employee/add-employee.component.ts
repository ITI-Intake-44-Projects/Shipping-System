import { Component, OnInit } from '@angular/core';
import { FormGroup, FormsModule, ReactiveFormsModule ,FormBuilder,Validators} from '@angular/forms';
import { EmployeeService } from '../employee.service';
import { Employee } from '../../../Models/Employee';
import { response } from 'express';
import { Router } from '@angular/router';


@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent implements OnInit {
  employeeForm!: FormGroup;

  constructor(private fb: FormBuilder, private employeeService: EmployeeService,private route :Router) {
    
  }

  ngOnInit(): void {

    this.employeeForm = this.fb.group({
      name: ['', Validators.required],
      phone: ['', Validators.required],
      status:[false,Validators.required],
      userName:[''],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.required]],
      branch: [''],
      role: [''],
    });
  }

  onSubmit(): void {

    if (this.employeeForm.invalid) {

      console.log(this.employeeForm.errors)
      return
     
    }

    let employee : Employee = {
      fullName : this.employeeForm.get('name')?.value,
      // userName : this.employeeForm.get('name')?.value.split(' ')[0],
      userName : "null",
      email : this.employeeForm.get('email')?.value, 
      phone : this.employeeForm.get('phone')?.value,
      password : this.employeeForm.get('password')?.value,
      status : this.employeeForm.get('status')?.value,
      branchId : null,
      roles : null,
      branchName:null
    }

    this.employeeService.addItem(employee).subscribe({
      next:(response=>{
        console.log(response)
      })
      ,
      error:(error)=>{
        console.log(error)
      }
    })
    console.log(employee)

    // this.employeeService.addEmployee(this.employeeForm.value).subscribe(response => {
    //   console.log('Employee added successfully:', response);
    //   this.employeeForm.reset();
    // });
  }
}
