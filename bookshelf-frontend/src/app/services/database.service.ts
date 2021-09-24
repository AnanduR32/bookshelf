import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from '../models/book';

@Injectable({
  providedIn: 'root'
})
export class DatabaseService {

  constructor(private http: HttpClient ) { }

  private BASE_ENDPOINT:string = "http://localhost:11970/api/books"

  getBooks(): Observable<any>{
    return this.http.get(this.BASE_ENDPOINT)
  }
}
