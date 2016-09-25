using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductVendingMachine;
using System.Collections;
using System.Collections.Generic;

namespace VendingTests
{
    /// <summary>
    /// Tests grouped here by methods
    /// </summary>
    [TestClass]
    public class VendingMachineTests
    {
        /// <summary>
        /// Test coins amounts data
        /// </summary>
        Money zeroCoinAmount = new Money { Evros = 0, Cents = 0 };
        Money fiftyCentsAmount = new Money { Evros = 0, Cents = 50 };
        Money oneEuroAmount    = new Money { Evros = 1, Cents = 0 };
        //

        /// <summary>
        /// Test products array
        /// </summary>
        /// <param name="q">Quantity of products can be increased</param>
        /// <param name="count">Count of available items of product</param>
        /// <param name="evros">Evro part of product price</param>
        /// <param name="cents">Evrocent part of product price</param>
        /// <param name="name">Product name</param>
        /// <returns>Returns array of products</returns>
        Product[] ProductsArray(int q = 1, int count = 1, int evros = 1, int cents = 0, string name = "testProduct")
        {
            Product product;
            List<Product> products = new List<Product>();

            for (int i = 0; i < q; i++)
            {
                product = new Product();
                product.Available = count;
                product.Price = new Money { Evros = evros, Cents = cents };
                product.Name = name + name + DateTime.Now.Millisecond.ToString();
                products.Add(product);
            }
            
            return products.ToArray();
        }


        /// <summary>
        /// Tests initialisation new VendingMachine object
        /// </summary>
        [TestMethod]
        public void init_AmountIsZero_True()
        {
            VendingMachine machine = new VendingMachine();
            Assert.AreEqual(zeroCoinAmount, machine.Amount, "Incorrect Amount init(");
        }

        public void init_ManufactererIsBosch_True()
        {
            VendingMachine machine = new VendingMachine();
            Assert.AreEqual(machine.Manufacturer, "Bosch", "Wrong name Initial!");
        }

        [TestMethod]
        public void init_ProductsArrayIsNotEmpty_True()
        {
            VendingMachine machine = new VendingMachine();
            Product[] testProducts = ProductsArray();
            machine.Products = ProductsArray();
            Assert.IsNotNull(machine.Products, "Products were not set.");
        }
        /// 


        /// <summary>
        /// Tests insert coin method
        /// </summary>
        [TestMethod]
        public void InsertCoin_OneEvroConvertsCorrectFromTwoFiftyCentsCoins_True()
        {
            VendingMachine machine = new VendingMachine();
            machine.Products = ProductsArray();
            machine.InsertCoin(fiftyCentsAmount);
            machine.InsertCoin(fiftyCentsAmount);
            Assert.AreEqual(oneEuroAmount, machine.Amount, "Transfering coins while inserting doesn`t work(");
        }


        [TestMethod]
        public void InsertCoin_AmountGrowsToFiftyCentsFromInsertedFiftedCents_True()
        {
            VendingMachine machine = new VendingMachine();
            machine.Products = ProductsArray();
            machine.InsertCoin(fiftyCentsAmount);
            Assert.AreEqual(fiftyCentsAmount, machine.Amount, "Moneys doesn`t collect from inserting(");
        }


        [TestMethod]
        [ExpectedException(typeof(ExceptionIncorrectCoin), "Vending machine take wrong EvroCent coin(")]
        public void InsertCoin_TryGiveWrongEvrocentCoinAndWaitVendmachineThrowException_True()
        {
            VendingMachine machine = new VendingMachine();
            Money notAllowedCoinEvrocent = new Money { Cents = 2 };
            machine.InsertCoin(notAllowedCoinEvrocent);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionIncorrectCoin), "Vending machine take wrong Evro coin(")]
        public void InsertCoin_TryGiveWrongEvroCoinAndWaitVendmachineThrowException_True()
        {
            VendingMachine machine = new VendingMachine();
            Money notAllowedCoinEvro = new Money { Evros = 100 };
            machine.InsertCoin(notAllowedCoinEvro);
        }
        ///


        /// <summary>
        /// Tests return money method
        /// </summary>
        [TestMethod]
        public void ReturnMoney_AmountAfterReturnMoneyDoesntReset_False()
        {
            VendingMachine machine = new VendingMachine();
            machine.Products = ProductsArray();
            machine.InsertCoin(fiftyCentsAmount);
            machine.ReturnMoney();
            Assert.AreEqual(zeroCoinAmount, machine.Amount, "Amount after return money doesn`t reset(");
        }
        
        [TestMethod]
        public void ReturnMoney_AfterInsertingOneEuroAndThenReturningMoneyTheReturnedMoneyAmountIsOneEuro_True()
        {
            VendingMachine machine = new VendingMachine();
            machine.Products = ProductsArray();
            machine.InsertCoin(oneEuroAmount);
            Money returnedMoney = machine.ReturnMoney();
            Assert.AreEqual(oneEuroAmount, returnedMoney, "Money doesn`t return correctly(");
        }
        
        [TestMethod]
        public void ReturnMoney_AmountReturnedFromZeroAccountIsZero_True()
        {
            VendingMachine machine = new VendingMachine();
            Money returnedMoney = machine.ReturnMoney();
            Assert.AreEqual(zeroCoinAmount, returnedMoney, "Money were returned from zero amount.");
        }
        ///


        /// <summary>
        /// Tests buy method
        /// </summary>
        [TestMethod]
        public void Buy_IsAvailableCountOfProductItemsDecreasesAfterBuying_True()
        {
            VendingMachine machine = new VendingMachine();
            machine.InsertCoin(oneEuroAmount);
            Product product = ProductsArray(evros: 1, cents: 0)[0];
            machine.Products = ProductsArray(evros: 1, cents: 0);
            Product productBought = machine.Buy(0);
            Assert.AreEqual(machine.Products[0].Available, product.Available - 1, "Items of product doesn`t decrease(");
        }

        [TestMethod]
        public void Buy_IsCorrectProductBought_True()
        {
            VendingMachine machine = new VendingMachine();
            machine.Products = ProductsArray(evros: 1, cents: 0);
            Product product = ProductsArray(evros: 1, cents: 0)[0];
            Product productBought = machine.Buy(0);
            Assert.AreEqual(product.Name, productBought.Name, "Wrong product was bought(");
        }


        [TestMethod]
        public void Buy_IsCorrectAmountWithdrawWneProductBought_True()
        {
            VendingMachine machine = new VendingMachine();
            machine.Products = ProductsArray();
            machine.InsertCoin(oneEuroAmount);
            machine.Buy(0);
            Product product = ProductsArray()[0];
            Assert.AreEqual(oneEuroAmount - product.Price, machine.Amount, "Cost of product doesn`t withdrawn correctly(");
        }

        [TestMethod]
        public void Buy_VendmachineReturnsCorrectOddMoney_True()
        {
            VendingMachine machine = new VendingMachine();
            machine.Products = ProductsArray();
            machine.InsertCoin(oneEuroAmount);
            machine.Buy(0);
            Assert.AreEqual(oneEuroAmount - machine.Products[0].Price, machine.ReturnMoney(), "Machine returns incorrect odd money!");
        }


        [TestMethod]
        [ExpectedException(typeof(ExceptionNotEnoughAmount), "We can buy product with not enough money!")]
        public void Buy_CanWeBuyProductWithNotEnoghMoney_False()
        {
            VendingMachine machine = new VendingMachine();
            machine.Products = ProductsArray(evros: 1);
            machine.InsertCoin(fiftyCentsAmount);
            machine.Buy(0);
        }
        ///

    }
}
