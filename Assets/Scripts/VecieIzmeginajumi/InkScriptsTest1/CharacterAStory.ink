INCLUDE globals.ink
-> Start

== Start ==
* [Option 1: Greet the character] -> Greet
~ previous_choice = "Greet"
* [Option 2: Ask for advice] -> AskAdvice
~ previous_choice = "AskAdvice"

== Greet ==

Character 1:  Hello! Nice to meet you.
-> END

== AskAdvice ==

Character 1: I would recommend checking out the village nearby.
-> END
