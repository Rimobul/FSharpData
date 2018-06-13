namespace App

open System
open Library

module Program = 
    [<EntryPoint>]
    let main argv =
        LoanPayments.paymentData
        |> Array.map (fun data -> Console.WriteLine data)
        |> ignore
        0 // return an integer exit code
