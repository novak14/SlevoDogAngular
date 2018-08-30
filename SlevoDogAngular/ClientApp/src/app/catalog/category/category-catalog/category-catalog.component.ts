import { ActivatedRoute, Params } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { CatalogService } from '../../catalog.service';
import { Sale } from '../../sale.model';
import { Subscription } from 'rxjs';
import { async } from 'q';

@Component({
  selector: 'app-category-catalog',
  templateUrl: './category-catalog.component.html',
  styleUrls: ['./category-catalog.component.css', '../../catalog.component.css']
})
export class CategoryCatalogComponent implements OnInit, OnDestroy {
  categoryId: number;
  public browse: Sale;
  sortOrder: string;
  paramsSubscription: Subscription;


  constructor(private catalogService: CatalogService,
              private route: ActivatedRoute) { }

  async ngOnInit() {
     this.paramsSubscription = await this.route.params
      .subscribe(
        (params: Params) =>  {
          // console.log('CategoryId: ' + this.categoryId + ' params: ' + +params['id']);
          this.categoryId = +params['id'];
          this.catalogService.getCategoryItems(this.categoryId, '').then( res => {
            this.browse = res;
            this.catalogService.browse = this.browse;
          });
        }
      );
  }

  async sortServer(sortValue: string) {
    console.log('sortValue: ' + sortValue);
    this.browse = await this.catalogService.getCategoryItems(this.categoryId, sortValue);
  }

  ngOnDestroy() {
    this.paramsSubscription.unsubscribe();
  }

}
