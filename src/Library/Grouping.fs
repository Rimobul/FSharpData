namespace Library

module Grouping = 
    let unfoldExample () = 
        1 
        |> Seq.unfold( fun n -> 
            if n < 300
                then Some(n, n + n)
                else None)
        |> Seq.iter(printf "%d ")

    let fibonacci () =
        (1, 0)
        |> Seq.unfold( fun (m, n) -> Some(m + n, (n, m + n)))
        |> Seq.take 15
        |> Seq.iter(printf "%d ")

    let someNumbers = [1..5]
    let moreNumbers = [6..10]
    let stillMoreNumbers = [11..15]
    let combinedNumbers = List.append someNumbers moreNumbers

    let squareAndCube n =
        [
            n * n
            n * n * n
        ]

    combinedNumbers
    |> List.collect squareAndCube
    |> List.iter (printf "%d ")

    let zippedList = List.zip someNumbers moreNumbers
    let unzippedList = List.unzip zippedList
    let zipped3 = List.zip3 someNumbers moreNumbers stillMoreNumbers
    let unzipped3 = List.unzip3 zipped3

    // The lists have to be the same size
    let notZippedList = List.zip someNumbers combinedNumbers
    let zippedSeq = Seq.zip someNumbers moreNumbers

    zippedSeq
    |> Seq.iter(fun duo -> duo.ToString() |> printf("%s "))

    someNumbers |> List.pairwise |> ignore 
    someNumbers |> List.windowed 2 |> ignore
    someNumbers |> List.windowed 3 |> ignore

    let petBreeds = 
        [
            ("Cat", "Persian")
            ("Dog", "Collie")
            ("Cat", "Russian Blue")
            ("Bird", "Canary")
            ("Dog", "Corgie")
            ("Cat", "Siamese")
        ]

    petBreeds
    |> List.groupBy fst
    |> ignore