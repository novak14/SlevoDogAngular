import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {Sale} from './sale.model';
import {CatalogService} from './catalog.service';

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
  sortValueBefore: string;

  constructor(private catalogService: CatalogService,
              private route: ActivatedRoute,
              private router: Router) {
  }

  async sortServer(sortValue: string) {
    if (this.sortValueBefore && this.sortValueBefore === sortValue) {
      console.log('Sor: ' + sortValue);
      sortValue = 'default';
      this.router.navigate(['/catalog']);
    }
    this.sortValueBefore = sortValue;
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
    this.sortValueBefore = this.sortOrder;
    this.browse = await this.catalogService.getItems(this.sortOrder);
  }
}
