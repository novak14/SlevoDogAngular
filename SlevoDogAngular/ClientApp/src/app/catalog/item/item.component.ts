import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {ActivatedRoute, Params} from '@angular/router';
import {CatalogService} from '../catalog.service';
import {Sale} from '../sale.model';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css', '../catalog.component.css']
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
  }

  async increaseRank() {
    const rank = this.item.rankSale + 1;
    await this.catalogService.addRankToSale(this.item.id, Number(rank))
      .then( res => {
      })
      .catch(console.log);

    this.item.rankSale = rank;
    }

  async decreaseRank() {
    let rank = this.item.rankSale;
    if (this.item.rankSale > 0) {
      rank = this.item.rankSale - 1;
    }
    await this.catalogService.decreaseRankToSale(this.item.id, Number(rank))
      .then( res => {
      })
      .catch(console.log);

    this.item.rankSale = rank;
  }
}
