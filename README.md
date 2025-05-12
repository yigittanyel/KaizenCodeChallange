# 1. Soru - Campaign Code Generator
Bu proje, hızlı tüketim sektöründeki bir kampanya senaryosunda kullanılmak üzere, ürün ambalajlarına yerleştirilecek 8 karakterlik, eşsiz ve algoritmik olarak doğrulanabilir kampanya kodlarının üretilmesi ve doğrulanmasını amaçlamaktadır.

#### Çözüm: Checksum Tabanlı Algoritma
Kodlar şu mantıkla üretilmektedir:

İlk 7 karakter rastgele karakter kümesinden seçilir.
Bu karakterlerin alfabetik sırasına karşılık gelen indekslerin toplamı alınır.
Toplam % 23 (karakter kümesi uzunluğu) alınarak ortaya çıkan değer,
8.karakter olarak kodun sonuna eklenir (checksum karakteri).

###### Bu yapı sayesinde her kod:
- Tahmin edilmesi güç bir kontrol mekanizmasına sahiptir,
- Üretildiği anda doğrulama kriterini içinde taşır,
- Harici bir veri kaynağına gerek kalmadan stateless şekilde doğrulanabilir.

##### Mimari Yaklaşım
Kaizen.CampaignCodeGenerator
│
├── Models
│   └── CodeConfig        → Kod uzunluğu ve geçerli karakter kümesi gibi ortak sabitleri tutar
│
├── Services
│   ├── Abstract
│   │   ├── ICodeGenerator → Kod üretimi için arayüz (interface)
│   │   └── ICodeValidator → Kod doğrulama için arayüz (interface)
│   │
│   └── Concrete
│       ├── CodeGenerator → Rastgele 7 karakter + 1 checksum ile geçerli kodları üretir
│       └── CodeValidator → Girilen kodun algoritmaya uygun olup olmadığını kontrol eder
│
├── Utils
│   └── RandomProvider    → Tek bir Random instance üzerinden güvenli rastgelelik sağlar
│
└── Program.cs            → Uygulamanın giriş noktası ve konsol menüsü (kod gir, tüm kodları listele vb.)

##### Karakter Kümesi:
ACDEFGHKLMNPRTXYZ234579
→ Bu 23 karakter kullanılır. Her birinin bir alfabetik sırası vardır (0-22 arası).

##### İlk 7 karakter:
Random olarak seçilir (örneğin G, 5, P, ...).

##### Checksum hesaplama:
İlk 7 karakterin alfabetik indeksleri toplanır
Örn: G = 5, 5 = 21, P = 12 → 5 + 21 + 12 + ...
→ Toplam % 23 alınır

##### 8. karakter (doğrulama):
Elde edilen mod 23 sonucu, karakter kümesinden o sıradaki harf olur.

##### Doğrulama (Validate) işlemi:
Kodun ilk 7 karakteri alınıp aynı şekilde toplamı ve mod hesaplanır, 8. karakterle karşılaştırılır. Uyum varsa geçerlidir.

# 2. Soru - Receipt Parser
Bu projede amaç, OCR servisinden gelen her bir fiş görseline ait JSON yanıtını parse ederek, kelimeleri görseldeki yatay hizalarına en yakın olacak şekilde satır bazlı metin çıktısına dönüştürmektir.

OCR servisi, fiş görselini analiz edip her kelime için "description" ve "boundingPoly" (4 köşeden oluşan koordinat verisi) döndürmektedir. Amaç bu kelimeleri:

- Satır bazında gruplayarak
- Soldan sağa sıralayarak

Görseldeki insan gözüyle algılanan doğal sıralamaya mümkün olduğunca yakın bir şekilde tekil metin satırlarına dönüştürmektir.

#### Mimari Yaklaşım
Kaizen.ReceiptParser
│
├── Models
│   ├── OCRWord       → OCR'den gelen kelime nesnesi
│   ├── BoundingBox   → boundingPoly verisi (koordinatlar)
│   ├── Vertex        → x, y nokta verisi
│   └── Line          → Aynı satıra ait kelimeleri temsil eden yapı
│
├── Services
│   └── ReceiptProcessor → OCRWord listesini satırlara dönüştüren mantık
│
└── Utils
    └── JsonLoader        → JSON dosyasını okuyup deserialize eden yardımcı sınıf
    
####  Algoritmanın İşleyişi
##### Veri Hazırlığı (JsonLoader)
Belirtilen JSON dosyası okunur ve List<OCRWord> nesnesine deserialize edilir.

OCRWord modelleri "description" ve "boundingPoly" içeriğine sahiptir.

##### Temizlik ve Filtreleme
description alanı boş olan ya da boundingPoly bilgisi eksik olan kelimeler filtrelenir.

##### Satır Gruplama (ReceiptProcessor)
Her kelimenin en küçük Y koordinatı (yani görseldeki en üst noktası) alınır.

Daha önce oluşturulmuş satırların Y konumuyla kıyaslanarak, LineThreshold toleransı içinde yer alıyorsa o satıra eklenir.

Aksi halde yeni bir Line nesnesi oluşturulur.

Math.Abs(line.Y - wordY) < LineThreshold

Line.Y değeri, satırdaki kelimelerin en küçük Y değeriyle hesaplanır.

Bu tercih, görselde üst üste duran kelimeleri gruplarken daha stabil sonuçlar sağlar.

##### Satır İçi Sıralama (Line.ToString())
Her satır, içindeki kelimeleri GetLeft() yani en küçük X değerine göre soldan sağa sıralar.

Ardından string.Join(" ", ...) ile okunabilir metin haline getirilir.

##### Satır Sıralama
Tüm Line nesneleri, Y değerlerine göre küçükten büyüğe sıralanır. Böylece görselde yukarıdan aşağı doğru bir akış elde edilir.


##### Not: Her iki proje için de ekran görüntüleri screen-shots dosyasında proje bazlı verilmiştir. 
