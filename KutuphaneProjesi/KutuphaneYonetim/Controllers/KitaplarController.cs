using Microsoft.AspNetCore.Mvc;
using KutuphaneYonetim.Data;
using KutuphaneYonetim.Models;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneYonetim.Controllers
{
    public class KitaplarController : Controller
    {
        private readonly UygulamaDbContext _context;

        public KitaplarController(UygulamaDbContext context)
        {
            _context = context;
        }

        // 1. VATANDAŞ İÇİN LİSTELEME VE ARAMA (Index)
        public async Task<IActionResult> Index(string arananKelime)
        {
            var kitaplar = from k in _context.Kitaplar
                           select k;

            if (!string.IsNullOrEmpty(arananKelime))
            {
                // Kitabın Adında veya Yazarında arama yapar
                kitaplar = kitaplar.Where(s => s.Ad.Contains(arananKelime) || s.Yazar.Contains(arananKelime));
            }

            return View(await kitaplar.ToListAsync());
        }

        // 2. YÖNETİM PANELİ (Yonetim)
        // Bu metod sayesinde "Yönetim Paneli" linki çalışır.
        public async Task<IActionResult> Yonetim()
        {
            return View(await _context.Kitaplar.ToListAsync());
        }

        // 3. EKLEME SAYFASI (Ekle)
        public IActionResult Ekle()
        {
            return View();
        }

        // EKLEME İŞLEMİ (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ekle([Bind("Id,Ad,Yazar,SayfaSayisi")] Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kitap);
                await _context.SaveChangesAsync();
                // İşlem bitince Yönetim paneline geri dön
                return RedirectToAction(nameof(Yonetim)); 
            }
            return View(kitap);
        }

        // 4. GÜNCELLEME SAYFASI (Guncelle)
        public async Task<IActionResult> Guncelle(int? id)
        {
            if (id == null) return NotFound();

            var kitap = await _context.Kitaplar.FindAsync(id);
            if (kitap == null) return NotFound();
            
            return View(kitap);
        }

        // GÜNCELLEME İŞLEMİ (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guncelle(int id, [Bind("Id,Ad,Yazar,SayfaSayisi")] Kitap kitap)
        {
            if (id != kitap.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kitap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Kitaplar.Any(e => e.Id == id)) return NotFound();
                    else throw;
                }
                // İşlem bitince Yönetim paneline geri dön
                return RedirectToAction(nameof(Yonetim));
            }
            return View(kitap);
        }

        // 5. SİLME İŞLEMİ (Sil)
        public async Task<IActionResult> Sil(int? id)
        {
            if (id == null) return NotFound();

            var kitap = await _context.Kitaplar.FindAsync(id);
            if (kitap != null)
            {
                _context.Kitaplar.Remove(kitap);
                await _context.SaveChangesAsync();
            }
            
            // Silme bitince Yönetim paneline geri dön
            return RedirectToAction(nameof(Yonetim));
        }
    }
}