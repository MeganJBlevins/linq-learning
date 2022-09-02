using System.Text.Json.Serialization;
using System.Xml.Linq;
using LinqLearning.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinqLearning.Pages;


public class OwnerData
{
    public int OwnerId { get; set; }
    public string OwnerName { get; set; }
    public string Address { get; set; }
    public string HomeColor { get; set; }
}
public class Join : PageModel
{
    public string xml = @"<Homes>
            <Home OwnerId='1' Address='Ozark, MO' HouseColor='red' />
    <Home OwnerId='2' Address='Springfield Somewhere' HouseColor='blue' />
    <Home OwnerId='3' Address='Springfield Lake' HouseColor='purple' />
    <Home OwnerId='4' Address='Willard' HouseColor='orange' />
    </Homes>";
    public XDocument Homes { get; set; }
    
    public IList<OwnerData> FullList { get; set; }
    public SqlService _sqlService { get; set; }

    public Join(SqlService sqlService)
    {
        _sqlService = sqlService;
    }
    
    public void OnGet()
    {
        Homes = XDocument.Parse(xml);
        FullList = getFullList();
    }

    public IList<OwnerData> getFullList()
    {
        IList<Owner> allOwners = _sqlService.getAllOwners();
        IEnumerable<Home> homes = (from _home in Homes.Element("Homes").Elements("Home")
            select new Home()
            {
                OwnerId = Int32.Parse(_home.Attribute("OwnerId").Value),
                HouseColor = _home.Attribute("HouseColor").Value,
                Address = _home.Attribute("Address").Value
            });

        IEnumerable<OwnerData> result = from owner in allOwners
            join house in homes on owner.Id equals house.OwnerId
            select new OwnerData
            {
                OwnerId = owner.Id,
                OwnerName = owner.Name,
                Address = house.Address,
                HomeColor = house.HouseColor,
            };
        
        return result.ToList();

    }

}