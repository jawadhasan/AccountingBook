import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.css']
})
export class CompanyComponent implements OnInit {

  company:any = {};

   //injected the service
   constructor(public apiService: ApiService){}
   
  ngOnInit(): void {
    this.apiService.getCompanyData().subscribe(res=>{
      this.company = res;
    });
  }

  save(){
    this.apiService.saveCompanyData(this.company);
  }

}
