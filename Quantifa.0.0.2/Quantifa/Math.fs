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

// ====================================================
// Copyright (C) 2009 by Ning Cao
// 
// Applies only to the modified part of this fuction.
// This function is modified by Ning Cao into F# based 
// on code written in C by Sun Microsystems, Inc.
// Permission to use, copy, modify, and distribute this
// software is freely granted, provided that this notice
// is preserved.
//
// ====================================================
// Copyright (C) 1993 by Sun Microsystems, Inc. All rights reserved.
//
// Developed at SunPro, a Sun Microsystems, Inc. business.
// Permission to use, copy, modify, and distribute this
// software is freely granted, provided that this notice
// is preserved.
//
// ====================================================

#light
module Quantifa.Math

    open System
    open Quantifa
    open Quantifa.Const
    //Error Function    
    let erf x =
    
        let tiny = Epsilon
        let one =  1.00000000000000000000e+00
        let erx =  8.45062911510467529297e-01

        // Coefficients for approximation to erf on [0, 0.84375)

        let efx  =  1.28379167095512586316e-01
        let efx8 =  1.02703333676410069053e+00
        let pp0  =  1.28379167095512558561e-01
        let pp1  = -3.25042107247001499370e-01
        let pp2  = -2.84817495755985104766e-02
        let pp3  = -5.77027029648944159157e-03
        let pp4  = -2.37630166566501626084e-05
        let qq1  =  3.97917223959155352819e-01
        let qq2  =  6.50222499887672944485e-02
        let qq3  =  5.08130628187576562776e-03
        let qq4  =  1.32494738004321644526e-04
        let qq5  = -3.96022827877536812320e-06

        // Coefficients for approximation to erf in [0.84375, 1.25)

        let pa0  = -2.36211856075265944077e-03
        let pa1  =  4.14856118683748331666e-01
        let pa2  = -3.72207876035701323847e-01
        let pa3  =  3.18346619901161753674e-01
        let pa4  = -1.10894694282396677476e-01
        let pa5  =  3.54783043256182359371e-02
        let pa6  = -2.16637559486879084300e-03
        let qa1  =  1.06420880400844228286e-01
        let qa2  =  5.40397917702171048937e-01
        let qa3  =  7.18286544141962662868e-02
        let qa4  =  1.26171219808761642112e-01
        let qa5  =  1.36370839120290507362e-02
        let qa6  =  1.19844998467991074170e-02

        // Coefficients for approximation to erf in [1.25, 1/0.35)

        let ra0  = -9.86494403484714822705e-03
        let ra1  = -6.93858572707181764372e-01
        let ra2  = -1.05586262253232909814e+01
        let ra3  = -6.23753324503260060396e+01
        let ra4  = -1.62396669462573470355e+02
        let ra5  = -1.84605092906711035994e+02
        let ra6  = -8.12874355063065934246e+01
        let ra7  = -9.81432934416914548592e+00
        let sa1  =  1.96512716674392571292e+01
        let sa2  =  1.37657754143519042600e+02
        let sa3  =  4.34565877475229228821e+02
        let sa4  =  6.45387271733267880336e+02
        let sa5  =  4.29008140027567833386e+02
        let sa6  =  1.08635005541779435134e+02
        let sa7  =  6.57024977031928170135e+00
        let sa8  = -6.04244152148580987438e-02

        // Coefficients for approximation to erf in [1/0.35, 6)

        let rb0  = -9.86494292470009928597e-03
        let rb1  = -7.99283237680523006574e-01
        let rb2  = -1.77579549177547519889e+01
        let rb3  = -1.60636384855821916062e+02
        let rb4  = -6.37566443368389627722e+02
        let rb5  = -1.02509513161107724954e+03
        let rb6  = -4.83519191608651397019e+02
        let sb1  =  3.03380607434824582924e+01
        let sb2  =  3.25792512996573918826e+02
        let sb3  =  1.53672958608443695994e+03
        let sb4  =  3.19985821950859553908e+03
        let sb5  =  2.55305040643316442583e+03
        let sb6  =  4.74528541206955367215e+02
        let sb7  = -2.24409524465858183362e+01
        
        let ax = abs x
        if ax < 0.84375 then
        // erf(x) for x in [0, 0.84375)
        //        if ax < 3.7252902984e-09 then
        //            if ax < 6.9388939039e-18 then 0.125 * (8.0 * x + efx8 * x)
        //            else x + efx * x
            let z = x * x
            let r = pp0 + z * (pp1 + z * (pp2 + z * (pp3 + z * pp4)))
            let s = one + z * (qq1 + z * (qq2 + z * (qq3 + z * (qq4 + z * qq5))))
            let y = r/s
            x + x * y
        elif ax < 1.25 then
        // erf(x) for |x| in [0.84375, 1.25)
            let s = ax - one
            let P = pa0 + s * (pa1 + s * (pa2 + s * (pa3 + s * (pa4 + s * (pa5 + s * pa6)))))
            let Q = one + s * (qa1 + s * (qa2 + s * (qa3 + s * (qa4 + s * (qa5 + s * qa6)))))
            if x>= 0.0 then erx + P / Q
            else - erx - P / Q
        elif ax < 2.857142 then
        // erf(x) for |x| in [1.25, 2.857142) 
            let s = one / x * x
            let R = ra0 + s * (ra1 + s * (ra2 + s * (ra3 + s * (ra4 + s * (ra5 + s * (ra6 + s * ra7))))))
            let S = one + s * (sa1 + s * (sa2 + s * (sa3 + s * (sa4 + s * (sa5 + s * (sa6 + s * (sa7 + s * sa8)))))))
            let r = exp( -0.5625 - x * x + R / S)
            if x >= 0.0 then one - r / x
            else - r / x - one
        elif ax < 6.0 then
        // erf(x) for |x| in [1/0.35, 6)
            let s = one / x * x
            let R = rb0 + s * (rb1 + s * (rb2 + s * (rb3 + s * (rb4 + s * (rb5 + s * rb6)))))
            let S = one + s * (sb1 + s * (sb2 + s * (sb3 + s * (sb4 + s * (sb5 + s * (sb6 + s * sb7))))))
            let r = exp( -0.5625 - x * x + R / S)
            if x >= 0.0 then one - r / x
            else - r / x - one
        else
        // erf(x) for |x| in [6,inf)
            if x >= 0.0 then one - tiny
            else tiny - one