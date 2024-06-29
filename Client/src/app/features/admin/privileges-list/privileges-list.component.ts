import { Component, OnInit } from '@angular/core';
import { PrivilegeService } from '../privilege.service';
import { PrivilegeResponseDTO } from '../interfaces/privilege-response-dto';

@Component({
  selector: 'app-privileges-list',
  templateUrl: './privileges-list.component.html',
  styleUrls: ['./privileges-list.component.css']
})
export class PrivilegesListComponent implements OnInit {
  privileges: PrivilegeResponseDTO[] = [];

  constructor(private privilegeService: PrivilegeService) {}

  ngOnInit(): void {
    this.loadPrivileges();
  }

  loadPrivileges(): void {
    this.privilegeService.getPrivileges().subscribe((data) => {
      this.privileges = data;
    });
  }

  onEdit(privilege: PrivilegeResponseDTO): void {
    // Redirect to edit page with privilege ID
  }

  onDelete(id: number): void {
    this.privilegeService.deletePrivilege(id).subscribe(() => {
      this.loadPrivileges();
    });
  }
}
