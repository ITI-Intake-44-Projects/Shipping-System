import { Component, OnInit } from '@angular/core';
import { LoggerService } from '../../services/logger.service';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  constructor(private logger : LoggerService){}
  ngOnInit(): void {
    this.logger.log('navbar on init called');
  }
  isLoggedIn(){

  }
  getUsername(){

  }
  logout(){

  }
}
