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

  async ngOnInit() {
    await this.route.params
      .subscribe(
        (params: Params) => {
          this.id = +params['id'];
        }
      );
    this.item = await this.catalogService.getSale(this.id);
    console.log('Comm: ' + JSON.stringify(this.item.comments[0].text));
  }
}
