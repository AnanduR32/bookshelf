import { Component, OnInit } from '@angular/core';
import { Book } from 'src/app/models/book';
import { DatabaseService } from 'src/app/services/database.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(private db: DatabaseService) { }

  Books: Book[] = []
  ngOnInit(): void {
    this.db.getBooks().subscribe(
      (response) => {
        this.Books = response
      },
      (error) => {
        alert("Failed to fetch bookshelf repository!")
      })
  }

}
