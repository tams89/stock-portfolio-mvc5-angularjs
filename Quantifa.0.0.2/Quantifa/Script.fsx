// This file is a script that can be executed with the F# Interactive.  
// It can be used to explore and test the library project.
// Note that script files will not be part of the project build.
module Quantifa.l
#load "Types.fs"
#load "Period.fs"
#load "Date.fs"
#load "DayCounter.fs"


open Quantifa.DayCounter

    let a = dayCount((1,1,2001),(1,1,2003));;