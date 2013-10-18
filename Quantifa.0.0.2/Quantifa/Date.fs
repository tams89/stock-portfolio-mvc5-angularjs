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

    type Date (dt : DateTime)=
        // constructors
        // new () = new Date ()
        new (serialNumber : int) =
            let dt1 = (new DateTime(1899, 12, 31)).AddDays((float)serialNumber - 1.0)
            new Date (dt1)
        new (d, m, y) =
            let dt2 = new DateTime(d, (int)m, y)
            new Date (dt2)

        // getters
        member x.date = dt
        member x.serialNumber() = (x.date - (new DateTime(1899, 12, 31))).Days + 1
        member x.Day with get() = x.date.Day
        member x.Month with get() = x.date.Month
        member x.month() = x.date.Month
        member x.Year with get() = x.date.Year
        member x.year() = x.date.Year
        member x.DayOfYear with get() = x.date.DayOfYear
        member x.weekday() = (int)x.date.DayOfWeek + 1
        member x.DayOfWeek with get() : DayOfWeek  = x.date.DayOfWeek

        member x.ToLongDateString() = x.date.ToLongDateString()
        member x.ToShortDateString() = x.date.ToShortDateString()
        override x.ToString() = x.ToShortDateString()
        override x.Equals(obj : Object) =
                match obj with
                    | :? Date as y -> x.date = y.date
                    | _ -> false
        override x.GetHashCode() = 0

        interface IComparable with
        // IComparable interface
            member x.CompareTo(obj) =
                match obj with
                    | :? Date as y -> compare x.date y.date
                    | _ -> invalidArg "obj" "not a Date type"

        // static properties
        static member minDate() = new Date(1, 1, 1901)
        static member maxDate() = new Date(31, 12, 2199)
        static member Today with get() = new Date(DateTime.Today)
        static member IsLeapYear(y) = DateTime.IsLeapYear(y)
        static member DaysInMonth(y, m) = DateTime.DaysInMonth(y, m)
        static member endOfMonth(d : Date) = (d - d.Day) + Date.DaysInMonth(d.Year, d.Month)
        static member isEndOfMonth(d : Date) = d.Day = Date.DaysInMonth(d.Year, d.Month)

        //! next given weekday following or equal to the given date
        static member nextWeekday(d : Date, dayOfWeek : DayOfWeek) =
            let wd = (int)(dayOfWeek - d.DayOfWeek)
            if wd >=0 then d + wd   else  d + (int)(wd + 7)

        //! n-th given weekday in the given month and year, e.g., the 4th Thursday of March, 1998 was March 26th, 1998.
        static member nthWeekday(nth, dayOfWeek : DayOfWeek , m, y) =
            if nth < 1 || nth > 5 then invalidArg "nth" "wrong n-th weekday in a given month/year"
            let first = (new DateTime(y, m, 1)).DayOfWeek
            let skip =
                if dayOfWeek >= first then nth - 1 else nth
            new Date(1, m, y) + ((int)(dayOfWeek - first) + skip * 7)

        static member monthOffset(m, leapYear) =
            let MonthOffset = [0; 31; 59; 90; 120; 151; 181; 212; 243; 273; 304; 334; 365] // used in dayOfMonth to bracket day
            if leapYear && m >1 then MonthOffset.[m - 1] + 1 else MonthOffset.[m - 1]

        static member advance(d : Date, n: int, u) =
            match u with
                | Days -> d + n;
                | Weeks -> d + 7 * n;
                | Months -> new Date(d.date.AddMonths(n))
                | Years -> new Date(d.date.AddYears(n))
                | _ -> invalidArg "Unknown TimeUnit: " + (string)u

        // operator overloads
        static member op_Subtraction (d1 : Date, d2 : Date) = (d1.date - d2.date).Days
        static member op_Addition (d : Date, days : int) : Date = new Date(d.serialNumber()+days)
        static member op_Subtraction (d : Date, days : int) : Date = new Date(d.serialNumber()-days)
        static member op_Addition (d : Date, u : TimeUnit) : Date = Date.advance(d, 1, u)
        static member op_Subtraction (d : Date, u : TimeUnit) : Date = Date.advance(d, -1, u)
        static member op_Addition (d : Date, p : Period) : Date = Date.advance(d, p.length(), p.unit())
        static member op_Subtraction (d : Date, p : Period) : Date = Date.advance(d, -p.length(), p.unit())
        static member op_PlusPlus (d : Date) = d + 1
        static member op_MinusMinus (d : Date) = d - 1

        static member Min(d1 : Date, d2 : Date) = if d1 < d2 then d1 else d2
        static member Max(d1 : Date, d2 : Date) = if d1 > d2 then d1 else d2

        static member op_Equality (d1 : Date, d2 : Date) =
            if d1 :> Object = null || d2 :> Object = null then (d1 :> Object = null && d2 :> Object = null) else d1.date = d2.date
        static member op_Inequality (d1 : Date, d2 : Date) = not(d1.date = d2.date)
        static member op_LessThan (d1 : Date, d2 : Date) = d1.date < d2.date
        static member op_LessThanOrEqual (d1 : Date, d2 : Date) = d1.date <= d2.date
        static member op_GreaterThan (d1 : Date, d2 : Date) = d1.date > d2.date
        static member op_GreaterThanOrEqual (d1 : Date, d2 : Date) = d1.date >= d2.date