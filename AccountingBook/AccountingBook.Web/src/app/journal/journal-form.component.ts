import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormArray, FormBuilder, Validators } from '@angular/forms';
import { JournalLine, Journal } from '../models/journal';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'journal-form',
  templateUrl: './journal-form.component.html',
  styleUrls: ['./journal-form.component.css']
})
export class JournalFormComponent implements OnInit {

  operationText = 'Insert';

  journalEntry: Journal = {
    id: 0,
    date: new Date(),
    referenceNo: 'default ref',
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

  //get-only property
  get lines(): FormArray {
    return <FormArray>this.JournalEntryForm.get("lines")
  }

  constructor(private router: Router,
    private route: ActivatedRoute, private fb: FormBuilder) { }

  ngOnInit(): void {
  
    this.JournalEntryForm = this.fb.group({
      id: 0,
      date: [this.journalEntry.date, [Validators.required]],
      referenceNo: [this.journalEntry.referenceNo],
      posted: this.journalEntry.posted,
      lines: this.fb.array([this.buildLine()])
    })

    const id = this.route.snapshot.params['id'];
    if (id !== '0') {
      this.operationText = 'Update';
      this.getJournal(id);
    }

  }

  getJournal(id: string) {
    // this.companyServie.getCompany(id)
    //   .subscribe((data: Envelop<ICompany>) => {
    //       this.company = data.result;
    //     },
    //   (err: any) => console.log(err),
    //     () => console.log(this.company)
    //     );
    console.log(id);
  }

  backToList() {   
    this.router.navigate(['/journal']);
  }

  saveEntry() {
    // const p = { ...this.journalEntry, ...this.JournalEntryForm.value };

    //header mapping
    this.journalEntry.id = this.JournalEntryForm.value.id;
    this.journalEntry.date = this.JournalEntryForm.value.date;
    this.journalEntry.referenceNo = this.JournalEntryForm.value.referenceNo;

    //lines mapping
    this.JournalEntryForm.value.lines.map(l => {

      const line: JournalLine = {
        id: 0,//temp value
        accountId: l.accountId.id,
        drCrId: l.drcrId.id,
        lineAmount: l.lineAmount,
        lineMemo: l.lineMemo

      };

      this.journalEntry.lines.push(line);
    });

    console.log('dataToSave', this.journalEntry);
    console.log('TODO: AJAX CALL');
    this.operationText = 'Journal Saved!';
   // this.backToList();
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
      id:0,
      accountId: [this.accounts[0]],
      drcrId: [this.drcr[0]],
      lineAmount: 1,
      lineMemo: ''
    })
  }

   //Private (not in use)

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
