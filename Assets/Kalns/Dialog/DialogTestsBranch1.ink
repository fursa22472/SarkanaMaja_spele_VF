VAR triggerCharacter = ""
-> start

=== start ===
Oh religion, such an interesting topic. Did you know that it is an essential part of Latvian culture?
#audio:Intro

+ [Tell me about the history of ancient religions.]
    Ancient Latvian religion was connected with nature gods and rituals. It tried to perceive the rhythm of life and nature, recognizing the connection with the surrounding environment.
    #audio:AncientReligion
    {triggerCharacter == "NextCharacterAncientReligion"}
    -> branchToGuard 

+ [What is the current religious landscape in Latvia?]
    Christianity, especially Lutheranism and Catholicism, dominates in Latvia. Churches and congregations are an important part of the community where religious services and religious festivals take place.
    #audio:CurrentLandscape
    {triggerCharacter == "NextCharacterCurrentLandscape"}
    -> branchToGuard 

+ [Yes, tell me a personal story about religion.]
    When I was young, I participated in a fun horse festival where we celebrated the cutting of the sun. It was then that I learned about our ancestors' respect for the forces of nature and their understanding of a cyclical world order.
    #audio:PersonalStory
   {triggerCharacter == "NextCharacterPersonalStory"}
    -> branchToGuard 

=== branchToGuard ===
The villager finishes the conversation. You move on to meet the guard.
-> continue2

=== continue ===
+ [Tell me about religious rituals and traditions.]
    The Latvian folk calendar is rich in religious celebrations and rituals. For example, Midsummer's Day and St. John's Day are important summer solstice festivals where the nation celebrates fertility and the renewal of life.
    #audio:RitualsAndTraditions
    -> continue2

+ [Yes, tell me another religious story.]
    I remember my parents talking about serving the Gods and how religious rituals are connected with a deep respect for life and the order of nature.
    #audio:AnotherStory
    -> continue2

=== continue2 ===
* [Thank you.]
    You're welcome. Religion is an integral part of Latvian culture, which helps to understand our nation's deep connection with the environment and spirituality.
    #audio:End
-> DONE
