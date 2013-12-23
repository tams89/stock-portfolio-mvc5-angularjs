module AlgoTrader.BlackScholesMonteCarlo

open System
open System.Diagnostics
open MathNet

let N = 1000000 // number of interations
let rnd = new MathNet.Numerics.Random.MersenneTwister(1)
let randomPairs = List.init N (fun i -> (rnd.NextDouble(), rnd.NextDouble()))
let boxMullerTransform (u1, u2) = 
    sqrt (-2.0 * log (u1)) * cos (2.0 * Math.PI * u2)
let randomNormals = randomPairs |> List.map boxMullerTransform
let callPayout k s = max (s - k) 0.0
let putPayout k s = max (k - s) 0.0

let europeanOptionPrice (payout : float -> float) s0 r vol t = 
    let sT x = s0 * exp ((r - 0.5 * (vol ** 2.0)) * t + vol * sqrt (t) * x)
    randomNormals
    |> List.map sT
    |> List.map payout
    |> List.average
    |> (*) (exp (-r * t))

type optionType = Call | Put

/// k - Strike Price
/// s0 - Spot Price/Last Price/Closing Price
/// r - risk free interest rate
/// vol - volatility.
/// t - time to expiry in years
let europeanOptionPriceBlackScholes optionType k s0 r vol t = 
    match optionType with
        | Call -> europeanOptionPrice (callPayout k) s0 r vol t
        | Put -> europeanOptionPrice (putPayout k) s0 r vol t



/// Example code

//let s0 = 50.0
//let k = 40.0
//let r = 0.01
//let vol = 0.2
//let t = 0.25
//
//let price() = 
//    let price = europeanOptionPrice (callPayout k) s0 r vol t
//    printfn "option price = %f" price
//
//let time f = 
//    let proc = Process.GetCurrentProcess()
//    let cpu_time_stamp = proc.TotalProcessorTime
//    let timer = new Stopwatch()
//    timer.Start()
//    try
//        f()
//    finally
//        let cpu_time = (proc.TotalProcessorTime-cpu_time_stamp).TotalMilliseconds
//        printfn "CPU time = %dms" (int64 cpu_time)
//        printfn "Absolute time = %dms" timer.ElapsedMilliseconds
//
//time price