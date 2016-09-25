using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductVendingMachine
{
    public class VendingMachine : IVendingMachine
    {
        private Money _amount;
        private string _manufacturer;
        private bool _bought = false;
        private Product[] _products;

        public VendingMachine()
        {
            _amount.Evros = 0;
            _amount.Cents = 0;
            _manufacturer = "Bosch";
        }

        public string Manufacturer
        {
            get { return _manufacturer; }
        }

        public Money Amount
        {
            get { return _amount; }
        }

        public Product[] Products
        {
            get; set;
        }

        /// <summary>
        /// During inserting we must check type of money
        /// </summary>
        public Money InsertCoin(Money amount)
        {
            if (!Enum.IsDefined(typeof(EvrosCoins), amount.Evros) || !Enum.IsDefined(typeof(CentCoins), amount.Cents))
            {
                throw new ExceptionIncorrectCoin("Incorrect coin inserted! Please insert another coin and try again!");
            }

            _amount += amount;
                        
            return this.Amount;
        }

        public Money ReturnMoney()
        {
            Money amountToReturn = _amount;
            _amount.Evros = 0;
            _amount.Cents = 0;
            return amountToReturn;
        }

        /// <summary>
        /// When we buy product we must check if another product has already bought (by tech task)
        /// Then we check if chosen product exist in array
        /// Also we check is product available and is there enought money in automat
        /// </summary>
        public Product Buy(int number)
        {
            if (_bought)
            {
                throw new ExceptionAlreadyBought("One product was already bought during current session!");
            }

            Product product = new Product();

            try 
            {
                product = this.Products[number];
            }

            catch (Exception)
            {
                throw new ExceptionIncorrectProduct("No such product!");
            }

            if (product.Available == 0)
            {
                throw new ExceptionNotSuchProduct("Sorry, no this product more, please choose another item!");
            }

            if (product.Price > _amount)
            {
                throw new ExceptionNotEnoughAmount(String.Format("Not enoght money inserted. Insert {0} to buy what you want.", product.Price - _amount));
            }

            this.Products[number].Available--;

            _amount -= product.Price;

            _bought = true;

            return product;
        }
    }
}
