VAR option1 = false
VAR option2 = false
VAR option3 = false

-> PianistIntro

=== PianistIntro ===
#audio:Pianist_7_01
"Oh, there you are! Did you hear that? The sound... it’s like everything finally snapped into place.  
You can’t see it, but it’s there, like a whisper in the dark. That’s music, you know? The only thing that makes sense."
-> A2


=== A2 ===
* [I’d like to hear you play. Not just at the concert, but now.] when (option1 == false)
    ~ option1 = true
    #audio:Pianist_7_02
    "Really? You’re serious? Most people just want a performance, but you’re asking to *listen*. There’s a difference, you know. Alright, I’ll play something. But it’s not the whole piece. Just a glimpse. Close your eyes and listen. There... did you feel it? The way it built up, the tension? That’s what this concert is about. I’m going to take all that tension and make them *feel* it, make them understand."
    -> ReturnToChoices

* [If you’re this dedicated, why aren’t you already famous?] when (option2 == false)
    ~ option2 = true
    #audio:Pianist_7_03
    "Hah, maybe because real music isn’t always what people want to hear. Or maybe it’s because I don’t play by their rules. It’s easier to shine if you fit the mold, but I’ve never been interested in that. Does that make me less dedicated? Maybe. Or maybe it means I’m doing it right."
    -> ReturnToChoices

* [I don’t think many people would understand what you’re trying to say.] when (option3 == false)
    ~ option3 = true
    #audio:Pianist_7_04
    "Maybe not, but that’s no excuse to stop trying. If everyone waited for the world to catch up, nothing would ever change. I’m not here to make them understand—I’m here to make them *feel*. Even if it’s just a flicker. But it sounds like you don’t get that... or maybe you’re just afraid to."
    -> ReturnToChoices

=== ReturnToChoices ===
{option1 and option2 and option3:
    -> 1A
- else:
    -> A2
}

=== 1A ===
# Continue with the next part of the story
"Great! You've explored all the options, now let's move on..."
-> DONE
