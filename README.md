# tpj_power2

#### Final project for the exam "Teorija Progamskih Jezika" at my university. Small desktop app which should minimize the effort needed for tedious task of generating periodic reports with images from a number of different but constant sources.

##IDEA  
Izvestaji mogu da budu periodicni ili zakazani( ili odmah).  
Navodi se niz standardnih opcija(naslov, tema, font itd mada je moguce da se postave i podrazumevani(jedna firma recimo stalno koristi jedne te iste))  
Izvestaji su podeljeni u sekcije.  
Za svaku sekciju navodi se naslov(ako ne ostaje prazan) i jedan ili vise source-va podataka.  
Source moze da bude folder sa slikama, ili path do tabele u bazi sa cijim podacima se kreira neki chart(opciono) ili se sama tabela prikazuje.  
Svakih n elemenata(slika ili chart-ova) dobija poseban slajd, naslov ostaje prazan, u slucaju da je n = 1 onda je naslov naziv slike.  
Na kraju sam izvestaj se moze da posalje mejlom i/ili storuje u posebnu bazu sa svim izvestajima.  
Sve sto je ostalo je da neko prodje kroz slajdove i dokuca potrebna posebna objasnjenja(koja se mahom ne dodaju).  

##TODO  
  - [ ] __SETUP__  
    - [x] Git  
    - [ ] Add Microsoft Server databases for later testing  
    - [x] Add VisualStudio SSDT  
    - [x] Test connecting and basic operations  
  - [ ] __INTERFACE__  
    - [x] Interface organization  
    - [x] Initial interface  
    - [x] Dinamically add interface components
    - [ ] Beautify  
  - [ ] __BACKEND__  
    - [x] Dynamically add interface components  
    - [ ] Add event schedulers  
    - [x] Add powerpoint functionality  
    - [x] Add whole loop for images usecase  
    - [x] Test images usecase  
    - [ ] Figure out all needed parameters for chart component  
    - [ ] Define standard representations of datasets
    - [ ] Add whole loop for chart usecase
    - [ ] Test chart usecase
