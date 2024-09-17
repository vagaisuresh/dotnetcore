using Microsoft.AspNetCore.Mvc;
using SecureSensitiveData.Models;
using SecureSensitiveData.Services;
using System.Diagnostics;

namespace SecureSensitiveData.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEncryptionService _encryptionService;

        public HomeController(ILogger<HomeController> logger, IEncryptionService encryptionService)
        {
            _logger = logger;
            _encryptionService = encryptionService;
        }

        public string Encryption(string id)
        {
            return _encryptionService.Encrypt(id);
        }

        public string Decryption()
        {
            string id = "qUk0pWll8EWfcizu/13zBLgNyVrCuZZgRhGR4doNWYE=";
            return _encryptionService.Decrypt(id);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
