namespace App

open System
open Library

module Program = 
    [<EntryPoint>]
    let main argv =
        Say.hello "world"
        0 // return an integer exit code
