import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, Observable } from 'rxjs';
import jwt_decode from 'jwt-decode';

import { AuthResponse, Login, Register, User } from '../types';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private API_URL = 'http://localhost:5265/public';

  private userSubject?: BehaviorSubject<User | null>;
  public user?: Observable<User | null>;

  constructor(private http: HttpClient) {
    // check the token when the refresh the page
    const token = this.restoreToken();

    if (!token) return;

    const user = this.decodeToken(token);

    this.userSubject = new BehaviorSubject<User | null>(user);

    this.user = this.userSubject.asObservable();
  }

  register(body: Register) {
    return this.http.post<AuthResponse>(this.API_URL + '/register', {
      ...body,
      email: body.username,
    });
  }

  login(body: Login) {
    return this.http
      .post<AuthResponse>(this.API_URL + '/login', {
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

          this.userSubject!.next(user);

          return user;
        })
      );
  }

  logout() {
    this.removeToken();
  }

  private storeToken(token: string) {
    localStorage.setItem('token', JSON.stringify(token));
  }

  private restoreToken(): string | null {
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
