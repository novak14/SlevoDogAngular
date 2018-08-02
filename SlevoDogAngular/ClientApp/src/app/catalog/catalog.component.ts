import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {forEach} from '@angular/router/src/utils/collection';
import index from '@angular/cli/lib/cli';
import {ActivatedRoute, ParamMap, Params} from '@angular/router';
import {switchMap} from 'rxjs/operators';
import {Sale} from './sale.model';
import {CatalogService} from './catalog.service';
import {CookieService} from 'ngx-cookie-service';

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent implements OnInit {
  public browse: Sale;
  baseUrlTest: string;
  index: number;
  filter: string;
  sortOrder: string;

  constructor(private catalogService: CatalogService,
              private route: ActivatedRoute,
              private cookieService: CookieService) {
  }

  async sortServer(sortValue: string) {
    this.browse = await this.catalogService.getItems(sortValue);
  }

  async ngOnInit() {
    await this.route.queryParams
      .subscribe(
        (queryParams: Params) => {
          this.sortOrder = queryParams['sort'];
        }
      );
    // this.sortServer(this.sortOrder);
    this.browse = await this.catalogService.getItems(this.sortOrder);
  }
}
