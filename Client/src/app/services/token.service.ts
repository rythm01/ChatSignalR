import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
@Injectable({
  providedIn: 'root',
})
export class TokenService {
  constructor() {}

  GetSession() {
    let token = localStorage.getItem('token');

    if (token) {
      let data: any = jwtDecode(token);

      let exp = data['exp'];

      const currentTime = new Date().getTime() / 1000;

      if (currentTime > exp) {
        return false;
      }
      if (data) {
        return true;
      }
    }
    return false;
  }

  GetData() {
    let token = localStorage.getItem('token');

    if (token) {
      let data = jwtDecode(token);
      if (data) {
        return data;
      }
    }
    return [];
  }
}
