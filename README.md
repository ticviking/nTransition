eStateMachine: Simple Fluent State Machines. 
=============

eStateMachine is a simple fluent api to define and validate changes. It can be used to model state machines, validate input, and run tasks when particular transitions occur.

The current version is v0.0.1 the api is definately still evolving, but as soon as there is a stable API I will tick release v1.0.0 and push a version to nuget. 

Defining A Machine
---

`TransitionMachine<TState>` provide a class that can represent any set of transitions. The constructor accepts a function to configure a transition table describing transitions between entities of type `TState`. Any `IComparable` can be used for the `TState`. This table used internally to validate transitions.


``` C# 
var trasitionMachine = new TransitionMachine<int>(
  (config) => {
    // Defines a valid transition from 1 to 2
    config.From(1).To(2).Done();
    // Defines a transition from 2 to 3 if the current user is admin
    config.From(2).To(3).If( () => UserService.CurrentUser.IsAdmin).Done();
    // Defines a transition from 3 to 1 that logs the reset of the machine to a log function
    config.From(3).To(1).Then( () => LoggingService.Log("The statemachine has been reset")).Done();
  });
```

`StateMachine<TInput, TState>` defines a more formal finite state machine. It is like the transition machine but adds the `On(TInput)` & `On(IEnumerable<TInput>)` methods to define what inputs can trigger that transition. Defining a FSM using this API is the same as calling `On(Input).From(Start).To(End)` for each edge in the FSM.

``` C#
var fm = new StateMachine<char, int>(
  (config) => {
    // The On method defines what input is acceptable
    config.On('a').From(1).To(2).Done();
    // On can also accept an Ienumerable of the input type.
    // State Machines can use both If, and Then clauses to check conditions or fire events when used
    config.On( new []{'b', 'c'}).From(2).To(3).If( () => return UserService.CurrentUser.IsAdmin).Done();
  });
```

Using a Machine
------

TODO: Move the example code out to here.
