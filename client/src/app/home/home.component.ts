import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { AccountService } from 'app/_services/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode=false;
  @Input() changePasswordMode:boolean = false;
  users:any;
  constructor(private http:HttpClient,public accountService:AccountService) { }

  ngOnInit(): void {
    this.getUsers();
  }

  registerToggle(){
    this.registerMode=!this.registerMode;
  }

  getUsers(){
    this.http.get('https://localhost:6001/api/Users/GetUsers').subscribe({
    next:response=>{
      this.users=response;},
    error:error=>{
      console.log(error);},
    })
  }

  cancelRegisterMode(event:boolean){
    this.registerMode=event;
  }

  cancelChangePasswordMode(event:boolean){
    this.changePasswordMode=event;
  }

}
