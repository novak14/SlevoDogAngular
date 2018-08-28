import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { CatalogComponent } from './catalog/catalog.component';
import { ItemComponent } from './catalog/item/item.component';
import {AppRoutingModule} from './app-routing.module';
import {CatalogService} from './catalog/catalog.service';
import { SignupComponent } from './auth/signup/signup.component';
import { SigninComponent } from './auth/signin/signin.component';
import {AuthService} from './auth/auth.service';
import { CommentsComponent } from './catalog/item/comments/comments.component';
import {CookieService} from 'ngx-cookie-service';
import { AdminComponent } from './admin/admin.component';
import {AuthGuard} from './auth/auth-guard.service';
import {AdminService} from './admin/admin.service';
import { CategoryComponent } from './catalog/category/category.component';
import { ShopsComponent } from './catalog/shops/shops.component';
import { SharedService } from './shared/shared.service';
import { CategoryCatalogComponent } from './catalog/category/category-catalog/category-catalog.component';
import { CatalogListComponent } from './catalog/catalog-list/catalog-list.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    CatalogComponent,
    ItemComponent,
    SignupComponent,
    SigninComponent,
    CommentsComponent,
    AdminComponent,
    CategoryComponent,
    ShopsComponent,
    CategoryCatalogComponent,
    CatalogListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    ReactiveFormsModule
  ],
  providers: [CatalogService, AuthService, CookieService, AdminService, SharedService, AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
