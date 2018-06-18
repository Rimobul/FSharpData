namespace App

open System
open Library

module Program = 
    let writePaymentData () = 
        printf "Writing loan payment data..."
        LoanPayments.paymentData
        |> Array.map Console.WriteLine
        |> ignore

    let writeSequenceExpressions () =
        printfn "Writing sequence expressions..."
        SequenceExpressions.demo1()
        SequenceExpressions.demo2()
        SequenceExpressions.demo3()
        SequenceExpressions.demoNestedLoop()
        SequenceExpressions.listAllFiles @"../../"
            |> Seq.iter (printfn "%s")
        SequenceExpressions.readFileLineByLine @"./Program.fs"
            |> Seq.iter (printfn "%s")

    [<EntryPoint>]
    let main argv =
        // writePaymentData()
        writeSequenceExpressions()
        0 // return an integer exit code
