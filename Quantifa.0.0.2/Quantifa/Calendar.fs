﻿(*
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

namespace Quantifa

    open System
    open Quantifa
    [<AbstractClass>]
    // base class for calendar
    type Calendar (c : Calendar)=

        //member x.Calendar with get() = calendar_ and set(c) = calendar_ <- value

        //public List<Date> addedHolidays = new List<Date>(), removedHolidays = new List<Date>();

        //abstract members
        abstract member name: unit -> string
        abstract member isBusinessDay: Date -> bool
        abstract member isWeekend: DayOfWeek -> bool
        //member x.empty() = calendar = null
        member x.isHoliday(d : Date) = not(x.isBusinessDay(d))
        // Returns true iff the date is last business day for the month in given market.
        //member x.isEndOfMonth(d : Date) = (d.Month <> x.adjust(d + 1).Month)
        // last business day of the month to which the given date belongs
        //member x.endOfMonth(d : Date) = x.adjust(Date.endOfMonth(d), BusinessDayConvention.Preceding)
        // Adjusts a non-business day to the appropriate near business day  with respect to the given convention.
        //member x.adjust(d : Date) =x.adjust(d, BusinessDayConvention.Following)