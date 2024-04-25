using System.Buffers;
using System.Net.WebSockets;
using ClientCH;


Client myClient = new();
List<Client> listOfClients = [];

LoadFileValuesToMemory(listOfClients);

bool again = true;
do
{
    try
	{
		DisplayMainMenu();
		string mainMenuChoice = Prompt("\nEnter a Main Menu Choice: ").ToUpper();
		if (mainMenuChoice == "N")
			myClient = NewClient();
		if (mainMenuChoice == "S")
			ShowClientInfo(myClient);
		if (mainMenuChoice == "A")
			AddClientToList(myClient, listOfClients);
		if (mainMenuChoice == "F")
			myClient = FindClientInList(listOfClients);
		if (mainMenuChoice == "R")
			RemoveClientFromList(myClient, listOfClients);
		if (mainMenuChoice == "D")
			DisplayAllClientsInList(listOfClients);
		if (mainMenuChoice == "Q")
		{
			SaveMemoryValuesToFile(listOfClients);
			again = false;
			throw new Exception("Bye, hope to see you again.");
		}
		if (mainMenuChoice == "E")
		{
			while (true)
			{
				DisplayEditMenu();
				string editMenuChoice = Prompt("\nEnter a Edit Menu Choice: ").ToUpper();
				if (editMenuChoice == "F")
					GetFirst(myClient);
				if (editMenuChoice == "L")
					GetLast(myClient);
				if (editMenuChoice == "W")
					GetWeight(myClient);
				if (editMenuChoice == "H")
					GetHeight(myClient);
				if (editMenuChoice == "R")
					throw new Exception("Returning to Main Menu");
			}
		}
	}
	catch (Exception ex)
	{
		Console.WriteLine($"{ex.Message}");
	}
} while (again);

void DisplayMainMenu()
{
	Console.WriteLine("\nMain Menu");
	Console.WriteLine("N) New Client");
	Console.WriteLine("S) Show Client Info");
	Console.WriteLine("E) Edit Client Info ");
	Console.WriteLine("A) Add Client To List PartB");
	Console.WriteLine("F) Find Client In List PartB");
	Console.WriteLine("R) Remove Client From List PartB");
	Console.WriteLine("D) Display all Clients in List PartB");
	Console.WriteLine("Q) Quit");
}

void DisplayEditMenu()
{
	Console.WriteLine("Edit Menu");
	Console.WriteLine("F) First Name");
	Console.WriteLine("L) Last Name");
	Console.WriteLine("W) Weight");
	Console.WriteLine("H) Height");
	Console.WriteLine("R) Return to Main Menu");
}

void ShowClientInfo(Client myClient)
{
	if(myClient == null)
		throw new Exception($"No Client In Memory");
	Console.WriteLine($"Client Name: {myClient.FullName}");
    Console.WriteLine($"Client Height: {myClient.Height}");
    Console.WriteLine($"Client Weight: {myClient.Weight}");
    Console.WriteLine($"Client BMI Status: {myClient.BmiStatus}");
    Console.WriteLine($"Client BMI Score: {myClient.BmiScore}");
}

string Prompt(string prompt)
{
	string myString = "";
	while (true)
	{
		try
		{
		Console.Write(prompt);
		myString = Console.ReadLine().Trim();
		if(string.IsNullOrEmpty(myString))
			throw new Exception($"Empty Input: Please enter something.");
		break;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}
	return myString;
}

int PromptIntBetweenMinMax(String msg, int min, int max)
{
	int num = 0;
	while (true)
	{
		try
		{
			Console.Write($"{msg} between {min} and {max} inclusive: ");
			num = int.Parse(Console.ReadLine());
			if (num < min || num > max)
				throw new Exception($"Must be between {min:n2} and {max:n2}");
			break;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Invalid: {ex.Message}");
		}
	}
	return num;
}

Client NewClient()
{
	//Console.WriteLine("Not Implemented Yet PartA");
	Client myClient = new Client();
	GetFirst(myClient);
	GetLast(myClient);
	GetWeight(myClient);
	GetHeight(myClient);
	return myClient;
}

void GetFirst(Client client)
{
	//Console.WriteLine("Not Implemented Yet PartA");
	string myString = Prompt($"Enter First Name: ");
	client.FirstName = myString;
}

void GetLast(Client client)
{
	//Console.WriteLine("Not Implemented Yet PartA");
	string myString = Prompt($"Enter Last Name: ");
	client.LastName = myString;
}

void GetWeight(Client client)
{
	//Console.WriteLine("Not Implemented Yet PartA");
	int myInt = PromptIntBetweenMinMax("Enter Weight in pounds: ", 0, 1000);
	client.Weight = myInt;
}

void GetHeight(Client client)
{
	//Console.WriteLine("Not Implemented Yet PartA");
	int myInt = PromptIntBetweenMinMax("Enter Height in inches: ", 0, 1000);
	client.Height = myInt;
}

void AddClientToList(Client myClient, List<Client> listOfClients)
{
	//Console.WriteLine("Not Implemented Yet PartB");
	if(myClient == null)
		throw new Exception($"No Client provided to add to list");
	listOfClients.Add(myClient);
	Console.WriteLine($"Client Added");
}

Client FindClientInList(List<Client> listOfClients)
{
	//Console.WriteLine("Not Implemented Yet PartB");
	string myString = Prompt($"Enter Partial Client Name: ");
	foreach(Client client in listOfClients)
		if(client.FullName.Contains(myString))
			return client;
	Console.WriteLine($"No Clients Match");
	return null;
}

void RemoveClientFromList(Client myClient, List<Client> listOfClients)
{
	//Console.WriteLine("Not Implemented Yet PartB");
	if(myClient == null)
		throw new Exception($"No Client provided to remove from list");
	listOfClients.Remove(myClient);
	Console.WriteLine($"Client Removed");
}

void DisplayAllClientsInList(List<Client> listOfClients)
{
	//Console.WriteLine("Not Implemented Yet PartB");
	foreach(Client client in listOfClients)
		ShowClientInfo(client);
}

void LoadFileValuesToMemory(List<Client> listOfClients)
{
	while(true){
		try
		{
			//string fileName = Prompt("Enter file name including .csv or .txt: ");
			string fileName = "regin.csv";
			string filePath = $"./data/{fileName}";
			if (!File.Exists(filePath))
				throw new Exception($"The file {fileName} does not exist.");
			string[] csvFileInput = File.ReadAllLines(filePath);
			for(int i = 0; i < csvFileInput.Length; i++)
			{
				//Console.WriteLine($"lineIndex: {i}; line: {csvFileInput[i]}");
				string[] items = csvFileInput[i].Split(',');
				for(int j = 0; j < items.Length; j++)
				{
					//Console.WriteLine($"itemIndex: {j}; item: {items[j]}");
				}
				Client myClient = new Client(items[0], items[1], int.Parse(items[2]), int.Parse(items[3]));
				listOfClients.Add(myClient);
			}
			Console.WriteLine($"Load complete. {fileName} has {listOfClients.Count} data entries");
			break;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Message}");
		}
	}
}

void SaveMemoryValuesToFile(List<Client> listOfClients)
{
	//string fileName = Prompt("Enter file name including .csv or .txt: ");
	string fileName = "regout.csv";
	string filePath = $"./data/{fileName}";
	string[] csvLines = new string[listOfClients.Count];
	for (int i = 0; i < listOfClients.Count; i++)
	{
		csvLines[i] = listOfClients[i].ToString();
	}
	File.WriteAllLines(filePath, csvLines);
	Console.WriteLine($"Save complete. {fileName} has {listOfClients.Count} entries.");
}