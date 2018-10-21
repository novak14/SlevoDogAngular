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
  keywords = [];
  shopId: number;
  keywordId: number;
  keywordsModel = [];
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
    console.log('Res: ' + JSON.stringify(this.categoryModel));
    this.dropdownList = [
      { id: 1, keyword: 'Mumbai' }
    ];
    this.selectedItems = [
      { item_id: 3, item_text: 'Pune' },
      { item_id: 4, item_text: 'Navsari' }
    ];
    this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'keyword',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: true
    };
  }

  onItemSelect (item:any) {
    console.log(item);
  }
  onSelectAll (items: any) {
    console.log(items);
  }

  async addShop(shop: ShopModel) {
    this.adminForm.form.patchValue({
      nameShop: shop.name
    });
    this.shopId = shop.id;
    this.shopModel = null;
  }

  async addKeyword(keyword: KeywordModel) {
    this.adminForm.form.patchValue({
      keyword: keyword.keyword
    });
    this.keywordId = keyword.id;
    const index = this.keywordModel.indexOf(keyword);
    this.keywordsModel.push(keyword);
    console.log('KeywordsModel: ' + this.keywordsModel);
}

  async GetShops(shopName: string) {
    if (shopName.length > 2) {
      await this.adminService.GetShops(shopName).then((res: ShopModel[]) => {
        console.log('ShopMOdel: ' + JSON.stringify(res));
        this.shopModel = res;
      });
      console.log('Event: ' + shopName);
    }
    // await this.adminService.GetShops()
  }

  async GetKeywords(keyword: string) {
    if (keyword.length > 2) {
      await this.adminService.GetKeyWordsSuggest(keyword).then((res: KeywordModel[]) => {
        console.log('ShopMOdel: ' + JSON.stringify(res));
        this.keywordModel = res;
      });
      console.log('Event: ' + keyword);
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
this.keywords.push(this.adminForm.value.keyword);
    this.adminFormModel = new AdminModel(this.adminForm.value.name,
      this.adminForm.value.priceAfterSale, this.adminForm.value.averagePrice,
      this.adminForm.value.originPrice, this.adminForm.value.image,
      this.adminForm.value.validFrom, this.adminForm.value.validTo,
      this.adminForm.value.linkFirm, this.adminForm.value.description,
      this.adminForm.value.nameShop, this.keywords, true, this.checkCategoryBox);
    // this.adminForm.value.disabled
    // console.log('Checkbox: ' + this.adminForm.value.option);

    const test = JSON.stringify(this.adminFormModel);
    this.adminService.sendAdminForm(this.adminFormModel);
    // this.adminForm.reset();
  }

}
