import { Component, OnInit } from '@angular/core';
import { AlertService } from './services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  title = 'client';

  constructor() {}

  ngOnInit(): void {}
}
