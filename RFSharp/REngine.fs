module RFSharp.REngine

open RDotNet

let R = REngine.CreateInstance("R_INSTANCE", @"C:\Program Files\R\R-3.0.2\bin\x64\R.dll")

R.Initialize()