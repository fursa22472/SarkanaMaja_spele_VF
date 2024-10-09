VAR triggerCharacter = ""

-> start

=== start ===
  {triggerCharacter == "NextCharacterAncientReligion"}
    The guard approaches you, recognizing that you have an interest in ancient religions.
    "Ah, I see you've been talking to the villagers about the old ways. Not many people appreciate the ancient gods anymore."
    #audio:GuardIntroAncientReligion
    
    + [Tell me more about the old rituals.]
        "Our ancestors believed that every tree, every stone had a spirit. Rituals to the sun, moon, and earth helped us maintain balance in the world."
        #audio:GuardAncientRituals
        -> ancientReligionBranch
        
    + [Do you still practice any of these rituals?]
        "In secret, some of us do. It's hard to let go of traditions that have been passed down for centuries, even if most people turn to Christianity now."
        #audio:GuardAncientPractice
        -> ancientReligionBranch

  {triggerCharacter == "NextCharacterCurrentLandscape"}
    The guard looks at you with curiosity. "I heard you were asking about religion in the village. The churches have been around for a long time, but things have changed."
    #audio:GuardIntroCurrentReligion
    + [What changes have you seen?]
        "When I was a child, everyone went to church every Sunday. But now, fewer people attend services. People are more focused on their daily lives."
        #audio:GuardReligionChanges
        -> currentReligionBranch
    + [Why do you think fewer people go to church?]
        "The world is changing quickly. With the rise of technology and the internet, people question old beliefs. Traditions aren't as strong as they used to be."
        #audio:GuardReligionDecline
        -> currentReligionBranch

  {triggerCharacter == "NextCharacterPersonalStory"}
    The guard smiles warmly. "So, you've heard about our celebrations. I grew up with them, too. They were some of the happiest memories of my childhood."
    #audio:GuardIntroPersonalStory
    + [Tell me about your favorite festival.]
        "It was always the summer solstice. We danced around bonfires, sang songs, and stayed up all night to watch the sunrise. Those were magical times."
        #audio:GuardFavoriteFestival
        -> personalStoryBranch
    + [What was the most important lesson you learned from those celebrations?]
        "The celebrations taught me that life is cyclical. Every season has its role, just as every person has their purpose. It helped me see my place in the world."
        #audio:GuardCelebrationLesson
        -> personalStoryBranch

=== ancientReligionBranch ===
* [Thank you for sharing.]
    The guard nods. "It's rare to meet someone who's interested in the old ways. Perhaps there's still hope for the ancient beliefs."
    #audio:GuardEndAncientReligion
    -> end

=== currentReligionBranch ===
* [Thank you for sharing.]
    The guard shrugs. "Times are changing, but it's important to remember where we came from. Even if fewer people go to church, the traditions still matter."
    #audio:GuardEndCurrentReligion
    -> end

=== personalStoryBranch ===
* [Thank you for sharing.]
    The guard smiles. "Our traditions may seem strange to outsiders, but they are what keep us connected to our land and our ancestors."
    #audio:GuardEndPersonalStory
    -> end

=== end ===
You leave the guard with a deeper understanding of the religious and cultural traditions in Latvia.
#audio:End
-> DONE
