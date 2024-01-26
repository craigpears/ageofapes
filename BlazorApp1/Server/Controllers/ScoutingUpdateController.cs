using BlazorApp1.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScoutingUpdateController : ControllerBase
{
    private readonly ILogger<ScoutingUpdateController> _logger;

    public ScoutingUpdateController(ILogger<ScoutingUpdateController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public ActionResult Post(ScoutingEvent update)
    {
        // TODO: make output folder a shared constant
        var outputFolder = @"\\nas-pears\documents\AgeOfApes\ScoutingScreenshots\FilesToProcess\Output";
        System.IO.File.AppendAllText($"{outputFolder}\\{DateTime.Now.ToString("ddMMyyyy")}_clear.txt", update.ToOutputLine());
        return Ok();
    }
}