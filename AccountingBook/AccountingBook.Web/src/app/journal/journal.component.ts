import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';


export interface Journal {
  id: number;
  date: Date;
  referenceNo: string; 
  readyForPosting: boolean,
  posted: boolean,
  lines?: JournalLine[];
}

export interface JournalLine {
  id: number;
  accountId: number,
  drCrId: number,
  lineAmount: number,
  lineMemo: string
}

@Component({
  selector: 'journal',
  templateUrl: './journal.component.html',
  styleUrls: ['./journal.component.css']
})
export class JournalComponent implements OnInit {
  display: boolean = false;
  dialogTitle = "";

  journalEntry: Journal = {
    id: 0,
    date: new Date(),
    referenceNo:'default ref',      
    posted: false,
    readyForPosting: false,
    lines: []
  };
  JournalEntryForm: FormGroup;

  accounts: any = [
    { id: 1, name: 'Account-1' },
    { id: 2, name: 'Account-2' },
    { id: 3, name: 'Account-3' },
    { id: 4, name: 'Account-4' }
  ]

  drcr: any = [
    { id: 1, name: 'Debit' },
    { id: 2, name: 'Credit' }
  ]

  journalEntries = [
    { id: 1, date: new Date('2020-02-13'), referenceNo:'Ref1', debit: 13000, credit: 13000, readyForPosting: true, posted: true },
    { id: 2, date: new Date('2020-02-13'),referenceNo:'Ref2', debit: 12000, credit: 11000, readyForPosting: false, posted: false },
    { id: 3, date: new Date('2020-02-13'),referenceNo:'Ref3', debit: 5000, credit: 5000, readyForPosting: true, posted: false }
  ]

  //get-only property
  get lines(): FormArray {
    return <FormArray>this.JournalEntryForm.get("lines")
  }

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.JournalEntryForm = this.fb.group({
      id: 0,
      date: [this.journalEntry.date, [Validators.required]],
      referenceNo: [this.journalEntry.referenceNo],
      posted: this.journalEntry.posted,     
      lines: this.fb.array([this.buildLine()])
    })
  }


  editEntry(index: number) {
    console.log('edit journal', index);
  }

  deleteEntry(index: number) {
    console.log('delete journal', index);
  }

  postEntry(index: number) {
    console.log('post journal', index);
  }




  //Line Actions

  addLine(): void {
    this.lines.push(this.buildLine());
  }

  deleteLine(index: number): void {
    console.log(index);

    if (this.lines.length == 1) {
      //we want to keep one-line atleast all the time.
      this.lines.clear();
      this.addLine();

    } else {
      this.lines.removeAt(index);
    }
    this.lines.markAsDirty();
  }

  private buildLine(): FormGroup {
    return this.fb.group({
      // accountId: '',
      // drcrId: [{'id':0, 'name':'Select Type'}],
      accountId: [this.accounts[0]],
      drcrId: [this.drcr[0]],
      lineAmount: 1,
      lineMemo: ''
    })
  }



  //UI Actions
  closeDialog() {
    this.display = false;
  }


  saveEntry() {
    // const p = { ...this.journalEntry, ...this.JournalEntryForm.value };
    console.log(this.JournalEntryForm.value);

    //header mapping
    this.journalEntry.id = this.JournalEntryForm.value.id;
    this.journalEntry.date = this.JournalEntryForm.value.date;
    this.journalEntry.referenceNo = this.JournalEntryForm.value.referenceNo;

    //lines mapping
   this.JournalEntryForm.value.lines.map(l=>{    

      const line:JournalLine = {
        id:0,//temp value
        accountId: l.accountId.id,
        drCrId: l.drcrId.id,
        lineAmount: l.lineAmount,
        lineMemo: l.lineMemo

      };

      this.journalEntry.lines.push(line);
    });

    console.log('dataToSave', this.journalEntry);
    console.log('TODO: AJAX CALL');
    this.closeDialog();
  }

  showAddDialog() {
    this.resetForm();
    this.dialogTitle = 'Add New Journal Entry';
    this.display = true;
  }

  showEditDialog(id) {

    this.dialogTitle = 'Journal Entry';

    //TODO: ajaxCall getData
    //TODO: patch JournalEntryForm

    this.display = true;
    //   this.JournalEntryForm.setControl('lines', this.fb.array([]));//this.fb.array(this.product.tags || [])
  }


  //Private

  private resetForm(): void {

    this.JournalEntryForm.reset();
    this.resetLines();

    //initialize  
    this.JournalEntryForm.patchValue({
      date: new Date(),
      id: 0,
      posted: false

    });


  }


  private resetLines(): void {
    //we want to keep one-line atleast all the time.
    this.lines.clear();
    this.addLine();
  }

}
