using Bank.Domain;
using System;
using Xunit;

namespace Bank.Domain.Tests;
public class BankAccountTests
{
    [Theory]
    [InlineData(11.99, 4.55, 7.44)]
    [InlineData(12.3, 5.2, 7.1)]
    public void MultiDebit_WithValidAmount_UpdatesBalance(
        double beginningBalance, double debitAmount, double expected )
    {
        // Arrange
        BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
        // Act
        account.Debit(debitAmount);
        // Assert
        double actual = account.Balance;
        Assert.Equal(Math.Round(expected,2), Math.Round(actual,2));
    }

    [Fact]
    public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
    {
        // Arrange
        double beginningBalance = 11.99;
        double debitAmount = -100.00;
        BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
        // Act and assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => account.Debit(debitAmount));

        Assert.Equal("amount", exception.ParamName); // ✅ Esta línea resuelve la mutación

        Assert.Throws<System.ArgumentOutOfRangeException>(() => account.Debit(debitAmount));
    }

    [Fact]
    public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
    {
        // Arrange
        double beginningBalance = 11.99;
        double debitAmount = 20.0;
        BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

        // Act & Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => account.Debit(debitAmount));
        Assert.Equal("amount", exception.ParamName); // ✅ Añadir esta línea

        Assert.Contains(BankAccount.DebitAmountExceedsBalanceMessage, exception.Message);
    }
    [Fact]
    public void Debit_AmountEqualToBalance_ShouldSucceed()
    {
        // Arrange
        double balance = 50.00;
        BankAccount account = new BankAccount("Cliente", balance);

        // Act
        account.Debit(50.00);

        // Assert
        Assert.Equal(0, account.Balance);
    }
    [Fact]
    public void Credit_ZeroAmount_ShouldNotChangeBalance()
    {
        // Arrange
        double balance = 100;
        BankAccount account = new BankAccount("Cliente", balance);

        // Act
        account.Credit(0);

        // Assert
        Assert.Equal(balance, account.Balance);
    }

    
    [Fact]
    public void Debit_WithZeroAmount_ShouldNotThrow()
    {
        var account = new BankAccount("Gabriela", 100.0);

        var exception = Record.Exception(() => account.Debit(0));

        Assert.Null(exception); // Esperamos que no se lance excepción
    }

    [Fact]
    public void Credit_WithValidAmount_UpdatesBalance()
    {
        // Arrange
        double beginningBalance = 11.99;
        double creditAmount = 5.00;
        BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

        // Act
        account.Credit(creditAmount);

        // Assert
        double expected = beginningBalance + creditAmount;
        Assert.Equal(expected, account.Balance);
    }
    [Fact]
    public void Credit_NegativeAmount_ShouldThrowWithCorrectMessage()
    {
        BankAccount account = new BankAccount("Cliente", 100);
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => account.Credit(-5));
        Assert.Contains("amount", ex.Message); // Puedes ajustar si usas un mensaje específico
    }

    [Fact]
    public void CustomerName_ReturnsCorrectName()
    {
        // Arrange
        string expectedName = "Mr. Bryan Walton";
        BankAccount account = new BankAccount(expectedName, 11.99);

        // Act
        string actualName = account.CustomerName;

        // Assert
        Assert.Equal(expectedName, actualName);
    }
    [Fact]
    public void Balance_ReturnsInitialBalance()
    {
        // Arrange
        double expectedBalance = 11.99;
        BankAccount account = new BankAccount("Mr. Bryan Walton", expectedBalance);

        // Act
        double actualBalance = account.Balance;

        // Assert
        Assert.Equal(expectedBalance, actualBalance);
    }


}