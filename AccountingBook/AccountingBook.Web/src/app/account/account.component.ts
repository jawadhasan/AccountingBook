import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import {TreeNode} from 'primeng/api';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {
  coa: TreeNode[];
  // accounts: any []=[];

  constructor(public apiService: ApiService) { }

  ngOnInit(): void {

    //get data from server
    this.apiService.getCoa().subscribe((res: any) => {
     // console.log('coa: ', res);
     // this.accounts = res;
      this.coa = res;
      console.log(this.coa);
    });
  }

}
