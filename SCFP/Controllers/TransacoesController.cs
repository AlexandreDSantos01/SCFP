using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SCFP.Data;
using SCFP.Models;

namespace SCFP.Controllers
{
    [Authorize]
    public class TransacoesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public TransacoesController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var transacoes = await _context.Transacoes
                .Include(t => t.Categoria)
                .Where(t => t.UsuarioId == userId)
                .ToListAsync();

            return View(transacoes);
        }

        public IActionResult Create()
        {
            var userId = _userManager.GetUserId(User);
            ViewBag.Categorias = _context.Categorias.Where(c => c.UsuarioId == userId).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Transacao transacao)
        {
            var userId = _userManager.GetUserId(User);
            transacao.UsuarioId = userId;

            if (ModelState.IsValid)
            {
                _context.Add(transacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Recarregar categorias caso de erro na validação
            ViewBag.Categorias = _context.Categorias
                .Where(c => c.UsuarioId == userId)
                .ToList();

            return View(transacao);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var userId = _userManager.GetUserId(User);
            var transacao = await _context.Transacoes
                .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == userId);

            if (transacao == null) return NotFound();

            ViewBag.Categorias = _context.Categorias.Where(c => c.UsuarioId == userId).ToList();
            return View(transacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Transacao transacao)
        {
            if (id != transacao.Id) return NotFound();

            var userId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                var transacaoExistente = await _context.Transacoes
                    .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == userId);

                if (transacaoExistente == null) return NotFound();

                transacaoExistente.Descricao = transacao.Descricao;
                transacaoExistente.Valor = transacao.Valor;
                transacaoExistente.Data = transacao.Data;
                transacaoExistente.Tipo = transacao.Tipo;
                transacaoExistente.CategoriaId = transacao.CategoriaId;

                _context.Update(transacaoExistente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categorias = _context.Categorias.Where(c => c.UsuarioId == userId).ToList();
            return View(transacao);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User);
            var transacao = await _context.Transacoes
                .Include(t => t.Categoria)
                .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == userId);

            if (transacao == null) return NotFound();

            return View(transacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);
            var transacao = await _context.Transacoes
                .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == userId);

            if (transacao != null)
            {
                _context.Transacoes.Remove(transacao);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var userId = _userManager.GetUserId(User);
            var transacao = await _context.Transacoes
                .Include(t => t.Categoria)
                .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == userId);

            if (transacao == null) return NotFound();

            return View(transacao);
        }

        public async Task<IActionResult> ResumoMensal()
        {
            var userId = _userManager.GetUserId(User);
            var hoje = DateTime.Today;
            var inicioMes = new DateTime(hoje.Year, hoje.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);

            var transacoes = await _context.Transacoes
                .Include(t => t.Categoria)
                .Where(t => t.UsuarioId == userId && t.Data >= inicioMes && t.Data <= fimMes)
                .ToListAsync();

            var totalEntradas = transacoes
                .Where(t => t.Tipo == TipoTransacao.Entrada)
                .Sum(t => t.Valor);

            var totalSaidas = transacoes
                .Where(t => t.Tipo == TipoTransacao.Saida)
                .Sum(t => t.Valor);

            ViewBag.TotalEntradas = totalEntradas;
            ViewBag.TotalSaidas = totalSaidas;

            return View(transacoes);
        }
    }
}
