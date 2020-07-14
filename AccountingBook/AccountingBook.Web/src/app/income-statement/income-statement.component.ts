import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'income-statement',
  templateUrl: './income-statement.component.html',
  styleUrls: ['./income-statement.component.css']
})
export class IncomeStatementComponent implements OnInit {

  incomeStatement: any[] = [];

  
  getSumRevenue() {
    return this.incomeStatement
    .filter(item => !item.isExpense)
    .map(v => v.amount)
    .reduce((acc, score) => acc + score, 0);
  } 

  getSumExpense() {
    return this.incomeStatement
    .filter(item => item.isExpense)
    .map(v => v.amount)
    .reduce((acc, score) => acc + score, 0);
  } 

  constructor(public apiService: ApiService) { }

  ngOnInit(): void {
     //get data from server
     this.apiService.getIncomeStatement().subscribe((res: any) => {
      console.log('income-statement', res);
      this.incomeStatement = res;
    }, err => {
      console.log(err);
    })
  }

}
