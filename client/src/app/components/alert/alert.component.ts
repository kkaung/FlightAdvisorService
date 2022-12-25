import { Component, OnDestroy, OnInit } from '@angular/core';
import { AlertService } from 'src/app/services';
import { Subscription } from 'rxjs';

import { Alert, AlertType } from '../../types';

@Component({
  selector: 'alert',
  templateUrl: './alert.component.html',
})
export class AlertComponent implements OnInit, OnDestroy {
  alerts: Alert[] = [];
  alertSubcriptions?: Subscription;

  constructor(private alertService: AlertService) {}

  ngOnInit(): void {
    this.alertService.onAlert().subscribe((alert) => {
      if (!alert.message) return;

      this.alerts.push(alert);
    });
  }

  ngOnDestroy(): void {
    this.alertSubcriptions?.unsubscribe();
  }

  close(alert: Alert) {
    this.alerts.splice(this.alerts.indexOf(alert), 1);
  }

  cssClass(alert: Alert) {
    if (!alert) return;

    const classes = ['container'];

    const alertTypeClass = {
      [AlertType.Success]: 'alert alert-success',
      [AlertType.Danger]: 'alert alert-danger',
      [AlertType.Info]: 'alert alert-info',
      [AlertType.Warning]: 'alert alert-warning',
    };

    classes.push(alertTypeClass[alert.type!]);

    return classes.join(' ');
  }
}
