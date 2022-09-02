using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinqLearning.Pages;

public class LearningsModel : PageModel
{
    private readonly ILogger<LearningsModel> _logger;

    public LearningsModel(ILogger<LearningsModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

