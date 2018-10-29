import { NgModule } from '@angular/core';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AppRoutingModule } from '../app-routing.module';
import { CatalogService } from '../catalog/catalog.service';
import { AuthService } from '../auth/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { AdminService } from '../admin/admin.service';
import { SharedService } from '../shared/shared.service';
import { AuthGuard } from '../auth/auth-guard.service';
import { CommonModule } from '@angular/common';
import { CategoryComponent } from '../catalog/category/category.component';

@NgModule({
    declarations: [
        NavMenuComponent,
        HomeComponent,
        CategoryComponent
    ],
    imports: [
        CommonModule,
        AppRoutingModule
    ],
    exports: [
        AppRoutingModule,
        NavMenuComponent
    ],
    providers: [
        CatalogService,
        AuthService,
        CookieService,
        AdminService,
        SharedService,
        AuthGuard
    ]
})
export class CoreModule {}
