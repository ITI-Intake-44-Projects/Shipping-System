import { AuthService } from './../../auth/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormsModule, ReactiveFormsModule ,FormBuilder,Validators} from '@angular/forms';
import { EmployeeService } from '../employee.service';
import { response } from 'express';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee } from '../../../Models/Employee';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent implements OnInit {
  employeeForm!: FormGroup;

  employeeId : string = ''

   

  constructor(private fb: FormBuilder, private employeeService: EmployeeService,private route :Router,private authService : AuthService,private activatedRoute:ActivatedRoute) {
    this.employeeForm = this.fb.group({
      id:['0'],
      name: ['', Validators.required],
      phone: ['', Validators.required],
      status:[false,Validators.required],
      userName:[''],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
      branch: [''],
      branchName:[''],
      role: [''],
    });
  
  }

  ngOnInit(): void {


   
  this.activatedRoute.params.subscribe({
      next:(params:any)=>{
        this.employeeId = params['id']
        if(this.employeeId.length !=0){
          this.employeeService.getById(params.id).subscribe({
            next:(data:Employee)=>{
              this.employeeForm.get('name')?.setValue(data.fullName);
              this.employeeForm.get('phone')?.setValue(data.phone);
              this.employeeForm.get('status')?.setValue(data.status);
              this.employeeForm.get('email')?.setValue(data.email);
              // this.employeeForm.get('password')?.setValue(data.password);
              this.employeeForm.get('branch')?.setValue(data.branchId)

            }
          })
        }
        else {
         
          this.employeeForm.get('name')?.setValue('');
          this.employeeForm.get('phone')?.setValue('');
          this.employeeForm.get('status')?.setValue('');
          this.employeeForm.get('email')?.setValue('');
          // this.employeeForm.get('password')?.setValue(data.password);
          this.employeeForm.get('branch')?.setValue('')
        }
       
      }
    })
    


   

    // this.authService.getUserDetails(localStorage.getItem('token')?.toString()).subscribe({
    //   next:(response:any)=>
    //     console.log(response)
    // })

   

  }

  onSubmit(): void {

    if (this.employeeForm.invalid) {

      console.log(this.employeeForm.errors)
      return
     
    }

    if(this.employeeId.length == 0 ){
      console.log("here")
      let employee : Employee = {
        id:"0",
        fullName : this.employeeForm.get('name')?.value,
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
    }

    else {

      let employee : Employee = {
        id:this.employeeId,
        fullName : this.employeeForm.get('name')?.value,
        userName : "null",
        email : this.employeeForm.get('email')?.value, 
        phone : this.employeeForm.get('phone')?.value,
        password : this.employeeForm.get('password')?.value,
        status : this.employeeForm.get('status')?.value,
        branchId : this.employeeForm.get('branch')?.value,
        roles : null,
        branchName:null
      }
      this.employeeService.editItem(this.employeeId,employee).subscribe({
        next:(response)=>{
          console.log(response)
        },
        error:(error)=>{
          console.log(error.error)
        }
      })

    }


  }
}
