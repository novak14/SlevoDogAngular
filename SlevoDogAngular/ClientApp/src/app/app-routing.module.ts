import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {CatalogComponent} from './catalog/catalog.component';
import {CounterComponent} from './counter/counter.component';
import {ItemComponent} from './catalog/item/item.component';
import {FetchDataComponent} from './fetch-data/fetch-data.component';
import {HomeComponent} from './home/home.component';
import {SignupComponent} from './auth/signup/signup.component';
import {SigninComponent} from './auth/signin/signin.component';
import {AdminComponent} from './admin/admin.component';
import {AuthGuard} from './auth/auth-guard.service';
import {CategoryComponent} from './catalog/category/category.component';
import {ShopsComponent} from './catalog/shops/shops.component';
import { CategoryCatalogComponent } from './catalog/category/category-catalog/category-catalog.component';

const appRoutes: Routes = [
  { path: '', component: CatalogComponent, pathMatch: 'full' },
  { path: 'catalog', component: CatalogComponent},
  { path: 'item/:id', component: ItemComponent},
  { path: 'signup', component: SignupComponent},
  { path: 'signin', component: SigninComponent},
  { path: 'category/:id', component: CategoryCatalogComponent},
  { path: 'shops', component: ShopsComponent},
  { path: 'admin', component: AdminComponent, canActivate: [AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}
