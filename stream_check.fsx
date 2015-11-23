#r "packages/FSharp.Data/lib/net40/FSharp.Data.dll"
open FSharp.Data

// Patterns http://regexr.com/3c936

type MovieSearch = JsonProvider<"samples/search.json">
type MovieDetails = JsonProvider<"samples/details.json">

let searchApiUrl = "http://www.canistream.it/services/search"
let queryApiUrl  = "http://www.canistream.it/services/query"

// Search for the movie by name.
let searchMovie movie =
   Http.RequestString (searchApiUrl,
    query = ["movieName", movie; ],
    headers = [HttpRequestHeaders.UserAgent "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36"] ) |> 
   MovieSearch.Parse
   
// Get the details of the movie

// get list of things to search for

   
