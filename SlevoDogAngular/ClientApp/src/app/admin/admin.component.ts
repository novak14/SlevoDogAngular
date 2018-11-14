import {Component, OnInit, ViewChild} from '@angular/core';
import {FormGroup, NgForm} from '@angular/forms';
import {AdminModel} from './admin.model';
import {AdminService} from './admin.service';
import {CategoryModel} from '../shared/category.model';
import { SharedService } from '../shared/shared.service';
import { ShopModel } from '../shared/shops.model';
import { KeywordModel } from '../shared/keyword.model';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  @ViewChild('f') adminForm: NgForm;
  adminFormModel: AdminModel;
  categoryModel: CategoryModel[];
  shopModel: ShopModel[];
  keywordModel: KeywordModel[];
  checkCategoryBox = [];
  keywords = new Array<string>();
  shopId: number;
  keywordId: number;
  keywordsModel: KeywordModel[] = [];
  idList = new Array<number>();
  dropdownList = [];
  selectedItems = [];
  dropdownSettings = {};

  constructor(private adminService: AdminService,
              private sharedService: SharedService) { }

  async ngOnInit() {
    await this.adminService.GetCategories().then((res: CategoryModel[]) => {
      this.categoryModel = res;
      }
    );
  }

  async addShop(shop: ShopModel) {
    this.adminForm.form.patchValue({
      nameShop: shop.name
    });
    this.shopId = shop.id;
    this.shopModel = null;
  }

  async addKeyword(keyword: KeywordModel) {
    if (keyword.id !== undefined) {
      this.idList.push(keyword.id);
    }
}

  async removeKeyword(keyword: KeywordModel) {
    if (keyword.id !== undefined) {
      const index = this.idList.indexOf(keyword.id);
      this.idList.splice(index, 1);
    }
  }

  async GetShops(shopName: string) {
    if (shopName.length > 2) {
      await this.adminService.GetShops(shopName).then((res: ShopModel[]) => {
        this.shopModel = res;
      });
      console.log('Event: ' + shopName);
    }
  }

  async GetKeywords(keyword: string) {
    if (keyword.length > 2) {
      await this.adminService.GetKeyWordsSuggest(keyword, this.idList).then((res: KeywordModel[]) => {
        this.keywordModel = res;
      });
    }
  }

  onSubmit() {
    console.log('CategoryModel: ' + JSON.stringify(this.categoryModel));
    for (const item of this.categoryModel) {
      console.log('Item: ' + item.checkCategory);
      if (item.checkCategory) {
        this.checkCategoryBox.push(item.id);
      }
    }
console.log('Box: ' + this.checkCategoryBox);
this.keywordsModel.forEach(keyword => {
  if (keyword.id === null || keyword.id === undefined) {
    this.keywords.push(keyword.fullKeyword);
  }
});
    this.adminFormModel = new AdminModel(this.adminForm.value.name,
      this.adminForm.value.priceAfterSale, this.adminForm.value.averagePrice,
      this.adminForm.value.originPrice, this.adminForm.value.image,
      this.adminForm.value.validFrom, this.adminForm.value.validTo,
      this.adminForm.value.linkFirm, this.adminForm.value.description,
      this.adminForm.value.nameShop, this.keywords, this.idList, true, this.checkCategoryBox);
    // this.adminForm.value.disabled
    // console.log('Checkbox: ' + this.adminForm.value.option);

    const test = JSON.stringify(this.adminFormModel);
    this.adminService.sendAdminForm(this.adminFormModel);
    this.adminForm.reset();
  }

}
