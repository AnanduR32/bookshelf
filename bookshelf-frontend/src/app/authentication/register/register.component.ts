import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  user: User
  agreementTerms: boolean = false;

  constructor(private auth: AuthService) {
    this.user = new User()
  }

  onSubmit(): void {
    this.auth.register(this.user).subscribe(
      (response) => {
        console.log("Attempting to register user...")
        this.auth.saveLoggedInData(this.user)
      },
      (error)=>{
        alert("Error registering user")
      },
      () => {
        alert("Successfully registered user!")
      }
    )
  }


  ngOnInit(): void {
  }

}
