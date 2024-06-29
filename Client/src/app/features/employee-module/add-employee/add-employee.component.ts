import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EmployeeService } from '../employee.service';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent implements OnInit {
  addEmployeeForm: FormGroup;

  constructor(private fb: FormBuilder, private employeeService: EmployeeService) {
    this.addEmployeeForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      branch: ['', Validators.required],
      authorizations: [''],
      status: ['']
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.addEmployeeForm.valid) {
      this.employeeService.addEmployee(this.addEmployeeForm.value).subscribe(response => {
        console.log('Employee added successfully:', response);
        this.addEmployeeForm.reset();
      });
    }
  }
}
