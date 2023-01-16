import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService, AlertService } from 'src/app/services';

@Component({
  selector: 'app-signin',
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {
  form!: FormGroup;
  isLoading = false;
  passwordShow = true;
  rememberMe = false;
  isSubmitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private alertService: AlertService,
    private router: Router
  ) {}

  get f() {
    return {
      username: this.form.controls['username'],
      password: this.form.controls['password'],
    };
  }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      username: ['string@gmail.com', [Validators.required, Validators.email]],
      password: ['string', Validators.required],
    });
  }

  togglePasswordShow() {
    this.passwordShow = !this.passwordShow;
  }

  onSubmit() {
    this.isSubmitted = true;

    // clear all alerts
    this.alertService.clear();

    this.isLoading = true;

    if (this.form.invalid) return;

    this.authService
      .login({
        username: this.f.username.value,
        password: this.f.password.value,
      })
      .subscribe({
        next: () => {
          console.log('login');
          const returnUrl = '/';

          //  if successful login
          this.router.navigateByUrl(returnUrl);
        },
        error: (res) => {
          const error = res.error?.message;
          this.alertService.error(error);
          this.isLoading = false;
        },
      });
  }
}
