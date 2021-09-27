import { Component, OnInit } from '@angular/core';
import { DatabaseService } from 'src/app/services/database.service';

@Component({
  selector: 'app-side',
  templateUrl: './side.component.html',
  styleUrls: ['./side.component.css']
})
export class SideComponent implements OnInit {

  categories: string[] = []
  constructor(private db: DatabaseService) { }

  ngOnInit(): void {
    this.db.getCategories().subscribe(
      (response) =>{
        this.categories = response
      },
      (error) => {
        alert('Failed to fetch categories!')
      }
    )
  }

}
