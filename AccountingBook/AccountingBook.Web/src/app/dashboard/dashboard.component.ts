import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'ab-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  
  stats:any=[];

  constructor(public apiService: ApiService) { }

  ngOnInit(): void {

    this.stats = [
      {icon:"fa fa-line-chart",label:"Assets",value:"81,132",colour:"#00ACAC"},
      {icon:"fa fa-bar-chart",label:"Liabilities",value:"27,835",colour:"#2F8EE5"},
      {icon:"fa fa-pie-chart",label:"Revenue",value:"7,763",colour:"#6C76AF"},
      {icon:"fa fa-area-chart",label:"Expenses",value:"4,456",colour:"#EFA64C"},
      {icon:"fa fa-book",label:"Equity",value:"8,456",colour:"#8BA39C"}
    ];
  }

}
