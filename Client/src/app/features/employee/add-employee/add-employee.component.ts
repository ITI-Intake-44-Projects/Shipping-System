import { AuthService } from './../../auth/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormsModule, ReactiveFormsModule ,FormBuilder,Validators} from '@angular/forms';
import { EmployeeService } from '../employee.service';
import { response } from 'express';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee } from '../../../Models/Employee';
import { BranchService } from '../../../services/branch.service';
import { GroupService } from '../../admin/Services/group.service';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent implements OnInit {
  employeeForm!: FormGroup;

  employeeId : string = ''

  editFlag : boolean = false ;

  submitted = false;

  serverErrors: { [key: string]: string[] } = {};

  branches: any[] = [];
  roles: any[] = [];

  constructor(private fb: FormBuilder, private employeeService: EmployeeService,private route :Router,private authService : AuthService,private activatedRoute:ActivatedRoute, private branchService: BranchService, private groupService: GroupService) {
    this.employeeForm = this.fb.group({
      id:['0'],
      name: ['', Validators.required],
      phone: ['', Validators.required],
      status:[false,Validators.required],
      userName:['',Validators.required],
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
        console.log(params['id']);
        if(this.employeeId != undefined){
          this.employeeService.getById(params.id).subscribe({
            next:(data:Employee)=>{
              this.employeeForm.get('name')?.setValue(data.fullName);
              this.employeeForm.get('userName')?.setValue(data.userName);
              this.employeeForm.get('phone')?.setValue(data.phone);
              this.employeeForm.get('status')?.setValue(data.status);
              this.employeeForm.get('email')?.setValue(data.email);
              // this.employeeForm.get('password')?.setValue(data.password);
              this.employeeForm.get('branch')?.setValue(data.branchId)
              this.editFlag = true
            }
          })
        }
        else {
          this.employeeForm.get('name')?.setValue('');
          this.employeeForm.get('userName')?.setValue('');
          this.employeeForm.get('phone')?.setValue('');
          this.employeeForm.get('status')?.setValue(false);
          this.employeeForm.get('email')?.setValue('');
          // this.employeeForm.get('password')?.setValue(data.password);
          this.employeeForm.get('branch')?.setValue('')
        }

      }
    });

    this.getAllBranches();
    this.getAllGroups();
  }

  onSubmit(): void {
    this.submitted = true

    if (this.employeeForm.invalid) {
      console.log(this.employeeForm.errors)
      return;
    }

    if(this.employeeId == undefined ){
      let employee : Employee = {
        id:"0",
        fullName : this.employeeForm.get('name')?.value,
        userName : this.employeeForm.get('userName')?.value,
        email : this.employeeForm.get('email')?.value,
        phone : this.employeeForm.get('phone')?.value,
        password : this.employeeForm.get('password')?.value,
        status : this.employeeForm.get('status')?.value,
        branchId : this.employeeForm.get('branch')?.value,
        roles : this.employeeForm.get('role')?.value,
        branchName: null,
        isDeleted:false
      }

      this.employeeService.addItem(employee).subscribe({
        next:(response=>{
          console.log(response)
          this.route.navigate(['/employee/all'])
        }),
        error:(responseError)=>{
          console.log("error");
          responseError.error.errors.forEach((element:any) => {
            if(element['code'] == "DuplicateEmail"){
              this.employeeForm.get('email')?.setErrors({ 'duplicateEmail': true });
            }

            if(element['code'] == "DuplicateUserName"){

              this.employeeForm.get('userName')?.setErrors({ 'duplicateUserName': true });

            }
            console.log(element);
          })

          // Object.keys(responseError.error)
        }
      })
    }

    else {

      let employee : Employee = {
        id:this.employeeId,
        fullName : this.employeeForm.get('name')?.value,
        userName : this.employeeForm.get('userName')?.value,
        email : this.employeeForm.get('email')?.value,
        phone : this.employeeForm.get('phone')?.value,
        password : this.employeeForm.get('password')?.value,
        status : this.employeeForm.get('status')?.value,
        branchId : this.employeeForm.get('branch')?.value,
        roles : null,
        branchName:null,
        isDeleted:false
      }
      this.employeeService.editItem(this.employeeId,employee).subscribe({
        next:(response)=>{
          console.log(response)
          this.route.navigate(['/employee/all'])
        },
        error:(error)=>{
          console.log(error.error)
        }
      })

    }
  }

  getAllBranches(){
    return this.branchService.getBranches().subscribe({
      next: (data)=>{
        this.branches = data;
        console.log(`all branches ${JSON.stringify(this.branches)}`);
      },
      error: (err)=>{
        console.log(`get all branches failed ${JSON.stringify(err)}`);
      }
    })
  }

  getAllGroups(){
    return this.groupService.getAllGroups(1, 100).subscribe({
      next: (data)=>{
        this.roles = data;
        console.log(`all roles ${JSON.stringify(this.roles)}`);
      },
      error: (err)=>{
        console.log(`get all roles failed ${JSON.stringify(err)}`);
      }
    });
  }

}
