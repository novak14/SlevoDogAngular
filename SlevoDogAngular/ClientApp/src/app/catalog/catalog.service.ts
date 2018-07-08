import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {ActivatedRoute} from '@angular/router';
import {Sale} from './sale.model';

@Injectable()
export class CatalogService {
  baseUrl: string;
  test: Sale;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string,
              private route: ActivatedRoute) {
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
    //   .subscribe(result => {
    //   this.item = result;
    //   console.log(result);
    // }, error => console.error(error));
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
