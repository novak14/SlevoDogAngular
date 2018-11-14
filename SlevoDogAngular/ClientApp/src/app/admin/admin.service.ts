import {CookieService} from 'ngx-cookie-service';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Inject, Injectable} from '@angular/core';
import {AdminModel} from './admin.model';
import { CategoryModel } from '../shared/category.model';
import { ShopModel } from '../shared/shops.model';
import { KeywordModel } from '../shared/keyword.model';
import { jsonpCallbackContext } from '@angular/common/http/src/module';

@Injectable()
export class AdminService {
  baseUrl: string;
  categories: CategoryModel[];

  constructor(private http: HttpClient,
              @Inject('BASE_URL') baseUrl: string,
              private cookieService: CookieService) {
    this.baseUrl = baseUrl;
  }

  sendAdminForm(adminFormModel: AdminModel) {
    console.log('adminForm: ' + JSON.stringify(adminFormModel));
    return this.http.post(this.baseUrl + 'api/Admin/InsertItemAsync', adminFormModel).toPromise();
  }

  async GetCategories() {
    return await this.http.get<CategoryModel[]>(this.baseUrl + 'api/Admin/GetCategories').toPromise();
  }

  async GetShops(shopName: string) {
    return await this.http.get<ShopModel[]>(this.baseUrl + 'api/Admin/GetShops', {
      params: new HttpParams().set('shopName', shopName)
    }).toPromise();
  }

  async GetKeyWordsSuggest(keyword: string, keywordIds: Array<number>) {
    let params = new HttpParams();

    if (keywordIds && keywordIds.length > 0) {
      keywordIds.forEach(id => {
        params = params.append('keywordIds', id.toString());
      });
    }
    params = params.append('keyword', keyword);
    return await this.http.get<KeywordModel[]>(this.baseUrl + 'api/Admin/GetKeyWordsSuggest', {params}).toPromise();
  }
}
