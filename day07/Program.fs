﻿open Xunit 
open FsUnit.Xunit

let part1 (equations: (int64 * int64 seq) seq) =
    let rec collectResult a acc =
        match a with 
        | [] -> acc
        | h :: t -> 
            let newAcc = acc |> List.collect (fun s -> [s + h; s * h])
            collectResult t newAcc

    equations
    |> Seq.filter (fun (s, a) -> 
        let a = List.ofSeq a
        collectResult (List.tail a) [ List.head a ] |> List.contains s)
    |> Seq.sumBy fst


let parse (input: string) = 
    input.Split("\n")
    |> Seq.map (fun line -> 
        let line = line.Split(": ")
        (int64 line[0], Seq.map int64 (line[1].Split(" "))))


module Example = 
    let input = 
        "190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20"

    [<Fact>]
    let testPart () = 
        parse input |> part1 |>  should equal 3749L

open System.Diagnostics

[<EntryPoint>]
let main _ =
    let input = stdin.ReadToEnd().TrimEnd()
    let equations = parse input

    let stopwatch: Stopwatch = Stopwatch.StartNew()

    equations |> part1 |> printfn "Part 1: %d"

    stopwatch.Stop()
    printfn $"Elapsed time: %A{stopwatch.Elapsed}"

    0