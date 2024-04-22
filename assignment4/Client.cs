namespace ClientCH
{
    public class Client 
    {
    private string _firstName;
    private string _lastName;
    private int _weight;
    private int _height;

    public Client(string firstName, string lastName, int weight, int height)
    {
        FirstName = firstName;
        LastName = lastName;
        Weight = weight;
        Height = height;
    }

    public string FirstName
    {
        get { return _firstName; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentNullException("Name is required. Must not be empty or blank.");
			_firstName = value;
	    }
    }

    public string LastName
    {
        get { return _lastName; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentNullException("Name is required. Must not be empty or blank.");
			_lastName = value;
	    }
    }

    public int Weight
    {
        get { return _weight; }
        set
        {
            if (value < 0)
				throw new ArgumentException("Weight is required. Must be positive value (greater than 0)");
			_weight = value;
	    }
    }

    public int Height
    {
        get { return _height; }
        set
        {
            if (value < 0)
				throw new ArgumentException("Weight is required. Must be positive value (greater than 0)");
			_height = value;
	    }
    }

    public double BmiScore
    {
        get
        {
            double bmiscore = Weight / Math.Pow(Height, 2) * 703;
            return bmiscore;
        }
    }

    public string BmiStatus
    {
        get
        {
            string status;
            if(BmiScore <= 18.4)
                status = "Underweight";
            else if(BmiScore <= 24.9)
                status = "Normal";
            else if(BmiScore <= 39.9)
                status = "Overweight";
            else
                status = "Obese";
            return status;
        }
    }

    public string FullName
    {
        get
        {
            string FullName = $"{LastName}, {FirstName}";
            return FullName;
        }
    }
}
}
