import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient, private route: Router) {
    this.user = new User()
    this.loadLoggedInData()
  }

  BASE_URL: string = "http://localhost:11970/auth"
  private user: User

  login(user: User): Observable<any> {
    return this.http.post(this.BASE_URL + '/login', user)
  }

  register(user: User): Observable<any> {
    return this.http.post(this.BASE_URL + '/register', user)
  }

  saveLoggedInData(data: User) {
    this.user = data
    localStorage.setItem('user', JSON.stringify(this.user))
  }

  loadLoggedInData() {
    let user = JSON.parse(localStorage.getItem('user')!)
    if (user == null) {
      console.log('User not logged in')
    }
    else {
      this.user = user
      console.log(`${this.user.Email} is logged in`)
    }
  }
  isLoggedIn(): boolean {
    if (this.user.Email == null) {
      return false

    }
    else {
      return true
    }
  }
  getUsername(): string {
    return this.user.Name!
  }

  logout() {
    localStorage.removeItem('user')
    this.user = new User()
    this.route.navigate(['login']).catch((error) => { console.log('Failed to navigate to home') })
  }
}
