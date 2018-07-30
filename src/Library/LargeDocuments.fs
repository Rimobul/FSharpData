namespace Library

open System.IO

module LargeDocuments = 
    let wikiFile = @""

    let readAllLines fileName = 
        fileName
        |> File.ReadAllLines
        |> Seq.ofArray

    let readLines fileName = 
        fileName 
        |> File.ReadLines

    let readSingleLines filePath = 
        seq {
            let rec readLine (reader: StreamReader) = 
                seq {
                    let line = reader.ReadLine()
                    if line <> null
                        then
                            yield line
                            yield! readLine reader
                }

            use stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None)
            let reader = new StreamReader(stream)
            yield! readLine reader
        }

    wikiFile |> readAllLines |> Seq.length |> printfn "%d" 
    wikiFile |> readLines |> Seq.length |> printfn "%d"
    wikiFile |> readSingleLines |> Seq.length |> printfn "%d"

    let readWordsInLine (line: string) = 
        line.Split([|' '; '_'; '\\'; '/'; ' '|])
        |> Seq.ofArray
        |> Seq.map(fun s -> s.Trim())

    let readWordsInFile fileName = 
        fileName 
        |> readLines
        |> Seq.collect readWordsInLine

    let wikiTitlesWords = readWordsInFile wikiFile  

    // 8:24      