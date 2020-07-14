import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router'
import { HttpClientModule } from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MenuModule,PanelModule, InputTextModule, ButtonModule, TableModule, DialogModule, CalendarModule, CheckboxModule, DropdownModule, InputNumberModule, FieldsetModule, CardModule, TreeTableModule, ToastModule, MessageService, TabViewModule, ChartModule} from 'primeng';


import {BrowserAnimationsModule} from "@angular/platform-browser/animations";

import { AppComponent } from './app.component';
import { CompanyComponent } from './company/company.component';
import { StatisticsComponent } from './statistics/statistics.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { JournalComponent } from './journal/journal.component';
import { JournalFormComponent } from './journal/journal-form.component';
import { CommonModule } from '@angular/common';
import { AccountComponent } from './account/account.component';
import { LedgerComponent } from './ledger/ledger.component';
import { ReportsComponent } from './reports/reports.component';
import { TrialBalanceComponent } from './trial-balance/trial-balance.component';
import { BalanceSheetComponent } from './balance-sheet/balance-sheet.component';
import { IncomeStatementComponent } from './income-statement/income-statement.component';
import { AboutComponent } from './about/about.component';

const routes = [
  { path: "", redirectTo: "/dashboard", pathMatch: "full" },
  { path: 'coa', component: AccountComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'company', component: CompanyComponent },
  { path: 'journal', component: JournalComponent },
  {path: 'journal/:id', component: JournalFormComponent},
  { path: 'ledger', component: LedgerComponent },
  { path: 'reports', component: ReportsComponent },
  { path: 'about', component: AboutComponent }

]

@NgModule({
  declarations: [
    AppComponent,
    CompanyComponent,
    StatisticsComponent,
    DashboardComponent,
    JournalComponent,
    JournalFormComponent,
    AccountComponent,
    LedgerComponent,
    ReportsComponent,    
    TrialBalanceComponent,
    BalanceSheetComponent,
    IncomeStatementComponent,
    AboutComponent
    
  ],
  imports: [
    BrowserModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes),
    BrowserAnimationsModule,
    PanelModule,
    CardModule,
    MenuModule,
    InputTextModule,
    InputNumberModule,
    ButtonModule,
    FieldsetModule,
    TableModule,
    TreeTableModule,
    DialogModule,
    CalendarModule,
    CheckboxModule,
    DropdownModule,
    ToastModule,
    TabViewModule,
    ChartModule    
  ],
  providers: [MessageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
