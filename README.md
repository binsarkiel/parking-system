# Aplikasi Sistem Parkir Console

## Daftar Isi
- [Deskripsi](#deskripsi)
- [Fitur](#fitur)
- [Kebutuhan Sistem](#kebutuhan-sistem)
- [Cara Menjalankan](#cara-menjalankan)
- [Perintah](#perintah)
- [Contoh Output](#contoh-output)
- [Lisensi](#lisensi)
- [Penulis](#penulis)

---

## Deskripsi
Aplikasi ini adalah **sistem parkir berbasis console** yang dibangun menggunakan **.NET 5**. Aplikasi ini mensimulasikan sistem manajemen parkir, di mana kendaraan (mobil kecil dan motor) dapat diparkir pada slot yang tersedia. Setiap kendaraan mendapatkan slot unik, dan laporan status parkir dapat dihasilkan secara rinci.

---

## Fitur
- Mengalokasikan slot parkir untuk kendaraan (hanya mobil kecil dan motor).
- Membatasi satu slot parkir per kendaraan berdasarkan nomor registrasi.
- Slot diberikan secara otomatis dan sekuensial.
- Kendaraan dapat keluar, sehingga slot kembali tersedia untuk kendaraan lain.
- Menyediakan laporan:
  - Jumlah slot yang terisi dan tersedia.
  - Kendaraan berdasarkan nomor ganjil/genap.
  - Kendaraan berdasarkan jenis (mobil/motor).
  - Kendaraan berdasarkan warna.

---

## Kebutuhan Sistem
- **.NET 5 SDK**
- Sistem operasi yang didukung: Windows, Mac, atau Linux.

---

## Cara Menjalankan
1. Clone repositori ini:
   ```bash
   git clone https://github.com/nama-pengguna/sistem-parkir.git
   cd sistem-parkir
   ```

2. Bangun proyek:
   ```bash
   dotnet build
   ```

3. Jalankan aplikasi:
   ```bash
   dotnet run
   ```

---

## Perintah
Berikut adalah daftar perintah yang dapat digunakan dalam aplikasi sistem parkir:

| Perintah                                     | Deskripsi                                                                 |
|---------------------------------------------|---------------------------------------------------------------------------|
| `create_parking_lot <kapasitas>`            | Membuat tempat parkir dengan kapasitas tertentu.                         |
| `park <nomor_polisi> <warna> <jenis>`       | Memasukkan kendaraan ke dalam slot parkir yang tersedia. Jenis harus `Mobil` atau `Motor`. |
| `leave <nomor_slot>`                        | Mengosongkan slot sehingga dapat digunakan kendaraan lain.               |
| `status`                                    | Menampilkan status saat ini dari semua slot yang terisi.                 |
| `type_of_vehicles <jenis>`                  | Menampilkan jumlah kendaraan berdasarkan jenis (`Mobil` atau `Motor`).   |
| `registration_numbers_for_vehicles_with_ood_plate` | Menampilkan nomor registrasi kendaraan dengan nomor ganjil.              |
| `registration_numbers_for_vehicles_with_event_plate` | Menampilkan nomor registrasi kendaraan dengan nomor genap.               |
| `registration_numbers_for_vehicles_with_colour <warna>` | Menampilkan nomor registrasi kendaraan dengan warna tertentu.            |
| `slot_numbers_for_vehicles_with_colour <warna>` | Menampilkan nomor slot kendaraan dengan warna tertentu.                  |
| `slot_number_for_registration_number <nomor_polisi>` | Menampilkan nomor slot kendaraan berdasarkan nomor polisi tertentu.      |
| `exit`                                      | Keluar dari aplikasi.                                                    |

---

## Contoh Output
Berikut adalah contoh sesi aplikasi sistem parkir:

```plaintext
$ create_parking_lot 6
Created a parking lot with 6 slots

$ park B-1234-XYZ Putih Mobil
Allocated slot number: 1

$ park B-9999-XYZ Putih Motor
Allocated slot number: 2

$ leave 1
Slot number 1 is free.

$ status
Slot    No.Registration    Type        Colour
1       Empty              -           -
2       B-9999-XYZ         Motor       Putih

$ registration_numbers_for_vehicles_with_ood_plate
B-9999-XYZ

$ exit
```

---

## Lisensi
Proyek ini dilisensikan di bawah **[MIT License](LICENSE)**.

---

## Penulis
- **Yehezkiel Hasudungan**
- [GitHub](https://github.com/binsarkiel)
- [Email](mailto:binsarkiel@gmail.com)
