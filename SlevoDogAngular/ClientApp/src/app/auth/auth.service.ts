import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable()
export class AuthService {
  baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  signupUser(email: string, password: string) {
    return this.http.post(this.baseUrl + 'api/User/Login', {
      Email: email,
      Password: password,
      ConfirmPassword: password
    })
      .subscribe(res => {
          console.log(res[2]);
          console.log(res[2].value());
        },
        err => {
          console.log('Error: ' + err.toString());
        });
  }

  signinUser(email: string, password: string) {
    return this.http.post(this.baseUrl + 'api/User/LoginTest', {
      Email: email,
      Password: password,
      RememberMe: false
    });

  }
}
