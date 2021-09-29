import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user: User

  constructor(private auth: AuthService, private router: Router) {
    this.user = new User()
  }

  onSubmit(): void {
    this.auth.login(this.user).subscribe(
      (response) => {
        this.router.navigate(['']).catch((error) => { console.log('Failed to navigate to home') })
        this.auth.saveLoggedInData(response)
      },
      (error) => {
        alert("User authentication failed!")
      },
      ()=>{
        alert("Successfully logged in!")
      })
  }

  goToRegister() {
    this.router.navigate(['register'])
  }

  ngOnInit(): void {
  }

}
