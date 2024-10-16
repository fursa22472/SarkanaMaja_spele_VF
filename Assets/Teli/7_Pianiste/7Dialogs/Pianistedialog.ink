VAR option1 = false
VAR option2 = false
VAR option3 = false
VAR option4 = false
VAR option5 = false
VAR option6 = false

-> PianistIntro

=== PianistIntro ===
#audio:Pianist_7_01
"Oh, there you are! Did you hear that? The sound... it’s like everything finally snapped into place.  
You can’t see it, but it’s there, like a whisper in the dark. That’s music, you know? The only thing that makes sense."
-> A2


=== A2 ===
* [It’s clear you live for this. Music seems to be everything to you.] 
    ~ option1 = true
    #audio:Pianist_7_02
    "Everything? That word doesn’t do it justice. Music is like... breathing. It’s more than just sound; it’s a way to speak when words fail. When everything else is chaos, the notes align, and suddenly, there’s meaning."
    -> ReturnToChoices

* [What do you mean, “snapped into place”?] 
    ~ option2 = true
    #audio:Pianist_7_03
    "Like a puzzle piece falling into its spot after being lost for so long. I was playing, and for a moment, everything clicked. It’s hard to explain, but when it happens... it’s like the universe is listening."
    -> ReturnToChoices

* [I heard you’ve got a concert coming up. Is that what you’re preparing for?] 
    ~ option3 = true
    #audio:Pianist_7_04
    "Yeah. It’s not just any concert. It’s going to be the one where I make them hear me, really hear me. I’ve been putting everything I have into this piece. I can’t afford to be just background noise this time."
    -> ReturnToChoices

=== ReturnToChoices ===
{option1 and option2 and option3:
    -> 1A
- else:
    -> A2
}

=== 1A ===

#choice  
    * [I’d like to hear you play. Not just at the concert, but now.]
        #audio:Pianist_7_05
        "Really? You’re serious? Most people just want a performance, but you’re asking to *listen*. There’s a difference, you know. Alright, I’ll play something. But it’s not the whole piece. Just a glimpse. Close your eyes and listen. There... did you feel it? The way it built up, the tension? That’s what this concert is about. I’m going to take all that tension and make them *feel* it, make them understand."

        -> GoodIncline

    * [If you’re this dedicated, why aren’t you already famous?]
        #audio:Pianist_7_06
        "Hah, maybe because real music isn’t always what people want to hear. Or maybe it’s because I don’t play by their rules. It’s easier to shine if you fit the mold, but I’ve never been interested in that. Does that make me less dedicated? Maybe. Or maybe it means I’m doing it right."

        -> NeutralIncline

    * [I don’t think many people would understand what you’re trying to say.]
        #audio:Pianist_7_07
        "Maybe not, but that’s no excuse to stop trying. If everyone waited for the world to catch up, nothing would ever change. I’m not here to make them understand—I’m here to make them *feel*. Even if it’s just a flicker. But it sounds like you don’t get that... or maybe you’re just afraid to."

        -> BadIncline

=== GoodIncline ===

#audio:Pianist_7_08
    "You know... I’ve been playing a lot lately. Preparing for this concert, but also for something bigger. Like something’s building up inside of me, and it needs to be let out. I’d like you to be there. I think you’ll understand. Not many do."
    -> A3

=== A3 ===
    * [I’m intrigued, but what if they still don’t get it? What will you do then?] 
    ~ option4 = true
        #audio:Pianist_7_09
        "Then they’ll miss it, and that’s on them. But I’m not playing for everyone. I’m playing for the ones who can hear it, who can feel it. And if that’s just a handful, then so be it. I’d rather reach a few people deeply than impress a crowd that doesn’t care."

        -> ReturnToChoices2

    * [If it means so much to you, why invite me?] 
    ~ option5 = true
        #audio:Pianist_7_10
        "Because you’re different. You don’t just nod along. You question things. I see that, and I respect it. But more than that... I think you’ll listen. Not just to the music, but to what’s beneath it. That’s why I’m asking you to come, and I don’t ask lightly."

        -> ReturnToChoices2

    * [I’m not sure I’d understand, but I’m curious.] 
    ~ option6 = true
        #audio:Pianist_7_11
        "Curiosity is enough. It’s where everything starts, right? You don’t need to understand fully; you just need to be open. So come. Listen. And maybe you’ll find more than you expected. Or maybe you won’t... but at least you’ll know."

        -> ReturnToChoices2
        
        
        === ReturnToChoices2 ===
{option4 and option5 and option6:
    -> ContinueGoodPath
- else:
    -> A3
}

=== ContinueGoodPath ===

#audio:Pianist_7_12
    "Here, take this. An invite, just for you. Show it at the door, and they’ll let you in. You’ll have the best seat in the house. When I play, you’ll feel it, right in your bones."

+ [Thank you. I won’t miss it.]
    #audio:Pianist_7_13
    "Good. Don’t. ‘Cause this... this is everything. I don’t care if the world isn’t ready, but I am. And you? You’re gonna be there to see it happen."
     -> ConcertInvite

+ [Sounds like it’s going to be intense.]
    #audio:Pianist_7_14
    "Intense? Hah, you have no idea. It’s gonna be... wild. Beautiful. Maybe a little terrifying. But that’s how it should be, right? Real music doesn’t just sit there, it grabs you, drags you under, makes you feel something."
    -> ConcertInvite

=== NeutralIncline ===

#audio:Pianist_7_15
    "You know, I’ve been playing more than ever. Preparing for something big, but it’s like the more I play, the less they hear. It’s frustrating. It’s like shouting into a void."

#choice  
    * [Maybe they’re not ignoring you. They just need more time to understand.]
        #audio:Pianist_7_16
        "Time... yeah, maybe. But every second I’m not playing, I’m slipping further. I can’t wait for them to catch up. But you... you seem to understand a bit more. Maybe someday you’ll see what I’m trying to do."

        -> ContinueNeutralPath

    * [If they don’t hear you, maybe you’re not saying it the right way.]
        #audio:Pianist_7_17
        "Maybe. Or maybe they’re just not listening hard enough. I’m not about to change just to make it easier for them. If it’s worth hearing, it should be worth the effort. But maybe I’m asking too much."

        -> ContinueNeutralPath

    * [I think your message is strong, but not everyone’s ready for it.]
        #audio:Pianist_7_18
        "Yeah... I get that. Doesn’t make it any easier, though. I don’t have the luxury of waiting for them to catch up. I need them to hear it now, even if it means shouting until my voice breaks."

        -> ContinueNeutralPath

=== ContinueNeutralPath ===

#audio:Pianist_7_19
    "I’ve got to prepare. This concert... it’s like a storm, building up inside of me. But maybe it’s not meant for everyone. Not yet."

+ [I wish you luck. Maybe I’ll see you play someday.]
    #audio:Pianist_7_20
    "Maybe. But don’t hold your breath. I’m not here to be anyone’s background noise. I’ll be out there, playing louder than ever, and if you’re really listening... you’ll find me."
     -> NoConcert

+ [I understand. I’ll leave you to your music.]
    #audio:Pianist_7_21
    "Good. I need to be alone with it. This is all I’ve got, and I’m not sharing it with someone who doesn’t see. So... just go."
    -> NoConcert

=== BadIncline ===

#audio:Pianist_7_22
    "You know, people like you are the problem. Always dismissing music like it’s just... background noise. You think it’s just a hobby, a distraction. But for me, it’s survival. So don’t pretend to understand if you don’t."

#choice  
    * [Do you think you’re the only one who feels strongly about something?]
        #audio:Pianist_7_23
        "Of course not. But I’m the one willing to scream about it, even if no one listens. Maybe that’s not smart, but it’s honest. And if you don’t see that... well, I don’t see why we’re still talking."

        -> ContinueBadPath

    * [Your passion’s admirable, but you’re being a bit self-centered.]
        #audio:Pianist_7_24
        "Self-centered? Maybe. But it’s my music, my voice. If I don’t center myself, who will? I’d rather be selfish than silent. But you don’t get that, do you?"

        -> ContinueBadPath

    * [If it’s so important, why don’t you just make them listen?]
        #audio:Pianist_7_25
        "Oh, I plan to. But not for you. You don’t get it, and you never will. I’m done wasting my time explaining."

        -> ContinueBadPath

=== ContinueBadPath ===

#audio:Pianist_7_26
    "I’ve wasted enough time on you. You can go back to whatever it is you do, and I’ll keep playing for people who actually *get* it. Don’t bother showing up. You’re not invited."

-> END

=== ConcertInvite ===
-> END

=== NoConcert ===
-> END
