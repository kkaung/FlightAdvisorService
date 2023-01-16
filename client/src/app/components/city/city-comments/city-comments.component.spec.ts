import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CityCommentsComponent } from './city-comments.component';

describe('CityCommentsComponent', () => {
  let component: CityCommentsComponent;
  let fixture: ComponentFixture<CityCommentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CityCommentsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CityCommentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
