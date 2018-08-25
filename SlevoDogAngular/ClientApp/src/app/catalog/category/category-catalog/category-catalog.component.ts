import { ActivatedRoute, Params } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { CatalogService } from '../../catalog.service';
import { Sale } from '../../sale.model';

@Component({
  selector: 'app-category-catalog',
  templateUrl: './category-catalog.component.html',
  styleUrls: ['./category-catalog.component.css', '../../catalog.component.css']
})
export class CategoryCatalogComponent implements OnInit {
  categoryId: number;
  public browse: Sale;
  sortOrder: string;

  constructor(private catalogService: CatalogService,
              private route: ActivatedRoute) { }

  async ngOnInit() {
    await this.route.params
      .subscribe(
        (params: Params) => {
          this.categoryId = +params['id'];
        }
      );
    this.browse = await this.catalogService.getCategoryItems(this.categoryId);
  }

  async sortServer(sortValue: string) {
    this.browse = await this.catalogService.getItems(sortValue);
  }

}
