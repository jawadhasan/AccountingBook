import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';

@Component({
  selector: 'journal',
  templateUrl: './journal.component.html',
  styleUrls: ['./journal.component.css']
})
export class JournalComponent implements OnInit {

  display: boolean = false;
  JournalEntryForm: FormGroup;

  //get-only property
  get lines(): FormArray{
    return <FormArray> this.JournalEntryForm.get("lines")
  }

  accounts:any = [
    { id: 1, name: 'Account-1' },
    { id: 2, name: 'Account-2' },
    { id: 3, name: 'Account-3' },
    { id: 4, name: 'Account-4' }
  ]

  drcr:any = [
    { id: 1, name: 'Debit' },
    { id: 2, name: 'Credit' }  
  ]


  constructor(private fb: FormBuilder) { }

  journalEntries: any = [
    { id: 1, date: '2020-02-13', debit: 13000, credit: 13000, readyForPosting: true, posted: true },
    { id: 2, date: '2020-02-13', debit: 12000, credit: 11000, readyForPosting: false, posted: false },
    { id: 3, date: '2020-02-13', debit: 5000, credit: 5000, readyForPosting: true, posted: false }
  ]

  ngOnInit(): void {

    this.JournalEntryForm = this.fb.group({
      date: [new Date(), [Validators.required]],
      referenceNo : ['', [Validators.required]],
      posted : false,
      memo: ['', [Validators.maxLength(1000)]],

      lines: this.fb.array([ this.buildLine() ]) 
     
    });
  }


  addLine():void{
    this.lines.push(this.buildLine());
  }

  buildLine():FormGroup{
    return  this.fb.group({
      selectedAccount:'',
      selectedDrCr:'',
      lineAmount: 1,
      lineMemo:''
    })
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
