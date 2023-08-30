import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './views/pages/dashboard/dashboard.component';
import { ClientsComponent } from './views/pages/clients/clients.component';
import { AddclientComponent } from './views/pages/addclient/addclient.component';
import { HttpClientModule } from  '@angular/common/http';
import { CartComponent } from './views/pages/cart/cart.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/dashboard', pathMatch: 'full' 
  },
  {
    path: 'dashboard',
    component: DashboardComponent
  },
  {
    path: 'clients',
    component: ClientsComponent
  },
  {
    path: 'add-client',
    component: AddclientComponent
  },
  {
    path: 'cart',
    component: CartComponent
  },
  {
    path: '**',
    redirectTo: '/dashboard', pathMatch: 'full' 
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),

    HttpClientModule
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
