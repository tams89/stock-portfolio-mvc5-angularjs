module AlgoTrader.GoogleApi.Options

open System
open System.IO
open System.Net
open System.Runtime.Serialization.Json
open System.Text
open System.Text.RegularExpressions

let makeUrlOptions (expiryDate : DateTime option) ticker = 
    match expiryDate with
    | Some expiryDate -> 
        let day = expiryDate.Day.ToString()
        let month = expiryDate.Month.ToString()
        let year = expiryDate.Year.ToString()
        "http://www.google.com/finance/option_chain?q=" + ticker + "&expd=" + day + "&expm=" + month + "&expy=" + year 
        + "&output=json"
    | None -> "http://www.google.com/finance/option_chain?q=" + ticker + "&output=json"

type Expiry = 
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

type Option = 
    {Symbol : string
     StrikePrice : Decimal
     Expiry : DateTime
     Bid : Decimal
     Ask : Decimal
     Vol : int
     OpenInt : int
     InTheMoney : bool
     AtTheMoney : bool}

type OptionChain(expiry, expirations, calls, puts) = 
    class
        member this.Expiry : DateTime = expiry 
        member this.Expirations : Expiry [] = expirations 
        member this.Calls : RawOption [] = calls 
        member this.Puts  : RawOption [] = puts 
    end

/// Gets SOAP data and serialises into optionsData type.
let DownloadOptionsFeed(url : string) = 
    let wc = new WebClient()
    let mutable str = wc.DownloadString(url)
    str <- Regex.Replace(str, @"(\w+:)(\d+\.?\d*)", "$1\"$2\"")
    str <- Regex.Replace(str, @"(\w+):", "\"$1\":")
    let js = DataContractJsonSerializer(typeof<OptionChain>)
    use memStream = new MemoryStream(Encoding.UTF8.GetBytes(str))
    let obj = js.ReadObject(memStream) :?> OptionChain
    obj

/// Example: GetOptionsData "MSFT";;
let GetOptionsData ticker expiryDate = 
    let url = makeUrlOptions expiryDate ticker
    DownloadOptionsFeed(url)
