VAR option1 = false 
VAR option2 = false
VAR option3 = false
VAR option4 = false

Turpini iet. Te tev nav, ko darīt.
#audio:Bomzis_3_N_00
-> A1


=== A1 ===
* [Es negribēju traucēt.]
~ option1 = true
    Turpini iet. Te tev nav, ko darīt. 
    #audio:Bomzis_3_N_00
    -> ReturnToChoices
    

* [Ko jūs darat šeit viens pats?]
~ option2 = true
    Es šeit dzīvoju.
     #audio:Bomzis_3_N_02
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
    #audio:Bomzis_3_N_03
    -> A3
    

+ [Piedod, es negribēju tevi aizskart.]
    Nu jā. Vienkārši ej tālāk, kā visi pārējie. Esmu pie tā pieradis.
    #audio:Bomzis_3_N_04
    -> END
    

=== A3 ===
+ [Vai tiešām tu te dzīvo?]
   Jā. Un kas tad? Tā nav pils, bet vairāk nevajag.
    #audio:Bomzis_3_N_05
    -> A4
    

+ [Es neticu, ka kāds šādi dzīvo pēc izvēles.]
    Tu domā, ka esmu šeit, jo negribu neko labāku? Tici man, labāku par šo man nepienākas. 
     #audio:Bomzis_3_N_06
    -> A4
   

=== A4 ===
* [Kur ir tava ģimene?]
~ option3 = true
    Aizgāja. Atstāja mani. Droši vien tā bija labāk. Es viņus nevainoju. 
    #audio:Bomzis_3_N_07
    -> ReturnToChoices2
    

* [Tevi tiešām apmierina šāda dzīve?]
~ option4 = true
    Tu nesaproti, vai ne? Man nevajag izrādīties. Neviens par mani vairs nerūpējas, un tas ir labi. 
    #audio:Bomzis_3_N_08
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
   #audio:Bomzis_3_N_09
    -> A6
    

=== A6 ===
+ [Ja viņi atgrieztos, vai tu mēģinātu labot lietas?]
    Nu, rekur, gudriniece!
    Ir par vēlu. Viņi mani pat vairs nepazītu.
    #audio:Bomzis_3_N_10
    -> A7
    

=== A7 ===
+ [Šī ir izdzīvošana, nevis dzīvošana. Tev kaut kas ir jādara.]
    Tev sliktas atzīmes skolā, ko? Nesaproti neko? 
    Esmu mēģinājis. Nekas neizdevās. Neviens no manis neko negaida, un es neko negaidu no citiem.
    #audio:Bomzis_3_N_11
    -> A8
    

=== A8 ===

+ [Nav par vēlu mainīties. Varbūt tev būtu vēl viena iespēja.]
    Tu nezini to sajūtu, nu, kad tev tāds....Tu zini, ka esi pretīgs un vainīgs un, ja pat piedotu,, tad... Kuram es tāds vajadzīgs.
    #audio:Bomzis_3_N_12
    -> P1
    
    
+ [Tu esi padevies. Tu domā, ka neesi pelnījis neko labāku, bet tā nav. Beidz tā domāt.]
    Beidz stāvēt šeit. Beidz būt tik uzbāzīga. Beidz runāt ar mani. 
    #audio:Bomzis_3_N_13
    -> N1
    

=== P1 ===
+ [Jums ir jāsāk ar kaut ko mazu. ]
    Ir. Ir iespēja nesabojāt šo. Sākt no jauna.
    #audio:Bomzis_3_N_14
    -> P2
    
    
+ [Jums vajag palīdzību. Varbūt noīrēt dzīvokli?]
    Pareizi! Ka tā. Jā, rekur ir. Nu tad laikam iešu, paldies liels.
    #audio:Bomzis_3_N_15
    -> N2
    

=== P2 ===
+ [Tieši tā, jāsāk no jauna!]
    Sākšu ar to, ka aizmirsīšu par tām dienām, kad man bija ģimene.
    #audio:Bomzis_3_N_16
    -> P3
    

=== P3 ===
+ [Nē, jāsāk ar sevi.]
    Tev neliekas dīvaini, ka.... tu tāda vairāk, ar dzīves zināšanām. Pārzināšanu? It kā tev būrtu līdzīga situācija. Lūdz Dievu, lai nebūtu.
   #audio:Bomzis_3_N_17
    -> P4
    

=== P4 ===
+ [Es domāju, ka ticība jums palīdzētu. Kaut vai ticība sevī. Es satiku priesteri, kurš pats netic, bet viņš nepadevās.]
    Ak, tad tev patīk runāties. Man nepatīk. 
     #audio:Bomzis_3_N_18
    -> P5
   

=== P5 ===
+ [Es nezinu, kā jums palīdzēt. Jums nav auksti?]
    Nav, nav. Tātad...Tu runāji ar priesteri, ja? Nevarētu viņam paprasīt...Nē. Zini, nu ja tu te jau esi, tad nu jau neko. Kā tu domā... ja tev tēvs dzertu, tu viņam piedotu?
    #audio:Bomzis_3_N_19
    -> P6
    

=== P6 ===
+ [Mans tēvs nedzer. Es viņam visu piedotu. ]
    Paldies. Es savam nepiedodu. Nu cilvēki ir dažādi tad, jā? Ai. Zini ko? Mēģināsim. Atcerējos par to priesteri un uzreiz tā kā skaidrāk galvā. Ne par Dievu, bet par sarunām ar viņu. Tur bija tas sasodītais lietussargs.
   #audio:Bomzis_3_N_20
    -> P7
    

=== P7 ===
+ [Lietussargs?]
    Izkāpu es trolejbusā, nu, jo, nu nebija taloniņa, izeju, bet tur tas, tas vīriņš iet un iedod lietussargu pats , galvenais iet bez, bet man iedod. Beigās, nu, kautkur pazaudēju, bet...jā. Skaista diena bija. 
    Ir jācenšas.
   #audio:Bomzis_3_N_21
    -> P8
    
    
+ [Jūs varat konkrētāk?]
    Izkāpu es trolejbusā, nu, jo, nu nebija taloniņa, izeju, bet tur tas, tas vīriņš iet un iedod lietussargu pats , galvenais iet bez, bet man iedod. Beigās, nu, kautkur pazaudēju, bet...jā. Skaista diena bija. 
    Ir jācenšas.
    #audio:Bomzis_3_N_21
    -> P8
    
    
=== P8 ===
+ [Es arī tā domāju.]
    Tu esi ļoti jauka. Paldies.
    #audio:Bomzis_3_N_23
    -> P9
    
    
    
+ [Es nesapratu īsti par lietussargu, bet man prieks.]
    Tu esi ļoti jauka. Paldies.
    #audio:Bomzis_3_N_23
    -> P9
    
    
=== P9 ===
+ [Visu labu! Es ceru, ka palīdzēju...]
    Es domāju, ka tev taisnība. Es mēģināšu. Atā!
    #audio:Bomzis_3_N_25
    -> END
    

=== N1 ===
+ [Jums vajag palīdzību. Varbūt noīrēt dzīvokli?]
    Pareizi! Ka tā. Jā, rekur ir. Nu tad laikam iešu, paldies liels.
    #audio:Bomzis_3_N_15
    -> N2   
    
    
=== N2 ===
+ [Kad jūs domājat pārvākties?]
    Nu tulīt sākšu krāmēt mantas. Paldies vēlreiz. Vari iet jau. 
    #audio:Bomzis_3_N_27
    -> N3
    

=== N3 ===
+ [Kas notiek? Kādēļ jūs tad neejat?...]
    .... 
    Laikam būšu pazaudējis savus desmit tūkstošus kabatā. Diemžēl Dievs nav manā pusē
    #audio:Bomzis_3_N_28
    -> N4
    

=== N4 ===
+ [...Pagaidi. Tas bija joks? Jums nav naudas. ]
    Ej, es tevi lūdzu. Paej garām. 
    -> N5
    #audio:Bomzis_3_N_29

    === N5 ===
+ [Kā es varu paiet garām. Šis ir mans pienākums.]
    Pienākums nedot man mieru?
    #audio:Bomzis_3_N_30
    -> N6
    

        === N6 ===
+ [Pienākums, kā pilsonim palīdzēt tādiem kā jūs.]
    Kurš tev to galvā ielika? ...Māte? Skolotāja? Kāds tad es esmu, ka tev tāds...tas.... pienākums?
    #audio:Bomzis_3_N_31
    -> N7
    

        === N7 ===
+ [Es nezinu, kā jums palīdzēt. Jums nav auksti?]
    Nav, nav. Tātad...Tu runāji ar priesteri, ja? Nevarētu viņam paprasīt...Nē. Zini, nu ja tu te jau esi, tad nu jau neko. Kā tu domā... ja tev tēvs dzertu, tu viņam piedotu?
    #audio:Bomzis_3_N_32
    -> P6
    

+ [Jums ir jārīkojas. Jums ir jāsāk kaut ko darīt.]
    Ko darīt? Šeit var tikai noslīkt.
    #audio:Bomzis_3_N_33
    -> N8
    

            === N8 ===
+ [Nesakiet tā. Dzīve ir svarīga. Ir tikai jātic!]
    Kam man ticēt? Mani viss apmierina.
    #audio:Bomzis_3_N_34
    -> N9
    

                === N9 ===
+ [Varbūt jūs varētu prasīt naudu uz ielas? Sakrātu kaut ko, vai atrastu darbu..?]
    Nevajaga man neko. Liecies mierā.
    #audio:Bomzis_3_N_35
    -> N10
    

                   === N10 ===
+ [Jūs pat neklausaties, ko es saku!]
    Es neko negribu. Man ir jāiet....Laikam došos.
    #audio:Bomzis_3_N_36
    -> N11
    

                       === N11 ===
+ [Pagaidiet, es nepabeidzu!]
    Viss, čau. Jā...viss tik slikti, tiešām. Laikam, tad tā būs.
    #audio:Bomzis_3_N_37
    -> END
    