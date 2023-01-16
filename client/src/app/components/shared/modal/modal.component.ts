import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
})
export class ModalComponent implements OnInit {
  @Input() title = 'Add Comment';
  @Input() class = '';

  constructor(public activeModal: NgbActiveModal) {}

  ngOnInit(): void {
    console.log(this.class);
  }
}
