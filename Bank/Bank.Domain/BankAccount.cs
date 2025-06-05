/// <summary>
/// Representa una cuenta bancaria que mantiene el saldo y permite realizar operaciones de débito y crédito.
/// </summary>
public class BankAccount
{
    /// <summary>
    /// Mensaje usado cuando el monto a debitar excede el saldo disponible.
    /// </summary>
    public const string DebitAmountExceedsBalanceMessage = "Debit amount exceeds balance";

    /// <summary>
    /// Mensaje usado cuando el monto a debitar es menor que cero.
    /// </summary>
    public const string DebitAmountLessThanZeroMessage = "Debit amount is less than zero";

    private readonly string m_customerName;
    private double m_balance;

    /// <summary>
    /// Constructor privado para evitar la creación sin datos.
    /// </summary>
    private BankAccount() { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="BankAccount"/> con un nombre de cliente y saldo inicial.
    /// </summary>
    /// <param name="customerName">El nombre del cliente propietario de la cuenta.</param>
    /// <param name="balance">El saldo inicial de la cuenta.</param>
    public BankAccount(string customerName, double balance)
    {
        m_customerName = customerName;
        m_balance = balance;
    }

    /// <summary>
    /// Obtiene el nombre del cliente propietario de la cuenta.
    /// </summary>
    public string CustomerName { get { return m_customerName; } }

    /// <summary>
    /// Obtiene el saldo actual de la cuenta.
    /// </summary>
    public double Balance { get { return m_balance; }  }

    /// <summary>
    /// Reduce el saldo de la cuenta en la cantidad especificada.
    /// </summary>
    /// <param name="amount">El monto a debitar de la cuenta.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Se lanza cuando <paramref name="amount"/> es mayor que el saldo actual o menor que cero.
    /// </exception>
    public void Debit(double amount)
    {
        if (amount > m_balance)
            throw new ArgumentOutOfRangeException("amount", amount, DebitAmountExceedsBalanceMessage);
        if (amount < 0)
            throw new ArgumentOutOfRangeException("amount", amount, DebitAmountLessThanZeroMessage);
        m_balance -= amount; 
    }

    /// <summary>
    /// Aumenta el saldo de la cuenta en la cantidad especificada.
    /// </summary>
    /// <param name="amount">El monto a acreditar en la cuenta.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Se lanza cuando <paramref name="amount"/> es menor que cero.
    /// </exception>
    public void Credit(double amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException("amount");
        m_balance += amount;
    }
}
