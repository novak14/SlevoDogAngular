import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.css']
})
export class FilterComponent implements OnInit {
  browse: Model;
  baseUrlTest: string;
  index: number;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrlTest = baseUrl;
  }

  async sortServer(sortValue: string) {
    return await this.http.get<Model>(this.baseUrlTest + 'api/Catalog/Index', {
      params: new HttpParams().set('sortOrder', sortValue)
    })
      .subscribe(result => {
        this.browse = result;
      }, error => console.error(error));
  }

  ngOnInit() {
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
