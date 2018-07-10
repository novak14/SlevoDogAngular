import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {CatalogComponent} from './catalog/catalog.component';
import {CounterComponent} from './counter/counter.component';
import {ItemComponent} from './catalog/item/item.component';
import {FetchDataComponent} from './fetch-data/fetch-data.component';
import {HomeComponent} from './home/home.component';
import {SignupComponent} from './auth/signup/signup.component';

const appRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'counter', component: CounterComponent },
  { path: 'fetch-data', component: FetchDataComponent },
  { path: 'catalog', component: CatalogComponent},
  { path: 'item/:id', component: ItemComponent},
  { path: 'signup', component: SignupComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}
