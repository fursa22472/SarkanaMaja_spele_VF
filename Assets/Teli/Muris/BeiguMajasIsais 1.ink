VAR PiekritiPriesterim = false
VAR PiekritiBomzim = false
VAR PiekritiMaksliniekam = false
VAR PiekritiTantei = false
VAR PiekritiPankam = false
VAR PiekritiPianistei = false



Tas tik bija kaut kas! Ceļš tev apsolīja beigas, bet...
šeit tikai maziņais, nevainīgais es un tu, joprojām esi pazudusi.
#audio:Muris_0_b_01
-> A1

=== A1 ===
+ [Kur ir…?]
Kur ir šis, kur ir tas? Kur ir kas?
#audio:Muris_0_b_03
    -> A3
    
+ [Tu arī esi šeit mežā?]
Es neesmu mežā, tu esi mežā. 
#audio:Muris_0_b_02
    -> A2

+ [Tu nespīdi!]
Es esmu ne šis, ne tas. 
Ko citu gaidīji, “Ņau, ņau?”
#audio:Muris_0_b_04
    -> A2


=== A2 ===
+ [Kur ir mana māja?]
Ne šeit. Citur. Varbūt tās nav!
#audio:Muris_0_b_06
    -> A4
    
=== A3 ===
+ [Ko...]
Ko, ko? Šis ceļš nekad tevi nenovedīs līdz mājām.
#audio:Muris_0_b_05
    -> A4

=== A4 ===
+ [Bet tu teici, ka…]
Varbūt es meloju? Varbūt citi meloja?
#audio:Muris_0_b_07
    -> A5
    
=== A5 ===
+ [Man meloja?]
Tu pati nekad neredzēji šo māju. 
Kā varēji zināt, ka tā eksistē?
#audio:Muris_0_b_08
    -> A6
    
+ [Nēēēēē!!!]
Tu pati nekad neredzēji šo māju.
Kā varēji zināt, ka tā eksistē?
#audio:Muris_0_b_08
    -> A6
    
=== A6 ===
+ [Tad kāpēc atnācu?]
Beidzot tu jautā pareizos jautājumus! Dod padomāt…
#audio:Muris_0_b_09
    -> As7
    
    
   //TIE VARIANTI - ATRISINAT KA UZTAISIT TEHNISKI.  
    
    
=== As7 ===
{PiekritiPriesterim:
  + [...]
Zināju, ka esi viena no ticīgajiem. 
Paļaujies uz katra viltnieka pasaciņām. 
Dievs tev nepalīdzētu tikt mājās. Hmmm...
#audio:Muris_0_b_Priest_P
    -> As8
  - else:
  + [...]
Es redzēju, kā tu runāji ar priesteri. 
Domā esi stiprāka par viņa Dievu? Tu?! Redzēsim...
#audio:Muris_0_b_Priest_N
    -> As8
}
    
    
=== As8 ===
{PiekritiBomzim:
  + [...]
Mjā... Tu domā lauzt likumus kā tas bomzis? 
Bez likumiem nav kārtības, bez kārtības nebūs skaidra ceļa. 
Bez ceļa - nav mājas. 
#audio:Muris_0_b_Bomz_P
    -> As9
  - else:
  + [...]
Putniņš knapi sācis lidot un jau māca citus. 
Bet nemāci to putnu, kurš ir iestrēdzis ar knābi bundžā.
Labāk ļauj viņam slīkt savā aliņā…
#audio:Muris_0_b_Bomz_N
    -> As9
}

    
=== As9 ===
{PiekritiMaksliniekam:
  + [...]
Tu runāji ar mākslinieku. Īsta daiļkrāsotāja sirds. 
Tādas personības uzlido pārāk tuvu saulei…
Un nokrīt izceptas uz mana šķīvja.
#audio:Muris_0_b_Maksl_P
    -> As10
  - else:
  + [...]
Tu negribēji palikt pie mākslinieka? Man liekas jūs sadraudzētos… 
Divi. Traki. Vientuļnieki. Telts nav māja.
#audio:Muris_0_b_Maksl_N
    -> As10
}

    
=== As10 ===
{PiekritiTantei:
  + [...]
Tu piekriti tantei, ka māja ir tavs pienākums? 
Hm… Varbūt tavs, bet ne mans. Man rieeebbjas pienākumi.
#audio:Muris_0_b_Tante_P
    -> As11
  - else:
  + [...]
Tu nepiekriti tam tantukam, pareizi? 
Viņa man arī centās ieskaidrot, ka man jāsēž mājās. 
Bet skat, kur esam.
#audio:Muris_0_b_Tante_N
    -> As11
}


=== As11 ===
{PiekritiPankam:
  + [...]
Tu piekriti Pēterim, tam pankam? 
Esi droša, ka tev patika ideja nevis pats Pēteris?
#audio:Muris_0_b_Panks_P
    -> As12
  - else:
  + [...]
Tu biji tik nejauka pret Pēterīti. 
Viņš pēc tās sarunas ātri aizbēga prom. Viņam nebija vienalga. 
Bet viņam arī bija daļa taisnības - nevar visiem uzticēties. 
#audio:Muris_0_b_Panks_N
    -> As12
}


=== As12 ===
{PiekritiPianistei:
  + [...]
Tu piekriti pianistei, ka cilvēki ir viņu darba vērti. 
Ko tad tu meklē tās mājas? EJ, STRĀDĀ! 
#audio:Muris_0_b_Pianiste_P
    -> As13
  - else:
  + [...]
Tu domā, ka cilvēkus nevērtē pēc darba, tad pēc kā tavuprāt jāvērtē? 
Mūzika ir subjektīva, tā pat kā pianistes idejas par cilvēka vērtību. 
#audio:Muris_0_b_Pianiste_N
    -> As13
}

=== As13 ===
+ [...]
Mmm. Bet varbūt tu viņiem meloji?
#audio:Muris_0_b_10
    -> A10

=== A10 ===
+ [Jā, lai viņiem ir vieglāk.]
Bet tagad šim nav nozīmes, jo tu veici savas izvēles. 
Tagad tu vari tikai mācīties no tām. Tu neatradi nekādu māju. Ko darīsi tālāk?
#audio:Muris_0_b_labas_11
    -> A11
    
+ [Jā, jo man bija vienalga.]
Bet tagad šim nav nozīmes, jo tu veici savas izvēles. 
Tagad tu vari tikai mācīties no tām. Tu neatradi nekādu māju. Ko darīsi tālāk?
#audio:Muris_0_b_labas_11
    -> A11
    
+ [Nē, jo tas ir slikti.]
Bet tagad šim nav nozīmes, jo tu veici savas izvēles. 
Tagad tu vari tikai mācīties no tām. Tu neatradi nekādu māju. Ko darīsi tālāk?
#audio:Muris_0_b_labas_11
    -> A11
    
+ [Nē, jo man bija savs viedoklis.]
Bet tagad šim nav nozīmes, jo tu veici savas izvēles. 
Tagad tu vari tikai mācīties no tām. Tu neatradi nekādu māju. Ko darīsi tālāk?
#audio:Muris_0_b_labas_11
    -> A11
    
=== A11 ===
+ [Neviens man nepalīdzēja. Man nevajadzēja uzticēties citiem.]
Tev nevajadzēja uzticēties nevienam izņemot mani. 
Nekādas mājas nekad nebija, tu tikai iztērēji savu laiku. 
#audio:Muris_0_b_labas_12
    -> A12
    
+ [Tu esi melns, šmucīgs un vēl melis! Es atradīšu mājas bez tevis. ]
Rāmāk. Tu nezini par ko runā. 
Es gribēju tikai palīdzēt? Vai tu to pamanīji? 
#audio:Muris_0_b_labas_13
    -> A13
    
+ [Es nesaprotu, kāda bija šim jēga? ]
Beidzot! Es zināju, ka tu izdarīsi pareizo izvēli. 
Bet palika tikai pēdējais pārbaudījums, bez jokiem.
#audio:Muris_0_b_labas_14
    -> A15_1
    
    === A15_1 ===
+ [Par ko tu runā?]
Nu, nu, saki man! Saki, ja tu jau zināji, ka māju nav, ja tu lieki ticēji katram, 
kurš zināja labāk un tu joprojām cīnies, kāda jēga?
#audio:Muris_0_b_labas_17
    -> A15
    
    
    
=== A12 ===
+ [Tu iztērēji manu laiku.]
Es? Es jau neko. Tā bija tava ideja skriet un runāties ar kuru katru. 
Tā bija tava izvēle spēlēt šo spēli ar mani… Ņau, ņau.
#audio:Muris_0_b_labas_15_beigas
    -> END
    
+ [Ko man tagad darīt? Lūdzu palīdzi.]
Nu, nu, saki man! Saki, ja tu jau zināji, ka māju nav, ja tu lieki 
ticēji katram, kurš zināja labāk un tu joprojām cīnies, kāda jēga?
#audio:Muris_0_b_labas_17
    -> A15
    
=== A13 ===
+ [Ej prom. Tas ir tevis dēļ!]
Es taču neizvēlējos tavā vietā nonākt šeit. Ja gribi, lai eju – labi. 
Tavs ceļš, tavas kājas, tu izvēlies ko ar tām darīt. 
#audio:Muris_0_b_labas_16_beigas
    -> END
    
+ [Ko man tagad darīt? Lūdzu palīdzi.]
Nu, nu, saki man! Saki, ja tu jau zināji, ka māju nav, ja tu lieki 
ticēji katram, kurš zināja labāk un tu joprojām cīnies, kāda jēga?
#audio:Muris_0_b_labas_17
    -> A15
    
    
    
=== A14 ===
+ [...]
Nu, nu, saki man! Saki, ja tu jau zināji, ka māju nav, ja tu lieki 
ticēji katram, kurš zināja labāk un tu joprojām cīnies, kāda jēga?
#audio:Muris_0_b_labas_17
    -> A15
    
=== A15 ===
+ [Lai draudzētos un iegūtu jaunu pieredzi.]
Vai tu esi pārliecināta? 
Tas nebija, ko vēlējos dzirdēt. Izskatās, ka vērtē citus augstāk par sevi.
Liekas, ka šoreiz šis ceļojums tev neko nebija iemācījis…
Mēģini vēlreiz.
#audio:Muris_0_b_labas_23_nepareizs_BEIGAS
    -> END
    
+ [Lai mācītos no citiem un viņu kļūdām.]
Vai tu esi pārliecināta? 
Tas nebija, ko vēlējos dzirdēt. Izskatās, ka vērtē citus augstāk par sevi.
Liekas, ka šoreiz šis ceļojums tev neko nebija iemācījis… 
Mēģini vēlreiz.
#audio:Muris_0_b_labas_23_nepareizs_BEIGAS
    -> END
    
    
+ [Lai iemācītos izvēlēties, ko es gribu.]
Tu vienmēr zināji, ko tu dari.
Tu neesi zvērs, tev ir sava galva, kas domā. 
#audio:Muris_0_b_labas_18
    -> A16
    
=== A16 ===
+ [Es gribu tikt mājās.]
Šī dzīve pieder tev un tikai tev. 
Tā ir māja, kuru celsi un kurā dzīvosi.
#audio:Muris_0_b_labas_19
    -> A17
    
=== A17 ===
+ [...]
Māja, kur būs tikai tavas mēbeles. 
Māja, kur būs tie, kas tevi vienmēr sagaidīs. 
Kur sienas būs tavā mīļākajā krāsā. 
#audio:Muris_0_b_labas_20
    -> A18
    
=== A18 ===
+ [...]
Māja, kur tu darīsi visu, ko tu patiešām gribi.
#audio:Muris_0_b_labas_21
    -> A19
    
=== A19 ===
+ [Es saprotu… Tas ir viss?]
Šīs beigas tevi neapmierina, hm?
#audio:Muris_0_b_labas_22_pareizs_BEIGAS
    -> END
    
+ [Es patiešām gribu mājās.]
Šīs beigas tevi neapmierina, hm?
#audio:Muris_0_b_labas_22_pareizs_BEIGAS
    -> END
    
+ [Es nezinu, kā šis man palīdz.]
Šīs beigas tevi neapmierina, hm?
#audio:Muris_0_b_labas_22_pareizs_BEIGAS
    -> END
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
