import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-ledger',
  templateUrl: './ledger.component.html',
  styleUrls: ['./ledger.component.css']
})
export class LedgerComponent implements OnInit {

  ledgers: any []=[];
  debitSum: number=0; 
  creditSum: number=0;
  
  getSum(drcr){

    // this.ledgers.map( v => console.log(v.debit));  
    console.log('sum method called.');
    const sum = this.ledgers
    .map( v => v[drcr] )
    .reduce( (sum, current) => sum + current, 0 );
    return sum;
  }

  constructor(public apiService: ApiService) { }

  ngOnInit(): void {

     //get data from server
     this.apiService.getLedgerEntries().subscribe((res: any) => {
      console.log('res: ', res);  
      this.ledgers = res; 
      this.debitSum = this.getSum('debit');
      this.creditSum = this.getSum('credit');
    
     });
  }

}
