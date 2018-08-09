import {Component, OnInit, ViewChild} from '@angular/core';
import {FormGroup, NgForm} from '@angular/forms';
import {AdminModel} from './admin.model';
import {AdminService} from './admin.service';
import {CategoryModel} from '../shared/category.model';
import {forEach} from '@angular/router/src/utils/collection';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  @ViewChild('f') adminForm: NgForm;
  adminFormModel: AdminModel;
  categoryModel: CategoryModel[];
  checkCategoryBox = [];

  constructor(private adminService: AdminService) { }

  async ngOnInit() {
    await this.adminService.GetCategories().then((res: CategoryModel[]) => {
      this.categoryModel = res;
      }
    );
    console.log('Res: ' + JSON.stringify(this.categoryModel));

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

    this.adminFormModel = new AdminModel(this.adminForm.value.name,
      this.adminForm.value.priceAfterSale, this.adminForm.value.averagePrice,
      this.adminForm.value.originPrice, this.adminForm.value.image,
      this.adminForm.value.validFrom, this.adminForm.value.validTo,
      this.adminForm.value.linkFirm, this.adminForm.value.description,
      true, this.checkCategoryBox);
    // this.adminForm.value.disabled
    // console.log('Checkbox: ' + this.adminForm.value.option);

    const test = JSON.stringify(this.adminFormModel);
    this.adminService.sendAdminForm(this.adminFormModel);
    // this.adminForm.reset();
  }

}
