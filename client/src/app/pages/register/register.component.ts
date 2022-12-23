import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService, AlertService } from 'src/app/services';

@Component({
  selector: 'app-signup',
  templateUrl: './register.component.html',
})
export class SignupComponent implements OnInit {
  form!: FormGroup;
  passwordShow = false;
  isLoading = false;
  isSubmitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private alertService: AlertService
  ) {}

  get f() {
    const controls = this.form.controls;
    return {
      firstName: controls['firstName'],
      lastName: controls['lastName'],
      username: controls['username'],
      password: controls['password'],
    };
  }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      firstName: ['string', Validators.required],
      lastName: ['string', Validators.required],
      username: ['string@gmail.com', [Validators.required, Validators.email]],
      password: ['string', Validators.required],
    });
  }

  onSubmit() {
    this.isSubmitted = true;

    // clear all alerts
    this.alertService.clear();

    this.isLoading = true;
    console.log(this.form.controls);

    if (!this.form.valid) return;

    const body = {
      firstName: this.f.firstName.value,
      lastName: this.f.lastName.value,
      username: this.f.lastName.value,
      password: this.f.password.value,
    };


    this.authService.register(body).subscribe({
      next: () => this.router.navigateByUrl('/login'),
      error: (res) => {
        const error = res.error.message;
        this.alertService.error(error);
      },
    });
  }

  togglePasswordShow() {
    this.passwordShow = !this.passwordShow;
  }
}
