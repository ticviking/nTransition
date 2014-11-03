eStateMachine: Simple Fluent State Machines. 
=============

eStateMachine is a simple fluent DSL to express state machines and enforce validity of propreties

Current state of development is on `develop`. This doesn't work yet, but contribs are welcome. The goal here is to do one thing, and do it well, and composably.

Useage
------

``` C# 
Class ThingWithFoo{
  static StateMachine<Foo> FooMachine = new StateMachine( (config) => {
    // We can allow a particular change without a callback
    config.When(Foo.Start).To(Foo.Second); 
    
    // We can also include custom logic on a change
    config.When(Foo.Second).To(Foo.End).
      Then( (old, new) =>
        WriteLine("We just went from Second to End");
        // It is important to rememver that this Then action is the setter
        // Do all DB or long computations in async tasks. 
        Task.Run( ()=> SendSomeEmail("to@someone.com") );
  });
 
  private Foo _foo;
 
  public Foo foo {
    get: {return _foo;}
    set { _foo == FooMachine.Set(_foo, value); }
  }
}
```
