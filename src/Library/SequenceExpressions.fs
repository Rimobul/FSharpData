namespace Library

open System.IO

module SequenceExpressions = 
    let demo1 () =
        seq { 1..3 }
        |> Seq.iter (printfn "%d")

    let demo2 () = 
        seq { for i in 1..3 -> i*i }
        |> Seq.iter (printfn "%d")

    let demo3 () = 
        seq { for i in 1..3 do yield i*i }
        |> Seq.iter (printfn "%d")

    let demoNestedLoop () = 
        seq {
            for i in 1..3 do
                for j in 4..5 do
                    yield i + j
        }
        |> Seq.iter (printfn "%d")

    let rec listAllFiles (path: string) = 
        seq {
            for file in Directory.GetFiles path do
                yield file
            for directory in Directory.GetDirectories path do
                // expression after yield! evaluates into a sequence
                yield! listAllFiles directory                        
        }

    let readFileLineByLine (filePath: string) = 
        seq {
            let rec readLine (reader: StreamReader) = 
                seq {
                    if not reader.EndOfStream
                        then
                            yield reader.ReadLine()
                            yield! readLine reader
                }
            use stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None)
            let reader = new StreamReader(stream)
            yield! readLine reader
        }

    (*
        TRY THIS:
        copy the readLineByLine method into console
        run the following commands
            > let lines = readFileLineByLine @"./FSharpData/data/data.csv";;
            > let enumerator = lines.GetEnumerator;;
            > enumerator.MoveNext();;
            > enumerator.Current;;
        now, try to open the data.csv file manually
        run the following command
            > enumerator.Dispose();;
        try to open data.csv again        
    *)