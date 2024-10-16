VAR option1 = false
VAR option2 = false

-> VirsIntro

=== VirsIntro ===
"Turpini iet. Te tev nav, ko darīt."
-> A1

=== A1 ===
* [Es negribēju traucēt.]
~ option1 = true
    "Tiešām? Nu, tu traucēji. Man nevajag kompāniju."
    -> ReturnToChoices

* [Ko jūš darat šeit viens pats?]
~ option2 = true
    "Ko tev izskatās? Dzīvoju savu dzīvi. Nodarbojies ar savām lietām."
    -> ReturnToChoices
    
    === ReturnToChoices ===
{option1 and option2:
    -> InitialBranch
- else:
    -> A1
}

=== InitialBranch ===
+ [Tu neizskaties, ka tev klājas pārāk labi...]
    "Man viss ir kārtībā. Tu domā, ka pazīsti mani tikai no tā, kā izskatos? Man nevajag, lai mani glābj. Labāk ej, pirms zaudē savu laiku."
    -> HesitantReveal

+ [Piedod, es negribēju tevi aizskart.]
    "Nu jā. Vienkārši ej tālāk, kā visi pārējie. Esmu pie tā pieradis."
    -> END

=== HesitantReveal ===
+ [Vai tiešām šī ir tava mājvieta?]
    "Jā. Un kas tad? Tā nav pils, bet tas ir viss, kas man ir. Man vairāk nevajag."
    -> TruthOrFacade

+ [Es neticu, ka kāds šādi dzīvo pēc izvēles.]
    "Tu domā, ka esmu šeit, jo nezinu neko labāku? Tici man, man ir labāk tā. Nav cerību, nav, kam pievilt."
    -> TruthOrFacade

=== TruthOrFacade ===
+ [Kur ir tava ģimene?]
    "Aizgāja. Atstāja mani. Droši vien tā bija labāk. Es viņus nevainoju. Es nebiju tā vērts, lai pie manis paliktu."
    -> FamilyDiscussion

+ [Tu tiešām esi mierā ar šādu dzīvi?]
    "Tu nesaproti, vai ne? Man nevajag izrādīties. Neviens manī vairs nerūpējas, un tas ir labi. Man nevajag viņu žēlumu."
    -> FamilyDiscussion

=== FamilyDiscussion ===
+ [Tu tiešām domā, ka viņiem būtu labāk bez tevis?]
    "Jā, tā domāju. Es visu sabojāju. Droši vien viņi tagad dzīvo labāk, kad esmu ārpus attēla."
    -> NegativeOrPositiveIntro

+ [Varbūt viņi aizgāja, jo domāja, ka tu nemainīsies. Ko darīt, ja viņi atgrieztos, ja tu mēģinātu labot lietas?]
    "Tu domā, ka es par to neesmu domājis? Ir par vēlu. Es to tiltu nodedzināju sen. Viņi mani pat vairs nepazītu."
    -> NegativeOrPositiveIntro

=== NegativeOrPositiveIntro ===
+ [Kāpēc izliecies, ka viss ir kārtībā? Šī ir izdzīvošana, nevis dzīvošana. Tev kaut kas ir jādara, nevis jāslēpjas mežā.]
    "Tev sliktas atzīmes skolā, ko? Nesaproti neko? Esmu mēģinājis. Nekas neizdevās. Tas... tas ir vieglāk. Neviens no manis neko negaida, un es neko negaidu no citiem."
    -> NegativePath

+ [Nav par vēlu mainīties. Varbūt tev būtu vēl viena iespēja ar viņiem.]
    "Mainīties? Tu vienkārši mainies, un viss brīnumainā kārtā atgriežas vecajās sliedēs? Es visu esmu zaudējis. Neviens mani vairs negaida."
    -> PositivePushback

=== NegativePath ===
+ [Tu esi padevies. Tu domā, ka neesi pelnījis neko labāku, bet tā nav taisnība. Beidz tā domāt.]
    "Es *zinu*, ka neesmu pelnījis neko labāku. Daži cilvēki vienkārši nav domāti tam, lai izturētu, un es esmu viens no viņiem. Pārstāj mēģināt salabot to, kas ir salauzts."
    -> NegativePushFurther

+ [Tev taisnība. Cilvēki kā tu tiek pamesti. Neviens nenāks tevi glābt.]
    "Beidzot kāds, kas to saprot. Tu cīnies, un tas neko nemaina. Galu galā nekas nemainās. Cilvēki iet tālāk, un es palieku šeit."
    -> NegativePushFurther

=== NegativePushFurther ===

+ [Tu varētu mainīties, ja gribētu. Tu vienkārši baidies mēģināt.]
    "Es nebaidos. Es tikai zinu, ka tas neko nemainīs. Tagad ej, pirms vēl iznieko vairāk laika ar mani."
    -> NegativeEndingChoice

=== PositivePushback ===
+ [Tev nav jāturpina dzīvot tā visu mūžu. Ir vietas, kas varētu palīdzēt tev atkal piecelties kājās.]
    "Palīdzēt? Tu domā, ka patversmes rūpējas par tādiem cilvēkiem kā es? Tu esi tikai skaitlis, ķermenis. Nevienam nav laika otrajām iespējām."
    -> PositivePushFurther

+ [Tu varētu atrast darbu, nopelnīt naudu. Cilvēki saņem otro iespēju visu laiku.]
    "Darbs? Neviens mani neņemtu darbā. Esmu pārāk ilgi bijis ārpus sistēmas. Viņi negribēs, lai es viņus vilktu atpakaļ."
    -> PositivePushFurther

=== PositivePushFurther ===
+ [Tu nezināsi, kamēr nepamēģināsi. Lietas nemainīsies, ja neko nedarīsi.]
    "Pamēģināt? Tu nesaproti. Esmu mēģinājis iepriekš. Neizdevās katru reizi. Kas šoreiz būs savādāk?"
    -> EncouragingPath

+ [Tev nav ko zaudēt. Lietas varētu mainīties, bet tikai tad, ja tu pieliksi pūles.]
    "Tu tiešām tici, ka tādi cilvēki kā es var mainīties? Es nezinu... ir grūti redzēt izeju no šīs situācijas."
    -> EncouragingPath

=== EncouragingPath ===
+ [Cilvēki ir izkļuvuši no sliktākām situācijām. Tev ir jātic, ka tas ir iespējams.]
    "Pēc visa tā, kas noticis, ir grūti iedomāties kaut ko citu. Esmu tik ilgi bijis šeit... es pat nezinu, vai vēl protu cīnīties."
    -> EncouragingPath2

+ [Ko darīt, ja tava ģimene redzētu, ka tu mēģini mainīties? Varbūt viņi atgrieztos.]
    "Mana ģimene... es pat nezinu, vai viņi mani gribētu atpakaļ. Bet, ja viņi gribētu... varbūt man būtu iemesls mēģināt."
    -> EncouragingPath2

=== EncouragingPath2 ===
+ [Tev vēl ir iemesls. Vai tas būtu viņu dēļ vai sevis dēļ, tu vari vēl visu mainīt.]
    "Varbūt tev taisnība. Varbūt man ir jāmēģina. Sliktāk jau diez vai var kļūt, vai ne?"
    -> PositiveEnd

+ [Viss sākas ar to, ka tici, ka esi pelnījis ko labāku. Tas ir pirmais solis.]
    "Jā... varbūt esmu bijis par zemu tik ilgi, ka, ja nesākšu tagad, tad nekad nesākšu."
    -> PositiveEnd

=== PositiveEnd ===
+ [Lai veicas. Tu esi pelnījis labāku dzīvi par šo.]
    "Paldies, puika. Es nezinu, kas notiks, bet es mēģināšu. Tas ir viss, ko varu darīt, vai ne?"
    -> END

=== NegativeEndingChoice ===
+ [Bet varbūt pietiks žēloties un darīt kaut ko. Padomā par savu ģimeni. Man būtu ļoti dusmas, ja mans tētis būtu tāds kā tu.]
    "Tu domā, ka man patīk būt vilšanās? Tici man, es neesmu par to lepns. Bet tas nav tik vienkārši kā vienkārši sākt kaut ko darīt."
    -> PositivePushback

+ [Varbūt tev taisnība. Varbūt nekas tavā dzīvē nemainīsies.]
    "Tu saproti. Tādiem cilvēkiem kā man nav otrās iespējas. Es palikšu šeit, kā vienmēr."
    -> NegativeEnd

=== NegativeEnd ===
+ [Uzredzēšanos.]
    "Jā... uzredzēšanos. Es laikam arī...došos."
    -> END
