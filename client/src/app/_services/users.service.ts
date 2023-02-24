import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  baseUrl='https://localhost:6001/api/';
  constructor(private http:HttpClient) { }


}
