# Blazor Movie Picks

Welcome to the Blazor Movie Picks! 
This project is a Blazor WebAssembly (WASM) application developed using .NET 8, based on the [CoderFoundry Youtube code along](https://www.youtube.com/watch?v=5NDIqqw7HrE)

## Features

- **Browse Popular Movies:** View a list of trending movies.
- **Movie Details:** Click on any movie to see its full details including synopsis and languages.

## Getting Started

To run this project locally, you'll need to set up a few things first:

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8)
- An IDE that supports Blazor development (e.g., [Visual Studio](https://visualstudio.microsoft.com/vs/), [Visual Studio Code](https://code.visualstudio.com/))

### Setup

1. **Clone the repository**
   ```bash
   git clone [https://github.com/VinceNL/BlazorMoviePicks.git]
   cd blazormoviepicks
   
2. API Key Configuration
   - Obtain a free API key by signing up at The Movie Database (TMDB).
   - Navigate to the wwwroot/appsettings.json file in your cloned repository.
   - Replace the placeholder value with your TMDB API key:
```
{
  "TMDBKey": "YOUR_API_KEY"
}
```
3. Run the application
   - Execute the following command in the root directory of your project:
  ```
  dotnet run
  ```
   - Open your web browser and navigate to http://localhost:5000 to start exploring movies.

### Deployment
This application is currently hosted and can be accessed at [Zealous Beach Azure Static Apps](https://zealous-beach-07c5a0110.5.azurestaticapps.net/movie/575264).

Acknowledgments
The Movie Database (TMDB) for providing the API used in this project.
CoderFoundry for fantastic free tutorials and full project builds.
