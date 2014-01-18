#I "../packages/RProvider.1.0.5/lib"
#r "RDotNet.dll"
#r "RDotNet.FSharp.dll"
#r "RDotNet.NativeLibrary.dll"
#r "RProvider.dll"

#I "../packages/FSharp.Data.1.1.10/lib/net40"
#r "FSharp.data.dll"

open System
open System.Net
open FSharp.Data
open RDotNet
open RProvider
open RProvider.``base``
open RProvider.stats
open RProvider.tseries
open RProvider.zoo
open RProvider.graphics

type Stocks = CsvProvider<"http://ichart.finance.yahoo.com/table.csv?s=SPX">
 
/// Returns prices of a given stock for a specified number 
/// of days (starting from the most recent)
let getStockPrices stock count =
  let url = "http://ichart.finance.yahoo.com/table.csv?s="
  [| for r in Stocks.Load(url + stock).Take(count).Data -> float r.Open |] 
  |> Array.rev

/// Get opening prices for MSFT for the last 255 days
let msftOpens = getStockPrices "MSFT" 255

// Retrieve stock price time series and compute returns
let msft = msftOpens |> R.log |> R.diff


// Build a list of tickers and get diff of logs of prices for each one
let tickers = 
  [ "MSFT"; "AAPL"; "X"; "VXX"; "SPX"; "GLD" ]
let data =
  [ for t in tickers -> 
      printfn "got one!"
      t, getStockPrices t 255 |> R.log |> R.diff ]

// Create an R data frame with the data and call 'R.pairs'
let df = R.data_frame(namedParams data)
R.pairs(df)