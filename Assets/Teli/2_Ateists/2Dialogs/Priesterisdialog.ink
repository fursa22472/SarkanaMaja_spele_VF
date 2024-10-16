VAR option1 = false
VAR option2 = false
VAR option3 = false
VAR option4 = false
VAR option5 = false


"Labu vakaru, bērns. Ko tu dari tik vēlu ārā?"

+ [Labu vakaru. Es eju mājās...]
    "Zini, nav droši šajā laikā klīst apkārt, pat ja Dievs mūs pieskata. Ir tādi, kuru sirdīs ir tumsa. Cilvēku nodomi ir neskaidri, grūti saprotami. Pasaule ir pilna ar ēnām... tāpēc es cenšos cilvēkiem parādīt pareizo ceļu."
    "Tev nevajadzētu vienai staigāt tumsā, zini. Varbūt... varbūt kaut kas uz sirds? Tu izskaties aizdomājusies."

    -> ExploreBranch

+ [Man nav laika runāt. Atvainojiet.]
    "Protams. Ej, bet esi uzmanīga. Un, ja kādreiz jutīsies apmaldījusies, baznīcā vienmēr atradīsies vieta tev, pat ja tas būtu tikai, lai patvertos no aukstās nakts."

    -> END

=== ExploreBranch ===

+ [Es pēdējā laikā daudz domāju. Kāds pareizais ceļš?]
    "Nu, es strādāju baznīcā. Mans mērķis ir palīdzēt cilvēkiem rast mieru vai vismaz mēģināt. Daži nāk pēc atbildēm, daži vienkārši, lai justos mazāk vientuļi. Bet godīgi sakot, es neesmu pārliecināts, vai varu viņiem dot to, kas viņiem patiesībā ir vajadzīgs. Es nezinu... kas man šobrīd ir vajadzīgs."

    -> KidQuestions

=== KidQuestions ===
* [Kas ir ticība?]
 ~ option1 = true
    "Ticība? Ticība ir dīvaina lieta. Dažiem tas ir glābiņš. Citiem... tas ir kā akmens pie kājas, kurš slīcina. Ticība sev, citiem, Dievam... tās ir lietas, kuras cilvēkiem ir vajadzīgas, lai izdzīvotu. Bez tās mēs esam... pazuduši. Bet cilvēciskā ticība? Tā ir sarežģīta. Dažiem ticība ir tikai tas, ko viņi var redzēt un sataustīt. Bet īsta ticība... tā sniedzas tālāk par to, kas ir mūsu acu priekšā."
    -> ReturnToChoices

    * [Vai cilvēkiem ir nepieciešama skola, lai izprastu ticību?]
     ~ option2 = true
    "Skola māca faktus, bet ticība... ticība nāk no dzīves. Skola tev sniedz zināšanas, bet tā nemācīs, kā noticēt. Tās ir lietas, kuras tev jāatklāj pašam, caur savu pieredzi."
    -> ReturnToChoices

=== ReturnToChoices ===
{option1 and option2:
    -> People
- else:
    -> KidQuestions
}


=== People ===
+ [Kā ar cilvēkiem, kurus satikšu? Vai viņi visi ir pazuduši?]
    "Cilvēki, kurus tu satiksi? Daudzi ir... apmulsuši. Daži ir savtīgi, citi akli pret patiesību. To tu redzēsi pati. Esmu mēģinājis viņiem palīdzēt, bet... ar viņiem nav viegli. Viņi klīst, meklējot kaut ko, bet atsakās klausīties tos, kuri viņiem varētu parādīt ceļu."
    -> Whydont

=== Whydont ===
+ [Kāpēc tev viņi nepatīk?]
    "Nav tā, ka man viņi kā cilvēki nepatiktu, bet viņu izvēles... viņi sāpina sevi un tos, kas ir viņiem apkārt. Tu to sapratīsi, kad satiksi viņus. Esi uzmanīga."

    -> FormalTopics

=== FormalTopics ===
* [Vai cilvēki joprojām bieži nāk pie tevis?]
 ~ option3 = true
    "Jā, viņi nāk. Lielākoties no ieraduma, es domāju. Viņi lūdz lielākoties man nevis Dievam. Galvenokārt vienkāršas sarunas atrisina visas problēmas. Lai gan... dažreiz man šķiet, ka viņi gaida, lai es pateiktu, ka viss būs labi. Bet kā es to varu teikt, ja pats to nezinu?"
    -> ReturnToChoices2

* [Kāpēc tu kļuvi par priesteri?]
 ~ option4 = true
    "Tas nebija kāda liela plāna rezultāts. Es biju jauns, un es domāju, ka varu mainīt pasauli, dot cilvēkiem cerību. Bet ar laiku viss mainījās. Es mainījos. Tagad es vairs neesmu pārliecināts, vai vispār zinu, ko cerība nozīmē..."
    -> ReturnToChoices2

* [Vai tu kādreiz to nožēlo? Ka kļuvi par priesteri?]
 ~ option5 = true
    "Nožēlo? Dažreiz. Tas ir vientuļš ceļš, īpaši tad, kad sāc šaubīties par to, kam pats esi aicināts ticēt. Bet ko vēl es varēju darīt? Joprojām manī ir daļa, kas vēlas palīdzēt... pat ja nezinu, kā."
    -> ReturnToChoices2
    
    
    
    === ReturnToChoices2 ===
{option3 and option4 and option5:
    -> GodIntro
- else:
    -> FormalTopics
}



=== GodIntro ===
+ [Kā ar Dievu? Vai tu joprojām tici?]
    "Ah... lielais jautājums. Vai es ticu? Esmu sev šo jautājumu uzdevis neskaitāmas reizes. Dažreiz es domāju, ka ticu. Citos brīžos es domāju, vai tas ir tikai kaut kas, pie kā cilvēki pieķeras, lai nebūtu tik bail. Ko tu domā? Vai ticība tev ir svarīga?"

    -> BeliefQuestion

+ [Es nekad neesmu īsti domājusi par Dievu. Mans tēvs par Viņu nerunā.]
    "Tas ir saprotams. Ne visi uzaug ar ticību. Tev nav jāsaprot Dievs uzreiz. Tas nav par mācīšanos no grāmatām... tas vairāk ir par sajūtām, izpratni ar sirdi. Vismaz tā man vienmēr ir mācīts."

    -> BeliefQuestion

+ [Es domāju, ka bībele ir tikai pasaka, ko cilvēki izdomāja, lai justos labāk.]
    "Tu neesi pirmā, kas tā domā. Dažreiz... arī es dažreiz par to domāju. Bet, ja Dievs ir tikai stāsta galvenais varonis, kas mums tad atliek? Kam mēs ticam, kad viss kļūst grūti? Vai arī mēs vispār pārstājam ticēt?"

    -> NegativeBeliefBranch

=== BeliefQuestion ===
+ [Es nezinu. Dzīve ir sarežģīta.]
    "Tā ir. Daudz sarežģītāka, nekā es jebkad varētu izskaidrot. Bet dažreiz ticība kaut kam – jebkam – var būt pietiekama. Vai tu domā, ka ticība ir svarīgāka par izpratni?"

    -> PositiveBeliefBranch

+ [Vai tu domā, ka ticība ir svarīgāka par patiesības zināšanu?]
    "Esmu sev šo jautājumu uzdevis daudzas reizes. Varbūt ticība ir tā, kas mūs uztur, pat ja patiesība ir neskaidra. Varbūt tā ir tikai mierinājums. Bet bez tās... es nezinu, kas paliktu."

    -> PositiveBeliefBranch

=== PositiveBeliefBranch ===
+ [Ticība ir svarīga, ja tā kādam palīdz. Ja tā dod spēku.]
    "Jā... varbūt tas ir tas, ko meklējam. Ticība ir kā gaisma tumsā dažiem cilvēkiem. Nav svarīgi, kam viņi tic, kamēr tas palīdz viņiem iet uz priekšu. Esmu pieķēries šai idejai ilgu laiku, pat kad man pašam ticība šaubās."

    -> Goodpath

=== NegativeBeliefBranch ===
+ [Man vienalga. Ticība nepalīdz. Tā nav īsta.]
    "Varbūt tev taisnība. Varbūt esmu pieķēries kaut kam, kas jau ir saplīsis. Bet pat ja ticība nav atbilde, kas slikts tajā, ja kādam tā ir vajadzīga? Kad pasaule kļūst pārāk smaga, cilvēkiem dažreiz vajag kaut ko – jebko – pie kā pieķerties. Pie kā tu pieķeries, kad viss kļūst grūti?"

    -> Badpath

+ [Es domāju, ka ticība ir bezjēdzīga. Cilvēkiem jāsaskaras ar realitāti.]
    "Realitāte ir skarba. Bez ticības tā ir vēl skarbāka. Bet varbūt daži cilvēki ir pietiekami stipri, lai to izturētu bez ticības mierinājuma. Es novēlu, lai es būtu tik stiprs... bet es vairs neesmu pārliecināts."

    -> Badpath

=== Goodpath ===
"Paldies... Tu man devi kaut ko, par ko padomāt. Varbūt es varu atrast kaut ko, kam noticēt atkal, pat ja tas nav tas, kam es kādreiz ticēju. Vēl nav par vēlu, vai ne?"

    -> PriestExtraTopicsPositive

=== Badpath ===
"Varbūt tev ir taisnība. Varbūt esmu bijis pazudis jau ilgāk, nekā esmu sapratis. Ticība vai tās trūkums... tas mūs atstāj ar vieniem un tiem pašiem jautājumiem. Bet tu man esi parādījusi, ka pieķerties kaut kam saplīsušam varbūt ir vēl sliktāk nekā ļaut tam aiziet."

    -> PriestExtraTopicsNegative

=== PriestExtraTopicsPositive ===
+ [Vai tu joprojām jūties pazudis?]
    "Pazudis? Vairs ne tik ļoti. Saruna ar tevi... man atgādināja, ka ticība nav par atbildēm, bet par spēku turpināt uzdot jautājumus. Varbūt tas šobrīd ir pietiekami."

    -> WrapUpGood

=== PriestExtraTopicsNegative ===
+ [Vai tu nožēlo, ka tik ilgi ticēji?]
    "Nožēlot... varbūt. Bet nožēla neko nemainīs. Esmu pieķēries ticībai tik ilgi, ka nezinu, ar ko to aizstāt tagad. Varbūt man ir par vēlu atrast jaunu ceļu."
    
    "Bet esmu runājis pārāk daudz. Beigu beigās, tu izdarīsi savas izvēles. Pasaule turpinās spiest, un dažreiz tā tevi salauzīs. Es tikai ceru, ka tu atradīsi kaut ko, kam pieķerties, kad tas notiks."

    -> WrapUpBad

=== WrapUpGood ===
"Bet pietiek par mani. Es redzu, ka tev ir savas domas. Lai ko pasaule tev priekšā liktu, atceries: tev ir spēks izlemt, kam ticēt. Tu esi stiprāka, nekā tu domā."

+ [Paldies par sarunu.]
    "Lūdzu. Ceru, ka atradīsi atbildes, kuras meklē. Un varbūt, tikai varbūt, arī es atradīšu savējās."

    -> END

=== WrapUpBad ===

+ [Paldies par sarunu.]
    "Uz redzēšanos... un parūpējies par sevi. Pasaule nav laipna pret tiem, kas tai stājas pretī vieni.Tie kamiņi, kurus tu vēl nepazīsti, viņi centīšies tevi pārliecināt, ka tavām domām nav vietas šajā pasaulē. Tev jāizvēlās, vai gribi aizstāvēt savu viedokli līdz galam, vai pakļausies vieglprātībai."

    -> END
