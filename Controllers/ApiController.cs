using Microsoft.AspNetCore.Mvc;
using LinqLearning.Services;

namespace LinqLearning.Controllers
{
        public class ApiController : Controller
    {

        private readonly SqlService _sqlService;
        public ApiController(SqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public class OwnerIdJSON {
            public int ownerId;
        }

        [HttpPost]
        [Route("api/filterDogsByOwner/{id}")]
        public IActionResult filterDogsByOwner(int id)
        {
            IList<Dog> allDogs = _sqlService.getAllDogs();
            IList<Dog> filteredDogs = allDogs.Where(d => d.OwnerId == id).ToList();
            return Json(filteredDogs);
        }


        [HttpPost]
        [Route("api/getOwnerByDog/{id}")]
        public IActionResult getOwnerByDog(int id)
        {
            IList<Dog> allDogs = _sqlService.getAllDogs();
            IList<Owner> allOwners = _sqlService.getAllOwners();
            var query = allOwners.Join(allDogs,
                owner => owner.Id,
                dog => dog.OwnerId,
                (owner, dog) => new {
                    Owner = owner, 
                    Dog = dog
                }).Where(ownerAndDogs => ownerAndDogs.Dog.Id == id).ToList();

            return Json(query);
        }

        [HttpPost]
        [Route("api/getDogNamesAndTypes/{id}")]
        public IActionResult getDogNamesAndTypes(int id)
        {
            IList<Dog> allDogs = _sqlService.getAllDogs();
            IList<Owner> allOwners = _sqlService.getAllOwners();

            var query = allOwners.Join(allDogs,
                owner => owner.Id,
                dog => dog.OwnerId,
                (owner, dog) => new {
                    Owner = owner, 
                    Dog = dog
                }).Where(ownerAndDogs => ownerAndDogs.Owner.Id == id).Select(p => new { DogName = p.Dog.Name, DogType = p.Dog.Type});

            return Json(query); 
        }

        [HttpPost]
        [Route("api/groupByParam/{param}")]
        public IActionResult groupDogsByParam(string param)
        {
            IList<Dog> allDogs = _sqlService.getAllDogs();

            var results = allDogs.GroupBy(
                p => p.Size,
                p => p.Name, 
                (key, g) => new { Key = key, Dogs = g.ToList() });
            // var results = from p in allDogs
            //     group p.Name by p.Size into g
            //     select new { Age = g.Key, Dogs = g.ToList() };
            if(param == "Age") {
                results = allDogs.GroupBy(
                    p => p.Age,
                    p => p.Name, 
                    (key, g) => new { Key = key.ToString(), Dogs = g.ToList() });
                // results = from p in allDogs
                //     group p.Name by p.Age into g
                //     select new { Age = g.Key.ToString(), Dogs = g.ToList() };
                return Json(results);
            }
            
            return Json(results);
        }
        [HttpPost]
        [Route("api/doesDogHaveAge/{age}")]
        public IActionResult doesDogHaveAge(int age)
        {
            IList<Dog> allDogs = _sqlService.getAllDogs();

            var dogsWithAge = allDogs.Any(d => d.Age.ToString() == age.ToString());
            
            return Json(dogsWithAge);
        }
    }
}