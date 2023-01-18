import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { Airport, City, Response, Trip } from '../models';
import { environment } from '../../environements';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class CityService {
  constructor(private http: HttpClient, private authService: AuthService) {}

  search(searchTerms: { byName: string; commentsLimit: number }) {
    const commentsLimit: string = searchTerms.commentsLimit
      ? `?cLimit=${searchTerms.commentsLimit}`
      : '';

    return this.http
      .post<Response<City[]>>(
        `${environment.apiKey}/api/cities/search${commentsLimit}`,
        { byName: searchTerms.byName }
      )
      .pipe(map((res: Response<City[]>) => res.data));
  }

  searchAirports(byName: string) {
    return this.http
      .get<Response<Airport[]>>(
        `${environment.apiKey}/api/cities/airports?byName=${byName}`
      )
      .pipe(map((res: Response<Airport[]>) => res.data));
  }

  travel(from: string, to: string) {
    const token = this.authService.getToken();

    return this.http
      .get<Response<Trip[]>>(
        `${environment.apiKey}/api/cities/travel?from=${from}&to=${to}`,
        { headers: { Authorization: 'Bearer ' + token } }
      )
      .pipe(map((res: Response<Trip[]>) => res.data));
  }

  addComment(cityId: number, comment: string) {
    const token = this.authService.getToken();

    console.log(cityId, comment);

    return this.http
      .post<Response<City>>(
        `${environment.apiKey}/api/cities/${cityId}/comments`,
        {
          body: comment,
        },
        { headers: { Authorization: 'Bearer ' + token } }
      )
      .pipe(map((res: Response<City>) => res.data));
  }

  deleteComment(cityId: number, commentId: number) {
    const token = this.authService.getToken();

    return this.http
      .delete(
        `${environment.apiKey}/api/cities/${cityId}/comments/${commentId}`,
        { headers: { Authorization: 'Bearer ' + token } }
      )
      .pipe(map((res: Response<City>) => res.data));
  }

  updateComment(cityId: number, commentId: number, comment: string) {
    const token = this.authService.getToken();

    return this.http
      .put(
        `${environment.apiKey}/api/cities/${cityId}/comments/${commentId}`,
        {
          body: comment,
        },
        { headers: { Authorization: 'Bearer ' + token } }
      )
      .pipe(map((res: Response<City>) => res.data));
  }
}
