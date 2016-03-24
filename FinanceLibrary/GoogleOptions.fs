module AlgoTrader.GoogleApi.Options

open System
open System.IO
open System.Net
open System.Text.RegularExpressions
open System.Runtime.Serialization.Json
open System.Text

let makeUrlOptions (expiryDate: DateTime option) ticker =
    match expiryDate with
    | Some expiryDate -> 
         let day = expiryDate.Day.ToString()
         let month = expiryDate.Month.ToString()
         let year = expiryDate.Year.ToString()
         "https://www.google.com/finance/option_chain?q=" + ticker + "&expd=" + day + "&expm=" + month + "&expy=" + year + "&output=json"
    | None -> "https://www.google.com/finance/option_chain?q=" + ticker + "&output=json"

type Option = { 
    Symbol:string
    StrikePrice:Decimal
    Expiry:DateTime
    Bid:Decimal
    Ask:Decimal
    Vol:int
    OpenInt:int
    InTheMoney:bool 
    AtTheMoney:bool }

type OptionChain = {
    Expiry:DateTime
    Expirations:DateTime[]
    Calls: Option []
    Puts: Option []
}

/// Trys to convert a string to a decimal.
let cd value =
    let mutable result = 0.0M
    Decimal.TryParse(value, &result) |> ignore
    result

/// Trys to convert a string to an integer.
let ci value =
    let mutable result = 0
    Int32.TryParse(value, &result) |> ignore
    result

/// Calculates expiry date by whether option is mini-option or ordinary.
let daysToExpiry (expiryDate:DateTime) : string = 
    (expiryDate- DateTime.Today).TotalDays.ToString()

let InTheMoney symbol strikePrice marketPrice = 
    // Call option is in the money when the strike price is below the market price.
    if symbol.ToString().[10] = 'C' then strikePrice < marketPrice
    // Put option is in the money when the strike price is above the market price.
    elif symbol.ToString().[10] = 'P' then strikePrice > marketPrice
    else false;

/// Call or Put option is at the money when the strike price is equal to the market price.
let AtTheMoney strikePrice marketPrice = strikePrice = marketPrice

/// Gets SOAP data and serialises into optionsData type.
let DownloadOptionsFeed (url:string) =
    let req = WebRequest.Create(url) :?> HttpWebRequest
    use stream = req.GetResponse().GetResponseStream()
    use reader = new StreamReader(stream)

    let read = reader.ReadToEnd()
    let read = Regex.Replace(read, @"(\w+:)(\d+\.?\d*)", "$1\"$2\"");
    let read = Regex.Replace(read, @"(\w+):", "\"$1\":");
    
    let js = DataContractJsonSerializer(typeof<OptionChain>);
    use memStream = new MemoryStream(Encoding.UTF8.GetBytes(read))
    let obj = js.ReadObject(memStream) :?> OptionChain

    obj

/// Example: GetOptionsData "MSFT";;
let GetOptionsData ticker expiryDate =
     let url = makeUrlOptions expiryDate ticker
     DownloadOptionsFeed(url)