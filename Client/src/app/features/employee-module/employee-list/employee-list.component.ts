import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {
  employees = [
    // mock data or fetched data
    { id: 1, name: 'John Doe', email: 'john@example.com', phone: '1234567890', branch: 'Branch 1', authorization: 'Admin', status: 'Active' },
    { id: 2, name: 'Jane Doe', email: 'jane@example.com', phone: '0987654321', branch: 'Branch 2', authorization: 'User', status: 'Inactive' },
    // add more employees
  ];
  p: number = 1; // current page

  constructor() { }

  ngOnInit(): void {
    // fetch your data here if not using mock data
  }

  toggleStatus(employee: any): void {
    employee.status = employee.status === 'Active' ? 'Inactive' : 'Active';
    // Optionally, make an API call to save the status change
  }
}
