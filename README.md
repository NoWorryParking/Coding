# NoWorryParking 
## _Aplicatie android pentru rezervarea parcarii_

Vrei sa uiti de cautarea frenetica a parcarii in captiala sau de stresul de a pleca cu o ora inainte doar pentru a cauta parcare? Atunci **NoWorryParking** e pentru tine.

## Membrii echipei
* [Anghelache Vlad Alexandru](https://github.com/vladanghelache)
* [Iordache Denisa-Elena](https://github.com/denisaiordache)
* [Iuga Paula](https://github.com/iuga-paula)
* [Nedelcu Radu-Andrei](https://github.com/NedelcuRadu)

## [Demo](https://drive.google.com/file/d/117l5bA6Nf0wC51igSB-6kePkw7okigHM/view?usp=sharing)

## User stories
_Ca un ... as vrea ..., ca sa pot/ pentru a putea..._
1. Ca un turist, as vrea  sa stiu care e cel mai apropiat loc de parcare fata de destinatia mea, pentru a putea vizita cat mai multe obiective intr-un timp cat mai scurt.
2. Ca o persoana din campul muncii, as vrea sa ma asigur ca o sa gasesc o parcare in timp util, pentru a nu intarzia la slujba sau la intalniri.
3. Ca un introvertit, as vrea sa nu fie nevoie sa interactionez cu lumea atunci cand caut un loc de parcare pentru a nu iesi din zona de confort.
4. Ca o persoana cu un venit redus, as vrea sa stiu care este cea mai convenabila parcare  pentru a nu imi depasi bugetul.
5. Ca o persoana foarte activa, as vrea sa reduc timpul necesar platii locului de parcare, pentru a-mi eficientiza timpul.
6. Fiind o persoană ce nu obisnuieste sa achite cash in general, aș vrea să pot efectua plata cu cardul atunci când caut un loc de parcare.
7. Ca un/o student/a, as vrea sa imi pot rezeva un loc de parcare in acelasi loc sau in aceeasi zona, pentru a nu intarzia la cursuri.
8. Fiind o persoană nevaccinată, aș vrea să pot plăti fără a pune mâna pe bancnote / interacționa cu oamenii pentru a reduce riscul de răspândire a COVID-19.
9. Ca un sofer fara experienta, as vrea sa gasesc o parcare intr-o zona neaglomerata pentru a putea efectua manevre in liniste.
10. Ca un proprietar de parcare privata, as vrea sa imi promovez parcarea, pentru a atrage mai mult clienti. 
<!--11. Ca un utilizator comun, as vrea sa fiu anuntat daca parcarea pe care am rezervat-o e ocupata de altcineva, pentru a putea alege alta in timp util.-->

## Descriere
Un utilizator obisnuit, odata ce aceeaza aplicatia poate vedea o harta a Bucurestiului de unde poate selecta zone in care doreste sa rezerve loc de parcare.
Aplicatia rezerva loc de parcare doar in anumite zone de parcare publica/privata cu plata.
O data selectata o zona de pe harta, 
  - daca exista parcare in acea zona, va trebui sa selecteze o ora si o data in care vrea sa ii rezerve parcarea. Poate sa vada total locuri de parcare si cate locuri de parcare sunt disponibile. 
  - daca nu exista, aplicatia ii va genera o parcare intr-o zona aflata in aproximitatea celei cautate.
O data ales un numar de loc de parcari utilizatorul le poate rezerva pentru un anumit intrerval de timp (mimim o ora, maxim o zi) si le poate si plati.
Utilizatorul isi poate vedea un istoric al locurilor rezervate anterior si poate vedea locația unei foste comenzi din istoric. Poate vedea și o secțiune cu parcările rezervate active - pe care trebuie să le onoreze în viitor.



## Tehnologii
Aplicatia Android este creata in **Unity** utilizand:
* C#
* Unity SDK (pt. harti)
* Unity IAP (pt. plati)
* FireBase


## Wireframe
Un model al aplicației și câteva detalii de implementare se pot găsi [aici](https://app.moqups.com/TB9tngLAkw/view)

## Diagrama Conceptuală
![image](https://user-images.githubusercontent.com/61518083/121806338-19626800-cc58-11eb-950e-e1dc76f2740e.png)

## Diagrama UML de stări
![bd](https://github.com/NoWorryParking/Coding/blob/main/NoWorryPStateDiagram.png)

## Cum se importa proiect nou
```
D:cale_folder\Coding-main\Assets\Scenes
Welcome => pagina de start
```

## Cum se pune sdk pe mobil de pe Unity
* Usb trb sa fie conectat cu Developer Mode
* Telefonul trb sa fie cu usb conectat pentru transfer files
* Sa fie setat Android in File > Build Settings , altfel click pe Android si switch platform
```
File > Build and Run

```
