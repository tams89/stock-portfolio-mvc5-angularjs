(*
 Copyright (C) 2009 Ning Cao (tony.ning.cao@gmail.com)
  
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
namespace Quantifa

    open System
    open Quantifa

    type DayCounter( d: DayCounterTypes ) =
        let mutable dayCounter_ = Actual365Fixed
        // this is a placeholder for actual day counters for Singleton pattern use   
        member x.dayCounter with get() = dayCounter_ and set(v : DayCounterTypes) = dayCounter_ <- v
        // constructors
        (*! The default constructor returns a day counter with a null implementation, which is therefore unusable except as a
            placeholder. *)
        new() = new DayCounter (Actual365Fixed)

        // comparison based on name
        // Returns <tt>true</tt> iff the two day counters belong to the same derived class.
        static member op_Equality (d1 : DayCounter, d2 : DayCounter) = 
            if d1 :> Object = null || d2 :> Object = null then d1 :> Object = null && d2 :> Object = null else 
                (d1.isEmpty() && d2.isEmpty()) || (not(d1.isEmpty()) && not(d2.isEmpty()) && d1.name() = d2.name())

        static member op_Inequality (d1 : DayCounter, d2 : DayCounter) = not(DayCounter.op_Equality (d1, d2))
        member x.isEmpty() = (dayCounter_ :> Object = null)
        override x.Equals(obj : Object) = 
            match obj with
                | :? DayCounter as y -> y = x
                | _ -> false
        override x.GetHashCode() = 0
        override x.ToString() = x.name()
     
        member x.name() = 
            match dayCounter_ with
                | Actual360 -> "Actual/360"
                | Actual365Fixed -> "Actual/365 (Fixed)"
                | ActualActual (ISMA | Bond) -> "Actual/Actual (ISMA)"
                | ActualActual (ISDA | Historical | Actual365) -> "Actual/Actual (ISDA)"
                | ActualActual (AFB | Euro) -> "Actual/Actual (AFB)"
                //| Business252 (_) -> "Business/252(" + calendar_.name() + ")"
                | One -> "1/1"
                | Simple -> "Simple"
                | Thirty360 (USA | BondBasis) -> "30/360 (Bond Basis)"
                | Thirty360 (European | EurobondBasis) -> "30E/360 (Eurobond Basis)"
                | Thirty360 (Italian) -> "30/360 (Italian)"  
                
        member x.dayCount (d1 : Date, d2 : Date) =
            match dayCounter_ with
            | Actual360 | Actual365Fixed | ActualActual (_) -> d2 - d1
            //| Business252 (_)  -> calendar_.businessDaysBetween(d1, d2)
            | One -> if d2 >= d1 then 1 else -1
            | Simple | Thirty360 (USA | BondBasis) -> 
                let dd1 = d1.Day
                let mutable dd2 = d2.Day
                let mm1 = d1.Month
                let mutable mm2 = d2.Month
                let yy1 = d1.Year
                let yy2 = d2.Year
                if dd2 = 31 && dd1 < 30 then dd2 <- 1; mm2 <- mm2 + 1 
                360 * (yy2 - yy1) + 30 * (mm2 - mm1 - 1) + max 0 (30 - dd1) + min 30 dd2
            | Thirty360 (European | EurobondBasis) -> 
                let dd1 = d1.Day
                let dd2 = d2.Day
                let mm1 = d1.Month
                let mm2 = d2.Month
                let yy1 = d1.Year
                let yy2 = d2.Year
                360 * (yy2 - yy1) + 30 * (mm2 - mm1 - 1) + max 0 (30 - dd1) + min 30 dd2 
            | Thirty360 (Italian) -> 
                let mutable dd1 = d1.Day
                let mutable dd2 = d2.Day
                let mm1 = d1.Month
                let mm2 = d2.Month
                let yy1 = d1.Year
                let yy2 = d2.Year
                if mm1 = 2 && dd1 > 27 then dd1 <- 30
                if mm2 = 2 && dd2 > 27 then dd2 <- 30
                360 * (yy2 - yy1) + 30 * (mm2 - mm1 - 1) + max 0 (30 - dd1) + min 30 dd2  
        
        member x.yearFraction(d1 : Date, d2 : Date) = x.yearFraction(d1, d2, d1, d2)
        
        member x.yearFraction(d1 : Date, d2 : Date, d3 : Date, d4 : Date) : float =
            if d1 > d2 then -x.yearFraction (d2, d1, d3, d4) 
            elif not(d4 > d3 && d4 > d1) then failwith "d1" "Invalid reference period " else
            match dayCounter_ with
            | Actual360 | Thirty360 (_) | Simple -> (float)(x.dayCount(d1, d2)) / 360.0
            | Actual365Fixed -> (float)(x.dayCount(d1, d2)) / 365.0
            | ActualActual (y) -> 
                if d1 = d2 then 0.0 else
                match y with 
                    | ISMA | Bond -> 
                        
                        // estimate roughly the length in months of a period
                        let mutable months = (int)(0.5 + (12.0 * (float)(d4 - d3) / 365.0))
                        let mutable refPeriodStart = d3
                        let mutable refPeriodEnd = d4
                        // for short periods...
                        if months = 0 then refPeriodStart <- d1; refPeriodEnd <- d1 + TimeUnit.Years; months <- 12;
                        // ...take the reference period as 1 year from d1
                        let period = ((float)months / 12.0) 
                        let m = new Period(months, TimeUnit.Months)
                        if d2 <= refPeriodEnd then 
                            // here refPeriodEnd is a future (notional?) payment date
                            if d1 >= refPeriodStart then period * (float)(x.dayCount(d1, d2)) / (float)(x.dayCount(refPeriodStart, refPeriodEnd))
                                // here refPeriodStart is the last (maybe notional) payment date.
                                // refPeriodStart <= d1 <= d2 <= refPeriodEnd
                            else
                                // here refPeriodStart is the next (maybe notional) payment date and refPeriodEnd is the second next (maybe notional) payment date.
                                // d1 < refPeriodStart < refPeriodEnd
                                // AND d2 <= refPeriodEnd
                                // this case is long first coupon
                                // the last notional payment date
                                let previousRef = refPeriodStart - m;
                                if d2 > refPeriodStart then x.yearFraction(d1, refPeriodStart, previousRef, refPeriodStart) + x.yearFraction(refPeriodStart, d2, refPeriodStart, refPeriodEnd)
                                else x.yearFraction(d1, d2, previousRef, refPeriodStart)
                        else
                            // here refPeriodEnd is the last (notional?) payment date
                            // d1 < refPeriodEnd < d2 AND refPeriodStart < refPeriodEnd
                            if not(refPeriodStart <= d1) then failwith "invalid dates: d1 < refPeriodStart < refPeriodEnd < d2"
                            // now it is: refPeriodStart <= d1 < refPeriodEnd < d2
                            else
                            // count how many regular periods are in [refPeriodEnd, d2], then add the remaining time
                                let rec sum refStart refEnd=
                                    match d2 with
                                        | _ when d2 < refEnd -> x.yearFraction(refStart, d2, refStart, refEnd)
                                        | _ -> period + sum (refStart + m) (refEnd + m)
                                // the part from d1 to refPeriodEnd + the part from refPeriodEnd to d2
                                x.yearFraction(d1, refPeriodEnd, refPeriodStart, refPeriodEnd) + sum refPeriodEnd (refPeriodEnd + new Period(months, TimeUnit.Months))
     
                    | ISDA | Historical | Actual365 ->
                        let y1 = d1.Year
                        let y2 = d2.Year
                        let dib1 = if Date.IsLeapYear(y1) then 366 else 365
                        let dib2 = if Date.IsLeapYear(y2) then 366 else 365
                        (float)y2 - (float)y1 - 1.0 + (float)(x.dayCount(d1, new Date(1, 1, y1 + 1))) / (float)dib1 + (float)(x.dayCount(new Date(1, 1, y2), d2)) / (float)dib2
                    | AFB | Euro -> 
                        let rec sum (d : Date) =    
                            let y = d - TimeUnit.Years
                            let z = if y.Day = 28 && y.Month = 2 && Date.IsLeapYear (y.Year) then y + 1 else y
                            match z with
                                | _ when z >= d1 -> 1.0 + sum z
                                | _  -> 
                                    let den = 
                                        if Date.IsLeapYear(d.Year) then 
                                            let temp = new Date(29, 2, d.Year)
                                            if d > temp && d1 <= temp then 366.0 else 365.0
                                        elif Date.IsLeapYear(d1.Year) then
                                            let temp = new Date(29, 2, d1.Year)
                                            if d > temp && d1 <= temp then 366.0 else 365.0
                                        else 365.0
                                    (float)(x.dayCount(d1, d)) / den
                        sum d2
                        
            | Business252 -> (float)(x.dayCount(d1, d2)) / 252.0
            | One -> (float)(x.dayCount(d1, d2))
    