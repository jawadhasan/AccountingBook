import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import {TreeNode, MessageService} from 'primeng/api';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {
  coa: TreeNode[];

  constructor(public apiService: ApiService,
    private messageService: MessageService) { }

  ngOnInit(): void {

    //get data from server
    this.apiService.getCoa().subscribe((res: any) => {
      this.coa = res;
      console.log(this.coa);
    }, err => {
      this.messageService.add({severity:'error', summary: 'Error Message', detail:err.message});

    });
  }

  addForm(){
    console.log('add-new form');
    this.messageService.add({severity:'info', summary: 'Info Message', detail:'TODO: implementation'});
  }

}
