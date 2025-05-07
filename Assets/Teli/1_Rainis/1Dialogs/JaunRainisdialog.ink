VAR option1 = false
VAR option2 = false
VAR option3 = false
VAR option4 = false
VAR option5 = false

Ak jaunība, ak jaunība… Un vēl tas acu mirzums! 
#audio:Rainis_1_00

      + [Sveiki?]
Es tavā vecumā domāju, ka visa pasaule piederēs man. 
Ak naivā gara varenība…
#audio:Rainis_1_01
    -> A1
    
=== A1 ===
+ [Jūs šeit dzīvojat?]
Es? ... Nē. Es vienkārši neskrienu nekur... Viss pats atnāks. 
#audio:Rainis_1_02
    -> A2

=== A2===
+ [Interesanti.]
Tu ej mājās?
#audio:Rainis_1_03
    -> A3

=== A3 ===
+ [Jā...]
Izskaties pagalam nobijusies… dīdies un grozies. Laikam nezini, kur iet.
#audio:Rainis_1_04
    -> A4
    
+ [Nē.]
Tiešām? Nu nezinu, nezinu.
#audio:Rainis_1_05
    -> A4
    
+ [Kādēļ jautā?]
Eh... Jo bērns parasti māk tikai apmaldīties.
#audio:Rainis_1_06
    -> A4


=== A4 ===
+ [...]
Vai tad tev pulksteņa nav? Redzi, saule jau tā kā uz rietu. 
#audio:Rainis_1_07
   -> A5
   
 === A5 ===
+ [Es zinu.]
Šis man atgādina, zini, kad tiku labi tālu prom no mājām. Jā...
#audio:Rainis_1_08
   -> A6
   
=== A6 ===
+ [Pastāstiet.]
Domāju, aizbēgšu, redzēšu pasauli. Nē, nē. 
Iekarošu to! Bet… tur jau nevar aizbēgt. 
#audio:Rainis_1_09
   -> A7
   
=== A7 ===
+ [Kādā veidā?]
Es tālu neaizskrēju. Un neko nesasniedzu. 
Skrēju, skrēju, bet... eh, nezinu. Kāda jēga? Es tāpat atgriezos mājās. 
Varēju arī nekur neiet.
#audio:Rainis_1_11
   -> A8
   
+ [Es iešu.]
Ja gribi, tad ej. Es labāk nekur neiešu…
#audio:Rainis_1_10
   -> END
   
=== A8 ===
* [Kāpēc atgriezāties?]
 ~ option1 = true
Cilvēki pieaug un baidās. Baidās no pārmaiņām. 
Viņi paliek pie zināmā, runā kā vecāki, atkārto savus vecākos brāļus un māsas...
#audio:Rainis_1_12
   -> Atkartojums1

* [Kāpēc skrējāt prom?]
 ~ option2 = true
Es neatceros. Atceros, man gribējās aizskriet. 
Skan smieklīgi, bet jau tad likās, ka māja ir būris. Saproti?
#audio:Rainis_1_13
   -> Atkartojums1
   
* [Kam skrējāt pretī?]
 ~ option3 = true
Skrēju pretī gaišākajam pasaules stūrītim. 
Labākus laikapstākļus ...neatradu.
#audio:Rainis_1_14
   -> Atkartojums1
   
   
   === Atkartojums1 ===
{option1 and option2 and option3:
    -> A10
- else:
    -> A8
}
   
 === A10 ===
  + […]
Visur, kur eju, man pakaļ velkas tas sasodītais būris. 
Un gaida. 
#audio:Rainis_1_15
   -> A11

 === A11 ===
  + [Kāds būris?]
Tas tāds viltīgs būris. Jo tālāk eju, jo ātrāk viņš skrien pakaļ. 
Šitā, re! 
#audio:Rainis_1_16
   -> A12
   
=== A12 ===
+ [Un ko darījāt?]
Nu es, kā cilvēks gluži vienkāršs, izspriedu, ka jāstāv uz vietas. 
Stūrītī...
#audio:Rainis_1_17
   -> A13
   
=== A13 ===
+ [Jāstāv šeit…?]
Nav jau izvēles. Tā sanāk dzīvot. Es neko nevaru darīt.
#audio:Rainis_1_18
   -> A14

=== A14 ===
* [Kā nav izvēles?]
 ~ option4 = true
Mēs izvēlamies neizvēlēties... Tāda tauta... 
Tāpat pārmaiņas nenotiek pēc mūsu pavēles. 
#audio:Rainis_1_19
   -> Atkartojums2

* [Cilvēks nevar stāvēt visu laiku uz vietas.]
 ~ option5 = true
Cilvēkiem kājas ir, lai stāvētu. Ēkas pārbūvē, tehnoloģijas... ui kā tās attīstās. Bet cilvēks, ko viņš? Viņš nekad nemainās. Cilvēks nav mašīna. 
#audio:Rainis_1_20
   -> Atkartojums2
   
   
=== Atkartojums2 ===
{option4 and option5:
    -> A15
- else:
    -> A14
}

=== A15 ===
+ [Jūs neviens negaida?]
Ko nu, ko nu. Es nevienu negaidu. 
Vai tevi gaida?
#audio:Rainis_1_21
   -> A16

=== A16 ===
+ [Es nezinu.]
Ehhh... Nu varētu arī palikt ar mani. Divatā būrī mierīgāk...
#audio:Rainis_1_22
   -> A17
   
+ [Varbūt.]
Ehhh... Nu varētu arī palikt ar mani. Divatā būrī mierīgāk...
#audio:Rainis_1_22
   -> A17
   
=== A17 ===
+ [Nē, paldies.]
Nu, kā vēlies. Vienam būrī tomēr brīvāk. Nu tad skrien!
#audio:Rainis_1_23
   -> A18
   
=== A18 ===
+ [Es nezinu, kur iet!]
Tādi mēs cilvēciņi... nezinīgi. Ko tad man tev teikt? 
#audio:Rainis_1_24
   -> A19
   
=== A19 ===
+ [Jūs nevarat palīdzēt?]
Ko es? Nu kā es te līdzēšu? 
#audio:Rainis_1_25
   -> A20
   
=== A20 ===
+ [Varbūt jūs parādītu pareizo virzienu...]
Es nevaru neko, man ar savām problēmām pietiek. 
Paprasi vēl kādam... Kādam jaunietim.
#audio:Rainis_1_26
   -> A21

=== A21 ===
+ [Bet taču-!]
Ne, ne, ne. Kuš, kuš. Nebļauj. 
#audio:Rainis_1_27
   -> A22

+ [Kādas tev tur problēmas???]
Ne, ne, ne. Kuš, kuš. Nebļauj. 
#audio:Rainis_1_27
   -> A22

=== A22 ===
+ [Es nebļauju.]
Neatkarīgi no tā, ko darīsi, viss atrisināsies. 
Paliec - uzspēlēsim šahu. Aiziesi, arī nebūs pasaules gals...
#audio:Rainis_1_28
   -> A23
   
+ [Jo tu esi kurls kā zābaks.]
Neatkarīgi no tā, ko darīsi, viss atrisināsies. 
Paliec - uzspēlēsim šahu. Aiziesi, arī nebūs pasaules gals...
#audio:Rainis_1_28
   -> A23
   
+ [Piedod.]
Neatkarīgi no tā, ko darīsi, viss atrisināsies. 
Paliec - uzspēlēsim šahu. Aiziesi, arī nebūs pasaules gals...
#audio:Rainis_1_28
   -> A23
   
=== A23 ===
+ [Domājat labāk neiet mājās?]
Atkarīgs no tā, kādā būrī gribi atrasties. 
Es jau labprāt dzīvotu kādā smalkā, bet kā ir, tā ir. 
Nu es teiktu, labāk paliec te. Dzīve būs vieglāka.
#audio:Rainis_1_29
   -> A24

=== A24 ===
+ [Es nevaru stāvēt uz vietas. Mani gaida mājās.]
Tad tu izvēlies iet tālāk, jā? Tu šauj sev kājā. Nekur tālāk par mani netiksi... Bet vismaz būs tev dzīves pieredze.
#audio:Rainis_1_30
   -> A25
   
 + [Laikam... jums ir taisnība. Man nevajag nekur skriet. Tāpat būs labi]
Es zinu. Bet nekas. Svarīgi samierināties ar to, ka pasaule mums sasēja rokas un iemeta jūrā jau piedzimstot. 
Un tā tas vienkārši ir. 
#audio:Rainis_1_31
   -> A25
   
 === A25 ===
+ [Es tad došos.]
Uz galda ir ielūgums. Paņem to un aiznes priesterim. 
#audio:Rainis_1_32
   -> A26

 === A26 ===
+ [Kāpēc?]
Lai viņš nesēž tur viens brīvdienās. 
#audio:Rainis_1_33_beigas
   -> END

