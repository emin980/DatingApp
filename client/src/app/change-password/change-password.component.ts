import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from 'app/_services/account.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  model:any={}
  @Output() cancelChangePassword=new EventEmitter();
  constructor(private accountService:AccountService) { }

  ngOnInit(): void {
  }

  changePassword(){
    this.accountService.changePassword(this.model).subscribe({
      next:response=>{
        console.log(response);
      },
      error:error=>{
        console.log(error);
      }
    })
  }

  cancel(){
    this.cancelChangePassword.emit(false);
  }
}
