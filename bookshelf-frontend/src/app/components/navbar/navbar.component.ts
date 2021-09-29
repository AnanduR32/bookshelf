import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/authentication/auth.service';
import { DatabaseService } from 'src/app/services/database.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  categories: string[] = []
  constructor(private db: DatabaseService, private route: Router, private auth: AuthService) { }

  navigate(loc: string) {
    this.route.navigate([loc])
  }

  isLoggedIn():boolean{
    return this.auth.isLoggedIn()
  }

  getUsername(){
    return this.auth.getUsername();
  }

  logout() {
    this.auth.logout()
  }

  ngOnInit(): void {
    this.db.getCategories().subscribe(
      (response) => {
        this.categories = response
      },
      (error) => {
        alert('Failed to fetch categories!')
      }
    )
  }

}
