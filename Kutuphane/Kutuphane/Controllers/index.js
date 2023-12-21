const express = require("express");
const path = require("path");

const app = express();
const port = 3000;

// Middleware tanımlamaları
app.use(express.urlencoded({ extended: true }));

// Statik dosyaları servis etmek için
const staticPath = path.join(__dirname, "..", "Views"); // ".." dizini bir üst dizini ifade eder
app.use(express.static(staticPath));

// Ana sayfa route'u
app.get("/", (req, res) => {
  const kayitEkraniPath = path.join(staticPath, "Home", "KayitEkrani.html");
  res.sendFile(kayitEkraniPath);
});

// Dinleme
app.listen(port, () => {
  console.log(`Uygulama ${port} portunda çalışıyor`);
});
