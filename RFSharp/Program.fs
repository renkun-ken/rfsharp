module RFSharp.Program

open RDotNet
open RDotNet.NativeLibrary
open RDotNet.Internals
open RFSharp.REngine
open RFSharp.Extensions

R.Library("stats")
R.Library("tseries")

[<EntryPoint>]
let main argv = 
    for i in 1..100 do
        use rnorm1 = R.Evaluate("x <- rnorm(100)").AsNumeric()
        use rnorm2 = R.Evaluate("y <- 2*x+0.2*rnorm(100)").AsNumeric()
        use ts1 = R.Evaluate("ts1 <- ts(x)")
        let x1:double = ts1.ValueAt(10)
        
        use lm = R.Evaluate("m <- lm(y~x)")
        use pout = R.Evaluate("print(m)")
        use psummary = R.Evaluate("print(summary(m))")
        use coef = lm?coefficients.AsNumeric()
        printfn "%A" coef
        use test = R.Evaluate("adf.test(m$residuals)")
        let pvalue:double = test.Member("p.value").First()
        printfn "%f" pvalue
    0 // return an integer exit code
      
