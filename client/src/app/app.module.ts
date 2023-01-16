import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { TravelComponent } from './components/travel/travel.component';
import { HttpClientModule } from '@angular/common/http';
import { AlertComponent } from './components/shared/alert/alert.component';
import { AuthModule } from './components/auth/auth.module';
import { NgIconsModule } from '@ng-icons/core';
import { ionLogoGithub } from '@ng-icons/ionicons';
import { CityModule } from './components/city/city.module';
import { SharedModule } from './components/shared/shared.module';

@NgModule({
  declarations: [AppComponent, AlertComponent, HomeComponent, TravelComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule,
    AuthModule,
    CityModule,
    SharedModule,
    NgIconsModule.withIcons({ ionLogoGithub }),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
