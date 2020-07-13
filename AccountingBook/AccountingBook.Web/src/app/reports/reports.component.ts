import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {

  constructor(public apiService: ApiService) { }

  ngOnInit(): void {
   
  }

  handleChange(e) {
    var index = e.index;
    console.log('tabIndex: ',index)
}
}
