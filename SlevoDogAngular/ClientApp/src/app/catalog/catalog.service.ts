import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {ActivatedRoute} from '@angular/router';
import {Sale} from './sale.model';
import {CommentsModel} from './comments.model';
import {CookieService} from 'ngx-cookie-service';
import {Observable} from 'rxjs/Observable';

@Injectable()
export class CatalogService {
  baseUrl: string;
  test: Sale;
  username: string;
  // public comment = new CommentsModel(32, 'Maca', 'Super');
  //

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string,
              private route: ActivatedRoute,
              private cookieService: CookieService) {
    this.baseUrl = baseUrl;
  }

   async getItems(sortValue: string) {
     return await this.http.get<Sale>(this.baseUrl + 'api/Catalog/Index', {
      params: new HttpParams().set('sortOrder', sortValue)
    }).toPromise();
      // .subscribe((result: Sale) => {
      //   this.test = result;
      // }, error => console.error(error));
    // return this.test;
    //  this.test = das;
    //  return this.test;
  }

  getItemsSync(sortValue: string) {
    this.http.get<Sale>(this.baseUrl + 'api/Catalog/Index', {
      params: new HttpParams().set('sortOrder', sortValue)
    })
    .subscribe((result: Sale) => {
      this.test = result;
    }, error => console.error(error));
   return this.test;
  }

  async getSale(id: number) {
    return await this.http.get<Model>(this.baseUrl + 'api/Item/ItemAsync', {
      params: new HttpParams().set('id', String(id))
    }).toPromise();
    //   .subscribe(result => {`
    //   this.item = result;
    //   console.log(result);
    // }, error => console.error(error));
  }

  insertComment(comment: CommentsModel) {
    console.log('Jsem v komentys ' + comment.authorName);
    let cookie: string;
    const test = this.http.post(this.baseUrl + 'api/Item/AddComments', comment).subscribe((res: Response) => {
      cookie = res.toString();
      this.cookieService.set('UserComment', cookie);

      console.log('Text: ' + res);
    });
    console.log('Testes: ' + cookie);
    return cookie;
  }

  getUserForComment(cookie: string) {
    return this.http.get(this.baseUrl + 'api/Item/GetUserNameComment', {
      params: new HttpParams().set('cookie', cookie)
    });

  }

}

interface Model {
  id: number;
  name: string;
  image: string;
  priceAfterSale: string;
  originPrice: number;
  dateInsert: string;
  validFrom: string;
  validTo: string;
  bDisabled: boolean;
  linkFirm: string;
  description: string;
  percentSale: number;
  saleCollection: Test;
  // collections: Model[];
}

interface Test {
  collections: Model[];
}
