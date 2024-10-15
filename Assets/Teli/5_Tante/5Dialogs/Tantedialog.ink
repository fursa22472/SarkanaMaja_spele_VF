# audio:Tante_5_N_01
    Tu, bērns! Ko tu dari, ložņādama kā kāda žurka? Runājiet! Nevienam nav laika klusajām spēlēm! Tu taču, tu kaut ko spēlē?

+ [Es tikai eju garām. Piedodiet.]
    # audio:Tante_5_N_02
    Ej garām, vai? Vienmēr iet garām. Vienmēr ignorē. Es vienreiz zvanīju uz pastu, zvanīju, neatbildēja. Tāda viņiem sistēma. Jūs, jaunie, nekad neapstājaties, nekad neklausaties. Skrien apkārt, domājā par sevi šajā egoistu pasaulē. Jākaunās.
    
    -> casual_path

+ [Kādēļ tu kliedz uz mani..]
    # audio:Tante_5_N_03
    Kliedzu? Tu domājat, ka kliedzu? Tu vēl neesi dzirdējusi mani kliedzam. Kā jūs visus tagad audzina. Nopērk telefonu un viss, domā, ka nevajag piepūlēties. Manos laikos bija savādāk.
    
    -> casual_path

=== casual_path ===

+ [Es nesaprotu, kāpēc vajag tā dusmoties...]
    # audio:Tante_5_N_04
    Slinki cilvēki mani sadusmo. Es priecātos tikai, ja mani kāds novērtētu šajā pasaulē. Neviens tagad pavisam pat nebrauc ciemos. Saka, ka aizņemti. Meli.
    -> casual_continue

+ [Mani audzināja tēvs. Viņš nav egoists.]
    # audio:Tante_5_N_05
    Tavs tēvs nebūs dzīvojis tik ilgi, cik es. Tu arī esi tikai bērns, izaugtu - saprastu. Viss tik ātri skrien uz priekšu, bet es palieku, visi atstāj mani šeit. Neviens nerunā. Esmu aizmirsta pavisam. 
    -> casual_continue

=== casual_continue ===

+ [Dažreiz arī tā gadās, kad nepietiek laika sarunām. Es domāju tevi neviens neaizmirsa.]
    # audio:Tante_5_G_06
    Daudz tu zini, ha? Spiest podiņas laika pietiek, skatīties tos video jūtubē arī laiks ir, bet pazvanīt man nav. Manā jaunība vispār bija grūti pazvanīt kādam, mēs tikāmies, runājām. Tagad tikai telefona lodziņš labākais draugs.
    -> GoodPath1

+ [Man žēl. Man dažreiz arī liekas, ka neviens neklausās. Man tas nepatīk. Laikam visi ir nejauki...Varbūt man melo arī?]
    # audio:Tante_5_B_07
    Es nomiršu, un viņi pat nepamanītu. Tā arī drīz būs. Tu arī tagad esi šeit un šaubos, ka kāds padomā par tevi. Tava ģimene gan jau, ka šobrīt skatās kādu seriālu. Tāpat labu seriālu nav šodien.
    -> BadPath1

=== BadPath1 ===

+ [Es neskatos seriālus.]
    # audio:Tante_5_B_08
    Nav jau arī jāskatās. Ir jāmācās, jālasa grāmatas.
    -> BadPathContinue1

+ [Jums tiešām tā liekas? Es negribētu nomirt vientulībā]
    # audio:Tante_5_B_9
    Redzi, šajā pasasulē nekas nav atkarīgs no tevis. Pat Dievs vairs neskatās līdzi. Man liekas viņam riebjas tas, kur mēs nonācām.
    -> BadPathContinue2

=== GoodPath1 ===

+ [Varbūt tev vajag hobiju? Dažreiz, es zīmēju. Man nesanāk, bet es to daru sev.]
    # audio:Tante_5_G_11
   Sev? Kur vispār manā vecumā var kaut ko darīt sev? Man ir jāstrādā šeit, neviens taču to nedara, traukus nemazgā, ogas nevāc, nepalīdz ar māju. 
    -> GoodPathContinue

+ [Man neliekas, ka tā ir slikta lieta. Es skatos bildes internetā. Jūsu laikā tā nevarēja, bildes arī pazuda.]
    # audio:Tante_5_G_12
    Un ko tu tur par manu laiku stāsti? Nav jau tikai tajā internetā problēma. Visi cilvēki palika nejauki un neizpalīdzīgi. Tādi...ļoti. Ļoti it kā aizņemti.
    -> NeutralPath

=== BadPathContinue1 ===

+ [Es mājās mācos. Man īsti nepatīk grāmatas. Garlaicīgi.]
    # audio:Tante_5_B_13
    Nu tad ej pie manis, palīzēsi man te dārzā strādāt. Atradīšu tev darba cimdus un lāpstu. Un šitā tu visu nākotni esi gatava darboties? Nekas labs nav patīkams. Ja gribi kaut ko sasniegt, ir jāstrādā.
    -> BadEndingLead

    
    === BadPathContinue2 ===

+ [Es satiku priesteri. Viņš netic Dievam. Bet viņš neteica, ka viss tik slikti. Kaut kas ir tomēr atkarīgs no mums.]
    # audio:Tante_5_B_14
    Ja tā padomā...Varbūt. Kautk kā jau mēs nonācām līdz šajai nelaimei. Esmu tik nelaimīga. Gribētos aizbraukt kaut kur tālu, kur ir silti. Bet tā pensija jau nav nauda. 
    -> BadEndingLead

=== GoodPathContinue ===

+ [But people are still trying to make a difference. That’s what matters.]
    # audio:Tante_5_G_15
    Tante: You think that’s enough? Tryin' don’t get you nowhere without real action. And even action’s too little too late now. You’ll see, kid—you’ve got too much hope.
    -> GoodEndingLead

+ [Maybe you just don’t want to see things getting better.]
    # audio:Tante_5_G_16
    Tante: Me? Ha! You think I *want* this? I didn’t choose to watch it all fall apart. You’ll see one day. But by then, it’ll be too late.
    -> NeutralPath

=== NeutralPath ===

+ [I’m not sure about all this. You seem bitter.]
    # audio:Tante_5_N_17
    Tante: Bitter? Ha! You’d be bitter too if you saw what I’ve seen. This world’s broken, kid. I’m just the only one with eyes wide open.
    -> FinalChoice

+ [I don’t know... you might be exaggerating.]
    # audio:Tante_5_N_18
    Tante: Exaggeratin'? Ha! You think this is all some story? You’re blind, just like the rest of 'em. Keep thinkin’ that, kid. When it crumbles around you, you’ll know I was right.
    -> FinalChoice

=== FinalChoice ===

#choice  
    * [Maybe you’re right. The world’s a mess, and no one’s fixing it.]
        -> BadEnding
    * [No, I think you’re just stuck in your ways. People are doing their best.]
        -> GoodEnding

=== BadEndingLead ===

+ [I guess the world really is falling apart.]
    # audio:Tante_5_B_20
    Tante: Exactly. Welcome to the truth, kid. It ain’t pretty, but it’s real. No one’s gonna save us. You’ll get used to it.
    -> BadEnding

+ [I still don’t think it’s as bad as you say.]
    # audio:Tante_5_B_21
    Tante: Denial, huh? Well, you’ll see it eventually. By the time you do, it’ll be too late to change anything.
    -> BadEnding

=== BadEnding ===

# audio:Tante_5_B_22
    Tante: You’re startin’ to see it now, huh? The world’s crumblin’, and no one cares. Just another cog in a machine that’s rustin’ away. You’ll be no different.

-> END

=== GoodEndingLead ===


+ [That’s your problem. You’ve given up. People are still fighting.]
    # audio:Tante_5_G_24
    Tante: Fightin'? Ha! Go ahead and fight, but you’ll lose in the end. The world’s too far gone for hope to matter.
    -> GoodEnding

+ [You’re just bitter because you feel powerless. That’s on you.]
    # audio:Tante_5_G_25
    Tante: Powerless? Bitter? One day, kid, you’ll understand. Go on, fight for your dreams, but don’t say I didn’t warn you.
    -> GoodEnding

=== GoodEnding ===

# audio:Tante_5_G_26
    Tante: Ha! You think you’ve got all the answers, don’t you? Maybe you’ll change things. Or maybe you’ll end up just like me, wonderin' where it all went wrong.

-> END
