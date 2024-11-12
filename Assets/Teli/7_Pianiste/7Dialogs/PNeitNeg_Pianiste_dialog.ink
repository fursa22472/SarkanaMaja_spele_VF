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
#audio:Pianiste_7_N_00
-> A2


=== A2 ===
* [Ļoti skaista melodija.] 
    ~ option1 = true
    Līdzīgi vārdiem. Tu runā līdz tu sāc saprast nozīmi. Tas ir mans runas veids - Klavires.
    #audio:Pianiste_7_N_01
    -> ReturnToChoices

* [Es nedzirdu zvaigznes] 
    ~ option2 = true
    Viņas pašas bez manis nevar spēlēt. It kā šis ir orķestris un viņas spēlē pavadījumu. Trešā roka.
    #audio:Pianiste_7_N_02
    -> ReturnToChoices

* [Tad tev būs koncerts šeit?] 
    ~ option3 = true
    Mans pirmais koncerts šeit. Es spēlēju lielajās zālēs, bet aizmisu, cik patīkami būt vienai un spēlet tikai jums. 
    #audio:Pianiste_7_N_03
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
        #audio:Pianiste_7_N_04
        -> A3

    * [Kam tu spēlē?]
        Kām? Kam es varu spēlēt? Sev, citiem un visai pasaulei. 
        #audio:Pianiste_7_Neg_00
        -> N1

    * [Man liekas cits nesaprastu zvaigznes...]
         To nevar paskaidrot ar vādiem, tur ir tikai mūzika.
         #audio:Pianiste_7_N_06
        -> Navkoncerta

=== Navkoncerta ===
    * [Man ne īsti patīk tāda veida mūzika.] 
        Tiešām? Nu es saprotu...visiem viss nevar patikt. Tomēr, varbūt vismaz uz pāris minūtēm?
        #audio:Pianiste_7_N_07
        -> K1

=== K1 ===
    * [Es atvainojos, nevaru.] 
        Viss labi, tad. Līdz nākamajai reizei.
        #audio:Pianiste_7_N_08
        -> END
   * [Labi, pacentīšos.] 
        Burvīgi, tu nenožēlosi!
        #audio:Pianiste_7_N_09
        -> K2
        
=== K2 ===
    * [Labi. Es iešu.] 
        Līdz nākamajai reizei!
        #audio:Pianiste_7_N_10
        -> END 
 
 
        
=== A3 ===
    * [Bet ja nu man nepatiks?] 
    ~ option4 = true
        Tas ir nekas, galvenais, lai tu jūti līdzi. Vienmēr var aiziet.
        #audio:Pianiste_7_N_11

        -> ReturnToChoices2

    * [Cik ilgi jūs spēlējat?] 
    ~ option5 = true
        Dienā vai vispār? Katru dienu ap 5 stundām. Vispār es spēlēju no sešiem gadiem. Vecāki atļāva un kaut kā aizgāja. 
        #audio:Pianiste_7_N_12

        -> ReturnToChoices2

    * [Jūs pati izdomājāt šo darbu?] 
    ~ option6 = true
        It kā. Iedvesmu smeļos no citiem darbiem. Dažreiz liekas, O, šito dzirdēju, šis jau bija, šis nav oriģināls, bet nu...man patīk. 
        #audio:Pianiste_7_N_13

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
    #audio:Pianiste_7_N_14
     -> A5

=== A5 ===
    * [Tad es došos.]
       Līdz nākamajai reizei!
       #audio:Pianiste_7_N_10
       
        -> A6

=== A6 ===
+ [Labi, es padomāšu]
    Neturēšu tevi ilgāk.
    #audio:Pianiste_7_N_20
     -> END



=== N1 ===
    * [Bet vai ir jēga? Neviena nav...]
      Tu domā vajag jēgu? Domā tādas nav? Varbūt mums tad nav jēgas runāt?
      #audio:Pianiste_7_Neg_01
        -> N2

=== N2 ===
    * [Es to nedomāju tā...]
      Tieši tā, tu nedomā. Man nepatīk, kad nedomā. Punkts. Ej tālāk.
      #audio:Pianiste_7_Neg_02
        -> N3

=== N3 ===
    * [Piedodiet.]
      ...
        -> END
