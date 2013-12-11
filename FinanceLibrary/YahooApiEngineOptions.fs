module AlgoTrader.YahooApi.Options

open System
open System.IO
open System.Linq
open System.Net
open System.Xml
open System.Xml.Linq
open System.Globalization

let makeUrlOptions ticker =
    new Uri("http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.options%20where%20symbol%20in%20('" + ticker + "')&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys")

type OptionsData = { Symbol:string; ExpiryDate:DateTime; DaysToExpiry:string; StrikePrice:Decimal; LastPrice:Decimal; ChangeDirection:string; Change:Decimal; 
                     Bid:Decimal; Ask:Decimal; Vol:int; OpenInt:int; InTheMoney:bool; AtTheMoney:bool }

let xn s = XName.Get(s)
let (?) (el:XElement) name = el.Element(xn name).Value

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
let expiryDate (optionTicker:string) =
    if optionTicker.Substring(4,1) = "7" then // mini-option ticker.
       DateTime.ParseExact(String.Format("{0}/{1}/{2}", optionTicker.Substring(8 + 1, 2), optionTicker.Substring(6 + 1, 2), optionTicker.Substring(4 + 1, 2)), "dd/MM/yy", CultureInfo.InvariantCulture)
    elif optionTicker.Length = 19 then // standard 4 char symbol
       DateTime.ParseExact(String.Format("{0}/{1}/{2}", optionTicker.Substring(8, 2), optionTicker.Substring(6, 2), optionTicker.Substring(4, 2)), "dd/MM/yy", CultureInfo.InvariantCulture)
    else // 3 char symbol
       DateTime.ParseExact(String.Format("{0}/{1}/{2}", optionTicker.Substring(8-1, 2), optionTicker.Substring(6-1, 2), optionTicker.Substring(4-1, 2)), "dd/MM/yy", CultureInfo.InvariantCulture) 

let daysToExpiry (expiryDate:DateTime) : string = 
    (expiryDate- DateTime.Today).TotalDays.ToString()

let InTheMoney symbol strikePrice marketPrice =    
    // Call option is in the money when the strike price is below the market price.
    if symbol.ToString().[10] = 'C' then strikePrice < marketPrice
    // Put option is in the money when the strike price is above the market price.
    elif symbol.ToString().[10] = 'P' then strikePrice > marketPrice
    else false;

/// Call or Put option is at the money when the strike price is equal to the market price.
let AtTheMoney strikePrice marketPrice =    
    if strikePrice = marketPrice then true
    else false

/// Gets SOAP data and serialises into optionsData type.
let DownloadOptionsFeed (url:string) =
    let req = WebRequest.Create(url) :?> HttpWebRequest
    use stream = req.GetResponse().GetResponseStream()
    use reader = new StreamReader(stream)
    let read = reader.ReadToEnd()
    let feed = XDocument.Parse(read)
    let listOfElems = feed.Root.Element(xn "results").Elements(xn "optionsChain").Elements(xn "option")
    let elements =
      [ for e in listOfElems do
            yield
                { Symbol = e.Attribute(xn "symbol").Value
                  ExpiryDate = expiryDate(e.Attribute(xn "symbol").Value)
                  DaysToExpiry = daysToExpiry (expiryDate(e.Attribute(xn "symbol").Value))
                  StrikePrice = cd e?strikePrice
                  LastPrice = cd e?lastPrice
                  ChangeDirection = e?changeDir
                  Change = cd e?change
                  Bid = cd e?bid
                  Ask = cd e?ask
                  Vol = ci e?vol
                  OpenInt = ci e?openInt
                  InTheMoney = InTheMoney (e.Attribute(xn "symbol").Value) (cd e?strikePrice) (cd e?lastPrice) 
                  AtTheMoney = AtTheMoney (cd e?strikePrice) (cd e?lastPrice) }]
                  |> seq
    elements

/// Example: GetOptionsData "MSFT";;
let GetOptionsData ticker =
     let url = makeUrlOptions ticker
     DownloadOptionsFeed(url.AbsoluteUri)