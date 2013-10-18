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

    type Month =
        | January   = 1
        | February  = 2
        | March     = 3
        | April     = 4
        | May       = 5
        | June      = 6
        | July      = 7
        | August    = 8
        | September = 9
        | October   = 10
        | November  = 11
        | December  = 12
        | Jan = 1
        | Feb = 2
        | Mar = 3
        | Apr = 4
        | Jun = 6
        | Jul = 7
        | Aug = 8
        | Sep = 9
        | Oct = 10
        | Nov = 11
        | Dec = 12
        
    type TimeUnit = 
        | Days
        | Weeks
        | Months
        | Years
        
    type Frequency =
        | NoFrequency = -1     //!< null frequency
        | Once = 0             //!< only once, e.g., a zero-coupon
        | Annual = 1           //!< once a year
        | Semiannual = 2       //!< twice a year
        | EveryFourthMonth = 3 //!< every fourth month
        | Quarterly = 4        //!< every third month
        | Bimonthly = 6        //!< every second month
        | Monthly = 12         //!< once a month
        | EveryFourthWeek = 13 //!< every fourth week
        | Biweekly = 26        //!< every second week
        | Weekly = 52          //!< once a week
        | Daily = 365          //!< once a day
        | OtherFrequency = 999  //!< some other unknown frequency
   
    type Thirty360Convention =
        | USA
        | BondBasis
        | European
        | EurobondBasis
        | Italian
    
    type ActualActualConvention = 
        | ISMA
        | Bond
        | ISDA
        | Historical
        | Actual365
        | AFB
        | Euro 
        
    type DayCounterTypes =    
        | Actual360 
        | Actual365Fixed
        | ActualActual of ActualActualConvention
        | Business252  
        | One
        | Simple
        | Thirty360 of Thirty360Convention 
        
    type BusinessDayConvention =
        // ISDA
        | Following           (* Choose the first business day after
                                 the given holiday. *)
        | ModifiedFollowing   (* Choose the first business day after
                                 the given holiday unless it belongs
                                 to a different month, in which case
                                 choose the first business day before
                                 the holiday. *)
        | Preceding           (* Choose the first business day before
                                 the given holiday. *)
        // NON ISDA
        | ModifiedPreceding   (* Choose the first business day before
                                 the given holiday unless it belongs
                                 to a different month, in which case
                                 choose the first business day after
                                 the holiday. *)
        | Unadjusted          (* Do not adjust. *)
        

    
        

   