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
module Quantifa.Distributions
    open System
    open Quantifa
    open Quantifa.Const
    
    let normalCDF x average sigma = 
        let z = (x - average) / sigma
        0.5 * ( 1.0 + Math.erf( z * M_SQRT_2 ) )
        // todo: if result<=1e-8 then
        // todo: investigate the threshold level
        // Asymptotic expansion for very negative z following (26.2.12)
        // on page 408 in M. Abramowitz and A. Stegun,
        // Pocketbook of Mathematical Functions, ISBN 3-87144818-4.
    
    let normalPDF x average sigma =
        let z = (x - average) / sigma
        let normalizationFactor = M_SQRT_2 * M_1_SQRTPI / sigma
        normalizationFactor * exp (- 0.5 * z * z)
        
    let standardNormalCDF z =
        0.5 * ( 1.0 + Math.erf( z * M_SQRT_2 ) )
        
    let StandardNormalPDF z =
        let normalizationFactor = M_SQRT_2 * M_1_SQRTPI
        normalizationFactor * exp (- 0.5 * z * z)
        
        
        
            
    