import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

import { CityRoutingModule } from './city-routing.module';
import { CityLayoutComponent } from './city-layout/city-layout.component';
import { CitySearchComponent } from './city-search/city-search.component';
import { NgIconsModule } from '@ng-icons/core';
import { ionSearch, ionHappySharp } from '@ng-icons/ionicons';
import { ReactiveFormsModule } from '@angular/forms';
import { CityCommentsComponent } from './city-comments/city-comments.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    CityLayoutComponent,
    CitySearchComponent,
    CityCommentsComponent,
  ],
  imports: [
    NgbModule,
    ReactiveFormsModule,
    CommonModule,
    SharedModule,
    CityRoutingModule,
    NgIconsModule.withIcons({
      ionSearch,
      ionHappySharp,
    }),
  ],
  providers: [NgbActiveModal],
})
export class CityModule {}
