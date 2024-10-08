Hey, kid. Long day?

+ [Yeah, kinda. You know how it is]  
    Can’t say I do, actually. School wasn’t really my scene. You survive it, though?  
    -> A2

=== A2 ===
+ [Barely. Math’s trying to kill me.]  
    Ah, numbers. The enemy of creativity. What’s so bad about it?  
    -> A3

=== A3 ===
+ [Everything. It’s just... so pointless. I’m never gonna use these equations.]  
    They never explain the point of all that stuff. It’s all about testing how well you follow along. You’re not very obedient, are you?  
    -> A4

=== A4 ===
+ [Not really.]  
    Good. Obedience gets you nowhere. You keep thinking for yourself, maybe one day you’ll make something of this world. How’s your sketchbook coming along?  
    -> A5


=== A5 ===
+ [It’s... alright, I guess. Haven’t had much time lately.]  
    Not much time? Or not much inspiration?  
    -> A6


=== A6 ===
+ [A bit of both.]  
    Inspiration’s slippery like that. You can’t wait for it. Sometimes, you’ve got to wrestle it out of the mundane stuff. Ever try drawing something boring? Like... the corner of a room?  
    -> A7

=== A7 ===
+ [That sounds like the worst thing ever.]  
    Exactly. That’s when it gets interesting. You start with something dull, and then... you find the little details. Before you know it, you’ve made something out of nothing. You’d be surprised where you can find beauty.  
    -> A8

=== A8 ===
+ [You always make it sound so easy.]  
    That’s because I’m a fraud. I just make it up as I go along. Trick is, everyone else is doing the same.  
    -> A9
    
+ [Maybe I’ll give it a shot. But don’t blame me if it turns out terrible.]  
    Terrible is fine. Terrible is *great*. The only bad art is art that’s never made. If you wait around for perfection, you’ll be waiting forever. Just make stuff. The rest will figure itself out.  
    -> A8


+ [Yeah, maybe.]  
    Don’t let school grind you down too much, alright? You’ve got more going for you than equations and homework. Remember that.  
    -> A9

=== A9 ===
+ [Thanks. I’ll try.]  
    Alright, I’ve got a half-finished masterpiece waiting for me in there. Don’t let math kill you before you make something of your own, yeah?  
    -> B1

=== B1 ===
    You see this brush? It’s real. The bristles, the wood, the fibers—they have weight. When I paint, I feel the pull of the canvas. It *fights* me. There’s resistance. That’s what art is. It’s a struggle. You can’t get that with a screen.  
    -> RealArtBranch

=== RealArtBranch ===
+ [What do you mean by “real”?]  
    Real… as in tangible. Digital art? It’s a trick. Pixels flickering on a surface. Remove the device and it’s gone. But this—(He gestures to the canvas.)—this stays. It can outlive me. There’s permanence. Tradition.  
    -> B2

=== B2 ===
+ [That permanence is important to you?]  
    Isn’t it important to everyone? We all die, but the things we create—those can last. Think of the masters. Rembrandt, da Vinci. They didn’t paint on screens. They fought with their materials. They’re immortal. Because they were real. You can *feel* this. Even in its imperfection, it breathes.  
    -> GoodPath

+ [Maybe digital and modern art isn’t “real” art.]  
   Now you’re getting it. These new trends? They feel... hollow. Digital tools can create images, sure, but can they really create art? 
    -> BadPath

=== GoodPath ===
+ [Digital art can be imperfect too.]  
    Imperfect? No. Digital art is *clean*. Too clean. It’s undo-able. Art isn’t supposed to be that way. It’s supposed to break, to fracture, to bleed into the world. Every stroke is a commitment. Every choice... final.  
    -> C1

=== BadPath ===
+ [Yeah, it’s all fake. Real art needs to be something you can touch, not pixels on a screen.]  
    Exactly. Digital art is nothing more than a flash in the pan. The masters didn’t need Photoshop. Real creation demands real struggle.  
    -> C2

=== C1 ===
+ [You sound like a snob.]  
    Maybe I am. It’s easy to get precious about this stuff. But do you really think I’m wrong?  
    -> ScreenDebate

+ [What’s so bad about screens, anyway?]  
    Screens are shallow. They offer shortcuts. You don’t *feel* anything when you work on a screen. Thousands of images, none of them will *last*. They’ll flicker and disappear. Where’s the meaning in that?  
    -> ScreenDebate

=== ScreenDebate ===
+ [So... digital art isn’t art to you?]  
    I’m not saying it isn’t... it’s just different. It doesn’t hold the same weight. Without that tension, it’s missing something essential. Something that ties it to the human experience.  
    -> C3

+ [But don’t mistakes still happen in digital art?]  
    Mistakes? Sure. But they’re never permanent. You can always undo them. That’s the difference. Real art demands you live with your mistakes, work with them.  
    -> C3

=== C2 ===
+ [I’ve seen some digital stuff. It feels... shallow, like something’s missing]  
    Exactly. It’s missing that connection. The touch, the imperfections. Art isn’t about perfection; it’s about expression. Pixels on a screen don’t have the same weight as paint on canvas. 
    -> BadEnding

=== C3 ===
+ [I think you’re over-romanticizing the struggle.]  
    Maybe. But it’s what makes art real. The struggle leaves marks. Marks you can touch, not just see on a screen.  
    -> GoodEnding

+ [A little, yeah. Art’s art. It doesn’t need to be pretentious.]  
    Art needs to have heart. Pretentious? Maybe. But it should move you. That’s the point.  
    -> GoodEnding

=== GoodEnding ===
+ [Maybe not... but you sound like you care too much.]  
    Caring too much is better than not caring at all. This is what makes life meaningful.  
    -> END

=== BadEnding ===
+ [Yeah, it feels like they’re skipping the hard parts]  
   Exactly. Meaning comes from struggle, from the process. These digital artists—they don’t know what they’re missing. It’s not just about what you make; it’s about how you make it. 
    -> END
