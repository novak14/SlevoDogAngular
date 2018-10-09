import { Component, OnInit } from '@angular/core';
import {NgForm} from '@angular/forms';
import {AuthService} from '../auth.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  invalidLogin: boolean;

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  async onSignup(form: NgForm) {
    const credentials = JSON.stringify(form.value);

    await this.authService.signupUser(credentials);
  }

}
