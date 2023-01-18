import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Utils } from 'src/app/helpers';
import { Airport, Trip } from 'src/app/models';
import { AlertService } from 'src/app/services';
import { CityService } from 'src/app/services/city.service';

@Component({
  selector: 'app-travel',
  templateUrl: './travel.component.html',
})
export class TravelComponent implements OnInit {
  form!: FormGroup;
  isSearching: boolean = false;
  isSubmitted: boolean = false;
  isRoundTrip: boolean = true;
  airports: Airport[] = [];
  trips: Trip[] = [];
  isSearchingFromAirports: boolean = false;
  isSearchingToAirports: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private cityService: CityService,
    private alertService: AlertService
  ) {}

  get f() {
    const controls = this.form.controls;
    return {
      tripType: controls['tripType'],
      travellers: controls['travellopers'],
      from: controls['from'],
      to: controls['to'],
      dateFrom: controls['dateFrom'],
      dateTo: controls['dateTo'],
    };
  }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      tripType: '',
      travellers: 1,
      from: '',
      to: '',
      dateFrom: '',
      dateTo: '',
    });
  }

  onKeypress(event: any): void {
    if (!event.key) return;

    if (
      (event.key.length == 1 ||
        event.key == 'Backspace' ||
        event.key == 'Delete') &&
      event.target.value.length >= 3
    ) {
      this.searchAirports(event.target.name, event.target.value);
    }
  }

  onSelect(id: number): void {
    if (this.f.from.value == this.f.to.value)
      switch (id) {
        case 1: {
          this.f.from.setValue(this.f.from.value);
          this.f.to.setValue('');
          break;
        }
        case 2: {
          this.f.from.setValue('');
        }
      }
  }

  onSubmit() {
    this.isSubmitted = true;

    this.alertService.clear();

    if (this.form.invalid) return;

    this.searchTrips(this.f.from.value, this.f.to.value);
  }

  onTripTypeChange(type: boolean) {
    this.isRoundTrip = type;
  }

  private searchTrips(from: string, to: string) {
    this.isSearching = true;

    this.cityService.travel(from, to).subscribe({
      next: (trips) => {
        this.trips = trips!;
        this.isSearching = false;

        this.trips = trips!;

        console.log(this.trips);
      },
      error: (err) => {
        this.alertService.error(err);
        this.isSearching = false;
      },
    });
  }

  private searchAirports(target: string, value: string) {
    Utils.clear(this.airports);

    if (target == 'from') this.isSearchingFromAirports = true;
    else this.isSearchingToAirports = true;

    this.cityService.searchAirports(value).subscribe({
      next: (airports) => {
        {
          this.airports = airports!;
          this.isSearchingFromAirports = false;
          this.isSearchingToAirports = false;
        }
        error: (err: any) => {
          this.alertService.error(err);
          this.isSearchingFromAirports = false;
          this.isSearchingToAirports = false;
        };
      },
    });
  }
}
