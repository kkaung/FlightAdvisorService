import { NgModule } from '@angular/core';

import { HomeComponent } from './pages/home/home.component';
import { RouterModule, Routes } from '@angular/router';
import { SignupComponent } from './pages/register/register.component';
import { SigninComponent } from './pages/login/login.component';
import { TravelComponent } from './pages/travel/travel.component';
import { AuthGuard } from './helpers';

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'travel', component: TravelComponent, canActivate: [AuthGuard] },
  { path: 'register', component: SignupComponent },
  { path: 'login', component: SigninComponent },
  //   redirect to home
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
