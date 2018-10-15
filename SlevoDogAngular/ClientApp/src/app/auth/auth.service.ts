import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CookieService} from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';


@Injectable()
export class AuthService {
  baseUrl: string;
  invalidLogin: boolean;
  isAdmin = false;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string,
  private jwtHelper: JwtHelperService,
              private cookieService: CookieService,
              private router: Router) {
    this.baseUrl = baseUrl;
  }

  async signupUser(credentials: string) {
    return await this.http.post(this.baseUrl + 'api/User/RegisterTest', credentials, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).subscribe((response) => {
      const token = (<any>response).token;
      localStorage.setItem('jwt', token);
      this.invalidLogin = false;
      this.router.navigate(['/']);
    }, err => {
      this.invalidLogin = true;
    });
  }

  async signIn(credentials: string) {
    return await this.http.post(this.baseUrl + 'api/User/Login', credentials, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).toPromise();
  }

  isAuthenticated() {
    const token = localStorage.getItem('jwt');

    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    return false;
  }

  isAuthenticatedAdmin() {
    const token = localStorage.getItem('jwt');
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      console.log('predAdmin: ' + this.isAdmin);
      return true;
    }
    return false;
  }

  checkUser() {
    this.http.get(this.baseUrl + 'api/Admin/CheckUser')
        .subscribe((response) => {
          console.log('Response: ' + response);
          this.isAdmin = true;
        }, err => {
          this.isAdmin = false;
          console.log('Faaaaaaaaaaaaaaaaalse: ' + this.isAdmin);
        });
  }
}
