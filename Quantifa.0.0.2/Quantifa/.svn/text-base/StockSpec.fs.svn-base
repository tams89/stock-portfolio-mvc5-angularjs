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

open Quantifa 
    type Dividend =
        | Cash of float list
        | Continuous of float
    
    type StockSpec (s0 : float, sigma : float,  r : float, d : Dividend) = 
        new(s0 : float, sigma : float, r : float) = new StockSpec (s0, sigma, r, Continuous(0.0))
        
        member public x.s0 = s0
        member x.sigma = sigma
        member x.r = r
        member x.dividendCont = 
            match d with
                | Continuous (x) -> x
                | _ -> failwith "the dividend is not continuous"
        