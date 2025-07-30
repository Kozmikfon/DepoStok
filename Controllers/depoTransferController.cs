using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DepoStok.Data;
using DepoStok.Models.Entities;

namespace DepoStok.Controllers
{
    public class depoTransferController : Controller
    {
        private readonly StokDbContext _context;

        public depoTransferController(StokDbContext context)
        {
            _context = context;
        }

        // GET: DepoTransfer
        public async Task<IActionResult> Index()
        {
            var transfers = await _context.depoTransferleri
                .Include(t => t.kaynakDepo)
                .Include(t => t.hedefDepo)
                .ToListAsync();

            return View(transfers);
        }

        // GET: DepoTransfer/Create
        public IActionResult Create()
        {
            ViewBag.kaynakDepoId = new SelectList(_context.depolar.ToList(), "depoId", "depoAd");
            ViewBag.hedefDepoId = new SelectList(_context.depolar.ToList(), "depoId", "depoAd");

            return View();
        }


        // POST: DepoTransfer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(depoTransfer transfer)
        {
            if (ModelState.IsValid)
            {
                _context.depoTransferleri.Add(transfer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = transfer.transferId });
            }

            // ❗ Ekle: ViewBag'ler burada tekrar set edilmeli
            ViewBag.kaynakDepoId = new SelectList(_context.depolar, "depoId", "depoAd", transfer.kaynakDepoId);
            ViewBag.hedefDepoId = new SelectList(_context.depolar, "depoId", "depoAd", transfer.hedefDepoId);

            return View(transfer);
        }


        // GET: DepoTransfer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var transfer = await _context.depoTransferleri
                .Include(t => t.kaynakDepo)
                .Include(t => t.hedefDepo)
                .Include(t => t.depoTransferDetaylari)
                    .ThenInclude(d => d.malzeme)
                .FirstOrDefaultAsync(t => t.transferId == id);

            if (transfer == null) return NotFound();

            return View(transfer);
        }
    }
}
