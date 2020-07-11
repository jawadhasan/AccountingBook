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

  journalEntry: Journal = {
    id: 0,
    date: new Date(),
    referenceNo: 'default ref',
    posted: false,
    readyForPosting: false,
    lines: []
  };
  JournalEntryForm: FormGroup;

  accounts: any=[];

  drcr: any = [
    { id: 1, name: 'Debit' },
    { id: 2, name: 'Credit' }
  ]

  //get-only property
  get lines(): FormArray {
    return <FormArray>this.JournalEntryForm.get("lines")
  }

  constructor(private router: Router,
    private route: ActivatedRoute, private fb: FormBuilder,public apiService: ApiService) { }

  ngOnInit(): void {
  
    this.JournalEntryForm = this.fb.group({
      id: 0,
      date: [this.journalEntry.date, [Validators.required]],
      referenceNo: [this.journalEntry.referenceNo],
      posted: this.journalEntry.posted,
      lines: this.fb.array([])

      // lines: this.fb.array([this.buildLine()])
    })

        //get data from server
        this.apiService.getPostingAccounts().subscribe((res: any)=>{
          this.accounts = res;
        });

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

   const p = {... this.JournalEntryForm.value};

    console.log(this.JournalEntryForm.value);
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
        amount: l.amount,
        memo: l.memo

      };

      this.journalEntry.lines.push(line);
    });

    console.log('dataToSave', this.journalEntry);

    this.apiService.saveJournal(this.journalEntry).subscribe(res => {
      console.log(res);
      this.backToList();
    }, err => {
      console.error(err);
      this.operationText = err.error;
      this.journalEntry.lines.splice(0, this.journalEntry.lines.length);
    })  
  
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
      amount: 1,
      memo: ''
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
