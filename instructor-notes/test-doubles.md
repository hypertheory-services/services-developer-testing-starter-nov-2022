# Test Doubles

Replacements for "real" code we use in testing.

## Why?

1. To simulate scenarious to inspect how our code reacts.
2. To replace "expensive" things that are hard to run in our unit tests.
    - API calls, database stuff, etc.
    - Your Unit tests should take <5s to run. Ponder that.

## Types of Test Doubles
This is just ways to use them. They aren't "things"...

- Dummy
    - Isn't part of the test. Just has to be there to fill a paremeter. 
```csharp
 var account = new BankAccount(new Mock<INotifyOfOverdrafts>().Object);
```
- Mock - record interactions and can be verified after the code is exercised.


- Stub
    - provide canned responses to questions.
    - they don't pass or fail the test.
- Fake