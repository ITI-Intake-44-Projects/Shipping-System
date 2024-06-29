import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PrivilegeService } from '../privilege.service';
import { PrivilegeDTO } from '../interfaces/privilege-dto';

@Component({
  selector: 'app-add-privilege',
  templateUrl: './add-privilege.component.html',
  styleUrls: ['./add-privilege.component.css']
})
export class AddPrivilegeComponent implements OnInit {
  addPrivilegeForm: FormGroup;

  constructor(private fb: FormBuilder, private privilegeService: PrivilegeService, private router: Router) {
    this.addPrivilegeForm = this.fb.group({
      name: ['', Validators.required]
    });
  }
  ngOnInit(): void {  }

  onSubmit(): void {
    if (this.addPrivilegeForm.valid) {
      const newPrivilege: PrivilegeDTO = this.addPrivilegeForm.value;
      this.privilegeService.addPrivilege(newPrivilege).subscribe({
        next: (response) => {
          console.log('Privilege added successfully', response);
          this.router.navigate(['/admin/privilege-list']);
        },
        error: (error) => {
          console.error('Error adding privilege', error);
        }
      });
    }
  }
}
