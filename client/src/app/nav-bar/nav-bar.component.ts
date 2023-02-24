import { Component, OnInit } from '@angular/core';
import { AccountService } from 'app/_services/account.service';
import { UsersService } from 'app/_services/users.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  model:any={}
  loggedIn=false;
  userInformation:any;
  constructor(private accountService:AccountService,private usersService:UsersService) { }
  ngOnInit(): void {
    this.getCurrentUser();
  }


  getCurrentUser(){
    this.accountService.currentUser$.subscribe({
      next:user=>this.loggedIn=!!user,
      error:error=>console.log(error)
      
    })
  }

  login(){
    this.accountService.login(this.model).subscribe({
      next:response=>{
      this.loggedIn=true;
      this.userInformation=this.model;
    },
    error:error=>{
      console.log(error);
    }
  })
  }
  logout(){
    this.accountService.logout();
    this.loggedIn=false;
  }

}
