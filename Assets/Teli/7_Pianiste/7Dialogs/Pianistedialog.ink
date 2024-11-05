VAR option1 = false
VAR option2 = false
VAR option3 = false
VAR option4 = false
VAR option5 = false
VAR option6 = false

-> PianistIntro

=== PianistIntro ===
#audio:Pianist_7_01
Ak, tur tu esi! Dzirdi? Skaņa... šķiet, ka viss beidzot ir savās vietās.
Tu gan jau, ka nedzirdēsi, bet ši akustika ir kā atsevišķs skaņdarbs. It kā zvaigznes spēlē savas notis.
-> A2


=== A2 ===
* [Ļoti skaista melodija.] 
    ~ option1 = true
    #audio:Pianist_7_02
    Līdzīgi vārdiem. Tu runā līdz tu sāc saprast nozīmi. Tas ir mans runas veids - Klavires.
    -> ReturnToChoices

* [Es nedzirdu zvaigznes] 
    ~ option2 = true
    #audio:Pianist_7_03
    Viņas pašas bez manis nevar spēlēt. It kā šis ir orķestris un viņas spēlē pavadījumu. Trešā roka.
    -> ReturnToChoices

* [Tad tev būs koncerts šeit?] 
    ~ option3 = true
    #audio:Pianist_7_04
    Mans pirmais koncerts šeit. Es spēlēju lielajās zālēs, bet aizmisu, cik patīkami būt vienai un spēlet tikai jums. 
    -> ReturnToChoices

=== ReturnToChoices ===
{option1 and option2 and option3:
    -> 1A
- else:
    -> A2
}

=== 1A ===
    * [Es gribētu dzirdēt vēl kaut ko tagad.]
        #audio:Pianist_7_05
        Man ir prieks, ka tev neinteresē priekšnesums tik ļoti, cik pats darbs. Tomēr, es vēl nevaru atklāt visu, savādāk nebūs tā paša efekta... Bet tā es spēlēju pārāk bieži. Varbūt man vajag atpūšties. Beigās dzirdu tikai zvaigznes.
        -> A3

    * [Tev ir talants, bet tu neesi slavenība?]
        #audio:Pianist_7_06
        Kāpēc tu domā, ka neesmu? Esmu savu draugu lokā. Vairāk jau nevajag. 
        -> NeutralIncline

    * [Man liekas cits nesaprastu par zvaigznēm...]
        #audio:Pianist_7_07
        Es nevēlos izskaidrot to. To nevar paskaidrot ar vādiem, tur ir tikai mūzika.
        -> BadIncline


=== A3 ===
    * [Bet ja nu man nepatiks?] 
    ~ option4 = true
        #audio:Pianist_7_09
        Tas ir nekas, galvenais, lai tu jūti līdzi. Vienmēr var aiziet.

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
