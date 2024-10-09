"Kaimiņi, vai tad neesam kaimiņi? Labvakar! Ko tu šeit šajā laikā dari? Tevi neviens mājās negaida? Ja skriesi, varbūt paspēsi līdz saulrietam. Vecāki gan jau ka dusmojas... Tu vēl kādu šeit pa ceļam varbūt satiki?"
#audio:Rainis_1_N_01

+ [Nē.]
    "Nu, feini. Apmaldīties te tādā laikā ir vienkārši, labāk skaties, kur kāds atrodas. Tad jau nu grūtāk būs pazust, manuprāt."
    "Neturēšu tevi ilgi te, skat, jau saule laižas zemāk. Varbūt kaut kas uz sirds? Tu izskaties tāda pabāla. M?"
    #audio:Rainis_1_N_02


    -> ExploreBranch

+ [Jā. (samelo)]
    "Nu, feini. Apmaldīties te tādā laikā ir vienkārši, labāk skaties, kur kāds atrodas. Tad jau nu grūtāk būs pazust, manuprāt. Bet ja paej kādam garām, šaubos, ka ieraudzīsi vēlreiz."
    "Neturēšu tevi ilgi te, skat, jau saule laižas zemāk. Varbūt kaut kas uz sirds? Tu izskaties tāda pabāla. M?"
    #audio:Rainis_1_N_03

    -> ExploreBranch

=== ExploreBranch ===

+ [Nē, paldies. Viss labi. Es labāk iešu.]
    "Labi. Tad skrien, uz priekšu! Nodod sveicienus vecākiem!"
    #audio:Rainis_1_N_05
    -> END

+ [Aizdomājos par lietām.]
    "Pagaidi, dod tikai pateikt vēl pāris vārdus. Ja tomēr satiec kādu pa ceļam, tu apdomā. Domām vienmēr piemīt īpašs spēks. Ne vienmēr sakāmais var patikt, bet no meliem arī attiecības labas neuzbūvēsi. Vienkārši saki to, kas uz sirds, to, kas ir tuvāks tev."
     #audio:Rainis_1_N_06

    -> KaiminiBranch

=== KaiminiBranch ===
+ [Labi. Uzredzēšanos. (beigas)]
    "Nu viss, tagad skrien, uz priekšu! Nodod sveicienus vecākiem!"
     #audio:Rainis_1_N_05
    -> END

+ [Pagaidiet. Jūs minējāt tos... kaimiņus. Pastāsties vairāk]
    "Ak tad tā. Ko tieši vēlies zināt? (Papildus teksts, jo kad ir maz teksta tad neredz tekstu aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa. JAIZLABO)"
     #audio:Rainis_1_N_07

    -> FearOrGoodBranch

=== FearOrGoodBranch ===
+ [Vai man no viņiem jābaidās?]
    "Baidīties? Nē, nē. Dažiem nīst liekas vieglāk nekā mīlēt, bet tur jau arī vecums pie vainas. Jaunieši kļuvuši pavisam garīgi veci arī. Būtu tikai pareizi atgādināt viņiem, ka laiks nestāv uz vietas. Tur celiņš, tev jāiet tikai taisni uz priekšu, un tālāk jābūt kādai gaismai."
    "Nu viss, tagad skrien, uz priekšu! Nodod sveicienus vecākiem!"
         #audio:Rainis_1_N_08

    -> GoodLuckBranch

+ [Vai viņi ir slikti vai labi kaimiņi?]
    "Kas ir slikts un kas ir labs? Ļaudis ir vai nu noguruši no vientulības, vai nu nogurst no tā, ka nevar būt vieni. Tu redzēsi, ja atradīsi kādu."
         #audio:Rainis_1_N_09

    -> GoodLuckBranch

=== GoodLuckBranch ===

+ [Sapratu! Paldies. Un ko Jūs paši? Neiesiet mājās?]
    "Mājās? Es tepat jau netālu dzīvoju. Dažreiz izeju pastaigāties, paelpot svaigu gaisu. Man tā ārsts teica, kā labāk būtu. Tikai vienīgais neviens šeit negrib ar mani spēlēt šahu. Kārtis arī nespēlē. Paliek tikai lasīt un sēdēt ārā."
         #audio:Rainis_1_N_10

    -> ChessBranch
    
=== ChessBranch ===
+ [Šahu? Man arī patīk šahs.]
    "Ak, nu tad tu zini, cik daudz šahā atklājas par cilvēku. Tur nav tikai staigāšana pa galdiņu ar figūrām, bet arī domāšana vairākus soļus uz priekšu. Varbūt kādreiz mēs varam uzspēlēt. Lai gan... reti kurš te paliek tik ilgi, lai spēli pabeigtu."
          #audio:Rainis_1_N_11

    -> PersonalBranch

+ [Kāpēc šahu? Nav nekādu citu spēļu, kas jums patiktu?]
    "Šahs mani vienmēr piesaistījis. Tur ir stratēģija, pacietība, domas skaidrība. Citas spēles man vairs nav tās pašas — pārāk ātras, pārāk haotiskas. Dzīvē jau tāpat pietiek haosa."
          #audio:Rainis_1_N_12
    -> PersonalBranch
    
=== PersonalBranch ===
+ [Jūs minējāt, ka dzīvojat vienatnē. Vai jums šeit nepaliek vientuļi?]

"Vientulīgi? Dažreiz. Bet ir atšķirība starp to, būt vienam un būt vientuļam. Mežs ir mana sabiedrība, un reizēm tas ir tieši tas, kas nepieciešams. Zini, dzīvot šeit mani māca saprast pasauli pavisam citādi, kā es to redzēju, kad biju jaunāks."
      #audio:Rainis_1_N_13
    -> continue

+ [Kāpēc izvēlējāties šeit dzīvot tik ilgi?]
    "Ilgs stāsts... Kad biju jauns, es meklēju vietu, kurā varētu apdomāt savas domas. Pilsētas trokšņi mani noveda līdz šai vietai. Pēc kāda laika sapratu, ka esmu atradis mājas. Mežs ir kļuvis par manu dzīves ritmu."
          #audio:Rainis_1_N_14

    -> continue

=== continue ===
+ [...Meža sabiedrība...laikam sapratu]
"Bet nu pietiek par mani. Es redzu, ka esi nogurusi. Pasaule turpinās tev uzspiest savas cerības un viedokļus, es jau arī tagad laikam ielieku savas domas tavā galvā. Atceries, ka neviens nevar tevi veidot tādu, kāda tu neesi, ja vien tu to neļausi."
      #audio:Rainis_1_N_15
      -> DONE

+ [Paldies.]
    "Tu esi laipni aicināta nākt vēl. Tagad ej, un klausies, domā, runā. Tur ir tā dzīves jēga."
          #audio:Rainis_1_N_16
    -> DONE
