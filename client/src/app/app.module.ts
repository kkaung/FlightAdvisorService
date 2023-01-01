import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgIconsModule } from '@ng-icons/core';
import {
  ionMail,
  ionEye,
  ionEyeOff,
  ionLocation,
  ionLogoGithub,
} from '@ng-icons/ionicons';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SigninComponent } from './pages/login/login.component';
import { SignupComponent } from './pages/register/register.component';
import { HomeComponent } from './pages/home/home.component';
import { TravelComponent } from './pages/travel/travel.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AlertComponent } from './components/alert/alert.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';

@NgModule({
  declarations: [
    AppComponent,
    SigninComponent,
    SignupComponent,
    HomeComponent,
    TravelComponent,
    AlertComponent,
    DashboardComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgIconsModule.withIcons({
      ionMail,
      ionEye,
      ionEyeOff,
      ionLocation,
      ionLogoGithub,
    }),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
