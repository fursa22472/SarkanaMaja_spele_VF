VAR option1 = false
VAR option2 = false


Tu, bērns! Ko tu dari, ložņādama kā kāda žurka? Runā ar mani. Hallo! Nevienam nav laika spēlēm! Tu taču, kaut ko spēlē?
#audio:Tante_5_N_00

+ [Es tikai eju garām. Piedodiet.]
    Tad ej garām.
    #audio:Tante_5_N_01
    -> END

+ [Kādēļ kliegt uz mani..]
    
   Kliedzu? Tu domā? Eu nu, vēl neesi dzirdējusi mani kliedzam. Kā jūs visus tagad audzina? Nevar pat parunāt ar jums normāli.
   #audio:Tante_5_N_02
    -> A1

=== A1 ===
+ [Mēs? Kas mēs?]
    
    Jūs - jaunā paaudze. Traki ar jums. Grūti pat. Es saku, tā ir tā jaunā audzināšanas metode pie vainas.
    # audio:Tante_5_N_03
    -> A2

=== A2 ===
+ [Es nesaprotu, kāpēc vajag tā dusmoties...]
    Slinki cilvēki mani sadusmo. Es priecātos tikai, ja mani kāds novērtētu šajā pasaulē. Neviens tagad pavisam pat nebrauc ciemos. Saka, ka aizņemti. Visu laiku melo. 
    # audio:Tante_5_N_04
    -> C1

+ [Mani audzināja tēvs. Viņš nav slikts...]
    Tavs tēvs nebūs dzīvojis tik ilgi, cik es. Tu arī esi tikai bērns, izaugtu - saprastu. Viss tik ātri skrien uz priekšu, bet es palieku, visi atstāj mani šeit. Neviens nerunā. 
    # audio:Tante_5_N_05
    -> C2

=== C1 ===
+ [Dažreiz arī tā gadās, kad nepietiek laika sarunām. Es domāju tevi neviens neaizmirsa.]
    Daudz tu zini, ha? Spiest podiņas laika pietiek, skatīties tos video jūtubē arī laiks ir, bet pazvanīt man nav. Manā jaunība vispār bija grūti pazvanīt kādam, mēs tikāmies, runājām. Tagad tikai telefona lodziņš labākais draugs.
    # audio:Tante_5_N_06
    -> C3

=== C3 ===
+ [Varbūt tev vajag hobiju? Dažreiz, es zīmēju. Man nesanāk, bet es to daru sev.]
    Sev? Kur vispār manā vecumā var kaut ko darīt sev? Man ir jāstrādā šeit, neviens taču to nedara, traukus nemazgā, ogas nevāc, nepalīdz ar māju.
    # audio:Tante_5_N_07
    -> C6

+ [Man neliekas, ka tā ir slikta lieta. Es skatos bildes internetā. Jūsu laikā tā nevarēja, bildes arī pazuda.]
    Un ko tu tur par manu laiku stāsti? Nav jau tikai tajā internetā problēma. Visi cilvēki palika nejauki un neizpalīdzīgi. Tādi...ļoti. Ļoti aizņemti..
    # audio:Tante_5_N_08
    -> C6

=== C2 ===
+ [Man žēl. Man dažreiz arī liekas, ka neviens neklausās. Man tas nepatīk. Laikam visi ir nejauki...Varbūt man melo arī?]
    Es nomiršu, un viņi pat nepamanītu. Tā arī drīz būs. Tu arī tagad esi šeit un šaubos, ka kāds padomā par tevi. Tava ģimene gan jau, ka šobrīt skatās kādu seriālu. Tāpat labu seriālu nav šodien.
    # audio:Tante_5_N_09
    -> C4

=== C4 ===
+ [Jums tiešām tā liekas? Es negribētu nomirt vientulībā]
    Redzi, šajā pasaulē nekas nav atkarīgs no tevis. Pat Dievs vairs neskatās līdzi. Man liekas viņam riebjas tas, kur mēs nonācām. Viņš izveidoja pasauli dusmās.
    # audio:Tante_5_N_10
    -> C5

+ [Es neskatos seriālus.]
    Nav jau arī jāskatās. Ir jāmācās, jālasa grāmatas
    # audio:Tante_5_N_11
    -> C5

=== C5 ===
* [...]
~ option1 = true
    -> CC1

=== C6 ===
* [...]
~ option2 = true
    -> CC2


    === CC1 ===
{option1 and option2:
    -> A3
- else:
    -> C1
}

    === CC2 ===
{option1 and option2:
    -> A3
- else:
    -> C2
}





=== A3 ===
+ [Es satiku priesteri. Viņš netic Dievam. Bet viņš neteica, ka viss tik slikti. Kaut kas ir tomēr atkarīgs no mums.]
    Priesteri, kurš netic Dievam? Tas nav priesteris tad. Labāk, ja vinš tepat bez darba staigā, lai palīdz man ar dārzu. Atradīšu darba cimdus un lāpstu. 
Un šitā tu arī visu nākotni esi gatava darboties? Nekas labs nav patīkams. Ja gribi kaut ko sasniegt, ir jāstrādā.
# audio:Tante_5_N_12
    -> A4



=== A4 ===
+ [Es vēl tikai mācos.]
    Tad labi. Galvenais nenonākt līdz tādai nelaimei, kā man. Gribētos aizbraukt kaut kur tālu, kur ir silti. Bet tā pensija jau nav nauda. 
    # audio:Tante_5_N_13
    -> A5


=== A5 ===
+ [Un kas jums traucē aizbraukt?]
   Visu naudu aizsūtu saviem bērniņiem. Viņiem taču jāmācās. Arī jāmaksā par māju. Apkure paliek dārgāka. Ai... tu taču nezini. 
   # audio:Tante_5_N_14
    -> A6

+ [A, skaidrs...jā.]
   Un galvenais, Latvijai jau nav ne vainas. Es šeit uzaugu, mani vecāki un vecvecāki arī. Cilvēki ir tā galvenā problēma. Skaļi. Šeit nav vietas, kur dzīvot. Un visu laiku tā steiga!
   # audio:Tante_5_N_15
    -> A6
    
    
    === A6 ===
+ [Tāda sajūta, it kā jūs vienkārši negribat neko mainīt.]
    Man jau nav nekas jāmaina. Paskaties, ir jau pietiekami daudz cilvēku, kas visu laiku kaut ko maina. Man bērni nopirka jaunu televizoru, un kam? Es neprasīju
    # audio:Tante_5_N_16
    -> A7
    
     === A7 ===
+ [Man žēl...]
    Tas nav viss! Es izeju ārā, speciāli apejot visas tās jauniešu vietas... Un viņi tāpat parādās. Nepieklājīgi, ar savu skaļo mūziku, un tie mati! Tu nevari iedomāties, kāds man šoks, kad skaistas meitenes staigā ar īsiem matiem. Nogriež visu skaistumu un vēl uzliek to šausmīgo grimu pa virsu. Kā kaut kādi klauni. Mani mazbērni jau grib tā staigāt, es neļauju.
    # audio:Tante_5_N_17
    -> A8
    
    
    === A8 ===
+ [Bet man liekas skaisti..]
    Kas tur skaists? Un vēl uz rokām visādi uzraksti. Manos laikos par to sita. Viņus tas tumšais spēks kaut kad dabūs. Biežāk tādiem uz baznīcu. Kā tev liekas?
    # audio:Tante_5_N_18
    -> A9    
    
    
    === A9 ===
+ [Es nedomāju, ka viņi dara kaut ko ļaunu.]
   Viņi apzīmē ēkas, posta, nedod vietu trolejbusā un pat neklausās. Man tas traucē. Visur tikai jaunieši. 
   # audio:Tante_5_N_19
    -> A10     
    
    
     === A10 ===
+ [Neredzu neko sliktu tajā. Nejau visi ir nejauki pret jums...]
   Ar tevi viss skaidrs. Es padodos. Būs jāmirst te, vientulībā.  
   # audio:Tante_5_N_20
    -> A11    
    
    
+ [Godīgi sakot, man arī tādi nepatīk. Manam tēvam arī. ]
   Jo tu esi labi audzināts bērns! Zini kā, paliek jau tumšs. Nevēlies ienākt ciemos, es tieši izvārīju zupu? Ko saki? 
   # audio:Tante_5_N_21
    -> A12   
    
    
    
    === A11 ===
+ [Man liekas, jums jāparunā ar kādu citu. Piedodiet, es iešu mājās. Lai jums jauks vakars!]
    -> END     
    
    === A12 ===
+ [Nē, piedodiet, labāk iešu. Jāskrien mājās.]
Nu, skaties meitiņ. Ja kaut kas notiek ģimenē, vienmēr vari atrast mani un parunāties. Labi? Nu, attā!
# audio:Tante_5_N_22
    -> END  
    
+ [Varētu. Kāpēc gan ne? Paldies!]
    -> END     
