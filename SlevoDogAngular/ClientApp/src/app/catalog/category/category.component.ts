import { Component, OnInit } from '@angular/core';
import { CategoryModel } from '../../shared/category.model';
import { SharedService } from '../../shared/shared.service';
import { CatalogService } from '../catalog.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  categoryModel: CategoryModel[];

  constructor(private sharedService: SharedService,
              private catalogService: CatalogService) { }

  async ngOnInit() {
    await this.sharedService.GetCategories().then( (res: CategoryModel[]) => {
      this.categoryModel = res;
    });
  }

  async categoryItems(categoryId: number) {
    await this.catalogService.getCategoryItems(categoryId).then();
  }

}
