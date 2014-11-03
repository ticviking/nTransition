eStateMachine: Simple Fluent State Machines. 
=============

eStateMachine is a simple fluent DSL to express state machines and enforce validity of propreties

Useage
------

``` C# 
Class ThingWithFoo{
  static StateMachine<Foo> FooMachine = new StateMachine( (config) => {
    config.When(Foo.Start).To(Foo.Second)
    config.When(Foo.Second).To(Foo.End).
      Then( (old, new) =>
        WriteLine("We just went from Second to End");
  });
 
  private Foo _foo;
 
  public Foo foo {
    get: {return _foo;}
    set { _foo == FooMachine.Set(_foo, value); }
  }
}
```
