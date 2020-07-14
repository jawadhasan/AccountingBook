import { Component, ViewChild } from '@angular/core';
import {ApiService} from './services/api.service'
import { MenuItem, Menu } from 'primeng';
import { Router } from '@angular/router';

declare var jQuery :any;


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'accounting-app';
  menuItems: MenuItem[];
  miniMenuItems: MenuItem[];

  @ViewChild('bigMenu') bigMenu : Menu;
  @ViewChild('smallMenu') smallMenu : Menu;

  //injected the service
  constructor(private router : Router,public apiService: ApiService){}

  ngOnInit(){

    let handleSelected = function(event) {
      let allMenus = jQuery(event.originalEvent.target).closest('ul');
      let allLinks = allMenus.find('.menu-selected');

      allLinks.removeClass("menu-selected");
      let selected = jQuery(event.originalEvent.target).closest('a');
      selected.addClass('menu-selected');
      console.log('selectedMenu: ', selected);
    }

    this.menuItems = [
      {label: 'Dashboard', icon: 'fa fa-dashboard', routerLink: ['/dashboard'], command: (event) => handleSelected(event)},
      {label: 'COA', icon: 'fa fa-sitemap', routerLink: ['/coa'], command: (event) => handleSelected(event)},
      {label: 'Journal', icon: 'fa fa-table', routerLink: ['/journal'], command: (event) => handleSelected(event)},
      {label: 'Ledger', icon: 'fa fa-calculator', routerLink: ['/ledger'], command: (event) => handleSelected(event)},
      {label: 'Reports', icon: 'fa fa-bookmark', routerLink: ['/reports'], command: (event) => handleSelected(event)},
      {label: 'Company', icon: 'fa fa-building', routerLink: ['/company'], command: (event) => handleSelected(event)},
      {label: 'About', icon: 'fa fa-info-circle', routerLink: ['/about'], command: (event) => handleSelected(event)},
    ]

    this.miniMenuItems = [];
    this.menuItems.forEach( (item : MenuItem) => {
      let miniItem = { icon: item.icon, routerLink: item.routerLink }
      this.miniMenuItems.push(miniItem);
    })
   
  }

  selectInitialMenuItemBasedOnUrl() {
    let path = document.location.pathname;
    let menuItem = this.menuItems.find( (item) => { return item.routerLink[0] == path });
    if (menuItem) {
      let iconToFind = '.' + menuItem.icon.replace('fa ', ''); // make fa fa-home into .fa-home
      let selectedIcon = document.querySelector(`${iconToFind}`);
      jQuery(selectedIcon).closest('li').addClass('menu-selected');
    }
  }

  ngAfterViewInit() {
    this.selectInitialMenuItemBasedOnUrl();
  }

}
