import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/authentication/auth.service';
import { Book } from 'src/app/models/book';
import { DatabaseService } from 'src/app/services/database.service';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent implements OnInit {

  bookID: string = ''
  book: Book = new Book()
  newBook: Book = new Book()
  constructor(
    private activatedRoute: ActivatedRoute, 
    private db: DatabaseService, 
    private route: Router,
    private auth: AuthService
    ) {
    this.bookID = this.activatedRoute.snapshot.paramMap.get('id')!.toString()
  }

  isLoggedIn(){
    return this.auth.isLoggedIn()
  }
  editModeToggle: boolean = false;

  toggleEditMode(){
    this.editModeToggle = !this.editModeToggle
    if(this.editModeToggle === true){
      this.newBook = new Book().makeBook(this.book);
    }
    else{
      this.newBook = new Book()
    } 
  }
  updateBookDetails(){
    this.newBook.OldPrice = this.book.Price;
    this.db.updateBook(this.newBook).subscribe(
      (response)=>{
        console.log('Updating record')
      },
      (error)=>{
        alert("Failed to update record")
      },
      ()=>{
        alert("Successfully updated record")
        this.route.navigate([''])
      }
    )
  }

  deleteBook(){
    this.db.deleteBook(this.book.ISBN!).subscribe(
      (response)=>{
        console.log('Deleting record')
      },
      (error)=>{
        alert("Failed to delete record")
      },
      ()=>{
        alert("Successfully deleted record")
      }
    )
  }

  ngOnInit(): void {
    this.db.getBookByID(this.bookID).subscribe(
      (response) => {
        this.book = response
      },
      (error) => {
        alert('Failed to fetch details for book with ISBN: ' + this.bookID)
      })
  }

}
