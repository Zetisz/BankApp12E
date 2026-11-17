using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp12E
{
    [TestFixture]
    public class BankTest
    {
        Bank bank;

        [SetUp]
        public void SetUp()
        {
            bank = new Bank();
            bank.CreateAccount("1234", "Jancsi");
        }

        [Test]
        public void StartingBalanceIsZero()
        {
            Assert.That(bank.GetBalance("1234"), Is.Zero);
        }

        [Test]
        public void DepositOnce()
        {
            bank.Deposit("1234", 10000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(10000));
        }

        [Test]
        public void DepositMultiple()
        {
            bank.Deposit("1234", 10000);
            bank.Deposit("1234", 15000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(25000));
        }

        [Test]
        public void DepositNegativeAmount()
        {
            Assert.That(() => bank.Deposit("1234", -10000), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void DepositMultipleAccounts()
        {
            bank.CreateAccount("5678", "Jancsi");
            bank.Deposit("1234", 10000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(10000));
            Assert.That(bank.GetBalance("5678"), Is.Zero);
        }

        [Test]
        public void GetBalanceOfInvalidAccount()
        {
            Assert.That(() => bank.GetBalance("5678"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void DepositToInvalidAccount()
        {
            Assert.That(() => bank.Deposit("5678", 5000), Throws.TypeOf<ArgumentException>());
        }
        
        [Test]
        public void WithdrawOnce()
        {
            bank.Deposit("1234", 10000);
            Assert.That(bank.Withdraw("1234", 10000), Is.True);
        }
        
        [Test]
        public void WithdrawMultiple()
        {
            bank.Deposit("1234", 10000);
            bank.Withdraw("1234", 2500);
            bank.Withdraw("1234", 7500);
            Assert.That(bank.GetBalance("1234"), Is.Zero);
        }

        [Test]
        public void WithdrawNegativeAmount()
        {
            Assert.That(() => bank.Withdraw("1234", -10000), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void WithdrawMultipleAccounts()
        {
            bank.CreateAccount("5678", "Jancsi");
            bank.Deposit("1234", 20000);
            Assert.That(bank.Withdraw("1234", 10000),  Is.True);
            Assert.That(bank.GetBalance("5678"), Is.Zero);
        }

        [Test]
        public void WithdrawToInvalidAccount()
        {
            Assert.That(() => bank.Withdraw("5678", 5000), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void WithdrawWithInvalidBalance()
        {
            Assert.That(() => bank.Withdraw("1234", 10000), Is.False);
        }

        [Test]
        public void TransferOnce()
        {
            bank.CreateAccount("5678", "Jancsi");
            bank.Deposit("1234", 10000);
            Assert.That(bank.Transfer("1234", "5678", 10000), Is.True);
        }

        [Test]
        public void TransferMultiple()
        {
            bank.CreateAccount("5678", "Jancsi");
            bank.Deposit("1234", 10000);
            bank.Transfer("1234", "5678", 5000);
            bank.Transfer("1234", "5678", 5000);
            Assert.That(bank.GetBalance("5678"), Is.EqualTo(10000));
        }

        [Test]
        public void TransferNegativeAmount()
        {
            bank.CreateAccount("5678", "Jancsi");
            bank.Deposit("1234", 10000);
            Assert.That(() => bank.Transfer("1234", "5678", -5000), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TransferMultipleAccounts()
        {
            bank.CreateAccount("5678", "Jancsi");
            bank.CreateAccount("4444", "Juliska");
            bank.Deposit("1234", 7000);
            bank.Transfer("1234", "5678", 5000);
            bank.Transfer("1234", "4444", 2000);
            bank.Transfer("5678", "4444", 2444);
            Assert.That(bank.GetBalance("4444"), Is.EqualTo(4444));
        }

        [Test]
        public void TransferToInvalidAccount()
        {
            bank.Deposit("1234", 10000);
            Assert.That(() => bank.Transfer("1234", "5678", 10000), Throws.TypeOf<ArgumentException>());
        }
        
        [Test]
        public void TransferWithInvalidBalance()
        {
            bank.CreateAccount("5678", "Jancsi");
            Assert.That(bank.Transfer("1234", "5678", 10000), Is.False);
        }

        [Test]
        public void DeleteOnce()
        {
            Assert.That(bank.CloseAccount("1234"), Is.True);
        }

        [Test]
        public void DeleteAndGetBalance()
        {
            bank.CreateAccount("5678", "Ádám");
            Assert.That(bank.CloseAccount("5678"), Is.True);
            Assert.That(() => bank.GetBalance("5678"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void DeleteNonExistentAccount()
        {
            Assert.That(() => bank.CloseAccount("5678"), Throws.TypeOf<ArgumentException>());
        }
        
        [Test]
        public void DeleteWhenBalanceNotZero()
        {
            bank.Deposit("1234", 10000);
            Assert.That(() => bank.CloseAccount("1234"), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void GetOwnerOnce()
        {
            Assert.That(bank.GetOwner("1234"), Is.EqualTo("Jancsi"));
        }

        [Test]
        public void GetOwnerMultiple()
        {
            bank.CreateAccount("5678", "Ádám");
            Assert.That(bank.GetOwner("1234"), Is.EqualTo("Jancsi"));
            Assert.That(bank.GetOwner("5678"), Is.EqualTo("Ádám"));
        }

        [Test]
        public void GetOwnerOfInvalidAccount()
        {
            Assert.That(() => bank.GetOwner("5678"), Throws.TypeOf<ArgumentException>());
        }
    }
}
