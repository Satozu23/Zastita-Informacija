# Zastita-Informacija
Aplikacija za automatsko praćenje fajl sistema, enkripciju i bezbednu razmenu datoteka putem TCP soketa. Razvijeno kao projekat u okviru predmeta Zaštita informacija.
Glavne Karakteristike:
-File System Watcher (FSW): Automatska detekcija novih fajlova u definisanom direktorijumu.
Implementirana Kriptografija:
-Railfence Cipher: Transpoziciona šifra.
-XXTEA (Corrected Block TEA): Blokovska šifra visokih performansi.
-CBC (Cipher Block Chaining): Mod rada za ulančavanje blokova kod XXTEA algoritma.
-Integritet Podataka: Implementiran Tiger Hash algoritam (192-bitni heš) za verifikaciju datoteka nakon dekripcije.
-Mrežna Komunikacija: TCP klijent-server arhitektura za razmenu enkriptovanih paketa.
-Metadata Header: JSON formatirano zaglavlje unutar .enc fajlova (ime fajla, originalna veličina, heš, algoritam).
Tehnologije:
-Jezik: C#
-Platforma: .NET Windows Forms
-Komunikacija: System.Net.Sockets (TCP)
-Algoritmi: Ručno implementirani (bez korišćenja eksternih kripto biblioteka)
Struktura Paketa (.enc):
Sistem generiše binarni paket sledeće strukture:
-Header Length (4 bajta): Dužina JSON metapodataka.
-Metadata (JSON): Informacije o originalnom fajlu.
-Encrypted Content: Sadržaj fajla zaštićen izabranim algoritmom.
