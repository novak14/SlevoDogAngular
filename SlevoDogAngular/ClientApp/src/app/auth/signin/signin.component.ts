import { Component, OnInit } from '@angular/core';
import {AuthService} from '../auth.service';
import {NgForm} from '@angular/forms';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  onSignin(form: NgForm) {
    const email = form.value.email;
    const password = form.value.password;
    console.log('Email: ' + email + ' Password: ' + password);
    this.authService.signinUser(email, password).subscribe(res => {
        console.log(res);
      },
      err => {
        console.log('Error: ' + err.toString());
      });
  }

}
