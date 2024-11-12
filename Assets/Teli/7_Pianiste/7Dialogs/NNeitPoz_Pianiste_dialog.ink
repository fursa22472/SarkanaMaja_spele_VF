VAR option1 = false
VAR option2 = false
VAR option3 = false
VAR option4 = false
VAR option5 = false
VAR option6 = false

-> PianistIntro

=== PianistIntro ===
Ak, tur tu esi! Dzirdi? Skaņa... šķiet, ka viss beidzot ir savās vietās.
Tu gan jau, ka nedzirdēsi, bet ši akustika ir kā atsevišķs skaņdarbs. It kā zvaigznes spēlē savas notis.
-> A2


=== A2 ===
* [Ļoti skaista melodija.] 
    ~ option1 = true
    Līdzīgi vārdiem. Tu runā līdz tu sāc saprast nozīmi. Tas ir mans runas veids - Klavires.
    -> ReturnToChoices

* [Es nedzirdu zvaigznes] 
    ~ option2 = true
    Viņas pašas bez manis nevar spēlēt. It kā šis ir orķestris un viņas spēlē pavadījumu. Trešā roka.
    -> ReturnToChoices

* [Tad tev būs koncerts šeit?] 
    ~ option3 = true
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
        Man ir prieks, ka tev neinteresē priekšnesums tik ļoti, cik pats darbs. Tomēr, es vēl nevaru atklāt visu, savādāk nebūs tā paša efekta... Bet tā es spēlēju pārāk bieži. Beigās dzirdu tikai zvaigznes.
        -> A3

    * [Tev ir talants, bet tu neesi slavenība?]
        Kāpēc tu domā, ka neesmu? Esmu savu draugu lokā. Vairāk nevajag. 
        -> A4

    * [Man liekas cits nesaprastu zvaigznes...]
         To nevar paskaidrot ar vādiem, tur ir tikai mūzika.
        -> Navkoncerta

=== Navkoncerta ===
    * [Man ne īsti patīk tāda veida mūzika.] 
        Tiešām? Nu es saprotu...visiem viss nevar patikt. Tomēr, varbūt vismaz uz pāris minūtēm?
        -> K1

=== K1 ===
    * [Es atvainojos, nevaru.] 
        Viss labi, tad. Līdz nākamajai reizei.
        -> END
   * [Labi, pacentīšos.] 
        Burvīgi, tu nenožēlosi!
        -> K2
        
=== K2 ===
    * [Labi. Es iešu.] 
        Līdz nākamajai reizei!
        -> END 
 
 
        
=== A3 ===
    * [Bet ja nu man nepatiks?] 
    ~ option4 = true
        Tas ir nekas, galvenais, lai tu jūti līdzi. Vienmēr var aiziet.

        -> ReturnToChoices2

    * [Cik ilgi jūs spēlējat?] 
    ~ option5 = true
        Dienā vai vispār? Katru dienu ap 5 stundām. Vispār es spēlēju no sešiem gadiem. Vecāki atļāva un kaut kā aizgāja. 

        -> ReturnToChoices2

    * [Jūs pati izdomājāt šo darbu?] 
    ~ option6 = true
        It kā. Iedvesmu smeļos no citiem darbiem. Dažreiz liekas, O, šito dzirdēju, šis jau bija, šis nav oriģināls, bet nu...man patīk. 

        -> ReturnToChoices2
        
        
        === ReturnToChoices2 ===
{option4 and option5 and option6:
    -> A4
- else:
    -> A3
}

=== A4 ===
+ [Tad es labprāt atnāktu uz koncertu.]
    Ļoti labi! Šis man ir...ļoti svarīgi.
     -> A5

+ [Es ceru, ka koncerts būs neaizmirstams!]
     Es viņu neaizmirsīšu. Tu nevari iedomomāties. Būs skaisti. It īpaši vakarā.
    -> A5

=== A5 ===
    * [Es nesaprotu. Kur tieši?]
        Visu dzīvi es pavadīju klausoties skaņas. Cilvēks arī nevar neskanēt. Man liekas daba arī nevarbūt klusumā. Tāpat kā viss kosmoss. Pat aizverot acis un aiztaisot ausis, es dzirdu kaut kādas skaņas. Troksnis manā galvā. Vai tev jebkad tā bija?
        -> A6

=== A6 ===
+ [Nē.]
    Ā, nu, tad laikam man kaut kādas problēmas.
     -> A7

+ [Jā.]
    Tu saproti. Vienmēr būs kaut kas galvā, ja nu tikai tu nesēdēsi izolētā kamerā.
    -> A7

=== A7 ===
    * [Vai jūs vispār redzat kaut ko tumsā?]
      Es atceros visu no galvas. Mehāniska darbība. Ā, bet par tumsu. Tev nav jāiet?
        -> A8


=== A8 ===
    * [Jā, laikam...]
      Neturēšu tevi ilgāk.
        -> A9

=== A9 ===
  * [Tad es došos.]
      Attā!
        -> END

    * [...Tev pašai netraucē tās skaņas. Tur tālāk?]
      Tu runāji ar Pēteri? Man netraucē. Mēs bieži ar viņu runājam par mūziku. Centāmies pat spēlēt kopā, bet nesanāca.
        -> A10

=== A10 ===
    * [Viņs jūs pieminēja. Es pieteicos palīdzēt.]
      Tiešām? Tad man būs jāiedod tev labākā vieta. Būs aizņemti visi krēsli. Piemēram kā tagad.
        -> A11

=== A11 ===
    * [Tagad neviena nav]
      Cilvēku varbūt. Bet cilvēki jau nav vienīgie, kas klausās. 
        -> A12

=== A12 ===
    * [Labi es iešu.]
      Es turpināšu spēlēt. Uz redzēšanos.
        -> END


