import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ApiService {

  messages: any = [];

  constructor(private http: HttpClient) { }

  getData() {
    return this.http.get('/api/WeatherForecast').subscribe(res => {
      this.messages = res;
    });
  }

  getCompanyData() {
    return this.http.get('/api/company');
  }

  saveCompanyData(company) {
    return this.http.post('/api/company', company).subscribe(res => {
      console.log(res);
    }, err => {
      console.error(err);
    })
  }


  getPostingAccounts(){
    return this.http.get('/api/lookup/postingAccounts');
  }

  getJournalEntries(includePosted){
    if(includePosted){
      return this.http.get('/api/journal');
    }else{
      return this.http.get('/api/journal/off');
    }
    
  }

  getJournal(id: number) {
    return this.http.get('/api/journal/'+id);    
  }

  saveJournal(journal) {
    return this.http.post('/api/journal/saveJournal', journal);
  }


  deleteJournal(id) {
    return this.http.delete('/api/journal/'+id);
  }
  
  
  postJournal(id) {
    return this.http.post('/api/journal/postJournal/'+id,null);
  }

  getCoa() {
    return this.http.get('/api/accounts');    
  }



  //Ledger

  getLedgerEntries(){
    return this.http.get('/api/ledger');
  }


  //Reports
  
  getTrialBalance(){
    return this.http.get('/api/trialBalance');
  }

  
  getBalanceSheet(){
    return this.http.get('/api/balanceSheet');
  }

  getIncomeStatement(){
    return this.http.get('/api/incomeStatement');
  }

}
