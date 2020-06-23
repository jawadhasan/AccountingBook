import {Component, OnInit, Input} from '@angular/core';

@Component({
  selector: 'ab-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.css']
})
export class StatisticsComponent implements OnInit {

  @Input() icon : string;
  @Input() label : string;
  @Input() value: string;
  @Input() colour: string;

  constructor() { }

  ngOnInit(): void {
  }

}
