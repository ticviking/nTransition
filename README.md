eStateMachine: Simple Fluent State Machines. 
=============

eStateMachine is a simple fluent api to define and validate changes. It can be used to model state machines, validate input, and run tasks when particular transitions occur.

The current version is v0.0.1 the api is definately still evolving, but as soon as there is a stable API I will tick release v1.0.0 and push a version to nuget. 

Defining A Machine
---

The StateMachine<type> constructor accepts a function to configure a transition table describing transitions between object of type. This table used internally to build the state machine used to validate transitions. Any `IComparable` can be used for the types.

``` C# 
var m = TransitionMachine<int>(
  (config) => {
    // Defines a valid transition from 1 to 2
    config.From(1).To(2).Done();
    // Defines a transition from 2 to 3 if the current user is admin
    config.From(2).To(3).If( () => return UserService.CurrentUser.IsAdmin).Done();
    // Defines a transition from 3 to 1 that logs the reset of the machine to a log function
    config.From(3).To(1).Then( () => LoggingService.Log("The statemachine has been reset")).Done();
  });

// a more formal state machine 
var fm = StateMachine<char, int>(
  (config) => {
    // The On method defines what input is acceptable
    config.On('a').From(1).To(2).Done();
    // On can also accept an Ienumerable of the input type.
    // State Machines can use both If, and Then clauses to check conditions or fire events when used
    config.On( new []{'b', 'c'}).From(2).To(3).If( () => return UserService.CurrentUser.IsAdmin).Done();
  });
```

Usage
------

TODO: Move the 
