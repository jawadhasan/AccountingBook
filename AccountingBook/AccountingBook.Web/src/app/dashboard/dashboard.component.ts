import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';

const DEFAULT_COLORS = ['#3366CC', '#DC3912', '#FF9900', '#109618', '#990099',
  '#3B3EAC', '#0099C6', '#DD4477', '#66AA00', '#B82E2E',
  '#316395', '#994499', '#22AA99', '#AAAA11', '#6633CC',
  '#E67300', '#8B0707', '#329262', '#5574A6', '#3B3EAC']

@Component({
  selector: 'ab-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  
  stats:any=[];

  hoursByProject = [
    { id: 1, name: 'Payroll App', hoursSpent: 8 },
    { id: 2, name: 'Agile Times App', hoursSpent: 16 },
    { id: 3, name: 'Point of Sale App', hoursSpent: 24 },
  ];

 

  //transformations
  pieLabels = this.hoursByProject.map((proj) => proj.name);
  pieData = this.hoursByProject.map((proj) => proj.hoursSpent);
  pieColors = this.configureDefaultColours(this.pieData);


  private configureDefaultColours(data: number[]): string[] {
    let customColours = []
    if (data.length) {

      customColours = data.map((element, idx) => {
        return DEFAULT_COLORS[idx % DEFAULT_COLORS.length];
      });
    }

    return customColours;
  }


  hoursByProjectChartDataOrg = {
    labels: ["Payroll App","Agile Times App","Point of Sale App"],
    datasets:[
      
      {data:[8,16,24],
      backgroundColor:['#00ACAC','#2F8EE5','#6C76AF']}
    ]
  }

  hoursByProjectChartData = {
    labels: this.pieLabels,
    datasets:[
      
      {data:this.pieData,
      backgroundColor:this.pieColors}
    ]
  }

  hoursByTeamChartData = {

    labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
    datasets: [
      {
        label: 'Dev Team',
        backgroundColor: DEFAULT_COLORS[0],
        data: [65, 59, 80, 55, 67, 73],
        fill:false
      },
      {
        label: 'Ops Team',
        backgroundColor: DEFAULT_COLORS[1],
        data: [44, 63, 57, 90, 77, 70]
      }
    ]

  }
  constructor(public apiService: ApiService) { }

  ngOnInit(): void {

    this.stats = [
      {icon:"fa fa-line-chart",label:"Assets",value:"81,132",colour:"#00ACAC"},
      {icon:"fa fa-bar-chart",label:"Liabilities",value:"27,835",colour:"#2F8EE5"},
      {icon:"fa fa-pie-chart",label:"Revenue",value:"7,763",colour:"#6C76AF"},
      {icon:"fa fa-area-chart",label:"Expenses",value:"4,456",colour:"#EFA64C"},
      // {icon:"fa fa-file-text",label:"Equity",value:"8,456",colour:"#8BA39C"}
    ];
  }


  refresh(){
    console.log('refresh');
  }

}
