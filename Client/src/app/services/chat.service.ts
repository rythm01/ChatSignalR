import { Injectable } from '@angular/core';
import { User } from '../Models/User';
import { HttpClient } from '@angular/common/http';
import { BaseUrl } from '../config/environment';
import { TokenService } from './token.service';
import { catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  users: Array<User> = [];

  constructor(private http: HttpClient, private TS: TokenService) {
    let data: any = TS.GetData();

    let senderId =
      data[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
      ];

    http
      .get<User[]>(BaseUrl + 'api/User/' + senderId)
      .pipe(catchError((err) => throwError(() => err)))
      .subscribe((data) => (this.users = data));
  }
}
