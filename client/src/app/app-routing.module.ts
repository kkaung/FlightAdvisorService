import { NgModule } from '@angular/core';

import { HomeComponent } from './components/home/home.component';
import { RouterModule, Routes } from '@angular/router';
import { TravelComponent } from './components/travel/travel.component';
import { AuthGuard } from './helpers';
import { AuthModule } from './components/auth/auth.module';
import { CityModule } from './components/city/city.module';

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'travel', component: TravelComponent, canActivate: [AuthGuard] },
  { path: '', loadChildren: () => AuthModule },
  { path: 'cities', loadChildren: () => CityModule },

  //   //   redirect to home
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
