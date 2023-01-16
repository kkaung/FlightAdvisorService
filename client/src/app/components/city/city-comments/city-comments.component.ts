import { Component, OnDestroy, OnInit } from '@angular/core';
import { City } from 'src/app/models';
import { DatePipe } from '@angular/common';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-city-comments',
  templateUrl: './city-comments.component.html',
  providers: [DatePipe],
})
export class CityCommentsComponent implements OnInit, OnDestroy {
  city: City = new City('', '');

  constructor(private sessionService: SessionService) {
    console.log(this.city.comments);
  }

  ngOnInit(): void {
    this.city = this.sessionService.get('city');

    console.log(this.city.comments);
  }

  ngOnDestroy(): void {
    this.sessionService.remove('city');
  }
}
