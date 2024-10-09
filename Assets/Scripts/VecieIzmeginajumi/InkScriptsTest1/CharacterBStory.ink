INCLUDE globals.ink

-> Start

== Start ==
{ previous_choice == "Greet":
    Character 2: So you greeted my friend earlier, right?
}
{ previous_choice == "AskAdvice":
    Character 2: Did you get some advice from my friend?
}
{ previous_choice == "":
    Character 2: I'm not sure what you did before meeting me, but here I am!
}

* [Respond: Yes] -> YesResponse
* [Respond: No] -> NoResponse

== YesResponse ==
Character 2: Great! I hope it was helpful.
-> END

== NoResponse ==
Character 2: Hmm, you should talk to them again.
-> END
