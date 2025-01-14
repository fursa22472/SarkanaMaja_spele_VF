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
       Ne īsti? Varbūt neesi pieradusi pie īstās mākslas.
        #audio:Pianiste_7_GO_00
        -> K1

=== K1 ===
    * [Varbūt.] 
        Viņai ir jābūt asai. Šis ir mans dvēseles kliedziens, atskatīšanās uz pagātni.
        #audio:Pianiste_7_GO_01
        -> K2
        
=== K2 ===
    * [Bet tik skaļi?] 
       Jā, jo mana dvēsele kliedz.
         #audio:Pianiste_7_GO_02
        -> K3
 
 === K3 ===
    * [Kāpēc?] 
       Un kāpēc, lai tam jābūt savādāk?
         #audio:Pianiste_7_GO_03
        -> K4


 === K4 ===
    * [Es pat tā nemāku.] 
      Es varu tev iemācīt. Negribi pagaidīt līdz es pabeidzu un, tad mēs aizietu līdz manām mājām. Es tev kaut ko pamācītu. Tas būtu jautri!
         #audio:Pianiste_7_GO_04
        -> K5

 === K5 ===
    * [Nē, piedodiet.] 
       Skaidrs, tev vienkārši nepatīk mūzika. Netraucē.
         #audio:Pianiste_7_GO_05
        -> Beigas
        
           === Beigas ===
            * [Bet tā nav...] 
    Jā, jā. Nav par ko ar tevi runāt.
         #audio:Pianiste_7_GO_07
         -> END
        
         * [Varētu.] 
       Burvīgi, apsēdies tur stūrī. Es pēc stundas atbrīvošos un tad varam iet.
         #audio:Pianiste_7_GO_06
        -> K6
        
         === K6 ===
            * [Labi.] 
       Tikai vēl stundiņu. Atkārtošu šo daļu un skriesim. Tikai netraucē.
         #audio:Pianiste_7_GO_08
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
