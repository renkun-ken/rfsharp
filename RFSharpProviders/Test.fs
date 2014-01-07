module Test

let x = """
Hello, world!
"""
let y = 0
let z = String.length """
Hello, world!
"""
let w = String.length("""
function (x,y,z) {
    x <- rnorm(100)
    y <- 2*x+rnorm(100)*0.1
    m <- lm(y~x)
    print(m$coefficients[[2]])
}
""")