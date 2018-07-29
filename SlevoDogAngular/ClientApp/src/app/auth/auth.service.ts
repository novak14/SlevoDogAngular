///<reference path="../../../node_modules/oidc-client/index.d.ts"/>
import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { UserManager, UserManagerSettings, User } from 'oidc-client';
import {Observable} from 'rxjs/Observable';
import {CookieService} from 'ngx-cookie-service';

@Injectable()
export class AuthService {
  baseUrl: string;
  private manager: UserManager = new UserManager(getClientSettings());
  private user: User = null;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string,
              private cookieService: CookieService) {
    this.baseUrl = baseUrl;
    this.manager.getUser().then(user => {
      this.user = user;
    });
  }

  isLoggedIn(): boolean {
    return this.user != null && !this.user.expired;
  }

  getClaims(): any {
    return this.user.profile;
  }

  getAuthorizationHeaderValue(): string {
    return `${this.user.token_type} ${this.user.access_token}`;
  }

  startAuthentication(): Promise<void> {
    return this.manager.signinRedirect();
  }

  completeAuthentication(): Promise<void> {
    return this.manager.signinRedirectCallback().then(user => {
      this.user = user;
    });
  }

  signupUser(email: string, password: string) {
    return this.http.post(this.baseUrl + 'api/User/RegisterTest', {
      Email: email,
      Password: password,
      ConfirmPassword: password
    })
      .subscribe(res => {
        console.log('Readsfs: ' + res.toString());
          // console.log(res[2]);
          // console.log(res[2].value());
        },
        err => {
          console.log('Error: ' + err.toString());
        });
  }

  signinUser(email: string, password: string) {
    const params: any = {
      authority: 'https://localhost:44339/',
      grant_type: 'password',
      username: email,
      password: password,
      scope: 'api1 openid',
      client_id: 'ro.angular',
      client_secret: 'secret'
    };

    const mgr = new UserManager(params);

    console.log('Mgr: ' + mgr);
    const dest = mgr.signinRedirect();

    const getUser = mgr.getUser().then(user => {
      console.log('GetUser: ' + user.access_token + user.id_token + user.state);
    });

    // mgr.signinUser(email, password).subscribe(tes => {
    //   console.log('Tes: ' + tes);
    // });

    const body = JSON.stringify(params);
    console.log('Body: ' + body);
    const test = this.http.post('https://localhost:44339/connect/token', body).subscribe(res => {
      console.log('Reste: ' + res);
    });

    console.log('Uz jsem za');
    return this.http.post(this.baseUrl + 'api/User/LoginTest', {
      Email: email,
      Password: password,
      RememberMe: false
    });
  }

  signIn(email: string, password: string) {
    return this.http.post(this.baseUrl + 'api/User/Login', {
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
    const blabla = this.cookieService.get('blablabla');
    // console.log('TestAuth: ' + test + ' Blabla: ' + blabla + ' Boolean: ' + (test));
    if (test) {
      return true;
    }
    return false;
  }

}

export function getClientSettings(): UserManagerSettings {
  return {
    authority: 'https://localhost:44339/',
    client_id: 'ro.angular',
    redirect_uri: 'http://localhost:4200/auth-callback',
    post_logout_redirect_uri: 'http://localhost:4200/',
    response_type: 'id_token token',
    scope: 'api1 openid',
    filterProtocolClaims: true,
    loadUserInfo: true,
    automaticSilentRenew: true,
    // silent_redirect_uri: 'http://localhost:4200/silent-refresh.html'
  };
}

  // authority: 'https://localhost:44339/',
  //   grant_type: 'password',
  //   username: email,
  //   password: password,
  //   scope: 'api1 openid',
  //   client_id: 'ro.angular',
  //   client_secret: 'secret'
