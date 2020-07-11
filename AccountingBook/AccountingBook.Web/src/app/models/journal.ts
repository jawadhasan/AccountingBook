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