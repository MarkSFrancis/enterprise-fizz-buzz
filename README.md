# Enterprise FizzBuzz
An enterprise edition of the FizzBuzz game in C#

## FizzBuzz.DependencyInjection

Dependency injection is built custom for this application, and so an entire start-up engine, service provider and service builder is built in.

All applications should start with the following:
1. Construct an instance of `FizzBuzzEngine` with your instance of `IStartup`
1. Initialise the `FizzBuzzEngine` and any services your `IStartup` needs by calling `Build()` on your instance of `FizzBuzzEngine`
1. Run the application by calling either `Run` or `RunAsync` on your instance of `FizzBuzzEngine`. `Run` will stop further execution until your `IStartup` is finished executing.

Application state is managed via a provided implementation of `IStartup`, which is called after the `FizzBuzzEngine` has done any necessary start up.

You can add any core required parameters for startup into the constructor for your `IStartup` implementation, these will be injected automatically for you by the engine.

The following services are provided by default in `FizzBuzzEngine`:

* `IFizzBuzzService`
* `IServiceFactory`

Your `IStartup` can depend on any combination of these (including none or all).

Your `IStartup` can also replace either of these items with its own instances in the `AddServices` method - just add them as if they weren't already present, and you'll overwrite the existing instances

## FizzBuzz

This core library represents the core-most parts and logic for FizzBuzz. It contains a single service: `FizzBuzzService` which can be configured via the constructor to play any variance of FizzBuzz

## FizzBuzz.Console

This is a console implementation of the FizzBuzz game, with the necessary services required for console input/output.

`Startup` manages setting up dependencies and application configuration

`IFizzBuzzApp` manages running the application

`IFizzBuzzConsole` manages FizzBuzz specific input and output to the console

`IConsoleIo` manages all console input and output