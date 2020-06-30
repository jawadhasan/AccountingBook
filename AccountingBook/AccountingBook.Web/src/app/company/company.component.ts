import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.css']
})
export class CompanyComponent implements OnInit {

  company:any = {};

  companyForm: FormGroup;

   constructor(
     private fb: FormBuilder,
     public apiService: ApiService
     ){}
   
  ngOnInit(): void {  

    ///We call group method on form-builder, which is where we call to construct a new group of fields.

    this.companyForm = this.fb.group({
      //first '' is default value, and second is an array of validation constraints
      companyName: ['', [Validators.required]],
      shortName: ['', [Validators.required]],
      companyCode: ['', [Validators.required, Validators.maxLength(5)]]

    });

    //get data from server
    this.apiService.getCompanyData().subscribe((res: any)=>{

      this.companyForm.patchValue({
        companyName: res.companyName,
        shortName: res.shortName,
        companyCode: res.companyCode
      });

    });
  }

  save(){
   console.log(this.companyForm.value);
   this.apiService.saveCompanyData(this.companyForm.value);
  }

  
  hasFormErrors() {
    return !this.companyForm.valid;
  }

}
