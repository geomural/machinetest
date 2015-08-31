using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace machine
{
    public class Customer
    {
        private const int moneyInit = 150; //сколько изначально денег в кошельке
        private int Money { get; set; }

        public Customer()
        {
            Money = moneyInit;
        }

        public int ShowPurse() //сколько денег в кошельке
        {
            return Money;
        }

        public void Pay(int typeOfCoin)
        {
            Money -= typeOfCoin * typeOfCoin + 1; //сколько внесли в аппарат, вычисление по формуле
        }

        public bool IsEnoughMoney(int choice)
        {
            if (Money >= choice * choice + 1) //формула преобразования в цену товара
            {
                return true;
            }
            return false;
        }

        public void GetMoney(int backMoney) //при возврате денег из аппарата
        {
            Money += backMoney;
        }
    }
}
