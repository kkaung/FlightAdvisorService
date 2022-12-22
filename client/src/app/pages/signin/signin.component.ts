import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
})
export class SigninComponent implements OnInit {
  form?: FormGroup;
  isLoading = false;
  passwordShow = true;
  rememberMe = false;

  constructor(private formBuilder: FormBuilder) {}

  get controls() {
    return this.form?.controls;
  }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      username: '',
      password: '',
    });
  }

  togglePasswordShow() {
    this.passwordShow = !this.passwordShow;
  }

  onSubmit() {
    this.isLoading = true;
    console.log(this.form?.invalid);
  }
}
