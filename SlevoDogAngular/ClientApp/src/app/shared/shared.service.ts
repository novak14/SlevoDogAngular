import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class SharedService {
    baseUrl: string;

    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
      }

      async GetCategories() {
        return await this.http.get(this.baseUrl + 'api/Admin/GetCategories').toPromise();
      }
}
