import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Sale } from '../sale.model';
import { CatalogService } from '../catalog.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-catalog-list',
  templateUrl: './catalog-list.component.html',
  styleUrls: ['./catalog-list.component.css', '../catalog.component.css']
})
export class CatalogListComponent implements OnInit, OnDestroy {
  browse: Sale;
  browseSubscription: Subscription;


  constructor(private catalogService: CatalogService) {
    // console.log('Browse: ' + JSON.stringify(catalogService.browse));
    this.browse = catalogService.browse;
   }

  ngOnInit() {
    this.browse = this.catalogService.browse;
    // console.log('test: ' + JSON.stringify(this.browse));

    this.browseSubscription = this.catalogService.browseChanged.subscribe(
      (items: Sale) => {
        this.browse = items;
      }
    );
  }

  ngOnDestroy() {
    this.browseSubscription.unsubscribe();
  }

}
