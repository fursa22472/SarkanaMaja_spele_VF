VAR option1 = false
VAR option2 = false
VAR option3 = false
VAR option4 = false


Turpini iet. Te tev nav, ko darīt.
-> A1

=== A1 ===
* [Es negribēju traucēt.]
~ option1 = true
    Turpini iet. Te tev nav, ko darīt.
    -> ReturnToChoices

* [Ko jūs darat šeit viens pats?]
~ option2 = true
    Es šeit dzīvoju.
    -> ReturnToChoices
    
    === ReturnToChoices ===
{option1 and option2:
    -> A2
- else:
    -> A1
}

=== A2 ===
+ [Tu neizskaties, ka tev klājas pārāk labi...]
    Man viss ir kārtībā. Tu domā, ka saproti mani pēc  izskata? Labāk ej.
    -> A3

+ [Piedod, es negribēju tevi aizskart.]
    Nu jā. Vienkārši ej tālāk, kā visi pārējie. Esmu pie tā pieradis.
    -> END

=== A3 ===
+ [Vai tiešām tu te dzīvo?]
   Jā. Un kas tad? Tā nav pils, bet vairāk nevajag.
    -> A4

+ [Es neticu, ka kāds šādi dzīvo pēc izvēles.]
    Tu domā, ka esmu šeit, jo negribu neko labāku? Tici man, labāku par šo man nepienākas. 
    -> A4

=== A4 ===
* [Kur ir tava ģimene?]
~ option3 = true
    Aizgāja. Atstāja mani. Droši vien tā bija labāk. Es viņus nevainoju. 
    -> ReturnToChoices2

* [Tevi tiešām apmierina šāda dzīve?]
~ option4 = true
    Tu nesaproti, vai ne? Man nevajag izrādīties. Neviens par mani vairs nerūpējas, un tas ir labi. 
    -> ReturnToChoices2
    
    
        === ReturnToChoices2 ===
{option3 and option4:
    -> A5
- else:
    -> A4
}
    

=== A5 ===
+ [Tu tiešām domā, ka viņiem būtu labāk bez tevis?]
    Es zinu, ka esmu dzērājs. Es dzēru un vienmēr to darīšu. Tāda ir dzīve, tādi apstākļi.
Es visu sabojāju. Droši vien viņi tagad dzīvo labāk.
    -> A6


=== A6 ===
+ [Ja viņi atgrieztos, vai tu mēģinātu labot lietas?]
    Nu, rekur, gudriniece!
     Ir par vēlu. Viņi mani pat vairs nepazītu.
    -> A7


=== A7 ===
+ [Šī ir izdzīvošana, nevis dzīvošana. Tev kaut kas ir jādara.]
    Tev sliktas atzīmes skolā, ko? Nesaproti neko? 
    Esmu mēģinājis. Nekas neizdevās. Neviens no manis neko negaida, un es neko negaidu no citiem.
    -> A8

=== A8 ===

+ [Nav par vēlu mainīties. Varbūt tev būtu vēl viena iespēja.]
    Tu nezini to sajūtu, nu, kad tev tāds....Tu zini, ka esi pretīgs un vainīgs un, ja pat piedotu,, tad... Kuram es tāds vajadzīgs.
    -> P1
    
    + [Tu esi padevies. Tu domā, ka neesi pelnījis neko labāku, bet tā nav. Beidz tā domāt.]
   Beidz stāvēt šeit. Beidz būt tik uzbāzīga. Beidz runāt ar mani. 
    -> N1

=== P1 ===
+ [Jums ir jāsāk ar kaut ko mazu. ]
    Ir. Ir iespēja nesabojāt šo. Sākt no jauna.
    -> P2
    
    + [Jums vajag palīdzību. Varbūt noīrēt dzīvokli?]
    Pareizi! Ka tā. Jā, rekur ir. Nu tad laikam iešu, paldies liels.
    -> N2


=== P2 ===
+ [Tieši tā, jāsāk no jauna!]
    Sākšu ar to, ka aizmirsīšu par tām dienām, kad man bija ģimene.
    -> P3


=== P3 ===
+ [Nē, jāsāk ar sevi.]
    Tev neliekas dīvaini, ka.... tu tāda vairāk, ar dzīves zināšanām. Pārzināšanu? It kā tev būrtu līdzīga situācija. Lūdz Dievu, lai nebūtu.
    -> P4


=== P4 ===
+ [Es domāju, ka ticība jums palīdzētu. Kaut vai ticība sevī. Es satiku priesteri, kurš pats netic, bet viņš nepadevās.]
    Ak, tad tev patīk runāties. Man nepatīk. 
    -> P5

=== P5 ===
+ [Es nezinu, kā jums palīdzēt. Jums nav auksti?]
    Nav, nav. Tātad...Tu runāji ar priesteri, ja? Nevarētu viņam paprasīt...Nē. Zini, nu ja tu te jau esi, tad nu jau neko. Kā tu domā... ja tev tēvs dzertu, tu viņam piedotu?
    -> P6

=== P6 ===
+ [Mans tēvs nedzer. Es viņam visu piedotu. ]
   Paldies. Es savam nepiedodu. Nu cilvēki ir dažādi tad, jā? Ai. Zini ko? Mēģināsim. Atcerējos par to priesteri un uzreiz tā kā skaidrāk galvā. Ne par Dievu, bet par sarunām ar viņu. Tur bija tas sasodītais lietussargs.
    -> P7
    
    === P7 ===
+ [Lietussargs?]
    Izkāpu es trolejbusā, nu, jo, nu nebija taloniņa, izeju, bet tur tas, tas vīriņš iet un iedod lietussargu pats , galvenais iet bez, bet man iedod. Beigās, nu, kautkur pazaudēju, bet...jā. Skaista diena bija. 
Ir jācenšās.
    -> P8
    
+ [Jūs varat konkrētāk?]
    Izkāpu es trolejbusā, nu, jo, nu nebija taloniņa, izeju, bet tur tas, tas vīriņš iet un iedod lietussargu pats , galvenais iet bez, bet man iedod. Beigās, nu, kautkur pazaudēju, bet...jā. Skaista diena bija. 
Ir jācenšās.
    -> P8
    
    === P8 ===
+ [Es arī tā domāju.]
   Tu esi ļoti jauka. Paldies.
    -> P9
    
    + [Es nesapratu īsti par lietussargu, bet man prieks.]
    Tu esi ļoti jauka. Paldies.
    -> P9
    
    === P9 ===
+ [Visu labu! Es ceru, ka palīdzēju...]
    Es domāju, ka tev taisnība. Es mēģināšu. Atā!
    -> END
    
    
    
   === N1 ===
   + [Jums vajag palīdzību. Varbūt noīrēt dzīvokli?]
    Pareizi! Ka tā. Jā, rekur ir. Nu tad laikam iešu, paldies liels.
    -> N2   
    
=== N2 ===
+ [Kad jūs domājat pārvākties?]
   Nu tulīt sākšu krāmēt mantas. Paldies vēlreiz. Vari iet jau. 
    -> N3
    
=== N3 ===
+ [Kas notiek? Kādēļ jūs tad neejat?...]
   ....
Laikam būšu pazaudējis savus desmit tūkstošus kabatā. Diemžēl Dievs nav manā pusē.
    -> N4

=== N4 ===
+ [...Pagaidi. Tas bija joks? Jums nav naudas. ]
   Ej, es tevi lūdzu. Paej garām. 
    -> N5
    
    === N5 ===
+ [Kā es varu paiet garām. Šis ir mans pienākums.]
   Pienākums nedot man mieru?
    -> N6
    
        === N6 ===
+ [Pienākums, kā pilsonim palīdzēt tādiem kā jūs.]
   Kurš tev to galvā ielika? ...Māte? Skolotāja? Kāds tad es esmu, ka tev tāds...tas.... pienākums?
    -> N7

        === N7 ===
+ [Es nezinu, kā jums palīdzēt. Jums nav auksti?]
   Nav, nav. Tātad...Tu runāji ar priesteri, ja? Nevarētu viņam paprasīt...Nē. Zini, nu ja tu te jau esi, tad nu jau neko. Kā tu domā... ja tev tēvs dzertu, tu viņam piedotu?
    -> P6
    
+ [Jums ir jārīkojas. Jums ir jāsāk kaut ko darīt.]
   Ko darīt? Šeit var tikai noslīkt.
    -> N8
    
            === N8 ===
+ [Nesakiet tā. Dzīve ir svarīga. Ir tikai jātic!]
   Kam man ticēt? Mani viss apmierina.
    -> N9
    
    
                === N9 ===
+ [Varbūt jūs varētu prasīt naudu uz ielas? Sakrātu kaut ko, vai atrastu darbu..?]
   Nevajaga man neko. Liecies mierā.
    -> N10
    
                   === N10 ===
+ [Jūs pat neklausaties, ko es saku!]
   Es neko negribu. Man ir jāiet....Laikam došos.
    -> N11
    
                       === N11 ===
+ [Pagaidiet, es nepabeidzu!]
   Viss, čau. Jā...viss tik slikti, tiešām. Laikam, tad tā būs.
    -> END