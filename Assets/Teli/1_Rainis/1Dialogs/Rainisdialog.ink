VAR option1 = false
VAR option2 = false
VAR option3 = false
VAR option4 = false

Tu nesen ievācies. Redzēju. Labvakar! Ko tu šeit dari? Domā paspēsi līdz saulrietam?
#audio:Rainis_1_N_00

      + [Nē.]
    Nav jau no kā baidīties tumsā. 
    Neturēšu tevi ilgi te, skat, jau saule laižas zemāk.
    #audio:Rainis_1_N_01

    -> ExploreBranch

      + [Jā.]
    Nav jau no kā baidīties tumsā. 
    Neturēšu tevi ilgi te, skat, jau saule laižas zemāk.
    #audio:Rainis_1_N_01
    -> ExploreBranch

=== ExploreBranch ===

+ [Viss labi. Es labāk iešu.]
    Tik ātri? Ko nu, tad, labi. Skrien, uz priekšu! Nodod sveicienus vecākiem! Tikai lūgums, man ne blakus galdiņš. Vari paņemt vēstulīti un nodot manam draugam priesterim? Viņš te pat kaut kur ir. Pateicos.
    #audio:Rainis_1_N_03
    -> END

+ [Jums nav bail no tumsas?]
    Man? Ja nu tikai no citiem kaimiņiem. Redzēji kādu?
    Ja nu satiksi pa ceļam uz mājām, tad padomā uzmanīgi pirms pasaki kaut ko. 
    Ne vienmēr sakāmais var patikt, bet no meliem arī attiecības labas neuzbūvēsi. Vienkārši saki to, kas uz sirds.
    #audio:Rainis_1_N_04

    -> KaiminiBranch

=== KaiminiBranch ===

+ [Labi. Uzredzēšanos.]
    Nu viss, tagad skrien, uz priekšu! Nodod sveicienus vecākiem!
    #audio:Rainis_1_N_03
    -> END

+ [Pastāstīsiet vairāk?]
    Ak tad tā. Ko tieši vēlies zināt?
    #audio:Rainis_1_N_06

    -> FearOrGoodBranch

=== FearOrGoodBranch ===

* [Vai man no viņiem jābaidās?]
 ~ option1 = true
    Baidīties? Nē, nē. Dažiem nīst liekas vieglāk nekā mīlēt, bet tur jau arī vecums pie vainas. 
    Būtu tikai pareizi atgādināt viņiem, ka laiks nestāv uz vietas. Tur celiņš, tev jāiet tikai taisni uz priekšu, un tālāk jābūt kādai gaismai.
    #audio:Rainis_1_N_07

    -> Atkartojums1

* [Vai viņi ir slikti vai labi kaimiņi?]
 ~ option2= true
    Kas ir slikts un kas ir labs? Ļaudis ir vai nu noguruši no vientulības, vai nu nogurst no tā, ka nevar būt vieni. Vidēji normāli kaimiņi.
    #audio:Rainis_1_N_08

    -> Atkartojums1

=== Atkartojums1 ===
{option1 and option2:
    -> GoodLuckBranch
- else:
    -> FearOrGoodBranch
}

=== GoodLuckBranch ===

+ [Sapratu! Paldies. Un ko Jūs paši? Neiesiet mājās?]
    Mājās? Es tepat jau dzīvoju. Dažreiz izeju pastaigāties. Paelpoju. 
    Man tā ārsts teica, kad nesen gāju profilaksei. Neviens šeit negrib ar mani spēlēt šahu. Kārtis arī nespēlē. Paliek tikai lasīt un sēdēt ārā.
    #audio:Rainis_1_N_09

    -> ChessBranch

=== ChessBranch ===

+ [Šahu?]
    Ak, nu tu zini, cik daudz šahā pasaka par cilvēku kopumā? Var pamanīt, kā cilvēks domā, un vai viņš vispār māk domāt uz priekšu. Tā ir tāda prāta spēlīte īsumā, ar skaistām koka figūrām.
    #audio:Rainis_1_N_10

    -> Personal

+ [Nav nekādu citu spēļu, kas jums patiktu?]
    Bērnībā atceros tēvs man mācīja spēlēt, visu laiku pavadījām pie galdiņa runājot. Dažreiz padevās man, dažreiz bija garlaicīgi, bet kopumā spilgta bērnības atmiņa.
    Ā? Kāpēc patīk šahs? Neko citu nemāku. Ja pat mācētu, izvēlētos šahu. Kaut kā bieži sanāk domāt par tēvu pēdējā laikā...
    #audio:Rainis_1_N_11

    -> Personal

=== Personal ===

* [Jūs dzīvojat vienatnē. Vai jums šeit nepaliek vientuļi?]
 ~ option3 = true
    Vientulīgi? Dažreiz. Bet ir atšķirība starp to vai būt vienam vai vientuļam. 
    Esmu pieradis. Darba arī netrūkst un ja būtu vēlme, tad aizbrauktu tur, kur dzīvelīgāk. Tagad esmu šeit, kur saule liekas lielāka un prāts vieglāks.
    #audio:Rainis_1_N_12

    -> Atkartojums2

* [Kāpēc izvēlējāties dzīvot tieši šeit?]
 ~ option4 = true
    Ilgs stāsts... Jaunībā nepamani to troksni, vēlāk gribas mierīgāk. Kur cilvēki nav tik uzbāzīgi un mašīnas brauc retāk. Tā mainās dzīves ritms. Vietas mainās kopā ar to.
    #audio:Rainis_1_N_13

    -> Atkartojums2

=== Atkartojums2 ===
{option3 and option4:
    -> continue
- else:
    -> Personal
}

=== continue ===

+ [Sapratu...]
    Bet nu pietiek par mani. Es redzu, ka tu steidzies.
    Būs jāturpina šī saruna citu dienu. Līdz nākamajai reizei un nodod sveicienus vecākiem!
    #audio:Rainis_1_N_14

    -> TheEnd

=== TheEnd ===

+ [Paldies. Būs darīts!]
    Ej nu. Neturēšu ilgāk. Tik tālāk uz priekšu. Tur ir visa mana gudrība.
    #audio:Rainis_1_N_15
    -> DONE
