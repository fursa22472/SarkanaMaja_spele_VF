VAR previous_choice = ""
{ previous_choice == "Greet"}
{ previous_choice == "AskAdvice"}
{ previous_choice == ""}

~ previous_choice = "AskAdvice"
~ previous_choice = "Greet"
~ previous_choice = ""