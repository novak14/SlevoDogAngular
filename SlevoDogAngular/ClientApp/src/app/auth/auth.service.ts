///<reference path="../../../node_modules/oidc-client/index.d.ts"/>
import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CookieService} from 'ngx-cookie-service';

@Injectable()
export class AuthService {
  baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string,
              private cookieService: CookieService) {
    this.baseUrl = baseUrl;
  }

  async signupUser(email: string, password: string) {
    return await this.http.post(this.baseUrl + 'api/User/RegisterTest', {
      Email: email,
      Password: password,
      ConfirmPassword: password
    });
  }

  async signIn(email: string, password: string) {
    return await this.http.post(this.baseUrl + 'api/User/Login', {
      Email: email,
      Password: password,
      RememberMe: false
    }).subscribe((res: Response) => {
      const cookie = res.toString();
      this.cookieService.set('LoginTemp', cookie);
    });
  }

  isAuthenticated() {
    const test = this.cookieService.get('LoginTemp');
    if (test) {
      return true;
    }
    return false;
  }
}
