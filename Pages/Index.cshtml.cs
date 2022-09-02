using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LinqLearning.Services;

namespace LinqLearning.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly SqlService _sqlService;
    public IList<Dog> AllDogs { get; set; }
    public IList<Owner> AllOwners { get; set; }

    public IList<Dog> SortedDogs { get; set; }

    public IEnumerable<string> Names { get; set; }

    public Double AverageDogAge { get; set; }

    public IList<Dog> ThreeDogs { get; set; }
    
    public IList<Dog> FilterResults { get; set; }

    public IEnumerable<int> Numbers { get; set; }
    
    public IList<Dog> NewDogList { get; set; }
    public IList<Dog> DifferentDogs { get; set; }
    
    public IndexModel(ILogger<IndexModel> logger, SqlService sqlService)
    {
        _logger = logger;
        _sqlService = sqlService;
    }

    public void OnGet()
    {
        AllDogs = _sqlService.getAllDogs();
        AllOwners = _sqlService.getAllOwners();
        SortedDogs = AllDogs.OrderByDescending(d => d.Age).ToList();
        Names = AllOwners.Select(owner => owner.Name).Concat(AllDogs.Select(dog => dog.Name));
        AverageDogAge = AllDogs.Select(d => d.Age).Average();
        ThreeDogs = AllDogs.OrderByDescending(d => d.Age).Take(3).ToList();
        Numbers = Enumerable.Range(1, 5);
        NewDogList = AllDogs.Where(d => d.Age != 1).ToList();

    }

    public IList<Dog> getDogsFromOwner(int ownerId) {
        return AllDogs.Where(d => d.OwnerId == ownerId).ToList();
    }
}
