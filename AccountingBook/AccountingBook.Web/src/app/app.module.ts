import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router'
import { HttpClientModule } from '@angular/common/http';
import {FormsModule} from '@angular/forms'


import { AppComponent } from './app.component';
import { CompanyComponent } from './company/company.component';

const routes = [
  { path: 'company', component: CompanyComponent }
]

@NgModule({
  declarations: [
    AppComponent,
    CompanyComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
