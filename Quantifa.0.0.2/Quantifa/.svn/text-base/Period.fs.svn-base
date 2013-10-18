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

    type Period (length : int, unit : TimeUnit) =

        let mutable length_ = length
        let mutable unit_ = unit

        // properties
        member x.length() = length_
        member x.unit() = unit_

        new() = new Period (0, Days)
        
        new(f : Frequency) =
            let length, unit =
                match f with
                    | Frequency.Once | Frequency.NoFrequency -> 0, Days
                    | Frequency.Annual -> 1, Years
                    | Frequency.Semiannual | Frequency.EveryFourthMonth | Frequency.Quarterly | Frequency.Bimonthly | Frequency.Monthly -> 12 / (int)f, Months
                    | Frequency.EveryFourthWeek | Frequency.Biweekly | Frequency.Weekly -> 52 / (int)f, Weeks
                    | Frequency.Daily -> 1, Days
                    | _ -> invalidArg "f" "unknown Frequency"
            new Period (length, unit)
              
        // Create from a string like "1M", "2Y"...
        new(periodString : string) = 
            let periodStringToUpper = periodString.ToUpper()
            let length = Int32.Parse(periodStringToUpper.Substring(0, periodStringToUpper.Length - 1));
            let freq = periodStringToUpper.Substring(periodStringToUpper.Length - 1, 1);
            let unit =
                match freq with
                    | "D" -> Days
                    | "W" -> Weeks
                    | "M" -> Months
                    | "Y" -> Years
                    | _ -> invalidArg "periodString" "unknown TimeUnit"
            new Period (length, unit)

        member x.frequency() =
            let absLength_ = abs length_
            match unit_, absLength_ with
                | _, 0 -> Frequency.NoFrequency 
                | Years, 1 -> Frequency.Annual
                | Years, _ -> Frequency.OtherFrequency
                | Months, 1 -> Frequency.Monthly
                | Months, 2 -> Frequency.Bimonthly
                | Months, 3 -> Frequency.Quarterly
                | Months, 4 -> Frequency.EveryFourthMonth
                | Months, 6 -> Frequency.Semiannual
                | Months, 12 -> Frequency.Annual
                | Months, _ -> Frequency.OtherFrequency
                | Weeks, 1 -> Frequency.Weekly 
                | Weeks, 2 -> Frequency.Biweekly
                | Weeks, 4 -> Frequency.EveryFourthWeek
                | Weeks, _ -> Frequency.OtherFrequency
                | Days, 1 -> Frequency.Daily 
                | Days, _ -> Frequency.OtherFrequency
                | _, _ -> invalidArg "unit_" "unknown TimeUnit"    

        member x.normalize() =
            if length_ <> 0 then
                if unit_ = Days && length_ % 7 = 0 then length_ <- length_ / 7; unit_ <- Weeks
                elif unit_ = Months && length_ % 12 = 0 then length_ <- length_ /12; unit_ <- Years  
        static member op_UnaryNegation (p : Period) = new Period(-p.length(), p.unit())
        static member op_Multiply (n : int, p : Period) : Period = new Period(n * p.length(), p.unit())
        static member op_Multiply (p : Period, n : int) : Period = new Period(n * p.length(), p.unit())
          
        static member op_Equality (p1 : Period, p2 : Period) = not(Period.op_LessThan(p1, p2) || Period.op_LessThan(p2, p1))
        static member op_Inequality (p1 : Period, p2 : Period) = Period.op_LessThan(p1, p2) || Period.op_LessThan(p2, p1)
        static member op_LessThanOrEqual (p1 : Period, p2 : Period) = not(Period.op_LessThan(p2, p1))
        static member op_GreaterThanOrEqual (p1 : Period, p2 : Period) = not(Period.op_LessThan(p1, p2))
        static member op_GreaterThan (p1 : Period, p2 : Period) = Period.op_LessThan(p2, p1)
        static member op_LessThan (p1 : Period, p2 : Period) =  
            // special cases
            if p1.length() = 0 then p2.length() > 0
            elif p2.length() = 0 then p1.length() < 0
            
            // exact comparisons
            elif p1.unit() = p2.unit() then p1.length() < p2.length()
            elif p1.unit() = Months && p2.unit() = Years then p1.length() < 12 * p2.length()
            elif p1.unit() = Years && p2.unit() = Months then 12 * p1.length() < p2.length() 
            elif p1.unit() = Days && p2.unit() = Weeks then p1.length() < 7 * p2.length()
            elif p1.unit() = TimeUnit.Weeks && p2.unit() = TimeUnit.Days then 7 * p1.length() < p2.length() else
            
            // inexact comparisons (handled by converting to days and using limits)
            let pairPeriod (p:Period) =
                match p.unit() with
                        | Days -> (p.length(), p.length())            
                        | Weeks -> (7 * p.length(), 7 * p.length())
                        | Months -> (28 * p.length(), 31 * p.length())
                        | Years -> (365 * p.length(), 366 * p.length())
                        | _ -> invalidArg "p.units()" "unknown TimeUnit"
            
            let p1lim = pairPeriod p1
            let p2lim = pairPeriod p2
            if snd(p1lim) < fst(p2lim) || snd(p2lim) < fst(p1lim) then snd(p1lim) < fst(p2lim) else invalidArg "p1, p2 " "undecidable comparison between " 
        
        
        override x.Equals(obj : Object) =
                    match obj with 
                        | :? Period as y -> x = y
                        | _ -> false
        override x.GetHashCode() = 0
        override x.ToString() = "TimeUnit: " + unit_.ToString() + ", length: " + length_.ToString()
        
        member x.ToShortString() =
            let mutable result = "";
            let mutable n : int = length_
            let mutable m  : int = 0;
            match unit_ with
                | Days -> if n >= 7 then m <- n / 7; result <- result + m.ToString() + "W"; n <- n % 7
                          if n <> 0 || m = 0 then result + n.ToString() + "D" else result
                | Weeks -> result + n.ToString() + "W";
                | Months -> if n >= 12 then m <- n / 12; result <- result + (n / 12).ToString() + "Y"; n <- n % 12;
                            if n <> 0 || m = 0 then result + n.ToString() + "M" else result
                | Years -> result + n.ToString() + "Y"
                | _ -> invalidArg "unit_" "unknown TimeUnit"
            

            
