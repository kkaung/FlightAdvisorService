import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-travel',
  templateUrl: './travel.component.html',
})
export class TravelComponent implements OnInit {
  form!: FormGroup;

  constructor(private formBuilder: FormBuilder) {}

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
      form: '',
      to: '',
      dateFrom: '',
      dateTo: '',
    });
  }

  onSubmit() {
    const body = {
      tripType: this.f.tripType.value,
      travellers: this.f.travellers.value,
      from: this.f.from.value,
      to: this.f.to.value,
      dateFrom: this.f.dateFrom.value,
      dateTo: this.f.dateTo.value,
    };

    console.log(body);
  }
}
