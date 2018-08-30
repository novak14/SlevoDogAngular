import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CategoryModel } from './category.model';

@Injectable()
export class SharedService {
    baseUrl: string;
    categories: CategoryModel[];

    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
      }

      async GetCategories() {
        this.categories = await this.http.get<CategoryModel[]>(this.baseUrl + 'api/Admin/GetCategories').toPromise();
        return this.categories;
      }
}
