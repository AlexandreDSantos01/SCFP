using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SCFP.Models;
using SCFP.Services;
using SCFP.ViewModels;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SCFP.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly EmailSender _emailSender;

        public AccountController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, EmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        // GET: /Account/Registrar
        [HttpGet]
        public IActionResult Registrar() => View();

        // POST: /Account/Registrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(RegistroViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new Usuario
            {
                UserName = model.Email,
                Email = model.Email,
                Nome = model.Nome
            };

            var result = await _userManager.CreateAsync(user, model.Senha);

            if (result.Succeeded)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, true);
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Login inválido. Verifique o e-mail e a senha.");
                return View(model);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Senha, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Login inválido. Verifique o e-mail e a senha.");
                return View(model);
            }

            if (await _userManager.GetTwoFactorEnabledAsync(user))
            {
                var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                await _emailSender.SendEmailAsync(user.Email, "Código de Verificação", $"Seu código de verificação é: {token}");

                TempData["UserId"] = user.Id;
                TempData["ReturnUrl"] = returnUrl;

                return RedirectToAction("Verify2FA");
            }

            await _signInManager.SignInAsync(user, isPersistent: model.LembrarMe);
            return RedirectToLocal(returnUrl);
        }

        // GET: /Account/Verify2FA
        [HttpGet]
        public IActionResult Verify2FA()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        // POST: /Account/Verify2FA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Verify2FA(string code)
        {
            var userId = TempData["UserId"]?.ToString();
            var returnUrl = TempData["ReturnUrl"]?.ToString() ?? "/";

            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login");

            var user = await _userManager.FindByIdAsync(userId);
            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", code);

            if (!isValid)
            {
                ModelState.AddModelError(string.Empty, "Código inválido");
                return View();
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            return Redirect(returnUrl);
        }

        // POST: /Account/ResendCode
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResendCode()
        {
            var userId = TempData["UserId"]?.ToString();
            var returnUrl = TempData["ReturnUrl"]?.ToString() ?? "/";

            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return RedirectToAction("Login");

            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
            await _emailSender.SendEmailAsync(user.Email, "Código de Verificação", $"Seu código de verificação é: {token}");

            TempData["UserId"] = user.Id;
            TempData["ReturnUrl"] = returnUrl;
            TempData["Message"] = "Código reenviado com sucesso! Verifique seu e-mail.";

            return RedirectToAction("Verify2FA");
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            return Url.IsLocalUrl(returnUrl)
                ? Redirect(returnUrl)
                : RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Perfil()
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null) return NotFound();

            var model = new PerfilViewModel
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                FotoPerfilUrl = string.IsNullOrEmpty(usuario.FotoPerfil) ? "/images/default-profile.png" : $"/uploads/{usuario.FotoPerfil}"
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Perfil(PerfilViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null) return NotFound();

            usuario.Nome = model.Nome;

            // Validar e salvar arquivo se enviado
            if (model.FotoPerfilArquivo != null && model.FotoPerfilArquivo.Length > 0)
            {
                var extensoesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extensaoArquivo = Path.GetExtension(model.FotoPerfilArquivo.FileName).ToLowerInvariant();

                if (!extensoesPermitidas.Contains(extensaoArquivo))
                {
                    ModelState.AddModelError("FotoPerfilArquivo", "Formato inválido. Use jpg, jpeg, png ou gif.");
                    return View(model);
                }

                const long tamanhoMaximo = 2 * 1024 * 1024;
                if (model.FotoPerfilArquivo.Length > tamanhoMaximo)
                {
                    ModelState.AddModelError("FotoPerfilArquivo", "Arquivo muito grande. Máximo 2MB.");
                    return View(model);
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = $"{usuario.Id}_{Path.GetFileName(model.FotoPerfilArquivo.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.FotoPerfilArquivo.CopyToAsync(stream);
                }

                usuario.FotoPerfil = uniqueFileName;
            }

            await _userManager.UpdateAsync(usuario);

            // Atualizar senha (mesma lógica anterior)
            if (!string.IsNullOrWhiteSpace(model.NovaSenha) || !string.IsNullOrWhiteSpace(model.ConfirmarNovaSenha))
            {
                if (string.IsNullOrWhiteSpace(model.NovaSenha))
                {
                    ModelState.AddModelError("NovaSenha", "A nova senha não pode estar vazia.");
                    return View(model);
                }

                if (string.IsNullOrWhiteSpace(model.ConfirmarNovaSenha))
                {
                    ModelState.AddModelError("ConfirmarNovaSenha", "Confirme a nova senha.");
                    return View(model);
                }

                if (model.NovaSenha != model.ConfirmarNovaSenha)
                {
                    ModelState.AddModelError("ConfirmarNovaSenha", "As senhas não coincidem.");
                    return View(model);
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                var result = await _userManager.ResetPasswordAsync(usuario, token, model.NovaSenha);
                if (!result.Succeeded)
                {
                    foreach (var erro in result.Errors)
                        ModelState.AddModelError(string.Empty, erro.Description);
                    return View(model);
                }
            }

            ViewData["Mensagem"] = "Perfil atualizado com sucesso!";
            return View(model);
        }
    }
}
