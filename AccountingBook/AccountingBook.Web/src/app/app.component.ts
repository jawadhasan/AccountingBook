import { Component } from '@angular/core';
import {ApiService} from './services/api.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'accounting-app';

  //injected the service
  constructor(public apiService: ApiService){}

  ngOnInit(){
   
  }
}
