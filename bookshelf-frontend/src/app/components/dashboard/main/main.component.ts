import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Book } from 'src/app/models/book';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  @Input() Books:Book[] = []
  constructor(private route: Router) { }

  p: number = 1;

  ngOnInit(): void {
  }

  navigateToBook(book:Book){
    this.route.navigate(['book',book.ISBN]).catch((error)=>{ console.log("Failed to navigate to "+book.ISBN) })
  }

}
