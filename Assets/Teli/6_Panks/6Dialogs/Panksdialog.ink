#audio:Punk_5_N_01
 Jā? 

+ [Čau..?]
    #audio:Punk_5_N_02
    Čau, ja domā pievienoties, piedod šoreiz nē. Esmu tāds skaļš kaimiņš, zinu. Kā tev iet?
    -> 1A

+ [Tu izskaties tā... interesanti]
    #audio:Punk_5_N_03
    Nu, paldies. Tā kā daudz ko pasaka par mani. Man patīk tavs stils. Tev ir ..cik? Nū, 14? 15? Man tā neļāva vecāki ar basām kājām skraidīt ārā. Nu labi, kā tev iet?
    -> 1A

+ [Kas šeit notiek?]
    #audio:Punk_5_N_04
    Godīgi? Es nezinu, mani it kā pasauca draugs. Viņš kaut kur aizgāja. Un kas ar tevi notiek? Kur ej? Kā iet?
    -> 1A

=== 1A ===

#choice  
    * [Tevi uztver nopietni šādās drēbēs?]
        #audio:Punk_5_N_05
        Jā? N...ē? Skatoties kurš. Pagaidi, zini, man tu patīc. Tātad, īsumā. Nezinu vai tu zini, bet es šitā uz darbu neeju. Tikai šodien sanāca. Tāpēc, nu uztver nopietni. Ne visi. Kam man to? Kas ir nopietni? Vai es varu ar tevi parunāt? Vai tev rūp, kā citi tevi redz? Šis ir ļoti filozofiski.
        -> CasualContinue

    * [Es nevaru domāt tā kā tu. Man laikam nedrīkstētu nedomāt par citiem. Arī tādas drēbes nevilktu.]
        #audio:Punk_5_N_06
        Nevari vai negribi? Un kāpēc nedrīkt? Drīkst. 
        -> CasualContinue

    * [You sure seem to think you’ve got it all figured out.]
        #audio:Punk_5_N_07
        "Figured out? Nah, man. I ain’t got the answers, and I don’t pretend to. But I got my own way of dealin’ with things. My own way of walkin’ through this mess. That’s better than followin’ someone else’s map. But you... you think you’re on the right track, or just wanderin'?"
        -> CasualContinue

=== CasualContinue ===

#choice  
    * [But don’t you get tired of always fighting the system?]
        #audio:Punk_5_B_08
        "Fightin’ the system? Nah, that’s givin’ 'em too much credit. They want you to think they're in control, but really, they’re just flailin', tryin' to keep the whole thing from fallin' apart. I don’t fight it. I just... slip through the cracks, y'know? They don’t even see me comin’. So what about you? You think you can slip by unnoticed?"
        -> BadPath1

    * [I guess that’s true. People are too stuck on what others think.]
        #audio:Punk_5_G_09
        "Exactly. People get caught up chasin’ approval, like it’s gonna fill somethin’ inside them. But it won’t. You waste your time tryna make everyone else happy, you end up forgettin’ who you are. And once that happens... you’re just another ghost wanderin’ around. But hey, do you think you’d notice if you started slippin’ away?"
        -> GoodPath1

    * [Sounds like you’re running away more than living your truth.]
        #audio:Punk_5_B_10
        "Heh, maybe. Or maybe I’m just pickin’ my battles. Sometimes it ain’t about winnin', it’s about not lettin' 'em grind you down. You think you’d have the guts to push back, or would you just fold?"
        -> NeutralPath1

=== BadPath1 ===

#audio:Punk_5_B_11
    "You can’t control how the world spins, but you can control where you stand. That’s what matters. You live for you, the rest? It just fades out. People wanna talk, let 'em. They’re just talkin’ to themselves anyway. But you, you’re standin’ here listenin'—what’re you hopin' to hear?"

#choice  
    * [Maybe you’re right. Better to live for yourself.]
        #audio:Punk_5_B_12
        "See, that’s it. Don’t waste time playin’ to the crowd. The fakes will always find somethin' to say, but you? You just keep bein’ you. Life’s too short to be anyone else. But don’t get it twisted—it ain’t as easy as it sounds."
        -> BadEndingLead

    * [I still think there’s more to life than just doin’ your own thing.]
        #audio:Punk_5_B_13
        "More to life? Sure, but you can’t carry everyone else’s expectations on your back. You do, and they’ll crush you. So yeah, there’s more, but it starts with you holdin' your ground. Or do you think you can stand firm while carryin' the weight?"
        -> FinalChoice

=== GoodPath1 ===

#audio:Punk_5_G_14
    "You got it. People keep chasin’ somethin' that ain’t even real, like approval’s a prize they can hold. But it’s a mirage. You waste your time tryna make everyone else happy, you end up forgettin’ who you are. And once that happens... you’re just another ghost wanderin’ around. So, are you a ghost, or still hangin' on?"

#choice  
    * [Yeah, I don’t really care what people think either.]
        #audio:Punk_5_G_15
        "That’s the way to be, man. We’re all just passin' through, so why spend that time actin’ like someone you’re not? Do your thing, make your noise, and don’t let anyone turn the volume down. But it ain't just about bein' loud—it's about knowin' when to step back and listen, too."
        -> GoodPath2

    * [But what about people who rely on you? Don’t you care about them?]
        #audio:Punk_5_N_16
        "I care. But I ain’t gonna lose myself tryin’ to be what they need. If I got your back, I got your back, no questions. But I won’t fit into a mold to make it easier for 'em. If they’re rollin' with me, they gotta be cool with who I am, not who they want me to be. Ain’t nothin' wrong with standin' your ground."
        -> NeutralPath2

=== GoodPath2 ===

#audio:Punk_5_G_17
    "That’s the spirit! Live loud, live true. We’re all just passing through this world, so why hide? When you embrace who you are, you not only set yourself free, but you inspire others to do the same. Ever thought about how your vibe can impact others? You could spark a change, you know?"

#choice  
    * [I never thought about it that way. I guess my choices matter.]
        #audio:Punk_5_G_18
        "Damn straight! Every choice you make sends ripples out. You might just be the one to show someone else it’s okay to be real. That’s how we change the world—one bold move at a time. So, what’s your next bold move gonna be?"
        -> GoodEndingLead

    * [I’m not sure I want that kind of responsibility.]
        #audio:Punk_5_G_19
        "Responsibility? Nah, it’s not about that. It’s about freedom! You do what feels right for you, and the rest will follow. If you worry too much about the weight, you might miss the ride. So, are you ready to ride this wave, or would you rather play it safe?"
        -> NeutralEndingLead

=== NeutralPath1 ===

#audio:Punk_5_N_20
    "It’s a balance, kid. You don’t gotta stand alone, either. People rely on me, and I got 'em. But I keep my edges sharp, and I don’t lose 'em for nobody. You think you can do the same, or you’d just end up blunt?"

#choice  
    * [That’s fair. Everyone’s gotta do their own thing, right?]
        #audio:Punk_5_N_21
        "Exactly. We all got our own path, and sometimes they cross, sometimes they don’t. But no one should have to hide who they are to make someone else feel better. If they can’t handle you at your realest, they ain’t worth the trouble. You think you can live with that kind of truth, or would it scare you off?"
        -> FinalChoice

    * [I’m still not sure if I agree with you.]
        #audio:Punk_5_N_22
        "And that’s okay! Thinkin’ for yourself is what makes you strong. Just don’t get caught in the trap of conformin’ to fit in. You gotta be you, no matter what. So, how do you plan to break the mold, if at all?"
        -> FinalChoice

=== NeutralPath2 ===

#audio:Punk_5_N_23
    "It’s all about knowing your boundaries. I care about the people in my life, but I’m not about to let their needs swallow me whole. I’ll support them, but I won’t change who I am for their comfort. You think you can balance that out? Care without losing yourself?"

#choice  
    * [Yeah, I think I can find that balance.]
        #audio:Punk_5_N_24
        "Then you’re on your way! It’s a tricky road, but if you can walk it, you’ll earn mad respect. Just remember: support doesn’t mean sacrificing yourself. You stay true to you while lifting others. So, how do you plan to stay true?"
        -> FinalChoice

    * [I’m not sure if I can do that.]
        #audio:Punk_5_N_25
        "That’s real. Acknowledging your limits is the first step. It’s not easy, but being honest with yourself is key. You think you can take that step, or does it feel too heavy to lift right now?"
        -> FinalChoice

=== FinalChoice ===

#choice  
    * [I like your vibe. We could be friends.]
        #audio:Punk_5_G_26
        "Alright, kid. You got a good head on your shoulders, I respect that. Here’s a tip for when you meet the pianist—don’t push, just be chill. He’s got his own way of movin’ through the world. You show him you respect that, you’re halfway there."
        -> FriendshipPath

    * [I’m not sure I agree with everything you’re sayin'.]
        #audio:Punk_5_B_27
        "That’s cool, man. We don’t gotta see eye to eye. World’s big enough for all of us to do our own thing. Ain’t no hard feelings. Just remember—don’t lose yourself tryin’ to find your place. You don’t need to agree to be real."
        -> NoFriendshipPath

=== BadEndingLead ===

#audio:Punk_5_B_28
    "You can’t control how the world spins, but you can control where you stand. That’s what matters. You live for you, the rest? It just fades out. People wanna talk, let 'em. They’re just talkin’ to themselves anyway. But you, you’re standin’ here listenin'—what’re you hopin' to hear?"

-> END

=== GoodEnding ===

#audio:Punk_5_G_29
    "You got somethin’ good goin’ on. Don’t let the noise get to you. Stay true, and you’ll find your way. There’s always a way."

-> END

=== GoodEndingLead ===

#audio:Punk_5_G_30
    "You’re alright, kid. If more people thought like you, maybe things wouldn’t be so messed up. Keep doin’ your thing. You’re the kind who makes waves without even tryin'."

-> END

=== FriendshipPath ===

#audio:Punk_5_G_31
    "Alright, kid. You got a good head on your shoulders, I respect that. Here’s a tip for when you meet the pianist—don’t push, just be chill. He’s got his own way of movin’ through the world. You show him you respect that, you’re halfway there."

-> END

=== NoFriendshipPath ===

#audio:Punk_5_B_32
    "That’s cool, man. We don’t gotta see eye to eye. World’s big enough for all of us to do our own thing. Ain’t no hard feelings."

-> END

=== NeutralEndingLead ===

#audio:Punk_5_N_33
    "You know, finding that balance ain’t easy, but it’s worth it. You stand your ground and support those you care about without losing yourself. Keep that in mind, and you’ll navigate the chaos just fine. So, what’s your next move gonna be?"

-> END
