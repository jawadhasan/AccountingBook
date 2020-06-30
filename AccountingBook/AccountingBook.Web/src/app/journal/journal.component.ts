import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'journal',
  templateUrl: './journal.component.html',
  styleUrls: ['./journal.component.css']
})
export class JournalComponent implements OnInit {

  display: boolean = false;
  companyForm: FormGroup;

  constructor(private fb: FormBuilder) { }




  journalEntries: any = [
    { id: 1, date: '2020-02-13', debit: 13000, credit: 13000, readyForPosting: true, posted: true },
    { id: 2, date: '2020-02-13', debit: 12000, credit: 11000, readyForPosting: false, posted: false },
    { id: 3, date: '2020-02-13', debit: 5000, credit: 5000, readyForPosting: true, posted: false }
  ]

  ngOnInit(): void {

    this.companyForm = this.fb.group({
      date: ['', [Validators.required]],
      referenceNo : ['', [Validators.required]],
      memo: ['', [Validators.maxLength(1000)]]
    });
  }


  closeDialog() {
    this.display = false;
  }
  saveEntry() {
    console.log('TODO: save');
  }
  showDialog() {
    this.display = true;
  }

}
