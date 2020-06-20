import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ApiService {

  messages:any= [];

  constructor(private http: HttpClient) { }

  getData() {
    return this.http.get('/api/WeatherForecast').subscribe(res => {
      this.messages = res;
    });
  }

}
