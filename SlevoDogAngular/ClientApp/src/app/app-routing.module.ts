import {RouterModule, Routes, PreloadAllModules} from '@angular/router';
import {NgModule} from '@angular/core';
import {CatalogComponent} from './catalog/catalog.component';
import {ItemComponent} from './catalog/item/item.component';
import {AdminComponent} from './admin/admin.component';
import {AuthGuard} from './auth/auth-guard.service';
import {ShopsComponent} from './catalog/shops/shops.component';
import { CategoryCatalogComponent } from './catalog/category/category-catalog/category-catalog.component';

const appRoutes: Routes = [
  { path: '', component: CatalogComponent, pathMatch: 'full' },
  { path: 'catalog', component: CatalogComponent},
  { path: 'item/:id', component: ItemComponent},
  { path: 'category/:id', component: CategoryCatalogComponent},
  { path: 'shops', component: ShopsComponent},
  { path: 'admin', loadChildren: './admin/admin.module#AdminModule', canActivate: [AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes,
    {preloadingStrategy: PreloadAllModules})],
  exports: [RouterModule]
})
export class AppRoutingModule {

}
