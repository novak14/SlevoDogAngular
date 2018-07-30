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

const appRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'counter', component: CounterComponent },
  { path: 'fetch-data', component: FetchDataComponent },
  { path: 'catalog', component: CatalogComponent, canActivate: [AuthGuard]},
  { path: 'item/:id', component: ItemComponent, canActivate: [AuthGuard]},
  { path: 'signup', component: SignupComponent},
  { path: 'signin', component: SigninComponent},
  { path: 'admin', component: AdminComponent, canActivate: [AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}
