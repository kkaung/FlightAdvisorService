import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgIconsModule } from '@ng-icons/core';
import { AuthLayoutComponent } from './auth-layout/auth-layout.component';
import { ionMail, ionEye, ionEyeOff, ionLocation } from '@ng-icons/ionicons';
import { AuthRoutingRoutingModule } from './auth-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

@NgModule({
  declarations: [AuthLayoutComponent, LoginComponent, RegisterComponent],
  imports: [
    AuthRoutingRoutingModule,
    ReactiveFormsModule,
    CommonModule,
    NgIconsModule.withIcons({
      ionMail,
      ionEye,
      ionEyeOff,
      ionLocation,
    }),
  ],
})
export class AuthModule {
  constructor() {}
}
