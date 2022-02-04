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
    public IActionResult Signup(string returnUrl) 
        => View(new RegisterViewModel() { ReturnUrl = returnUrl ?? string.Empty });

    [HttpPost]
    public async Task<IActionResult> Signup(RegisterViewModel model)
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
        await _userManager.AddToRoleAsync(user, "student");

        return LocalRedirect($"/account/signin?returnUrl={model.ReturnUrl}");
    }

    [HttpGet]
    public IActionResult Signin(string returnUrl) 
        => View(new LoginViewModel() { ReturnUrl = returnUrl ?? string.Empty });

    [HttpPost]
    public async Task<IActionResult> Signin(LoginViewModel model)
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

    [HttpPost]
    public async Task<IActionResult> Signout()
    {
        if(_signinManager.IsSignedIn(User))
        {
            await _signinManager.SignOutAsync();

        }
        return LocalRedirect("/");
    }

    [HttpGet]
    public IActionResult AccessDenied(string returnUrl)
    {
        return View(new { ReturnUrl = returnUrl });
    }
}