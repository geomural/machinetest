using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace machine
{
    public class Machine
    {
        private const int cakePrice = 50;
        private const int cookiePrice = 10;
        private const int wafersPrice = 30;

        private const int cakeAmountInit = 4;
        private const int cookieAmountInit = 3;
        private const int wafersAmountInit = 10;

        private const int coinsAmountInit = 5; //по сколько монет будет в аппарате изначально

        private int[] CoinsAmount { get; set; } //coinsAmount[0] - количество монет достоинством 1 рубль и тд 
                                               //coinsAmount[3] - количество монет достоинством 10 рублей
        private int CakeAmount { get; set; }
        private int CookieAmount { get; set; }
        private int WafersAmount { get; set; }

        private int CustomersMoney { get; set; } //внесенные покупателем деньги

        public Machine()  //инициализация
        {
            CakeAmount = cakeAmountInit;
            CookieAmount = cookieAmountInit;
            WafersAmount = wafersAmountInit;

            CoinsAmount = new int[4];
            for (int i = 0; i < CoinsAmount.Length; i++)
            {
                CoinsAmount[i] = coinsAmountInit;
            }
            CustomersMoney = 0;
        }

        public int ShowBalance()
        {
            return CustomersMoney;
        }

        public void GetCoin(int typeOfCoin)
        {
            CustomersMoney += typeOfCoin * typeOfCoin + 1; //сколько внесли
            CoinsAmount[typeOfCoin]++; //что именно внесли
        }

        //i^2 + 1 формула преобразования индекса массива в номинал монет

        public bool IsReturnMoney(int customersMoneyTest, bool isTest) //проверка на наличие сдачи / возврат сдачи
        {
            int[] coinsAmountTest = new int[4];
            for (int i = 0; i < coinsAmountTest.Length; i++)
            {
                coinsAmountTest[i] = CoinsAmount[i];
            }
            for (int i = coinsAmountTest.Length - 1; i >= 0; i--)
            {
                while ((coinsAmountTest[i] != 0) && (customersMoneyTest >= i * i + 1)) //пока есть такие монеты в аппарате
                {
                    coinsAmountTest[i]--;
                    customersMoneyTest -= i * i + 1; //формула преобразования индекса массива в номинал монет
                }
            }
            if (customersMoneyTest == 0)
            {
                if (isTest == false) //если это не проверка на возможность выдачи сдачи
                {
                    CustomersMoney = customersMoneyTest; //возврат денег/сдачи прошел успешно
                    CoinsAmount = coinsAmountTest;
                }
                return true; //выдача сдачи возможна
            }
            return false; //нет сдачи
        }

        public bool IsFoodAvailable(int foodAmount) //есть ли выбранная еда в наличии
        {
            if (foodAmount == 0)
            {
                return false;
            }
            return true;
        }

        public bool IsEnoughMoney(int foodPrice)
        {
            if (CustomersMoney < foodPrice)
            {
                return false;
            }
            return true;
        }

        public int ReturnFoodErrorCode(int typeOfFood) //возвращается ли еда
        {
            int[] food = WhichFood(typeOfFood);
            int foodAmount = food[0];
            int foodPrice = food[1];
            if (IsFoodAvailable(foodAmount) == false) //данной еды нет в аппарате
            {
                return 1; //код ошибки
            }
            if (IsEnoughMoney(foodPrice) == false) //внесено недостаточно денег
            {
                return 2; 
            }

            int customersMoneyTest = CustomersMoney;
            customersMoneyTest -= foodPrice;

            bool isTest = true; //проверка возможности выдать сдачу
            if (IsReturnMoney(customersMoneyTest, isTest) == false)
            {
                return 3; //нельзя купить эту еду т.к. аппарат не сможет дать сдачи
            }

            CustomersMoney = customersMoneyTest;
            DecreaseFoodAmount(typeOfFood);
            return 0;
        }
        
        private int[] WhichFood(int typeOfFood)
        {
            int[] food = new int[2];
            if (typeOfFood == 0)
            {
                food[0] = CakeAmount;
                food[1] = cakePrice;
            }
            else if (typeOfFood == 1)
            {
                food[0] = CookieAmount;
                food[1] = cookiePrice;
            }
            else
            {
                food[0] = WafersAmount;
                food[1] = wafersPrice;
            }
            return food;
        }

        private void DecreaseFoodAmount(int typeOfFood) //уменьшаем количество еды в аппарате
        {
            if (typeOfFood == 0)
            {
                CakeAmount--;
            }
            else if (typeOfFood == 1)
            {
                CookieAmount--;
            }
            else
            {
                WafersAmount--;
            }
        }
    }
}
