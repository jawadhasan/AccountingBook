import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ApiService {

  messages: any = [];

  constructor(private http: HttpClient) { }

  getData() {
    return this.http.get('/api/WeatherForecast').subscribe(res => {
      this.messages = res;
    });
  }

  getCompanyData() {
    return this.http.get('/api/company');
  }

  saveCompanyData(company) {
    return this.http.post('/api/company', company).subscribe(res => {
      console.log(res);
    }, err => {
      console.error(err);
    })
  }
 

}
