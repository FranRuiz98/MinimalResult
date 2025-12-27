# Result

A small, dependency-free C# library that models operation outcomes with `Result` and `Result<T>` value types, plus ASP.NET Core helpers to convert results into HTTP responses.

This repository contains two projects:

- `Result` (core) — a small value-based result type and `Error` struct. Target: .NET Standard 2.1.
  - Includes `Errors` helper factory methods and `ErrorCategory` / `ErrorCategoryParser` used to map error codes to HTTP status codes.
- `Result.AspNetCore` — ASP.NET Core integration helpers. Target: .NET 8.

Why use this library

- Clear explicit representation of success/failure without exceptions.
- Lightweight, immutable value types (`struct`) with no runtime dependencies.
- Small set of functional helpers (`Match`, `Map`, `Bind`) to compose code safely.
- Helpers to convert results into HTTP responses for Minimal APIs and MVC controllers.

Core types

- `Result` — represents success or failure for operations that do not return a value.
  - Properties: `IsSuccess`, `IsFailure`, `Error`.
  - Factory methods: `Result.Success()`, `Result.Failure(Error)`.

- `Result<T>` — represents success carrying a value of type `T` or a failure.
  - Properties: `IsSuccess`, `IsFailure`, `Value`, `Error`.
  - Factory methods: `Result<T>.Success(T)`, `Result<T>.Failure(Error)`.

- `Error` — lightweight readonly struct with `Code` and `Message`.
  - A static `Error.None` value represents absence of an error.

- `Errors` — helper factory methods in the core project to construct categorized errors, e.g. `Errors.Validation("Name", "message")` produces an error with code `"Validation.Name"`.

- `ErrorCategory` and `ErrorCategoryParser` — public helpers in the core project used by `Result.AspNetCore` to determine HTTP status codes from error codes.

Functional helpers (extensions)

- `Match` — pattern match a `Result` or `Result<T>` into a return value.
  - `result.Match(() => onSuccess, () => onFailure)` for non-generic `Result`.
  - `resultT.Match(value => onSuccess, error => onFailure)` for `Result<T>`.

- `Map` — transform the success value of a `Result<T>` preserving failure: `r.Map(x => ...)`.
- `Bind` — chain non-generic `Result` computations: `r.Bind(() => anotherResult)`.

ASP.NET Core integration (`Result.AspNetCore`)

- Minimal APIs (returns `IResult`):
  - `result.ToHttpResult()` — returns `Results.Ok()` on success or `Results.Problem(...)` on failure.
  - `resultT.ToHttpResult()` — returns `Results.Ok(value)` on success.

- MVC controllers (returns `IActionResult`):
  - `result.ToActionResult()` — returns `OkResult` on success or an `ObjectResult` with `ProblemDetails` on failure.
  - `resultT.ToActionResult()` — currently returns `OkResult` on success (no body). Change to `OkObjectResult` if you want the value returned in the response body.

Error codes and HTTP mapping

- Error codes use a category prefix, e.g. `"Validation.Name"`, `"NotFound.Item"`, `"Conflict.Id"`.
- Mapping is implemented in the core via `ErrorCategoryParser` and consumed by the ASP.NET Core project:
  - `Validation.` -> 400 Bad Request
  - `NotFound.` -> 404 Not Found
  - `Conflict.` -> 409 Conflict
  - `Unauthorized.` -> 401 Unauthorized
  - `Forbidden.` -> 403 Forbidden
  - `Failure.` -> 500 Internal Server Error

Examples

Core usage

```csharp
using Result;

var ok = Result.Success();
var fail = Result.Failure(new Error("Validation.Name", "Name is required"));

var valueOk = Result<int>.Success(42);
var valueFail = Result<int>.Failure(new Error("NotFound.Item", "Item not found"));

// Match
var message = ok.Match(() => "OK", () => "Failed");
int doubled = valueOk.Match(v => v * 2, err => -1);

// Map
var mapped = valueOk.Map(v => (v * 2).ToString()); // Result<string>

// Bind
var chained = ok.Bind(() => Result.Failure(new Error("Failure.Server", "Unexpected")));
```

ASP.NET Core (Minimal API)

```csharp
using Result.AspNetCore;

IResult r = Result.Success();
return r.ToHttpResult();

IResult r2 = Result<int>.Failure(new Error("NotFound.Item", "Not found"));
return r2.ToHttpResult();
```

MVC Controller

```csharp
using Result.AspNetCore;

public IActionResult Get()
{
    Result<int> r = ...;
    return r.ToActionResult();
}
```

Build and tests

- Build the solution:

  dotnet build

- Run tests (test project targets .NET 8):

  dotnet test

Contributing

Contributions are welcome. Open an issue or submit a pull request. Keep changes small and add or update tests where appropriate.