import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormArray, FormBuilder, Validators } from '@angular/forms';
import { JournalLine, Journal } from '../models/journal';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'journal-form',
  templateUrl: './journal-form.component.html',
  styleUrls: ['./journal-form.component.css']
})
export class JournalFormComponent implements OnInit {

  operationText = 'Insert';

  journalEntry: Journal;
  JournalEntryForm: FormGroup;

  //drop-downs
  accounts: any = [];
  drcr: any = [
    { id: 1, name: 'Debit' },
    { id: 2, name: 'Credit' }
  ]

  //get-only property
  get lines(): FormArray {
    return <FormArray>this.JournalEntryForm.get("lines")
  }

  constructor(private router: Router, private route: ActivatedRoute,
    private fb: FormBuilder,
    public apiService: ApiService) { }

  ngOnInit(): void {

    //data-model
    this.journalEntry = {
      id: 0,
      date: new Date(),
      referenceNo: '',
      posted: false,
      readyForPosting: false,
      lines: []
    };

    //form-model
    this.JournalEntryForm = this.fb.group({
      id: 0,
      date: [this.journalEntry.date, [Validators.required]],
      referenceNo: [this.journalEntry.referenceNo],
      posted: this.journalEntry.posted,
      lines: this.fb.array([])
      //lines: this.fb.array([this.buildLine()])
    })

    const id = this.route.snapshot.params['id'];

    //get accounts-data from server
    this.apiService.getPostingAccounts().subscribe((res: any) => {
      this.accounts = res;

      if (id !== '0') {
        this.operationText = 'Update';
        console.log('idcalling');
        this.getJournal(id);
      }
    });



  }

  getJournal(id: number) {
    console.log('loading journal: ', id);
    let that = this;

    this.apiService.getJournal(id).subscribe((res: any) => {

      if (this.JournalEntryForm) {
        this.JournalEntryForm.reset();
      }
      //update data-model
      this.journalEntry = res;

      console.log('loaded journal:',this.journalEntry);

      //update form-model
      this.JournalEntryForm.patchValue({
        id: res.id,
        date: new Date(res.date),
        referenceNo: res.referenceNo,
        posted: res.posted
      });

      //lines mapping
      this.journalEntry.lines.map(l => {       
        this.addGroupLine(l);
      });

    });

  }

  backToList() {
    this.router.navigate(['/journal']);
  }

  saveEntry() {

    console.log('dataToSafe: ', this.JournalEntryForm.value)

    this.apiService.saveJournal(this.JournalEntryForm.value).subscribe(res => {
      this.backToList();
    }, err => {
      console.error(err);
      this.operationText = err.error;
    })

  }


  //Line Actions

  addLine(): void {
    this.lines.push(this.buildLine());
  }

  private buildLine(): FormGroup {
    return this.fb.group({
      id: 0,
      accountId: this.accounts[0].id,
      drCrId: this.drcr[0].id,
      amount: 1,
      memo: ''
    })
  }

  deleteLine(index: number): void {

    if (this.lines.length == 1) {
      this.lines.clear();
      //if we want to keep one-line atleast all the time.
      //this.addLine();

    } else {
      this.lines.removeAt(index);
    }
    this.lines.markAsDirty();
  }
  ///////////////////////////


  private addGroupLine(line) {
    this.lines.push(this.buildGroupLineFromLine(line))
  }
  private buildGroupLineFromLine(line): FormGroup {
    return this.fb.group({
      id: line.id,
      accountId: line.accountId,
      drCrId: line.drCrId,
      amount: line.amount,
      memo: line.memo
    })
  }




}
