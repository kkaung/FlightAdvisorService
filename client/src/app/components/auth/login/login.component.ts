import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
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
    private route: ActivatedRoute,
    private alertService: AlertService,
    private router: Router
  ) {
    this.router.navigate(['/login']);
  }

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
          // get return url from query parameters or default to home page
          const returnUrl: string =
            this.route.snapshot.queryParams['returnUrl'] || '/';

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
