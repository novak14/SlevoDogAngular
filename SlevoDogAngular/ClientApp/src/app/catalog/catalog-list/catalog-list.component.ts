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
    this.browse = catalogService.browse;
   }

  ngOnInit() {
    this.browse = this.catalogService.browse;

    this.browseSubscription = this.catalogService.browseChanged.subscribe(
      (items: Sale) => {
        this.browse = items;
      }
    );
  }

  ngOnDestroy() {
    this.browseSubscription.unsubscribe();
  }

  sendEvent = () => {
    (<any>window).ga('send', 'event', {
      eventCategory: 'Catalog',
      eventLabel: 'Item',
      eventAction: 'To Shop',
      eventValue: 10
    });
  }

  sendEventTo = () => {
    (<any>window).ga('send', 'event', {
      eventCategory: 'Catalog',
      eventLabel: 'Item',
      eventAction: 'To Detail',
      eventValue: 10
    });
  }

}
