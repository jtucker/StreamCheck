#r "packages/FSharp.Data/lib/net40/FSharp.Data.dll"
open FSharp.Data
open FSharp.Data.JsonExtensions

// Patterns http://regexr.com/3c936

type MovieSearch = JsonProvider<"samples/search.json">
type MovieDetails = JsonProvider<"samples/details.json">
 
 
let httpHeaders = [HttpRequestHeaders.UserAgent "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36"]
let searchApiUrl = "http://www.canistream.it/services/search"
let queryApiUrl  = "http://www.canistream.it/services/query"

// Search for the movie by name.
let searchMovie movie =
   Http.RequestString (searchApiUrl,
    query = ["movieName", movie; ],
    headers = httpHeaders ) |> 
   MovieSearch.Parse

// get movie MovieDetails
let getMovieDetails movieId =
    Http.RequestString(queryApiUrl, 
        query = ["movieId", movieId; "attributes", "1"; "mediaType", "streaming"], 
        headers = httpHeaders) |> 
    MovieDetails.Parse

let streamble (movie:MovieDetails.Root) =
    match movie.JsonValue.Properties with
    | x when Array.isEmpty x -> printfn "no streams found"
    | x -> 
        Array.iter (fun (name, value) -> 
          let friendlyName = value?friendlyName.AsString()
          printfn "Stream: %s" friendlyName
        ) x
    
let canistream moviename = 
    searchMovie(moviename) 
    |> Array.tryHead 
    |> function 
       | Some x -> streamble(getMovieDetails(x.Id))
       | None -> printfn "not found"