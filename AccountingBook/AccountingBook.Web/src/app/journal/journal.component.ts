import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'journal',
  templateUrl: './journal.component.html',
  styleUrls: ['./journal.component.css']
})
export class JournalComponent implements OnInit {

  journalEntries = [
    { id: 1, date: new Date('2020-02-13'), referenceNo:'Ref1', debit: 13000, credit: 13000, readyForPosting: true, posted: true },
    { id: 2, date: new Date('2020-02-13'),referenceNo:'Ref2', debit: 12000, credit: 11000, readyForPosting: false, posted: false },
    { id: 3, date: new Date('2020-02-13'),referenceNo:'Ref3', debit: 5000, credit: 5000, readyForPosting: true, posted: false }
  ]

  constructor(private router: Router) { }

  ngOnInit(): void {  
  }

  addForm() {
    this.router.navigate(['/journal', '0']);
  }

  editForm(index: number) {
    this.router.navigate(['/journal', index]);
  }

  deleteEntry(index: number) {
    console.log('delete journal', index);
  }

  postEntry(index: number) {
    console.log('post journal', index);
  }


 

}
