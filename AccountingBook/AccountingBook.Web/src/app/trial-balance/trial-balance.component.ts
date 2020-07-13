import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'trial-balance',
  templateUrl: './trial-balance.component.html',
  styleUrls: ['./trial-balance.component.css']
})
export class TrialBalanceComponent implements OnInit {
  
  trialbalance: any []=[];

  getSum(drcr){

    // this.ledgers.map( v => console.log(v.debit));  
    const sum = this.trialbalance
    .map( v => v[drcr] )
    .reduce( (sum, current) => sum + current, 0 );
    return sum;
  }
 
  constructor(public apiService: ApiService) { }

  ngOnInit(): void {
    //get data from server
    this.apiService.getTrialBalance().subscribe((res: any) => {
     console.log('trial-balance',res);
     this.trialbalance = res;
   }, err => {
    console.log(err);

   });

  }

}
