using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class ChatController : Controller
{
    private readonly ChatGptService _chatGptService;

    public ChatController(ChatGptService chatGptService)
    {
        _chatGptService = chatGptService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(string prompt)
    {
        if (string.IsNullOrEmpty(prompt))
        {
            ViewBag.Response = "Please enter a prompt.";
            return View();
        }

        var response = await _chatGptService.GetChatGptResponse(prompt);
        ViewBag.Response = response;
        return View();
    }
}
