import { Component, OnInit, Input } from '@angular/core';
import { Sale } from '../sale.model';
import { CatalogService } from '../catalog.service';

@Component({
  selector: 'app-catalog-list',
  templateUrl: './catalog-list.component.html',
  styleUrls: ['./catalog-list.component.css', '../catalog.component.css']
})
export class CatalogListComponent implements OnInit {
  @Input() browse: Sale;

  constructor(private catalogService: CatalogService) {
    console.log('Browse: ' + JSON.stringify(catalogService.browse));
    // this.browse = catalogService.browse;
   }

  ngOnInit() {
    // this.browse = this.catalogService.browse;
    console.log('test: ' + JSON.stringify(this.browse));
  }

}
