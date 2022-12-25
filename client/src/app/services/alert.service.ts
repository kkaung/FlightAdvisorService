import { Injectable } from '@angular/core';
import { Alert, AlertType } from '../types';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AlertService {
  private subject = new Subject<Alert>();

  constructor() {}

  onAlert() {
    return this.subject.asObservable();
  }

  alert(alert: Alert) {
    this.subject.next(alert);
  }

  error(message: string) {
    this.alert({ message, type: AlertType.Danger });
  }

  // clear alerts
  clear() {}
}
