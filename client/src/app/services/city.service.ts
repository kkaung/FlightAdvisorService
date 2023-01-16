import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { City, Response } from '../models';
import { environment } from '../../environement';

@Injectable({
  providedIn: 'root',
})
export class CityService {
  constructor(private http: HttpClient) {}

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

  addComment(cityId: number, comment: string) {
    return this.http
      .post<Response<City>>(
        `${environment.apiKey}/api/cities/${cityId}/comments`,
        {
          body: comment,
        }
      )
      .pipe(map((res: Response<City>) => res.data));
  }

  deleteComment(cityId: number, commentId: number) {
    return this.http
      .delete(`${environment.apiKey}/api/${cityId}/${commentId}`)
      .pipe(map((res: Response<City>) => res.data));
  }

  updaeteComment(cityId: number, commentId: number, comment: string) {
    return this.http
      .put(`${environment.apiKey}/api/${cityId}/${commentId}`, {
        body: comment,
      })
      .pipe(map((res: Response<City>) => res.data));
  }
}
