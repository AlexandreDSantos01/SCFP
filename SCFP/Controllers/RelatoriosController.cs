using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCFP.Data;
using SCFP.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
public class RelatoriosController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<Usuario> _userManager;

    public RelatoriosController(ApplicationDbContext context, UserManager<Usuario> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index(int? mes, DateTime? dataInicio, DateTime? dataFim)
    {
        var user = await _userManager.GetUserAsync(User);
        var userId = user?.Id;

        // Começa com todas as transações do usuário
        var query = _context.Transacoes
            .Include(t => t.Categoria)
            .Where(t => t.UsuarioId == userId);

        // Filtro por mês
        if (mes.HasValue)
        {
            query = query.Where(t => t.Data.Month == mes.Value);
        }

        // Filtro por intervalo de datas
        if (dataInicio.HasValue && dataFim.HasValue)
        {
            query = query.Where(t => t.Data >= dataInicio.Value && t.Data <= dataFim.Value);
        }

        var transacoes = await query.ToListAsync();

        // Log (opcional)
        Console.WriteLine($"Total de transações filtradas: {transacoes.Count}");
        foreach (var t in transacoes)
        {
            Console.WriteLine($"{t.Data:dd/MM/yyyy} - {t.Descricao} - {t.Tipo} - R${t.Valor}");
        }

        // Cálculo dos totais
        var totalEntradas = transacoes
            .Where(t => t.Tipo == TipoTransacao.Entrada)
            .Sum(t => t.Valor);

        var totalSaidas = transacoes
            .Where(t => t.Tipo == TipoTransacao.Saida)
            .Sum(t => t.Valor);

        // Agrupamento por categoria (somente saídas)
        var agrupadoPorCategoria = transacoes
            .Where(t => t.Tipo == TipoTransacao.Saida)
            .GroupBy(t => t.Categoria.Nome)
            .Select(g => new
            {
                Categoria = g.Key,
                Total = g.Sum(t => t.Valor)
            })
            .ToList();

        // Envia para a View
        ViewBag.TotalEntradas = totalEntradas;
        ViewBag.TotalSaidas = totalSaidas;
        ViewBag.DadosCategoria = agrupadoPorCategoria;
        ViewBag.NomeUsuario = user?.Nome ?? user?.UserName ?? "Usuário";

        return View();
    }
}
