VAR option1 = false
VAR option2 = false
VAR option3 = false
VAR option4 = false
VAR option5 = false


Es redzēju tevi ejam. Tu neapmaldījies?
#audio:Priesteris_2_N_00
    -> A00

=== A00 ===
+ [Es eju mājās... Neuztraucies.]
    Es tikai ceru, ka tas nekļūs par vēl vienu iemeslu Dieva uztraukumam. Zini, nav droši šajā laikā klīst apkārt, pat ja Viņš mūs pieskata. 
#audio:Priesteris_2_N_01
    -> A0

+ [Man nav laika runāt. Atvainojiet.]
   Pagaidi. Piedod, es pavisam aizmirsu. Man arī vajag, lai tu palīdzi nogādāt vienu lietu manam…draugam. Viņam bija grūta dzīve, un es no visas sirds vēlētos viņam palīdzēt. Šajā aploksnē ir nauda. Lūdzu nepazaudē. Tas cilvēks ir diezgan īsa auguma, man liekas viņam ir gandrīz 50, mugurā viņam vienmēr plāns zaļš krekls un jaka.. Teiksim, ka ši būs dzimšanas dienas dāvana. Paldies Tev.
#audio:Priesteris_2_N_Aploksne
    -> END

=== A0 ===
+ [Varbūt tam Dievam citādāk būs garlaicīgi.]
   Es tā nedomāju. Nedomāju, ka Viņam ir spēja just, kā mums, cilvēkiem. Tomēr esam tikai sīks puteklītis Viņa varā...
  #audio:Priesteris_2_N_03  
    -> A1
    

=== A1 ===
+ [Tad kāpēc pieminēji Dieva uztraukumu?]
     Es nezinu. Tas nāk pavisam automātiski, es aizdomājos. 
     #audio:Priesteris_2_N_04
     -> A2
     

+ [Domā Dievam arī tā liekas? Ka esam puteklīši?]
    Nē. Viņš, manuprāt, vispār nedomā. Vismaz šobrīd man tāda sajūta... Cilvēki ir kā sīkums paslēpts dziļoksnī. No malas nekad nepamanīsi, ja kārtīgi neieskatīsies konkrētā vietā. Bet neskatoties uz to, pa ceļam vienmēr pagadās atrast pāris sīkumu. Tāda ir dzīve.
    #audio:Priesteris_2_N_05
    -> A2

=== A2 ===
+ [Pagaidi, tad tu tici, pareizi?]
    Nē. Vairs nē.
    #audio:Priesteris_2_N_06
    -> A3


=== A3 ===
+ [Kā tas ir iespējams? Tu taču esi priesteris!]
    Jā. Un joprojām mēs esam tikai cilvēki. Mums ir grūti palīdzēt sev šādā gadījumā. Dažreiz liekas, it kā es pavadīju visu dzīvi skatoties griestos, nekad neredzot zvaigznes. Runājot ar sienu.
    #audio:Priesteris_2_N_07
    -> A4
    
 + [Tas ir joks? Tiešām?]
    Jā. Un joprojām mēs esam tikai cilvēki. Mums ir grūti palīdzēt sev šādā gadījumā. Dažreiz liekas, it kā es pavadīju visu dzīvi skatoties griestos, nekad neredzot zvaigznes. Runājot ar sienu.
    #audio:Priesteris_2_N_07
    -> A4
    
 + [Es nesaprotu.]
    Jā. Un joprojām mēs esam tikai cilvēki. Mums ir grūti palīdzēt sev šādā gadījumā. Dažreiz liekas, it kā es pavadīju visu dzīvi skatoties griestos, nekad neredzot zvaigznes. Runājot ar sienu.
    #audio:Priesteris_2_N_07
    -> A4

=== A4 ===
+ [Kāpēc? Ja nu tur ir tikai siena?]
   Pat tā, kāds vienmēr dzird šai sienai cauri. Kāds klausās, kamēr es nedzirdu. Vienīgais, ko es jūtu ir pateicību sejās, kas man ir tik svarīga. Es izvēlējos šo. 
    #audio:Priesteris_2_N_08
    -> A5


=== A5 ===
* [Tu vēl vari šo mainīt. Aiziet prom...]
 ~ option1 = true
    Aiziet var tikai no mājām. Aiziet no sevis nav iespējams. Es atgriezīšos tikai atpakaļ pie tukšas bļodas un baltas sienas. Tev ir jāsaprot, ka šī ticība pēkšņi neparādīsies. ...Bet tik pēkšņi spēj pazust.
#audio:Priesteris_2_N_09
    -> VisasIzveles1

* [Bet tev liekas, ka Dievs ir īsts?]
 ~ option2 = true
    Tas ir kā debesu milzums, kas aizpildīja telpu. Tagad skatoties augšup, es neko neredzēšu. Tāpēc es paliku šeit tik vēlu. Viņš ir īsts. Bet es esmu tik ļoti vīlies... 
#audio:Priesteris_2_N_10
    -> VisasIzveles1

* [Kas vispār ir šis Dievs?]
 ~ option3 = true
   Es iztērēju gandrīz visu savu dzīvi, domājot un lasot par šo. Tas nav kaut kas, uz ko ir iespējams atbildēt ar vienu teikumu. Viņš nebūtu Dievs, ja es varētu skaidri apgalvot par visu. Vieglāk teikt, kas Viņš nav.
   #audio:Priesteris_2_N_11
    -> VisasIzveles1

   
    === VisasIzveles1 ===
{option1 and option2 and option3:
    -> A6
- else:
    -> A5
}

=== A6 ===
* [Kāpēc Tu netici? Es nesaprotu.]
 ~ option4 = true
    Pabeidzot teoloģijas studijas, es devu svētsolījumu Dievam. Es solīju būt paklausīgs un dzīvot saskaņā ar likumu. Es darīju visu, ko Dievs no manis bija prasījis. Bet kad es paprasīju Dievam tikai vienu vienīgu lietu, Viņš neklausījās. Kopš tā laika, man nebija viegli. Es sāku šaubīties. 
   #audio:Priesteris_2_N_12
    -> VisasIzveles2

* [Vai tu kādreiz nožēlo? Ka kļuvi par priesteri?]
 ~ option5 = true
    Dažreiz. Bieži. Citiem liekas, ka tas ir kaut kas neīsts. Bet tā ir mana dzīve. Vai Tu nekad nenožēloji savā dzīvē? Es ticu, ka viss pāries. Tu neesi pirmā, ar kuru es par šo runāju.
      Joprojām manī ir daļa, kas vēlas palīdzēt... pat ja nezinu, kā.
     #audio:Priesteris_2_N_13
    -> VisasIzveles2

    
    === VisasIzveles2 ===
{option4 and option5:
    -> A7
- else:
    -> A6
}

    === A7 ===
+ [Izklausās, ka Jums ir grūti. Man žēl.]
  Katram cilvēkam ceļš līdz ticībai ir savs. Man ir šāds. Varbūt man šī saule bija par gaišu, tagad ir jāsēž tumsā, lai atkal kaut ko ieraudzītu. Pasaki man, vai tu tici,... ka mūs kāds pieskata?
#audio:Priesteris_2_N_14
    -> A8


=== A8 ===
+ [Cilvēki ir ļauni un nelaimīgi. Es neticu.]
    Teodiceja. Mūžīgais jautājums par Dieva attaisnošanu. Es saprotu. Tu neesi pirmā, kas tā domā. Bet, ja Dievs ir tikai stāsta galvenais varonis, kas mums tad atliek...
#audio:Priesteris_2_N_15
    -> Poz


+ [Kāda starpība? Liekas reliģija padara visus stulbus.]
    Es dzirdēju kaut ko līdzīgu arī iepriekš. Nekas nemainās. Es klausījos kā vecāki cilvēki strīdējās manu acu priekšā, lauza un kāvās. Ja pat Dieva uz šīs zemes nav, tas nenozīmē, ka pazūd pieklājība, līdzcietība un mīlestība pret saviem brāļiem un māsām.
    #audio:Priesteris_2_N_16
    -> Neg


+ [Jā...Es vēlos. Es ticu.]
    Ja tev šī ticība dod spēku, turies pie tās. Es nevaru teikt neko vairāk.
    #audio:Priesteris_2_N_17
    -> Poz


=== Neg ===
+ [Un tev ir vienalga, ka es neticu?]
Es nekad nevēlēšos sludināt savu patiesību. Man tādas nav. Baznīcā māca pieņemt visus, ja pat Tu atnāktu tikai, lai izbaudītu klusumu. Taču realitāte ir skarba. Bez ticības tā ir vēl skarbāka.
#audio:Priesteris_2_N_18
-> N1

=== N1 ===
+ [Beigās tāpat cilvēki mirst. Dievs ar to nepalīdz.]
Jā, bet tas jau nenozīmē, ka.. Varbūt tev ir taisnība. 
Ticība vai tās trūkums... tas mūs atstāj ar vieniem un tiem pašiem jautājumiem. Bet tu man esi parādījusi, ka pieķerties kaut kam varbūt ir vēl sliktāk nekā ļaut tam pazust. 
#audio:Priesteris_2_N_19
-> N2

=== N2 ===
+ [Vai Jums ir slikti?]
Piedod. Man vajag būt vienam. 
Esmu runājis pārāk daudz. Beigu beigās, tam nav nozīmes. Pasaule turpinās lauzt. Es tikai ceru, ka tu atradīsi kaut ko, kam pieķerties, kad tas notiks...
#audio:Priesteris_2_N_20
-> N3

=== N3 ===
+ [Vai Jūs spēsiet vēl kaut kad noticēt?]
Es ceru. Šobrīd es izvēlos klausīties cauri ļoti biezai sienai, bet...lai tava sirds ir vienmēr atvērta gaismai, kas ir mums apkārt, pat tumšākajā naktī. 
#audio:Priesteris_2_N_21
-> N4

+ [Es tevi sadusmoju? Es negribēju.]
Mans Tēvs, cik vēl pārbaudījumu man ir jāiztur? Es nezinu, vai esmu spējīgs... Es Tevi lūdzu...
#audio:Priesteris_2_N_22
-> END

=== N4 ===
+ [Labi, tad došos.]
Pagaidi. Piedod, es pavisam aizmirsu. Man arī vajag, lai tu palīdzi nogādāt vienu lietu manam…draugam. Viņam bija grūta dzīve, un es no visas sirds vēlētos viņam palīdzēt. Šajā aploksnē ir nauda. Lūdzu nepazaudē. Tas cilvēks ir diezgan īsa auguma, man liekas viņam ir gandrīz 50, mugurā viņam vienmēr plāns zaļš krekls un jaka.. Teiksim, ka ši būs dzimšanas dienas dāvana. Paldies tev.
#audio:Priesteris_2_N_Aploksne
    -> END




=== Poz ===
+ [Kā dzīvot, ja neko nesaproti?  ]
   Es saprotu pietiekami daudz savam apmierinājumam. Vai tevi nomāc nezināšana?
   #audio:Priesteris_2_N_23
    -> P1

=== P1 ===
+ [Nē. Izklausās interesantāk. Ja kādam patīk, tad kāpēc gan ne? ]
    Tu tā domā? Tiešām. Pietiek jau tikai ar to, ka kādam ir interesantāk. Tik vienkārši. 
Paldies... Tu man devi par ko padomāt. Varbūt es varu atrast kaut ko, kam noticēt atkal, pat ja tas nav tas, kam es kādreiz ticēju. Vēl nav par vēlu, vai ne?
#audio:Priesteris_2_N_24
    -> P2

=== P2 ===
+ [Piedod, ja es kaut ko ne tā pateicu. Tu joprojām jūties slikti?]
   Nē, nē! Nedomā! Kāds man prieks bija parunāt ar tevi. Ar tik jaunu meiteni.
   Es jūtos slikti, bet tas nav saistīts ar taviem jautājumiem. Viss, kas tiek jautāts, ir tas, kam ir jābūt atbildētam. Man šobrīd pietiek ar šādu atbildi. Paldies tev liels.
#audio:Priesteris_2_N_25
    -> P3

===P3===
+ [Tiešām?]
    Lai ko pasaule tev priekšā liktu, tu izvēlies, kam ticēt. Tas mani iedvesmo. 
    #audio:Priesteris_2_N_26
    -> P4

=== P4 ===
+ [Labi, tad došos.]
Pagaidi. Piedod, es pavisam aizmirsu. Man arī vajag, lai tu palīdzi nogādāt vienu lietu manam…draugam. Viņam bija grūta dzīve, un es no visas sirds vēlētos viņam palīdzēt. Šajā aploksnē ir nauda. Lūdzu nepazaudē. Tas cilvēks ir diezgan īsa auguma, man liekas viņam ir gandrīz 50, mugurā viņam vienmēr plāns zaļš krekls un jaka.. Teiksim, ka ši būs dzimšanas dienas dāvana. Paldies tev.
#audio:Priesteris_2_N_Aploksne
    -> P5
    
    
=== P5 ===
+ [Sarunāts.]
"Mana meita, tava ticība tev ir palīdzējusi, ej ar mieru!"
#audio:Priesteris_2_N_27
    -> END
