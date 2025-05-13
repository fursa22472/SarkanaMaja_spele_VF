VAR option1 = false 
VAR option2 = false


Hm?
#audio:Bomzis_3_00
-> A1


=== A1 ===
* [Es negribēju traucēt…]
~ option1 = true
    Tu traucē.
    #audio:Bomzis_3_01
    -> ReturnToChoices
    

* [Ko tu šeit dari?]
~ option2 = true
    Eksistēju. Garlaikojos. 
    Tu mani dzen prom?
     #audio:Bomzis_3_02
     -> ReturnToChoices
   
    
    === ReturnToChoices ===
{option1 and option2:
    -> A2
- else:
    -> A1
}

=== A2 ===
+ [Vai jums ir mājas?]
    Nu, jā, jā. Te un tur, un šeit. Tikai durvju nav. 
    Jauns mājas izkārtojums! Bez durvīm.
    #audio:Bomzis_3_03
    -> A3

=== A3 ===
+ [Par ko jūs runājat?]
 Nu par to, cik stulba tu esi, nav nekādu šaubu. 
 Tev teica iet - tu neej. Balsi!
    #audio:Bomzis_3_04
    -> A4
    

+ [Šeit nav nekādas mājas.]
 Nu par to, cik stulba tu esi, nav nekādu šaubu. 
 Tev teica iet - tu neej. Balsi!
     #audio:Bomzis_3_04
    -> A4
   

=== A4 ===
* […]
Hm... Uz komandām arī nereaģē.
 #audio:Bomzis_3_05
    -> A5
    

=== A5 ===
+ [Es atvainojos, labāk iešu… ]
Kā avju bars...
   #audio:Bomzis_3_06_beigas1
    -> END
    
+ [Es neesmu stulba.]
Visiem vienmēr vajag palīdzēt, tev arī. 
Es zinu. Ar mani neviens savādāk nerunātu.
   #audio:Bomzis_3_07
    -> A6
    
+ [Rupeklis.]
Visiem vienmēr vajag palīdzēt, tev arī. 
Es zinu. Ar mani neviens savādāk nerunātu.
   #audio:Bomzis_3_07
    -> A6
    

=== A6 ===
+ [Bet ir arī tie, kas palīdzēs jums.]
Protams, aizsūtīsiet mani prom uz hospisu. 
Ieslēgsiet tumšā kambarī un aizmirsīsiet par 
mani kā vecu un nevajadzīgu lupatu. 
    #audio:Bomzis_3_08
    -> A7
    
    
+ [Es jums varu palīdzēt]
Protams, aizsūtīsiet mani prom uz hospisu. 
Ieslēgsiet tumšā kambarī un aizmirsīsiet par 
mani kā vecu un nevajadzīgu lupatu. 
    #audio:Bomzis_3_08
    -> A7
    

=== A7 ===
+ [Tur rūpējas par cilvēkiem.]
  Vai kāds parūpēsies par tevi?
    #audio:Bomzis_3_09
    -> A8
    
+ [Ej uz mājām nevis slimnīcu.]
Uz ielas guļu kā līķis. Telpās smirdu kā viens.
Neviens nezinās, ka esmu miris. 
    #audio:Bomzis_3_10
    -> A8
    

=== A8 ===
+ [...]
Viņi tev kaut ko apsolīja vai tāpat vien?
    #audio:Bomzis_3_11
    -> A9
    
    
=== A9 ===
+ [Man neviens neko nesolīja.]
Tad tu esi vēl stulbāka. 
Pat suns iet mājās tikai, jo zina, ka viņu baros.
Tu būtu slikts suns.
    #audio:Bomzis_3_12
    -> A10
    
=== A10 ===
+ [Es nezinu, kur man iet. ]
  Visur. Nekur. 
  Mēs visi beigās nonāksim vienā un tajā pašā vietā.
    #audio:Bomzis_3_13
    -> P3
    
=== P3 ===
+ [Kur?]
  Labs jautājums. Pajautā vecākiem.
   #audio:Bomzis_3_14
    -> P4
    
 + [Jums nevajag vietu, kur atpūsties?]
Tev liekas, es neatpūšos? 
Saulīte spīd, putniņi dzied…
   #audio:Bomzis_3_15
    -> P4
    

=== P4 ===
+ [...]
Tu man traucē baudīt skaisto dienu.
Ej mājās.
     #audio:Bomzis_3_16
    -> P5
   

=== P5 ===
+ [Tā arī darīšu. Attā.]
Kā avju bars...
    #audio:Bomzis_3_06_beigas1
    -> END
    
+ [Es neiešu, es gribu tev palīdzēt.]
  Labāk palīdzi sev. Ieraugi beidzot, kā stulbi cilvēki šeit dzīvo.
  Aprecās, piedzemdē bērnus. Paiet 20 gadu un beigās paliek vientuļi.
  Neseko mums pa pēdām. Te vietas nav. 
    #audio:Bomzis_3_18
    -> P6
    
+ [Varbūt palīdzat man?]
EJ PROM!
    #audio:Bomzis_3_17_beigas2
    -> END
    
    
=== P6 ===
+ [Viņi nav stulbi, viņi grib dzīvot.]
Viņi grib nošauties. 
Katra diena ir kā elle, ja ir jāskatās uz tādām nožēlojamām,
raudošām fizionomijām kā tava.
   #audio:Bomzis_3_19
    -> P7
    

=== P7 ===
+ [Fuj...]
 Man palīdzēs tikai rublis alkašam un 
 ar to aizdzīt tevi prom.
   #audio:Bomzis_3_20
    -> P8
    
+ [Jūs esiet nožēlojams.]
Man palīdzēs tikai rublis alkašam un
ar to aizdzīt tevi prom.
    #audio:Bomzis_3_20
    -> P8
    
    + [Nē… Jums nav taisnība.]
Man palīdzēs tikai rublis alkašam un
ar to aizdzīt tevi prom.
    #audio:Bomzis_3_20
    -> P8
    

=== P8 ===
+ [Bet–]
Varbūt tu atcerēsies mani un teiksi,
“Viņš gudrs vīrs bija”.
    #audio:Bomzis_3_21
    -> N2
    
    
=== N2 ===
+ [Varbūt Jums ir taisnība. Labāk neko nedarīt un dzīvot ar savām problēmām.]
Nu re. Gudriniece. Visa pasaule pieder tev.
Lauz likumu, pretojies viņiem!
    #audio:Bomzis_3_22_Piekritibeigas
    -> END
    
 + [Es negribu padoties. Es risināšu problēmas un darīšu, ko varēšu.]
Johambī, nedod Dieviņ tu padomā ar galvu. 
Tu sevi met bezdibenī kā alā… Alus. Gribu aliņu...
    #audio:Bomzis_3_23_Nepiekritibeigas
        -> END
    
    
    
    
    
    
    
    
    