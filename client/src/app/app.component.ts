import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'The Dating App';
  users:any;
  constructor(private http: HttpClient)
  {

  }
  ngOnInit() {
    this.getUsers();
  }

  getUsers(){
    this.http.get('https://localhost:6001/api/Users/GetUsers').subscribe({
    next:response=>{
      this.users=response;},
    error:error=>{
      console.log(error);},
    })
  }
}
