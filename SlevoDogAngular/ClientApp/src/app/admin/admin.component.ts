import {Component, OnInit, ViewChild} from '@angular/core';
import {FormGroup, NgForm} from '@angular/forms';
import {AdminModel} from './admin.model';
import {AdminService} from './admin.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  @ViewChild('f') adminForm: NgForm;
  adminFormModel: AdminModel;

  constructor(private adminService: AdminService) { }

  ngOnInit() {
    // this.adminService.sendAdminForm(this.adminFormModel);
  }

  onSubmit() {
    this.adminFormModel = new AdminModel(this.adminForm.value.name,
      this.adminForm.value.priceAfterSale, this.adminForm.value.averagePrice,
      this.adminForm.value.originPrice, this.adminForm.value.image,
      this.adminForm.value.validFrom, this.adminForm.value.validTo,
      this.adminForm.value.linkFirm, this.adminForm.value.description,
      this.adminForm.value.disabled);

    const test = JSON.stringify(this.adminFormModel);
    // console.log('AdminForm: ' + test);
    this.adminService.sendAdminForm(this.adminFormModel);
    this.adminForm.reset();
  }

}
