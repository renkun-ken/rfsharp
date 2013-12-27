module RFSharpProviders.Extensions

open RDotNet
open RDotNet.Internals

type REngine with
    
    /// Load a library in current R environment.
    member this.Library(name: string) = 
        this.Evaluate(sprintf "library(%s)" name) |> ignore
    
    /// Require a library in current R environment.
    member this.Require(name: string) = 
        this.Evaluate(sprintf "require(%s)" name) |> ignore
    
    /// Run the source code in a file.
    member this.Source(filename: string) = 
        this.Evaluate(sprintf "source(%s)" filename)
    
    /// Assign a symbolic expression to a symbol and return the expression.
    member this.AssignTo (name: string) (expression: SymbolicExpression) = 
        this.SetSymbol(name, expression)
        expression

type SymbolicExpression with
    
    /// Get the member symbolic expression of given name.
    member this.Member(name: string) = 
        match this.Type with
        | SymbolicExpressionType.List -> this.AsList().[name]
        | SymbolicExpressionType.S4 -> this.GetAttribute(name)
        | _ -> invalidOp "Unsupported operation on R object"
    
    /// Get the value from a named vector by name.
    member this.ValueOf<'a>(name: string) = this.AsVector().[name] :?> 'a
    
    /// Get the value from an indexed vector by index.
    member this.ValueAt<'a>(index: int) = this.AsVector().[index] :?> 'a
    
    /// Convert the vector to an array.
    member this.ToArray<'a>() = 
        this.AsVector()
        |> Seq.cast<'a>
        |> Seq.toArray
    
    /// Convert the vector to a list.
    member this.ToList<'a>() = 
        this.AsVector()
        |> Seq.cast<'a>
        |> Seq.toList
    
    /// Get the first value of a vector.
    member this.First<'a>() = this.ValueAt<'a>(0)

let inline (?) (expr:SymbolicExpression) (mem:string) =
    expr.Member(mem)