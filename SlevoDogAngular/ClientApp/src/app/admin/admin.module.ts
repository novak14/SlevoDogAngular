import { NgModule } from '@angular/core';
import { AdminComponent } from './admin.component';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AppRoutingModule } from '../app-routing.module';
import { AdminRoutingModule } from './admin-routing.module';
import { NgSelectModule } from '@ng-select/ng-select';

@NgModule({
    declarations: [
        AdminComponent
    ],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        FormsModule,
        AdminRoutingModule,
        NgSelectModule
    ]
})
export class AdminModule {}
