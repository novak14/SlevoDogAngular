import { NgModule } from '@angular/core';
import { CatalogComponent } from './catalog.component';
import { CatalogListComponent } from './catalog-list/catalog-list.component';
import { CategoryComponent } from './category/category.component';
import { CategoryCatalogComponent } from './category/category-catalog/category-catalog.component';
import { ItemComponent } from './item/item.component';
import { CommentsComponent } from './item/comments/comments.component';
import { ShopsComponent } from './shops/shops.component';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from '../app-routing.module';

@NgModule({
    declarations: [
        CatalogComponent,
        CatalogListComponent,
        CategoryCatalogComponent,
        ItemComponent,
        CommentsComponent,
        ShopsComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        AppRoutingModule
    ]
})
export class CatalogModule {}
