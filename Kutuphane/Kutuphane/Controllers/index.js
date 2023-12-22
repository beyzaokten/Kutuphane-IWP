const express = require("express");
const path = require("path");

const app = express();
const port = 3000;

// Statik dosyaları servis etmek için
const staticPath = path.join(__dirname, "..", "Views");
app.use(express.static(staticPath));

// Giriş Ekranı route'u
app.get("/giris", (req, res) => {
  const girisEkraniPath = path.join(staticPath, "GirisEkrani.html");
  res.sendFile(girisEkraniPath);
});

// Ana Sayfa route'u
app.get("/anasayfa", (req, res) => {
  const anasayfaPath = path.join(staticPath, "Anasayfa.html");
  res.sendFile(anasayfaPath);
});

// Dinleme
app.listen(port, () => {
  console.log(`Uygulama ${port} portunda çalışıyor`);
});
