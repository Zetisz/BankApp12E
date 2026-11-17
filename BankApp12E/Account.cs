namespace BankApp12E;

public class Account
{
    public string Name
    {
        get => name;
        set => name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Balance
    {
        get => balance;
        set => balance = value;
    }

    public Account(string name, int balance)
    {
        this.name = name;
        this.balance = balance;
    }

    private string name;
    private int balance;
}