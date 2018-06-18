namespace Library

open System
open System.IO
open System.Globalization

module LoanPayments = 
    let lines = 
        File.ReadAllLines(@"../../data/data.csv")
        |> Array.distinct
        |> Array.map (fun str -> str.Split(','))

    let header = lines |> Array.take 1
    let data = lines |> Array.skip 1

    
    [<Measure>] type dollar
    [<Measure>] type days
    [<Measure>] type age
    [<Measure>] type terms

    type LoanStatus = 
        | PaidOff of PaidOffTime: DateTime
        | Collection of PastDueDays : int<days>
        | CollectionPaidOff of PaidOffTime : DateTime * PastDueDays : int<days>
  
    type Education = 
        | HighSchoolOrBelow
        | College
        | MasterOrAbove

    type Gender = 
        | Male
        | Female

    type LoanPaymentData = 
        {
            LoanId: string;
            LoanStatus: LoanStatus;
            Principal: int<dollar>;
            Terms: int<terms>;
            EffectiveDate: DateTime;
            DueDate: DateTime;
            Age: int<age>;
            Education: Education;
            Gender: Gender;
        }

    let dateTimeParseAdapter format provider date = 
        DateTime.ParseExact(date, format, provider)

    let parseUSDate date = 
        dateTimeParseAdapter "M/d/yyyy" CultureInfo.InvariantCulture date

    let parseUSDateTime date = 
        try 
            dateTimeParseAdapter "M/d/yyyy H:m" CultureInfo.InvariantCulture date
        with 
            | err -> (printfn "Error date %s" date); raise err

    let mapToLoanStatus (status, paidOffTime, pastDueDays) =
        match status with
        | "PAIDOFF"
            -> PaidOff(parseUSDateTime paidOffTime)
        | "COLLECTION"
            -> Collection(Int32.Parse(pastDueDays) * 1<days>)
        | "COLLECTION_PAIDOFF"
            -> CollectionPaidOff(parseUSDateTime paidOffTime, Int32.Parse(pastDueDays) * 1<days>)
        | unknown
            -> failwith (sprintf "Unrecognized loan status %s" unknown)

    let mapToEducation education = 
        match education with
        | "High School or Below"
            -> HighSchoolOrBelow
        | "college"
        | "Bechalor"
            -> College
        | "Master or Above"
            -> MasterOrAbove
        | unknown
            -> failwith (sprintf "Unrecognized education %s" unknown)        

    let mapToGender gender = 
        match gender with
        | "male"
            -> Male
        | "female"
            -> Female
        | _
            -> failwith (sprintf "We do not support other genders")

    let mapToLoanPaymentData (data: string array) = 
        {
            LoanId = data.[0];
            LoanStatus = mapToLoanStatus (data.[1], data.[6], data.[7]);
            Principal = Int32.Parse(data.[2]) * 1<dollar>;
            Terms = Int32.Parse(data.[3]) * 1<terms>;
            EffectiveDate = parseUSDate data.[4];
            DueDate = parseUSDate data.[5];
            Age = Int32.Parse(data.[8]) * 1<age>;
            Education = mapToEducation data.[9];
            Gender = mapToGender data.[10];
        }

    let paymentData = 
        data
        |> Array.map mapToLoanPaymentData    