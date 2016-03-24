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

let parseInt item = 
    let mutable result = 0
    let parsed = Int32.TryParse(item, &result)
    result, parsed

let parseDecimal item = 
    let mutable result = 0M
    let parsed = Decimal.TryParse(item, &result)
    result, parsed

let parseExpiry rawExpiry = DateTime(int rawExpiry.y, int rawExpiry.m, int rawExpiry.d)

let toOption rawOption = 
    {Symbol = rawOption.s
     StrikePrice = decimal rawOption.strike
     Expiry = DateTime.ParseExact(rawOption.expiry, "MMM dd, yyyy", System.Globalization.CultureInfo.CurrentCulture)
     Bid = parseDecimal rawOption.b |> fst
     Ask = parseDecimal rawOption.a |> fst
     Vol = parseInt rawOption.vol |> fst
     OpenInt = parseInt rawOption.oi |> fst}

let DownloadOptionsFeed(url : string) = 
    let wc = new WebClient()
    let mutable str = wc.DownloadString(url)
    let obj = JsonConvert.DeserializeObject<RawOptionChain>(str)
    {Expiry = parseExpiry obj.Expiry
     Expirations = 
         [|for e in obj.Expirations -> parseExpiry e|]
     Calls = 
         [|for o in obj.Calls -> toOption o|]
     Puts = 
         [|for o in obj.Puts -> toOption o|]}

/// Example: GetOptionsData "MSFT";;
let GetOptionsData ticker expiryDate = 
    let url = makeUrlOptions expiryDate ticker
    DownloadOptionsFeed(url)