import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) {
    this.user = new User()
    this.loadLoggedInData()
   }

  BASE_URL:string = "http://localhost:11970/api/auth" 
  private user: User

  login(user:User): Observable<any>{
    return this.http.get(this.BASE_URL+'?email='+user.Email+'&password='+user.Password)
  }

  register(user:User): Observable<any>{
    return this.http.post(this.BASE_URL, user)
  }

  saveLoggedInData(data: any) {
    let user = {
      'Name': data.user.Name,
      'Mobile': data.user.Mobile,
      'Email': data.user.Email,
      'Password': data.user.Password,
      'UserID': data.user.UserID
    }
    this.user.Name = user.Name
    this.user.Mobile = user.Mobile
    this.user.Email = user.Email
    this.user.Password = user.Password
    this.user.UserID = user.UserID
    localStorage.setItem('user', JSON.stringify(this.user))
  }

  loadLoggedInData() {
    let user = JSON.parse(localStorage.getItem('user')!)
    if(user == null){
      console.log('User not logged in')
    }
    else {
      this.user = user
      console.log(`${this.user.Name} is logged in`)
    }
  }
}
