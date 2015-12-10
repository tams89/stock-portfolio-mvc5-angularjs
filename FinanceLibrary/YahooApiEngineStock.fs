module AlgoTrader.YahooApi.Stock

open System
open System.IO
open System.Net
open System.Xml.Linq
open System.Xml.Serialization
open FSharp.Data

let makeUrlStocks ticker =
    new Uri("http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20(" + ticker + ")&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys")

[<CLIMutable>]
type StocksData = { Symbol:string; Ask:string; Bid:string; AverageDailyVolume:string; AskRealTime:string; BidRealTime:string;
                    BookValue:string; Change:string; Commision:string; ChangeRealTime:string; DividendShare:string;
                    LastTradeDate:string; EarningsShare:string; EPSEstimateCurrentYear:string; EPSEstimateNextYear:string;
                    EPSEstimateNextQuarter:string; DaysLow:string; DaysHigh:string; YearLow:string; YearHigh:string;
                    MarketCapitalization:string; FiftydayMovingAverage:string; TwoHundreddayMovingAverage:string;
                    Open:string; PreviousClose:string; PriceSales:string; PriceBook:string; PERatio:string; PEGRatio:string;
                    PriceEPSEstimateCurrentYear:string; PriceEPSEstimateNextYear:string; ShortRatio:string; OneyrTargetPrice:string;
                    Volume:string; StockExchange:string; DividendYield:string; }

type Stock = XmlProvider<"yqlstock.xml", false, false>

let DownloadStocksFeed (url:string) =
 let req = WebRequest.Create(url) :?> HttpWebRequest
 use stream = req.GetResponse().GetResponseStream()
 use reader = new StreamReader(stream)
 let read = reader.ReadToEnd()
 let feed = Stock.Parse(read)
 let result  = feed.Results.Quote.XElement.ToString()
 let reader = XDocument.Parse(result).CreateReader()
 let xRoot = XmlRootAttribute("quote")
 let ser = XmlSerializer(typeof<StocksData>, xRoot)
 let deser = ser.Deserialize(reader) :?> StocksData
 deser

let GetStocksData ticker =
    let url = makeUrlStocks ticker
    DownloadStocksFeed(url.AbsoluteUri)