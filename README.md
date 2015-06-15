# Plantain
A component based visual scripting solution for Unity.

## Why Plantain?

### Built like Unity

Plantain is designed to mimic Unity's component system, with many different and small parts acting in unison. Individual components are easy to understand because they don't do much, and then can do many interesting things when combined later.

<a href="http://imgur.com/pdn60Up"><img src="http://i.imgur.com/pdn60Up.gif" title="source: imgur.com" /></a>

### Open Source and Free

Edit whatever you want, find out how something works, and adjust it to your preferences. Let me know what you've made and how it's been used. Feel free to do a pull request if you have a thing that you think others can use.

### Features banana-like-objects

Truely revolutionary.

## Extending Plantain

Derive one of the two following classes and everything should be pretty ok:

### Plantain.Performer

A Performer __does something__ based on it's current state.

* `bool state` - The on (true)/off (false) state of a Performer.
* `void PerformTrigger(TriggerOption)` - A Performer recieves a `TriggerOption` from a `Trigger` when a condition is met via this function. `PerformTrigger(TriggerOption)` sets the state for you.
 
### Plantain.Trigger

A Trigger __notices something__ based on extreaneous information, and then `Fire()`s when that condition is filled.

* `TriggerOption triggerOption` - What the trigger will tell Performers when it fires.
* `void Fire()` - Fires the trigger, letting any Performers on the same GameObject know that something has happened.
