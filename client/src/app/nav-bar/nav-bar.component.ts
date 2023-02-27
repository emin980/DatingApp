import { Component, OnInit } from '@angular/core';
import { User } from 'app/_models/user';
import { AccountService } from 'app/_services/account.service';
import { UsersService } from 'app/_services/users.service';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  model:any={}

  constructor(public accountService:AccountService) { }
  ngOnInit(): void {
  }


  login(){
    this.accountService.login(this.model).subscribe({
      next:response=>{
    },
    error:error=>{
      console.log(error);
    }
  })
  }
  logout(){
    this.accountService.logout();
  }

}
