import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'balance-sheet',
  templateUrl: './balance-sheet.component.html',
  styleUrls: ['./balance-sheet.component.css']
})
export class BalanceSheetComponent implements OnInit {

  balanceSheet: any[] = [];
  accountTypes: any = [
    { id: 1, name: 'Assets' },
    { id: 2, name: 'Liabilities' },
    { id: 3, name: 'Equity' }
  ]

  getSumAmount(){

    // this.ledgers.map( v => console.log(v.debit));  
    const sum = this.balanceSheet
    .map( v => v['amount'] )
    .reduce( (sum, current) => sum + current, 0 );
    return sum;
  }

  constructor(public apiService: ApiService) { }

  ngOnInit(): void {
    //get data from server
    this.apiService.getBalanceSheet().subscribe((res: any) => {
      console.log('balance-sheet', res);
      this.balanceSheet = res;
    }, err => {

      console.log(err);
    })
  }

}
