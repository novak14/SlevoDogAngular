import {CookieService} from 'ngx-cookie-service';
import {HttpClient} from '@angular/common/http';
import {Inject, Injectable} from '@angular/core';
import {AdminModel} from './admin.model';

@Injectable()
export class AdminService {
  baseUrl: string;

  constructor(private http: HttpClient,
              @Inject('BASE_URL') baseUrl: string,
              private cookieService: CookieService) {
    this.baseUrl = baseUrl;
  }

  sendAdminForm(adminFormModel: AdminModel) {
    console.log('adminForm: ' + JSON.stringify(adminFormModel));
    return this.http.post(this.baseUrl + 'api/Admin/InsertItemAsync', adminFormModel).toPromise();
  }
}
