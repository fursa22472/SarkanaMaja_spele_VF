VAR option1 = false
VAR option2 = false
VAR option3 = false
VAR option4 = false
VAR option5 = false


Labu vakaru, bērns. Ko tu dari tik vēlu ārā?

+ [Labu vakaru. Es eju mājās...]
    Zini, nav droši šajā laikā klīst apkārt, pat ja Dievs mūs pieskata. Cilvēku nodomi ir neskaidri, grūti saprotami. Es pats cenšos saprast cilvēku nodomus. Vispārīgos. 
   Tev nevajadzētu vienai staigāt tumsā, zini. Varbūt... varbūt kaut kas uz sirds?

    -> A1

+ [Man nav laika runāt. Atvainojiet.]
    Protams. Ej, bet esi uzmanīga. Un, ja kādreiz jutīsies apmaldījusies, baznīcā vienmēr atradīsies vieta tev, pat ja tas būtu tikai, lai patvertos no aukstās nakts.

    -> END

=== A1 ===

+ [Vispārīgie nodomi? Tev ir smieklīgs tērps virsū]
    Es strādāju baznīcā. 
   Bet godīgi sakot, es neesmu pārliecināts, vai tas, ko es daru ir pareizi, vai vismaz dodu, kas viņiem patiesībā ir vajadzīgs. 
   Es nezinu... kas man šobrīd ir vajadzīgs. Tu zini, ko tādi, kā mēs darām baznīcā?
    -> SmiekligsTerps


=== SmiekligsTerps ===
+ [Jā, jūs stāstat pasakas. Melojat dažreiz. ]
     Mums nedrīkst melot. Kurš tev kaut ko tādu ir teicis? Pēteris? 
     Es vienmēr redzēju ticību, kā izeju. Kā atbildi, lai ir vieglāk. Bet es nezinu vai tu saproti, dažreiz it kā..... it kā tas ir vairāk jautājums, nekā atbilde? Saproti? 
     -> A2
     

+ [Nezinu. Jūs ticat Dievam un... daudz lasat? ]
    Diezgan precīzs raksturojums, jā. No vienas puses. Pārsvarā tas un vēl dažādi rituāli, piemēram, kad ... Neko.
    Es vienmēr redzēju ticību, kā izeju. Kā atbildi, lai ir vieglāk. Bet es nezinu vai tu saproti, dažreiz it kā..... it kā tas ir vairāk jautājums, nekā atbilde? Saproti? 
    -> A2

=== A2 ===
+ [Nē. nesaprotu, tad kāpēc daži tic un daži nē. Vai tad visiem nav jābūt izeja?]
    Ticība ir dīvaina. Dažiem tas ir glābiņš. Citiem... akmens pie kājas, kurš slīcina. Bet tad jādomā, vai cilvēks pats negrib noslīkt... Ticība sev, Dievam... tās cilvēkiem ir vajadzīgas. Bez tās mēs esam... pazuduši. 
    Tā ir sarežģīta. Dažiem sataustāma. Tā sniedzas tālāk par to, kas ir mūsu acu priekšā.
    -> A3




=== A3 ===
* [Vai cilvēkiem ir nepieciešama skola, lai izprastu ticību?]
 ~ option1 = true
    Skola māca faktus, bet ticība... ticība nāk no pieredzes, dzīvošanas. Sanāk dažādi.
Skola tev sniedz zināšanas, bet tā nemācīs, kā noticēt. Tās ir lietas, kuras tev jāatklāj pašai. 
    -> VisasIzveles1

* [Kā ar kaimiņiem? Vai viņi visi ir pazuduši?]
 ~ option2 = true
    Viņi? Daudzi ir... diezgan novirzījušies no pareizā ceļa. 
Esmu mēģinājis palīdzēt, bet... ar viņiem nav viegli. Viņi klīst, meklējot kaut ko, bet atsakās klausīties. Ai, nav vienkārši.
    -> VisasIzveles1

* [Vai cilvēki joprojām bieži nāk pie tevis parunāt]
 ~ option3 = true
   Es atceros, kad gāja diezgan bieži, uz svētkiem arī. Tagad es pats nenāku, tāpēc nezinu. Man nepatīk tā sajūta, kad uz mani skatās tā, it kā es pats dzirdētu Dieva vārdus. It kā es saprastu vairāk. Es nesaprotu. Nesaprotu neko.
   Dažreiz man šķiet, ka viņi gaida, lai es pateiktu, ka viss būs labi. Bet es pats to nezinu.
    -> VisasIzveles1


   
    === VisasIzveles1 ===
{option1 and option2 and option3:
    -> A4
- else:
    -> A3
}


=== A4 ===
* [Kāpēc tu kļuvi par priesteri?]
 ~ option4 = true
    Netīšām sanāca. Nē. Sanāca, jo es gribēju saprast un sajust. Man tad likās viss vienkāršāk.
   Ar laiku viss mainījās. Es mainījos. Piedod, skan laikam dīvaini.
    -> VisasIzveles2

* [Vai tu kādreiz to nožēlo? Ka kļuvi par priesteri?]
 ~ option5 = true
    Dažreiz. Bieži. Kad es esmu mājās es domāju par to. Ārā aizmirstu. Tagad runāju un atcerējos, ka nožēloju.
     Joprojām manī ir daļa, kas vēlas palīdzēt... pat ja nezinu, kā.
    -> VisasIzveles2

    
    === VisasIzveles2 ===
{option3 and option4 and option5:
    -> A5
- else:
    -> A4
}

    === A5 ===
+ [Kā ar Dievu? Vai tu joprojām tici?]
    Ah... haha.. svarīgākais jautājums. Vai es ticu? 
Grūti pateikt. Dažreiz es domāju, ka ticu. Citreiz domāju, vai tas ir tikai kaut kas, pie kā cilvēki pieķeras, lai nebūtu bail. Laikam bieži baidos. 
Ko tu domā? Ticēt ir svarīgi? Vai man ir jātic? Vai tas tiešām būtu mans pienākums?
    -> A6


=== A6 ===
+ [Es domāju, ka bībele ir tikai pasaka, ko cilvēki izdomāja, lai justos labāk. Ticība nepalīdz.]
    Tu neesi pirmā, kas tā domā. 
Bet, ja Dievs ir tikai stāsta galvenais varonis, kas mums tad atliek? Kam mums ticēt, kad viss kļūst grūti? 
    -> Neg

+ [Es nekad neesmu īsti domājusi par Dievu. Mans tēvs par Viņu nerunā.]
    Tas ir saprotams. Ne visi uzaug vienādi. Tas mani iepriecina. Tev nav jāsaprot Dievs. 
    Vismaz tā man vienmēr ir mācīts.
    -> Poz


=== Neg ===
+ [Vajag pievērsties realitātei. Ticēt īstiem cilvēkiem. ]
Realitāte ir skarba. Bez ticības tā ir vēl skarbāka. 
Bet varbūt tu esi stiprāka un tev nevajag kaut kādu mierinājumu. Es vēlētos domāt, kā tu. Es pazaudēju tik daudz laika šim un beigās... 
Beigās būs beigas. 
-> N1

=== N1 ===
+ [Beigās tāpat cilvēki mirst. Dievs ar to nepalīdz.]
Jā, bet tas jau nenozīmē, ka.. Varbūt tev ir taisnība. 
Ticība vai tās trūkums... tas mūs atstāj ar vieniem un tiem pašiem jautājumiem. Bet tu man esi parādījusi, ka pieķerties kaut kam varbūt ir vēl sliktāk nekā ļaut tam pazust. 
-> N2

=== N2 ===
+ [Tu nožēlo, ka tik ilgi ticēji?. ]
Piedod. Man vajag pabūt vienam. 
Esmu runājis pārāk daudz. Beigu beigās, tam nav nozīmes. Pasaule turpinās spiest, un beigās salauzīs. Es tikai ceru, ka tu atradīsi kaut ko, kam pieķerties, kad tas notiks...
-> N3

=== N3 ===
+ [Es tevi sadusmoju? Es negribēju.]
Viss labi.
Uz redzēšanos... un parūpējies par sevi.
-> END





=== Poz ===
+ [Kā dzīvot, ja neko nesaproti?  ]
   Es saprotu pietiekami daudz savam apmierinājumam. Vai tevi nomāc nezināšana?
    -> P1

=== P1 ===
+ [Nē. Man labi. Man patīk realitāte kopā ar Dievu. Izklausās interesantāk. Ja kādam patīk, tad kāpēc gan ne? ]
    Tu tā domā? Tiešām. Pietiek jau tikai ar to, ka kādam ir interesantāk. Tik vienkārši. 
Paldies... Tu man devi par ko padomāt. Varbūt es varu atrast kaut ko, kam noticēt atkal, pat ja tas nav tas, kam es kādreiz ticēju. Vēl nav par vēlu, vai ne?
    -> P2

=== P2 ===
+ [Piedod, ja es kaut ko ne tā pateicu. Tu joprojām jūties slikti?]
   Nē, nē! Nedomā! Kāds man prieks bija parunāt ar tādu jaunu un gudru meiteni. 
Saruna ar tevi man atgādināja, ka ticība nav par atbildēm, bet par pašu jautājumu uzdošanu. Man šobrīd pietiek ar šādu atbildi. Paldies tev liels, milzīgs. 
    -> P3

===P3===
+ [Tiešām?]
    Lai ko pasaule tev priekšā liktu, tu izvēlies, kam ticēt. Tas mani iedvesmo. Gribu, lai visi tā domā.
    -> P4

=== P4 ===
+ [Paldies par sarunu!]
Lūdzu. Ceru, ka atradīsi atbildes, kuras meklē. Un varbūt, tikai varbūt, arī es atradīšu savējās.
..Un tomēr... Lai Dievs tev stāv klāt. 
    -> END
