import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CityCommentsComponent } from './city-comments/city-comments.component';
import { CityLayoutComponent } from './city-layout/city-layout.component';
import { CitySearchComponent } from './city-search/city-search.component';

const routes: Routes = [
  {
    path: '',
    component: CityLayoutComponent,
    children: [
      { path: 'search', component: CitySearchComponent },
      { path: 'comments', component: CityCommentsComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CityRoutingModule {}
