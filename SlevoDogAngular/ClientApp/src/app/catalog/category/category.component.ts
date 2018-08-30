import { Component, OnInit } from '@angular/core';
import { CategoryModel } from '../../shared/category.model';
import { SharedService } from '../../shared/shared.service';
import { CatalogService } from '../catalog.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  categoryModel: CategoryModel[];
  test = true;

  constructor(private sharedService: SharedService,
              private catalogService: CatalogService,
              private router: Router) { }

  async ngOnInit() {
    if (!this.sharedService.categories) {
      console.log('Uvnist');
      await this.sharedService.GetCategories().then( (res: CategoryModel[]) => {
        this.categoryModel = res;
        this.test = false;
      });
    } else {
      console.log('Jinde');

      this.categoryModel = this.sharedService.categories;
    }
  }

  async categoryItems(categoryId: number) {
    this.router.navigate(['/category', categoryId]);
  }

}
