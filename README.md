# Result

A small, zero-dependency C# library that represents operation results as `Result` and `Result<T>` value types and a simple `Error` struct. It also includes ASP.NET Core helpers to convert `Result` values into HTTP responses.

Supported targets

- `Result` core library: .NET Standard 2.1
- `Result.AspNetCore`: .NET 8

Quick overview

- `Result` — non-generic success/failure value with an `Error`.
- `Result<T>` — success holding a `T` value or a failure with an `Error`.
- `Error` — simple readonly struct with `Code` and `Message`.
- `Extensions` — functional helpers: `Match`, `Map`, `Bind`.
- `Result.AspNetCore` — MVC and minimal API converters: `ToActionResult`, `ToHttpResult`.

Install

This repository contains two projects. Add a project reference from your application or package the projects as you prefer.

Build and run tests

```bash
dotnet build
dotnet test
```

Usage examples

Create results

```csharp
using Result;

var ok = Result.Success();
var fail = Result.Failure(new Error("Validation.Name", "Name is required"));

var valueOk = Result<int>.Success(42);
var valueFail = Result<int>.Failure(new Error("NotFound.Item", "Item not found"));
```

Match to handle cases

```csharp
var message = ok.Match(() => "OK", () => "Failed");

int doubled = valueOk.Match(v => v * 2, err => -1);
```

Map and Bind

```csharp
var mapped = valueOk.Map(v => (v * 2).ToString()); // Result<string>

var chained = ok.Bind(() => Result.Failure(new Error("Failure.Server", "Unexpected")));
```

ASP.NET Core integration

Minimal APIs (IResult)

```csharp
using Result.AspNetCore;

IResult r = Result.Success();
return r.ToHttpResult();

IResult r2 = Result<int>.Failure(new Error("NotFound.Item","Not found"));
return r2.ToHttpResult();
```

MVC controllers

```csharp
using Result.AspNetCore;

public IActionResult Get()
{
    Result<int> r = ...;
    return r.ToActionResult();
}
```

Behavior notes

- Failures are represented by `Error.Code` and `Error.Message`.
- `ResultMvcExtensions` maps failures to `ObjectResult` containing `ProblemDetails` and sets an appropriate HTTP status code using an internal `HttpStatusMapper` that inspects the `Error.Code` prefix (examples: `Validation.` -> 400, `NotFound.` -> 404, `Conflict.` -> 409, etc.).
- `Result<T>.ToActionResult()` currently returns an `OkResult` on success (no body). If you prefer `OkObjectResult` with the value, you can change the implementation in `ResultMvcExtensions`.

Contributing

Contributions are welcome. Open issues or PRs for bug fixes and improvements.

License

This project does not contain a license file. Add a license before redistributing or publishing the package.
