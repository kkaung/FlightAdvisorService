import { environment } from './../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { City } from 'src/app/models';
import { AlertService, AuthService } from 'src/app/services';
import { CityService } from 'src/app/services/city.service';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-city-search',
  templateUrl: './city-search.component.html',
})
export class CitySearchComponent implements OnInit, OnDestroy {
  form!: FormGroup;
  addForm!: FormGroup;
  isLoading: boolean = false;
  isSubmitted: boolean = false;
  cities: City[] = [];
  class: string = 'hidden';

  constructor(
    public activeModal: NgbActiveModal,
    private formBuilder: FormBuilder,
    private cityService: CityService,
    private alertService: AlertService,
    private sessionService: SessionService,
    private modalService: NgbModal,
    private http: HttpClient,
    private authService: AuthService
  ) {}

  get f() {
    return {
      byName: this.form.controls['byName'],
      commentsLimit: this.form.controls['commentsLimit'],
    };
  }

  get af() {
    return {
      description: this.addForm.controls['description'],
    };
  }

  ngOnInit(): void {
    this.form = this.formBuilder.group({ byName: [''], commentsLimit: [''] });

    this.addForm = this.formBuilder.group({ description: [''] });
  }

  ngOnDestroy(): void {}

  onSearchSubmit() {
    this.isSubmitted = true;

    // reset alerts on submit
    this.alertService.clear();

    if (this.form.invalid) return;

    this.search();

    this.isSubmitted = false;
  }

  onShow(city: City) {
    this.sessionService.put('city', city);
  }

  onAdd(cityId: number, modal: any) {
    this.modalService.open(modal);

    this.cityService.search({ byName: 'sydney', commentsLimit: 0 });

    this.sessionService.put('cityId', cityId);
  }

  onAddComment(modal: any) {
    this.isSubmitted = true;

    this.alertService.clear();

    if (this.addForm.invalid) return;

    this.cityService
      .addComment(this.sessionService.get('cityId'), this.af.description.value)
      .subscribe({
        next: (res) => {
          this.search();

          modal.dismiss('close modal');
        },
        error: () => {},
      });

    this.isSubmitted = false;
  }

  private search(): void {
    this.isLoading = true;

    this.cityService.search(this.form.value).subscribe({
      next: (cities) => {
        this.cities = cities!;
      },
      error: (error) => {
        console.log(error);
      },
    });

    this.isLoading = false;
  }

  checkCommentsLimitInput(value: number): boolean {
    if (value > 30) {
      this.f.byName.setValue(30);
      return false;
    }

    return true;
  }
}
