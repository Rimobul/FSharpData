namespace Library

open System.Diagnostics
module Timing = 

    (*
    #time
    *)

    let f = 
        fun () -> 
            for i in 1..100 do printfn "%d" i

    f()

    // Higher order function for timing
    let timeAFunction func = 
        let stopwatch  = Stopwatch.StartNew()
        let result = func()
        stopwatch.Stop()
        (stopwatch.ElapsedMilliseconds, result)

    timeAFunction f    