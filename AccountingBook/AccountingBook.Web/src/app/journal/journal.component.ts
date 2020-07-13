import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'journal',
  templateUrl: './journal.component.html',
  styleUrls: ['./journal.component.css']
})
export class JournalComponent implements OnInit {

  journalEntries:any=[];

  constructor(private router: Router,public apiService: ApiService) { }

  ngOnInit(): void {  

    // this.journalEntries = [
    //   { id: 1, date: new Date('2020-02-13'), referenceNo:'Ref1', debit: 13000, credit: 13000, readyForPosting: true, posted: true },
    //   { id: 2, date: new Date('2020-02-13'),referenceNo:'Ref2', debit: 12000, credit: 11000, readyForPosting: false, posted: false },
    //   { id: 3, date: new Date('2020-02-13'),referenceNo:'Ref3', debit: 5000, credit: 5000, readyForPosting: true, posted: false }
    // ]

  //get data from server
  this.apiService.getJournalEntries().subscribe((res: any)=>{
    console.log('journal entries: ',res);
    this.journalEntries = res;
  });

  }

  addForm() {
    this.router.navigate(['/journal', '0']);
  }

  editForm(index: number) {
    this.router.navigate(['/journal', index]);
  }

  deleteEntry(id: number) {
    console.log('delete journal-id', id);
    this.apiService.deleteJournal(id).subscribe((res: any)=>{      
      //TODO: refresh from server
      let index = this.journalEntries.findIndex(x => x.id === id);
      this.journalEntries.splice(index, 1);
    });
  }

  postEntry(id: number) {
    console.log('posting journal-id', id);
    this.apiService.postJournal(id).subscribe((res: any)=>{    
      console.log(res);  
      //TODO: refresh from server
     // let index = this.journalEntries.findIndex(x => x.id === id);
     // this.journalEntries.splice(index, 1);
    });
  } 

}
