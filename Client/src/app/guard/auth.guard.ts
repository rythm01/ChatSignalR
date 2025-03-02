import { inject } from '@angular/core';
import { TokenService } from '../services/token.service';
import { AuthService } from '../services/auth.service';

export const authGuard = () => {
  let ts = inject(TokenService);

  let AS = inject(AuthService);

  let data = ts.GetSession();

  if (data) {
    return true;
  }
  AS.Logout();
  return false;
};
