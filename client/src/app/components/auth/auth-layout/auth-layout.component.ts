import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { AuthService } from 'src/app/services';

@Component({
  selector: 'app-auth-layout',
  templateUrl: './auth-layout.component.html',
})
export class AuthLayoutComponent {
  constructor(private router: Router, private authService: AuthService) {
    if (this.authService.user) this.router.navigate(['/']);
  }
}
