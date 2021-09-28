import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class HttpHeaderInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const modifiedRequest = request.clone({
      headers: request.headers
      .set('content-type','application/json')
      .set('Access-Control-Allow-Origin','*'),
      body: JSON.parse(JSON.stringify(request.body))
    })
    return next.handle(request);
  }
}
