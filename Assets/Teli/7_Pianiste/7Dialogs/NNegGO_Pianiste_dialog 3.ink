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
* [Ko? Kādas zvaigznes?] 
    ~ option1 = true
    Viņas pašas bez manis nevar spēlēt. It kā šis ir orķestris un viņas spēlē pavadījumu. Trešā roka.
    -> ReturnToChoices

* [Tā mūzika..Man ne īsti] 
    ~ option2 = true
    Ne īsti? Tu varbūt neesi pieradusi pie īstās mākslas.
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
    * [Šī mūzika griež ausis.]
       Viņai ir jābūt asai. Tur ir manas emocijas un pārdzīvojums. Šis ir mans dvēseles kliedziens, atskatīšanās uz pagātni.
        -> A3

        
=== A3 ===
    * [Ļoti skaļi] 
    ~ option4 = true
        Jā, jo mana dvēsele kliedz...

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
 * [Bet vai ir jēga? Neviena nav...]
      Tu domā vajag jēgu? Domā tādas nav? Varbūt mums tad nav jēgas runāt?
     -> A5

=== A5 ===
    * [Jā...laikam]
       Un kāpēc, lai tam ir jābūt savādāk?
        -> A6

=== A6 ===
+ [Labi jūsu mūzika nav tik slikta. Es tā nemāku]
    Es varu tev iemācīt. Negribi pagaidīt līdz es pabeidzu, un tad mēs aizietu līdz manām mājām un es tev kaut ko pamācītu? Tas būtu jautri.
     -> A6

=== A7 ===
    * [Nē, paldies]
      Skaidrs. Tev vienkārši nepatīk mūzika.
        -> N1

  * [Varētu...]
      burvīgi! Apsēdies tur stūrī, es pēc stundas atbrīvošos.
        -> END

=== N1 ===
    * [Man patīk.]
      Jā, jā. Man nav par ko ar tevi runāt. Atā.
        -> N2

=== N2 ===
    * [Bet es pabprāt parunātu vēl...]
      ...
           -> END
      
       * [Nu labi...]
      ...
        -> END
