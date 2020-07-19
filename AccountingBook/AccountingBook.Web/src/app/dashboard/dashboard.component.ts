import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';

const DEFAULT_COLORS = ['#3366CC', '#DC3912', '#FF9900', '#109618', '#990099',
  '#3B3EAC', '#0099C6', '#DD4477', '#66AA00', '#B82E2E',
  '#316395', '#994499', '#22AA99', '#AAAA11', '#6633CC',
  '#E67300', '#8B0707', '#329262', '#5574A6', '#3B3EAC']

const DEFAULT_ICONS = ['fa fa-line-chart', 'fa fa-bar-chart',
  'fa fa-pie-chart',
  'fa fa-area-chart'
]

@Component({
  selector: 'ab-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  stats: any = []; //for statistic component  
  accountsStats: any;// data form server
  balanceByAccountChartData: any; //pie, doughtnut chart data

  constructor(public apiService: ApiService) { }

  ngOnInit(): void {
    //get data from server
    this.loadData();
  }

  loadData() {
    //get data from server
    this.apiService.getAccountsSats()
      .subscribe((res: any) => {
        console.log('statsData: ', res);
        this.accountsStats = res;
        this.setupStatsData() //stats tiles
        this.setupChartData();  //pie, doughnut charts    
      }, err => console.log(err));
  }

  setupStatsData() {

    let iconsArray = this.configureDefaultIcons(this.accountsStats);
    let colorArray = this.configureDefaultColours(this.accountsStats);

    console.log('colorArray', iconsArray);

    this.stats = this.accountsStats.map((a, idx) => {
      return {
        label: a.accountName,
        value: a.balance,
        icon: iconsArray[idx],
        colour: colorArray[idx]
      }
    })

  }
  setupChartData() {
    let pieLabels = this.accountsStats.map((account) => account.accountName);
    let pieData = this.accountsStats.map((account) => account.balance);
    let pieColors = this.configureDefaultColours(pieData);

    this.balanceByAccountChartData = {
      labels: pieLabels,
      datasets: [
        {
          data: pieData,
          backgroundColor: pieColors
        }
      ]
    }
  }

  //Utitlity methods
  private configureDefaultColours(data: number[]): string[] {
    let customColours = []
    if (data.length) {

      customColours = data.map((element, idx) => {
        return DEFAULT_COLORS[idx % DEFAULT_COLORS.length];
      });
    }
    return customColours;
  }
  private configureDefaultIcons(data: number[]): string[] {
    let customIcons = []
    if (data.length) {

      customIcons = data.map((element, idx) => {
        return DEFAULT_ICONS[idx % DEFAULT_ICONS.length];
      });
    }
    return customIcons;
  }
}
