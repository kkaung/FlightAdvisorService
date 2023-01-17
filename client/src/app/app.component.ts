import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { User } from './models';
import { AuthService } from './services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  travelForm?: FormGroup;
  user: User | null = null;

  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadUser();

    this.travelForm = this.formBuilder.group({
      travelingFrom: [''],
      goingTo: [''],
    });
  }

  logout() {
    this.authService.logout();

    this.router.navigate(['/login']);
  }

  private loadUser() {
    this.authService.getMe().subscribe({
      next: (user) => {
        this.user = user!;
      },
    });
  }
}
