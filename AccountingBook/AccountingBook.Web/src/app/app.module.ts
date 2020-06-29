import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router'
import { HttpClientModule } from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MenuModule,PanelModule, InputTextModule, ButtonModule} from 'primeng';


import {BrowserAnimationsModule} from "@angular/platform-browser/animations";

import { AppComponent } from './app.component';
import { CompanyComponent } from './company/company.component';
import { StatisticsComponent } from './statistics/statistics.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { JournalComponent } from './journal/journal.component';

const routes = [
  { path: "", redirectTo: "/dashboard", pathMatch: "full" },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'company', component: CompanyComponent },
  { path: 'journal', component: JournalComponent }
]

@NgModule({
  declarations: [
    AppComponent,
    CompanyComponent,
    StatisticsComponent,
    DashboardComponent,
    JournalComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes),
    BrowserAnimationsModule,
    PanelModule,
    MenuModule,
    InputTextModule,
    ButtonModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
