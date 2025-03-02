import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { ChatScreenComponent } from './chat-screen/chat-screen.component';
import { authGuard } from './guard/auth.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'chat',
    canActivate: [authGuard],
    component: ChatScreenComponent,
  },
];
