import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {ActivatedRoute, Params} from '@angular/router';
import {CatalogService} from '../catalog.service';
import {Sale} from '../sale.model';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent implements OnInit {
  public item: Sale;
  baseUrlTest: string;
  id: number;

  constructor(private catalogService: CatalogService,
              private route: ActivatedRoute) {
  }

  // async getValues3() {
  //   return await this.http.get<Model>(this.baseUrlTest + 'api/Item/ItemAsync', {
  //     params: new HttpParams().set('id', String(this.id))
  //   }).subscribe(result => {
  //     this.item = result;
  //     console.log(result);
  //   }, error => console.error(error));
  // }

  async ngOnInit() {
    await this.route.params
      .subscribe(
        (params: Params) => {
          this.id = +params['id'];
        }
      );
    this.item = await this.catalogService.getSale(this.id);

  }
}

interface Model {
  id: number;
  name: string;
  image: string;
  priceAfterSale: string;
  originPrice: string;
  dateInsert: string;
  validFrom: string;
  validTo: string;
  bDisabled: boolean;
  linkFirm: string;
  description: string;
  percentSale: number;
  comments: Comments;
}


interface Comments {
  id: number;
  fkSale: number;
  dateInsert: string;
  fkUser: string;
  authorName: string;
  rank: number;
  text: string;
  fkParrentComment: number;
  disabled: boolean;
}
