import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  email: string = '';
  password: string = '';

  constructor(
    private readonly AS: AuthService,
    private readonly route: Router,
    private toast: ToastrService
  ) {}

  HandleLogin() {
    this.AS.Login(this.email, this.password).subscribe({
      next: (data: any) => {
        localStorage.setItem('token', data['token']);
        this.route.navigateByUrl('chat');
        this.toast.success('Welcome!!');
      },
      error: (err) => {
        this.toast.error(err.error);
        console.log(err);
      },
    });
  }
}
