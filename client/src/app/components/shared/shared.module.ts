import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgIconsModule } from '@ng-icons/core';
import { ionClose } from '@ng-icons/ionicons';

@NgModule({
  declarations: [],
  imports: [CommonModule, NgbModule, NgIconsModule.withIcons({ ionClose })],
  exports: [],
})
export class SharedModule {}
