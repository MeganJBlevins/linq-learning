using Microsoft.Data.Sqlite;

namespace LinqLearning.Services
{
    public class SqlService
    {
        public SqlService()
        {
             using (var connection = new SqliteConnection("Data Source=learning.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    CREATE TABLE owners (
                        id INTEGER PRIMARY KEY,
                        name TEXT NOT NULL
                    );
                   CREATE TABLE dogs (
                    id INTEGER PRIMARY KEY,
                    name TEXT NOT NULL,
                    type TEXT NOT NULL,
                    owner_id INTEGER,
                    PRIMARY KEY (id),
                    FOREIGN KEY (owner_id) 
                        REFERENCES owners (id) 
                            ON DELETE CASCADE 
                            ON UPDATE NO ACTION,
                    );
                ";
            }

        }

        public IList<Owner> getAllOwners() {
            return new List<Owner>() { 
            new Owner() { Id = 1, Name = "Megan" },
            new Owner() { Id = 2, Name = "Tom" },
            new Owner() { Id = 3, Name = "Myke" },
            new Owner() { Id = 4, Name = "David" }
            };
        }

        public IList<Dog> getAllDogs() {
            Console.WriteLine("getting all dogs");
            return new List<Dog>() { 
                new Dog(){ Id = 1, Name="AnnaLeigh", Age=1, Type="Aussie", OwnerId=1, Size="Large"},
                new Dog(){ Id = 2, Name="Bo", Age=3, Type="Pit Mix", OwnerId=2, Size="Large"},
                new Dog(){ Id = 3, Name="Taj", Age=5, Type="Another Kind Of Dog", OwnerId=2, Size="Large"},
                new Dog(){ Id = 4, Name="Lindy", Age=8, Type="Small Something", OwnerId=3, Size="Small"},
                new Dog(){ Id = 5, Name="Zia", Age=4, Type="Leggy Something", OwnerId=3, Size="Large"},
                new Dog(){ Id = 6, Name="Loralie", Age=3, Type="Great Dane", OwnerId=4, Size="Large"},
            };
        }
    }
}