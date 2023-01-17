import { Component, OnDestroy, OnInit } from '@angular/core';
import { City, User } from 'src/app/models';
import { DatePipe } from '@angular/common';
import { SessionService } from 'src/app/services/session.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AlertService, AuthService } from 'src/app/services';
import { CityService } from 'src/app/services/city.service';

@Component({
  selector: 'app-city-comments',
  templateUrl: './city-comments.component.html',
  providers: [DatePipe],
})
export class CityCommentsComponent implements OnInit, OnDestroy {
  city: City = new City('', '');
  form?: FormGroup;
  isSubmitted = false;
  user: User | null = new User();

  constructor(
    private formBuilder: FormBuilder,
    private sessionService: SessionService,
    private modalService: NgbModal,
    private alertService: AlertService,
    private cityService: CityService,
    private authService: AuthService
  ) {}

  get f() {
    return { description: this.form!.controls['description'] };
  }

  ngOnInit(): void {
    this.user = this.authService.userValue;
    this.city = this.sessionService.get('city');
    this.form = this.formBuilder.group({ description: [''] });
  }

  ngOnDestroy(): void {
    this.sessionService.remove('city');
    this.sessionService.remove('commentId');
  }

  onUpdate(commentId: number, comment: string, content: any): void {
    this.modalService.open(content);
    this.sessionService.put('commentId', commentId);
    this.f.description.setValue(comment);
  }

  onUpdateComment(modal: any) {
    this.isSubmitted = true;

    this.alertService.clear();

    if (this.form?.invalid) return;

    this.cityService
      .updateComment(
        this.city.id,
        this.sessionService.get('commentId'),
        this.f.description.value
      )
      .subscribe({
        next: () => {
          this.refreshCities();

          modal.dismiss('Cross click');
        },
        error: () => {},
      });
  }

  onDeleteComment(commentId: number) {
    this.alertService.clear();

    this.cityService.deleteComment(this.city.id, commentId).subscribe({
      next: () => {
        this.refreshCities();
      },
      error: () => {},
    });
  }

  private refreshCities() {
    this.cityService
      .search({ byName: this.city.name, commentsLimit: 0 })
      .subscribe({
        next: (cities) => {
          this.city = cities ? cities[0] : new City('', '');
        },
      });
  }
}
