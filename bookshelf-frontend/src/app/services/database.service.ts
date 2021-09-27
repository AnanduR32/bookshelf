import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from '../models/book';

@Injectable({
  providedIn: 'root'
})
export class DatabaseService {

  constructor(private http: HttpClient ) { }

  private BASE_ENDPOINT:string = "http://localhost:11970/api/"

  getBooks(): Observable<any>{
    return this.http.get(this.BASE_ENDPOINT+'books')
  }
  getCategories(): Observable<any>{
    return this.http.get(this.BASE_ENDPOINT+'categories')
  }
  getBookByID(id:string): Observable<any>{
    return this.http.get(this.BASE_ENDPOINT+'books'+'/?isbn='+id)
  }

  updateBook(book:Book): Observable<any>{
    return this.http.put(this.BASE_ENDPOINT+'books',book);
  }
  deleteBook(id:string): Observable<any>{
    return this.http.delete(this.BASE_ENDPOINT+'books'+'/?isbn='+id);
  }
}
