import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, Observable } from 'rxjs';
import jwt_decode from 'jwt-decode';

import { Login, Register, User } from '../types';
import { environment } from 'src/environements';
import { Response } from '../models';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private userSubject: BehaviorSubject<User | null> | null = null;
  public user: Observable<User | null> | null = null;
  public userValue: User | null = null;

  constructor(private http: HttpClient) {
    // check the token when the refresh the page
    const token = this.getToken();

    if (!token) return;

    const user = this.decodeToken(token);

    this.userSubject = new BehaviorSubject<User | null>(user);

    this.user = this.userSubject.asObservable();

    this.getMe().subscribe({
      next: (user) => {
        this.userValue = user!;
      },
    });
  }

  register(body: Register) {
    return this.http.post<Response<any>>(
      `${environment.apiKey}/public/register`,
      {
        ...body,
        email: body.username,
      }
    );
  }

  login(body: Login) {
    return this.http
      .post<Response<string>>(`${environment.apiKey}/public/login`, {
        email: body.username,
        ...body,
      })
      .pipe(
        map((res) => {
          const token = res.data as string;

          // store the token in local storage
          this.storeToken(token);

          // decode the token
          const user = this.decodeToken(token);

          this.userSubject = new BehaviorSubject<User | null>(user);

          this.user = this.userSubject.asObservable();

          //   this.user = this.userSubject!.next(user);

          return user;
        })
      );
  }

  getMe() {
    return this.http
      .get<Response<User>>(`${environment.apiKey}/api/users/me`, {
        headers: { Authorization: 'Bearer ' + this.getToken() },
      })
      .pipe(map((res: Response<User>) => res.data));
  }

  logout() {
    this.removeToken();

    this.user = null;
  }

  private storeToken(token: string) {
    localStorage.setItem('token', JSON.stringify(token));
  }

  getToken(): string | null {
    const token = JSON.parse(localStorage.getItem('token') as string);
    if (!token) return null;
    return token;
  }

  private decodeToken(token: string): User {
    return jwt_decode<User>(token);
  }

  private removeToken() {
    localStorage.removeItem('token');
    this.userSubject?.next(null);
  }
}
