using Microsoft.AspNetCore.Mvc;

namespace webapp;

public class AccountController : Controller
{
   private readonly ILogger<AccountController> _logger;

    public AccountController(
        ILogger<AccountController> logger)
    {
        _logger = logger;
    }
    [HttpGet]
    public IActionResult Register(string returnUrl) 
        => View(new RegisterViewModel() { ReturnUrl = returnUrl ?? string.Empty });

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        return LocalRedirect($"/account/Login?returnUrl={model.ReturnUrl}");
    }

    [HttpGet]
    public IActionResult Login(string returnUrl) 
        => View(new LoginViewModel() { ReturnUrl = returnUrl ?? string.Empty });

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        return LocalRedirect("/");
    }
}