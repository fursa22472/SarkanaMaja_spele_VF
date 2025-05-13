VAR option1 = false
VAR option2 = false


Uzmanīgi, neiekrīti ūdenī.
#audio:Makslinieks_4_00

+ [Ā, paldies!]  
    Ja iekritīsi, varbūt pameklē tur kaut kur manu otiņu. 
    Lūdzu.
    #audio:Makslinieks_4_01
    -> A1

=== A1 ===
+ [Ko tu glezno?]  
Vietu, kur es gribētu … vienkārši būt.
    #audio:Makslinieks_4_02
    -> A2
    
+ [Es meklēju savas mājas.]  
Es varu iedomāties, bet ceļu nezināšu.
    #audio:Makslinieks_4_03
    -> A2


=== A2 ===
+ [Vai tu šeit dzīvo?]  
Vai tu zini atšķirību starp gribu dzīvot un dzīvošanu?
   #audio:Makslinieks_4_04
    -> A4

=== A4 ===
+ [Nē?]  
Es nedzīvoju ārā, bet es nejustos dzīvs, ja manis tagad šeit nebūtu.
    #audio:Makslinieks_4_05
    -> A5

=== A5 ===
+ [Ko tas nozīmē?]  
Man ir arī normālas mājas, ja tas ir tas, ko gribēji zināt. 
Neesmu nekāds bomzis.
    #audio:Makslinieks_4_06
    -> A6

=== A6 ===
+ [Ohh, sapratu.]  
Zini, kāpēc šeit ir labāk? Jo visu laiku viss mainās. 
Atšķirībā no senčiem, kuriem nekas nekad nemainās...
    #audio:Makslinieks_4_07
    -> A7

=== A7 ===
+ [...]  
Viņi nesen nopirka jaunu kumodi un dīvānu. 
Es nepamanīju. Bet ārā! Ārā var pamanīt tik daudz skaistu lietu. 
    #audio:Makslinieks_4_08
    -> A8

=== A8 ===
+ [Jā!]  
Tiešām piedod, ka nevaru palīdzēt. Vieta ko meklē,
manā izpratnē nekad nebija svarīga.
    #audio:Makslinieks_4_09
    -> A9
    
+ [Mājās arī ir daudz lietu.]  
Tiešām piedod, ka nevaru palīdzēt. Vieta ko meklē,
manā izpratnē nekad nebija svarīga.
    #audio:Makslinieks_4_09
    -> A9
    

=== A9 ===
+ [Ko darīsi ar šo gleznu?]  
    Uzkarināšu uz sienas. Iedomāšos, ka esmu tur. 
    Piemēram pie jūras vai mežā...
    #audio:Makslinieks_4_11
    -> A10
    
+ [Tad es labāk došos tālāk.]  
  Kā vēlies. Veiksmi.
    #audio:Makslinieks_4_10_beigas1
    -> END

=== A10 ===
* [Tev patīk tavas gleznas?] 
~ option1 = true
Man patīk, kad es zinu, ko es daru. Patīk vietas, kur es eju.
Patīk, kad spēju noķert to momentu. Gleznas arī nav sliktas.
    #audio:Makslinieks_4_12
    -> Atkartojums1
    
* [Vai citiem patīk, ko tu glezno? ] 
~ option2 = true
Man ir vienalga, ko citi domā. Es gleznoju sev. 
Ja kādam vēl patīk, tad tas jau ir tāds patīkams pārsteigums.
    #audio:Makslinieks_4_13
    -> Atkartojums1
    
    
  === Atkartojums1 ===
{option1 and option2:
    -> A11
- else:
    -> A10
}

    
 === A11 ===
  + [...]  
Tu tik labi iederētos šajā ainavā.
Nevēlies pastāvēt te? Es tevi iemūžinātu.
  #audio:Makslinieks_4_14
  -> A12

=== A12 ===
+ [Bet man jāiet…]  
Nu labi, tad nevajag. Tev kur jāiet?
Ā jā, uz mjā... mājām...
    #audio:Makslinieks_4_15
    -> A13

=== A13 ===
+ [Jums kaut kas pret to?]  
Man liekas, tā ir mana problēma, bet es nesaprotu kādēļ kādam ir vēlme 
sēdēt mājas, kad var apbrīnot to, kas ir ārpus tās.
    #audio:Makslinieks_4_16
    -> A14

=== A14 ===
+ [Apbrīnot ko?]  
  Jebko! 
  Iet uz izstādēm, ceļot, sēdēt bibliotēkā, tusēt ar draugiem.
    #audio:Makslinieks_4_17
    -> A15

=== A15 ===
+ [Es šeit esmu jau diezgan ilgi.]  
  Nu tad ej. 
    #audio:Makslinieks_4_18
    -> A16
    
+ [Ir vakars…]  
  Nu tad ej. 
    #audio:Makslinieks_4_18
    -> A16

=== A16 ===
+ [Bet ja tur būs… vientuļi un garlaicīgi.]  
   Tad paliec! Šeit pat liekas ir labāk. Visi savējie. 
   Mēs varētu gleznot no rītiem. Ēst sviestmaizes. Būtu jautri!
    #audio:Makslinieks_4_19
    -> A17
    
+ [Labi, tad došos.]  
  Kā vēlies. Veiksmi.
    #audio:Makslinieks_4_10_beigas1
    -> END

=== A17 ===
+ [Te nav kur gulēt.] 
Vari droši gulēt teltī. Ārā es jūtos kā mājās. 
Un jebkur citur, liekas, esmu svešinieks. Tev tā nav? 
#audio:Makslinieks_4_20
 -> A18

=== A18 ===
+ [Ir. Man šeit patīk vairāk.]  
 Tad paliec! Dari, ko sirds prasa.
    #audio:Makslinieks_4_21
    -> A19
    
+ [Nē! Es gribu pie mammas un tēta.]  
Nu jā, tu esi sīks knipsis.
    #audio:Makslinieks_4_22
    -> A19
    
+ [Viss ko mīlu ir mājās.]  
  Tādā gadījumā mūsu viedokļi atšķiras. 
  Viss, ko es mīlu ir šeit. 
    #audio:Makslinieks_4_23
    -> A19
    

=== A19 ===
+ [...]  
Man tik ļoti paveicās piedzimt brīvam šajā valstī. 
    #audio:Makslinieks_4_24
    -> A20

=== A20 ===
+ [Es saprotu.]  
   Vajag vairāk novērtēt šo brīvību. 
    #audio:Makslinieks_4_25
    -> A21
    
+ [Es par to nedomāju.]  
   Vajag vairāk novērtēt šo brīvību. 
    #audio:Makslinieks_4_25
    -> A21
    
+ [Man patīk, ka pasaka ko darīt.]  
  Vajag vairāk novērtēt šo brīvību. 
    #audio:Makslinieks_4_25
    -> A21


=== A21 ===
+ [Man ir ļoti bail neatgriezties.] 
Katru reizi, kad es uzvelku jaunu līniju, 
man ir bail, ka es to nevarēšu izdzēst.
#audio:Makslinieks_4_26
    -> A22

=== A22 ===
+ [Tiešām?]  
Jā… Tev neliekas, ka mājās tu nejutīsi to pašu brīvību, ko šeit? 
    #audio:Makslinieks_4_27
    -> A23

=== A23 ===
+ [Varbūt mājai nav tik liela nozīme... Tev taisnība – Tā ir tikai sajūta.]  
Viss ir tikai sajūtas. Dari to, kas liek tev justies labi. 
Ja jūties slikti, tad nav jēgas nekam. Arī mājām. Iemācies baudīt dzīvi.
    #audio:Makslinieks_4_28_Piekritibeigas
    -> A24
    
+ [Ar sajūtām cilvēkam nepietiek. Vajag vietu, kur atgriezties.]  
  Tad ej. Bet ārā tevi gaida vēl tik daudz lietu, kuras tev 
  liksies svarīgākas par atgriešanos. Tev vajag tikai pieaugt.
    #audio:Makslinieks_4_29_Nepiekritibeigas
    -> A24
    
    
    

=== A24 ===
+ [...]  
  Ā. Var jautāt? Es sen netaisīju portretus.
  Pēctam vari paņemt sev.
    #audio:Makslinieks_4_OBJ_ZIMEJ_01
    -> A25

=== A25 ===
+ [Nē, man jāiet.]  
Nu labi…
    #audio:Makslinieks_4_OBJ_ZIMEJ_02
    -> END
    
+ [Labi, papozēšu.]  
Piecas minūtes. 
    #audio:Makslinieks_4_OBJ_ZIMEJ_03
    -> END
    
    
    
    
    
    
