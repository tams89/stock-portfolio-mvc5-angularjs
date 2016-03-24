module AlgoTrader.GoogleApi.Options

open Newtonsoft.Json
open System
open System.Net

let makeUrlOptions (expiryDate : DateTime option) ticker = 
    match expiryDate with
    | Some expiryDate -> 
        let day = expiryDate.Day.ToString()
        let month = expiryDate.Month.ToString()
        let year = expiryDate.Year.ToString()
        "http://www.google.com/finance/option_chain?q=" + ticker + "&expd=" + day + "&expm=" + month + "&expy=" + year 
        + "&output=json"
    | None -> "http://www.google.com/finance/option_chain?q=" + ticker + "&output=json"

type RawExpiry = 
    {y : string
     m : string
     d : string}

type RawOption = 
    {cid : string
     s : string
     e : string
     p : string
     c : string
     b : string
     a : string
     oi : string
     vol : string
     strike : string
     expiry : string
     name : string
     cs : string
     cp : string}

type RawOptionChain = 
    {Expiry : RawExpiry
     Expirations : RawExpiry []
     Calls : RawOption []
     Puts : RawOption []}

type Option = 
    {Symbol : string
     StrikePrice : Decimal
     Expiry : DateTime
     Bid : Decimal
     Ask : Decimal
     Vol : int
     OpenInt : int}

type OptionChain = 
    {Expiry : DateTime
     Expirations : DateTime []
     Calls : Option []
     Puts : Option []}

let DownloadOptionsFeed(url : string) = 
    let wc = new WebClient()
    let mutable str = wc.DownloadString(url)
    let obj = JsonConvert.DeserializeObject<RawOptionChain>(str)
    obj

/// Example: GetOptionsData "MSFT";;
let GetOptionsData ticker expiryDate = 
    let url = makeUrlOptions expiryDate ticker
    DownloadOptionsFeed(url)
