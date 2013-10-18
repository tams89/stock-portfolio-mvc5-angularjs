(*
 Copyright (C) 2010 Ning Cao (tony.ning.cao@gmail.com)
  
 This file is part of Quantifa, an F# open-source library for quantitative 
 finance and risk management - http://quantifa.org/

 Quantifa is free software: you can redistribute it and/or modify it under 
 the terms of the quantifa license. You should have received a copy of the 
 license along with this program; if not, the license is available online 
 at <http://quantifa.org/license.shtml>.
  
 Quantifa can be viewed as a functional programming version of 
 QuantLib (http://quantlib.org/) and QLNet (http://www.qlnet.org), 
 free-software/open-source libraries for financial quantitative analysts 
 and developers written in C++ and C#, respectively. The QuantLib license 
 is available online at http://quantlib.org/license.shtml and the QLNet 
 license is available online at http://trac2.assembla.com/QLNet/wiki/License
 
 This program is distributed in the hope that it will be useful, but 
 WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
 or FITNESS FOR A PARTICULAR PURPOSE. See the license for more details.
*)

#light
module Quantifa.BlackScholes

        open System
        open Quantifa
        open Quantifa.Distributions
                
        let Pricing (o : OptionSpec) (s : StockSpec) =
            match o with
             | Vanilla (optionType, strike, settlementDate, maturity, dayCounterType) -> 
                 let counter = new DayCounter(dayCounterType)
                 let t = counter.yearFraction(settlementDate, maturity)
                 let d1 = (log s.s0 / strike + (s.r + 0.5 * s.sigma ** 2.0) * t) / (s.sigma * sqrt t)
                 let d2 = d1 - s.sigma * sqrt t
                 match optionType with
                    | Call ->  s.s0 * standardNormalCDF d1 - strike * exp (- s.r * t) * standardNormalCDF d2
                    | Put  ->  - s.s0 * standardNormalCDF (- d1) + strike * exp (- s.r * t) * standardNormalCDF (- d2) 
             | Digital (optionType, strike, settlementDate, maturity, dayCounterType) ->
                 let counter = new DayCounter(dayCounterType)
                 let t = counter.yearFraction(settlementDate, maturity)
                 let d2 = (log s.s0 / strike + (s.r - 0.5 * s.sigma ** 2.0) * t) / (s.sigma * sqrt t)
                 
                 match optionType with
                    | Call -> strike * exp (- s.r * t) * standardNormalCDF d2
                    | Put -> strike * exp (- s.r * t) * standardNormalCDF (- d2)                                                               
        
        (*let Sensitivity (o : OptionSpec) (s : StockSpec) (output : string list) =
            match o with
             | Vanilla (optionType, strike, settlementDate, maturity, dayCounterType) -> 
                 let counter = new DayCounter(dayCounterType)
                 let t = counter.yearFraction(settlementDate, maturity)
                 let d1 = (log s.s0/strike + (s.r + 0.5 * s.sigma ** 2.0) * t) / (s.sigma * sqrt t)
                 let d2 = d1 - s.sigma * sqrt t
                 match optionType with
                    | Call ->  output |> List.map (fun x -> match x with
                                                               | "price" -> 
                                                               | "delta" ->
                                                               | "gamma" ->
                                                               | "theta" ->
                                                               | _ -> failwith "unknown output")
                    | Put  -> output |> List.map (fun x -> match x with
                                                               | "price" -> 
                                                               | "delta" ->
                                                               | "gamma" ->
                                                               | "theta" ->
                                                               | _ -> failwith "unknown output")
                              
             *)
             