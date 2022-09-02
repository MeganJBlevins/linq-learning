public class Owner{

	public int Id { get; set; }
	public string Name { get; set; }
}

public class Dog{

	public int Id { get; set; }
	public string Name { get; set; }
	public int Age { get; set; }
	public string Size { get; set; }
	public string Type { get; set; }
	public int OwnerId { get; set; }
	public Boolean? IsBest { get; set; }
}

public class Home
{
	public int OwnerId { get; set; }
	public string Address { get; set; }
	public string HouseColor { get; set; }
}
