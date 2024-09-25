"Kaimiņi, vai tad neesam kaimiņi? Labvakar! Ko tu šeit šajā laikā dari? Tevi neviens mājās negaida? Ja skriesi, varbūt paspēsi līdz saulrietam. Vecāki gan jau ka dusmojas... Tu vēl kādu šeit pa ceļam varbūt satiki?"

+ [Nē.]
    "Nu, feini. Apmaldīties te tādā laikā ir vienkārši, labāk skaties, kur kāds atrodas. Tad jau nu grūtāk būs pazust, manuprāt."

    -> ExploreBranch

+ [Jā. (samelo)]
    "Nu, feini. Apmaldīties te tādā laikā ir vienkārši, labāk skaties, kur kāds atrodas. Tad jau nu grūtāk būs pazust, manuprāt. Bet ja paej kādam garām, šaubos, ka ieraudzīsi vēlreiz."

    -> ExploreBranch

=== ExploreBranch ===
"Neturēšu tevi ilgi te, skat, jau saule laižas zemāk. Varbūt kaut kas uz sirds? Tu izskaties tāda pabāla. M?"

+ [Nē, paldies. Viss labi. Es labāk iešu.]
    "Labi. Tad skrien, uz priekšu! Nodod sveicienus vecākiem!"
    -> END

+ [Aizdomājos par lietām.]
    "Pagaidi, dod tikai pateikt vēl pāris vārdus. Ja tomēr satiec kādu pa ceļam, tu apdomā. Domām vienmēr piemīt īpašs spēks. Ne vienmēr sakāmais var patikt, bet no meliem arī attiecības labas neuzbūvēsi. Vienkārši saki to, kas uz sirds, to, kas ir tuvāks tev."

    -> KaiminiBranch

=== KaiminiBranch ===
+ [Labi. Uzredzēšanos. (beigas)]
    "Nu viss, tagad skrien, uz priekšu! Nodod sveicienus vecākiem!"
    -> END

+ [Pagaidiet. Jūs minējāt tos... kaimiņus. Pastāsties vairāk]
    "Ak tad tā. Ko tieši vēlies zināt? (Papildus teksts, jo kad ir maz teksta tad neredz tekstu aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa. JAIZLABO)"

    -> FearOrGoodBranch

=== FearOrGoodBranch ===
+ [Vai man no viņiem jābaidās?]
    "Baidīties? Nē, nē. Dažiem nīst liekas vieglāk nekā mīlēt, bet tur jau arī vecums pie vainas. Jaunieši kļuvuši pavisam garīgi veci arī. Būtu tikai pareizi atgādināt viņiem, ka laiks nestāv uz vietas. Tur celiņš, tev jāiet tikai taisni uz priekšu, un tālāk jābūt kādai gaismai."

    -> GoodLuckBranch

+ [Vai viņi ir slikti vai labi kaimiņi?]
    "Kas ir slikts un kas ir labs? Ļaudis ir vai nu noguruši no vientulības, vai nu nogurst no tā, ka nevar būt vieni. Tu redzēsi, ja atradīsi kādu."

    -> GoodLuckBranch

=== GoodLuckBranch ===
"Nu viss, tagad skrien, uz priekšu! Nodod sveicienus vecākiem!"

+ [Sapratu! Paldies. Un ko Jūs paši? Neiesiet mājās?]
    "Mājās? Es tepat jau netālu dzīvoju. Dažreiz izeju pastaigāties, paelpot svaigu gaisu. Man tā ārsts teica, kā labāk būtu. Tikai vienīgais neviens šeit negrib ar mani spēlēt šahu. Kārtis arī nespēlē. Paliek tikai lasīt un sēdēt ārā."

    -> ChessBranch
    
=== ChessBranch ===
+ [Šahu? Man arī patīk šahs.]
    "Ak, nu tad tu zini, cik daudz šahā atklājas par cilvēku. Tur nav tikai staigāšana pa galdiņu ar figūrām, bet arī domāšana vairākus soļus uz priekšu. Varbūt kādreiz mēs varam uzspēlēt. Lai gan... reti kurš te paliek tik ilgi, lai spēli pabeigtu."

    -> PersonalBranch

+ [Kāpēc šahu? Nav nekādu citu spēļu, kas jums patiktu?]
    "Šahs mani vienmēr piesaistījis. Tur ir stratēģija, pacietība, domas skaidrība. Citas spēles man vairs nav tās pašas — pārāk ātras, pārāk haotiskas. Dzīvē jau tāpat pietiek haosa."
    -> PersonalBranch
    
=== PersonalBranch ===
+ [Jūs minējāt, ka dzīvojat vienatnē. Vai jums šeit nepaliek vientuļi?]
    "Vientulīgi? Dažreiz. Bet ir atšķirība starp to, būt vienam un būt vientuļam. Mežs man sniedz savu sabiedrību, un reizēm tas ir tieši tas, kas nepieciešams. Zini, dzīvot šeit mani māca saprast pasauli pavisam citādi, kā es to redzēju, kad biju jaunāks."

    -> continue

+ [Kāpēc izvēlējāties šeit dzīvot tik ilgi?]
    "Ilgs stāsts... Kad biju jauns, es meklēju vietu, kurā varētu apdomāt savas domas. Pilsētas trokšņi mani noveda līdz šai vietai. Pēc kāda laika sapratu, ka esmu atradis mājas. Mežs ir kļuvis par manu dzīves ritmu."

    -> continue

=== continue ===
"Bet nu pietiek par mani. Es redzu, ka tev ir daudz pārdomu. Pasaule ārpus meža turpinās tev uzspiest savas cerības un viedokļus. Atceries, ka neviens nevar tevi veidot tādu, kāda tu neesi, ja vien tu to neļausi."

+ [Paldies.]
    "Tu esi laipni aicināta. Tagad ej, un domā par to, par ko runājām. Atbildes tu pati atradīsi."
    -> DONE
