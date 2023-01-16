import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalComponent } from './modal/modal.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [ModalComponent],
  imports: [CommonModule, NgbModule],
  exports: [ModalComponent],
})
export class SharedModule {}
