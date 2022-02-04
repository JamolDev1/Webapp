using invoice.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace webapp;

public class AccountController : Controller
{
   private readonly ILogger<AccountController> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signinManager;

    public AccountController(
        ILogger<AccountController> logger,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signinManager)
    {
        _userManager = userManager;
        _signinManager = signinManager;
        _logger = logger;
    }
    [HttpGet]
    public IActionResult Register(string returnUrl) 
        => View(new RegisterViewModel() { ReturnUrl = returnUrl ?? string.Empty });

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if(!ModelState.IsValid)
        {
            return View(model);
        }

        if(await _userManager.Users.AnyAsync(u => u.Email == model.Email))
        {
            ModelState.AddModelError("Email", "Email already exists.");
            return View(model);
        }

        if(await _userManager.Users.AnyAsync(u => u.PhoneNumber == model.Phone))
        {
            ModelState.AddModelError("Phone", "Phone already exists.");
            return View(model);
        }

        var user = new AppUser()
        {
            Fullname = model.Fullname,
            Email = model.Email,
            PhoneNumber = model.Phone,
            UserName = model.Email.Substring(0, model.Email.IndexOf('@'))
        };

        await _userManager.CreateAsync(user, model.Password);

        return LocalRedirect($"/account/login?returnUrl={model.ReturnUrl}");
    }

    [HttpGet]
    public IActionResult Login(string returnUrl) 
        => View(new LoginViewModel() { ReturnUrl = returnUrl ?? string.Empty });

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if(!ModelState.IsValid)
        {
            return View(model);
        }

        var user = _userManager.Users.FirstOrDefault(u => u.Email == model.Email);
        if(user == default)
        {
            ModelState.AddModelError("Password", "Email yoki parol noto'g'ri kiritilgan.");
            return View(model);
        }

        var result = await _signinManager.PasswordSignInAsync(user, model.Password, false, false);
        if(result.Succeeded)
        {
            return LocalRedirect(model.ReturnUrl ?? "/");
        }

        return BadRequest(result.IsNotAllowed);
    }

}