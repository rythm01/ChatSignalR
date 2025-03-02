import { Injectable } from '@angular/core';
import { BaseUrl } from '../config/environment';
import { HttpClient } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient, private route: Router) {}

  Login(email: string, password: string) {
    return this.http
      .post(BaseUrl + 'api/Auth/login', {
        email: email,
        password: password,
      })
      .pipe(catchError((err) => throwError(() => err)));
  }

  Logout() {
    localStorage.removeItem('token');
    this.route.navigate(['']);
  }
}
