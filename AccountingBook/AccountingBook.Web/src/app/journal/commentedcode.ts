    //const p = { ...this.journalEntry, ...this.JournalEntryForm.value };
    //const p = {... this.JournalEntryForm.value};
    //console.log(this.JournalEntryForm.value);
    //console.log('pp',p);


    // this.journalEntry.id = this.JournalEntryForm.value.id;
    // this.journalEntry.date = this.JournalEntryForm.value.date;
    // this.journalEntry.referenceNo = this.JournalEntryForm.value.referenceNo;
    // this.journalEntry.lines = this.JournalEntryForm.value.lines;
    // console.log('dataToSave', this.journalEntry);

        //lines mapping
    // this.JournalEntryForm.value.lines.map(l => {

    //   console.log('map for line: ', l);
    //   const line: JournalLine = {
    //     id: l.id || 0,//temp value
    //     accountId: l.accountId.id,
    //     drCrId: l.drcrId.id,
    //     amount: l.amount,
    //     memo: l.memo
    //   };

    //   console.log('constline', line)
    //   this.journalEntry.lines.push(line);
    // });



    // let entryMap = this.JournalEntryForm.value.lines.map(function (l) {

    //     const line: JournalLine = {
    //       id: l.id || 0,//temp value
    //       accountId: l.accountId.id,
    //       drCrId: l.drcrId.id,
    //       amount: l.amount,
    //       memo: l.memo
    //     };
    //     return line;
    //   });
  
    //   console.log('entryMap ', entryMap);

          // this.journalEntry.lines.splice(0, this.journalEntry.lines.length);




          

    //   let tempLines = res.lines.map(function (entry) {
    //     let x = that.buildMapLine(entry)
    //     return x;
    //   });


    //   console.log('temp', tempLines);
    //   this.journalEntry.lines = tempLines;



     //Private (not in use)

//   private resetForm(): void {

//     this.JournalEntryForm.reset();
//     this.resetLines();

//     //initialize  
//     this.JournalEntryForm.patchValue({
//       date: new Date(),
//       id: 0,
//       posted: false

//     });


//   }

//   private resetLines(): void {
//     //we want to keep one-line atleast all the time.
//     this.lines.clear();
//     this.addLine();
//   }

      //  this.JournalEntryForm.setControl('lines', this.fb.array(this.journalEntry.lines || []));
     // this.lines.patchValue(this.journalEntry.lines || []);

