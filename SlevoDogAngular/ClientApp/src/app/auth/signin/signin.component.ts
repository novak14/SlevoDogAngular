import { Component, OnInit } from '@angular/core';
import {AuthService} from '../auth.service';
import {NgForm} from '@angular/forms';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {
  invalidLogin: boolean;

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  async onSignin(form: NgForm) {
    const credentials = JSON.stringify(form.value);
    await this.authService.signIn(credentials).then((response) => {
      const token = (<any>response).token;
      localStorage.setItem('jwt', token);
      this.invalidLogin = false;
    }, err => {
      this.invalidLogin = true;
    });
  }

}
