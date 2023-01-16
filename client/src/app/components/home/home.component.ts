import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  constructor(private authService: AuthService, private router: Router) {}

  onLogout() {
    this.authService.logout();
    this.authService.user?.subscribe({
      next: () => {
        this.router.navigateByUrl('/login');
      },
    });
  }
}
