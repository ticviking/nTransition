nTransition: Simple Fluent State Machines. 
=============

nTransition is a simple fluent api to define and validate changes. 

It is currently used to maintain bussiness logic for a custom CRM for Utah State University, and has been used to write some toy text adventures, and state machines.  

It is made available under the MIT Licence.


Defining A Machine
---

`TransitionMachine<TState>` provide a simple in-line way to represent a set of transitions. The constructor accepts a function to configure a transition table describing transitions between entities of type `TState`. Any `IComparable` can be used for the `TState`. This table used internally to validate transitions.


``` C# 
var trasitionMachine = new TransitionMachine<int>(
  (config) => {
    // Defines a valid transition from 1 to 2
    config.From(1).To(2).Done();
    // Defines a transition from 2 to 3 if the current user is admin
    config.From(2).To(3).When( () => UserService.CurrentUser.IsAdmin).Done();
    // Defines a transition from 3 to 1 that logs the reset of the machine to a log function
    config.From(3).To(1).Do( () => LoggingService.Log("The statemachine has been reset")).Done();
  });
```

Using A Machine
---

Once you have an instance of `TransitionMachine` you can use it as a setter to prevent invalid changes to the state of the object.
``` c#
class Foo {
  static TransitionMachine machine = // using the above example

  public int State {
      get;
      set { return machine.DoTransition(State, value) }
}}
```

A Foo will then throw an InvalidTransitionException if you attempt to assign ints to it in a way that violates the states you have defined. {Note 


### Upcoming Features

[ ] - `StateMachine<TInput, TState>` defines a more formal finite state machine. It is like the transition machine but adds the `On(TInput)` & `On(IEnumerable<TInput>)` methods to define what inputs can trigger that transition. Defining a FSM using this API is the same as calling `On(Input).From(Start).To(End)` for each edge in the FSM.

[ ] - A better way to define a transition or state machine statically.
