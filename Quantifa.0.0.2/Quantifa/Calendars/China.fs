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
namespace Quantifa

open System
open Quantifa

    //! Chinese calendar
    (*! Holidays:
        <ul>
        <li>Saturdays</li>
        <li>Sundays</li>
        <li>New Year's day, January 1st (possibly followed by one or
            two more holidays)</li>
        <li>Labour Day, first week in May</li>
        <li>National Day, one week from October 1st</li>
        </ul>

        Other holidays for which no rule is given (data available for
        2004-2008 only):
        <ul>
        <li>Chinese New Year</li>
        <li>Ching Ming Festival</li>
        <li>Tuen Ng Festival</li>
        <li>Mid-Autumn Festival</li>
        </ul>

        Data from <http://www.sse.com.cn/sseportal/en_us/ps/home.shtml>

        \ingroup calendars
    *)
    
    type China =
        inherit Calendar
        override x.name() = "Shanghai stock exchange"
        override x.isWeekend(w : DayOfWeek) = w = DayOfWeek.Saturday || w = DayOfWeek.Sunday
        override x.isBusinessDay(date : Date) =
            let w = date.DayOfWeek
            let d = date.Day
            let m = date.Month
            let y = date.Year
            if x.isWeekend(w)
                // New Year's Day
                || (d = 1 && m = 1)
                || (d = 3 && m =1 && y = 2005)
                || ((d = 2 || d = 3) && m = 1 && y = 2006)
                || (d <= 3 && m = 1 && y = 2007)
                || (d = 31 && m = 12 && y = 2007)
                || (d = 1 && m = 1 && y = 2008)
                // Chinese New Year
                || (d >= 19 && d <= 28 && m = 1 && y = 2004)
                || (d >= 7 && d <= 15 && m = 2 && y = 2005)
                || (((d >= 26 && m = 1) || (d <= 3 && m = 2))
                    && y = 2006)
                || (d >= 17 && d <= 25 && m = 2 && y = 2007)
                || (d >= 6 && d <= 12 && m = 2 && y = 2008)
                // Ching Ming Festival
                || (d = 4 && m = 4 && y <= 2008)
                // Labor Day
                || (d >= 1 && d <= 7 && m = 5 && y <= 2007)
                || (d >= 1 && d <= 2 && m = 5 && y = 2008)
                // Tuen Ng Festival
                || (d = 9 && m = 6 && y <= 2008)
                // Mid-Autumn Festival
                || (d = 15 && m = 9 && y <= 2008)
                // National Day
                || (d >= 1 && d <= 7 && m = 10 && y <= 2007)
                || (d >= 29 && m = 9 && y = 2008)
                || (d <= 3 && m = 10 && y = 2008)
            then false else true
            


