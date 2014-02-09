//module AlgoTrader.RiskFreeInterestRate
//
//open Microsoft.FSharp.Math;
//
///// V = Value
///// A = initial asset value?
///// r = annual compound interest rate
///// n = after 'n' years...
///// m = compounding occurs 'm' times per year
//let IntRate (compoundsPerYear:double) (initialValue:double) (finalValue:double) (time:double)
//     = compoundsPerYear * System.Math.Pow((finalValue/initialValue),(-(compoundsPerYear*time)))
//
//// http://antoniokantek.wordpress.com/2011/10/29/hello-world-or-just-calculating-compound-interest-rate-in-f/