using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace machine
{
    class UserInterface
    {
        public static void ChooseFood(Machine machine)
        {
            Console.WriteLine("Press 0 if you want a cake (price: 50 rubles)");
            Console.WriteLine("Press 1 if you want a cookie (price: 10 rubles)");
            Console.WriteLine("Press 2 if you want wafers (price: 30 rubles)");
            Console.WriteLine("Press 9 to exit to main menu");
            Console.WriteLine("____________________________");
            int choice;
            bool isCorrect = int.TryParse(Console.ReadLine(), out choice);

            if (isCorrect == false)
            {
                Console.WriteLine("Incorrect data. Try again");
            }
            else 
            {
                if (choice == 9)
                {
                    return;
                }

                if ((choice == 0) || (choice == 1) || (choice == 2))
                {
                    int errorCode = machine.ReturnFoodErrorCode(choice);
                    switch (errorCode)
                    {
                        case 1:
                            Console.WriteLine("There is no chosen food. Sorry");
                            break;
                        case 2:
                            Console.WriteLine("Not enough money. You need to pay more");
                            break;
                        case 3:
                            Console.WriteLine("Sorry, the machine will not be able to give you money. Choose other food or take your money back");
                            break;
                        default:
                            Console.WriteLine("Take your food! Thank you");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect data");
                }
            }
        }

        public static void Pay(Machine machine, Customer customer)
        {
            while (true)
            {
                Console.WriteLine("Press 0 if you pay 1 ruble");
                Console.WriteLine("Press 1 if you pay 2 rubles");
                Console.WriteLine("Press 2 if you pay 5 rubles");
                Console.WriteLine("Press 3 if you pay 10 rubles");
                Console.WriteLine("Press 9 to exit to main menu");
                Console.WriteLine("____________________________");
                int choice;
                bool isCorrect = int.TryParse(Console.ReadLine(), out choice);

                if (isCorrect == false)
                {
                    Console.WriteLine("Your balance is {0}", machine.ShowBalance());
                    Console.WriteLine("Incorrect data. Try again");
                }
                else
                {
                    if (choice == 9)
                    {
                        return;
                    }
                    if ((choice == 0) || (choice == 1) || (choice == 2) || (choice == 3))
                    {
                        if (customer.IsEnoughMoney(choice) == true) //есть ли эта сумма в кошельке
                        {
                            machine.GetCoin(choice);
                            customer.Pay(choice);
                            Console.WriteLine("Your balance is {0}", machine.ShowBalance());
                        }
                        else
                        {
                            Console.WriteLine("You haven't got enough money in your purse!");
                            Console.WriteLine("There are only {0} rubles in purse", customer.ShowPurse());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect data. Try again");
                    }
                }
            }
        }

        public static void GetMoneyBack(Machine machine, Customer customer)
        {
            bool isTest = false; //будет ли производиться возврат денег или же это просто проверка
            int customersMoneyTest = machine.ShowBalance();
            bool isReturnMoney = machine.IsReturnMoney(customersMoneyTest, isTest);
            if (isReturnMoney == true)
            {
                customer.GetMoney(customersMoneyTest);
                Console.WriteLine("Take your money!");
                Console.WriteLine("There are {0} rubles in the purse", customer.ShowPurse());
            }
            else
            {
                Console.WriteLine("Oh no! there is a mistake!");
            }
        }

        public static void Run()
        {
            Machine machine = new Machine();
            Customer customer = new Customer();
            while (true)
            {
                Console.WriteLine("Your balance is {0}", machine.ShowBalance());
                Console.WriteLine("What do you want?");
                Console.WriteLine("Press 0 if you want to choose food");
                Console.WriteLine("Press 1 if you want to pay");
                Console.WriteLine("Press 2 to get spare money back"); //получить сдачу
                Console.WriteLine("Press 9 to exit");
                Console.WriteLine("____________________________");
                int choice;
                bool isCorrect = int.TryParse(Console.ReadLine(), out choice);

                if (isCorrect == false)
                {
                    Console.WriteLine("Incorrect data. Try again");
                }
                else
                {
                    if (choice == 9)
                    {
                        break; //выход из программы
                    }
                    else
                    {
                        switch (choice)
                        {
                            case 0:
                                ChooseFood(machine);
                                break;
                            case 1:
                                Pay(machine, customer); 
                                break;
                            case 2:
                                GetMoneyBack(machine, customer);
                                break;
                            default:
                                Console.WriteLine("Incorrect data. Try again");
                                break;
                        }
                    }
                }
            }
        }

        static void Main()
        {
            Console.WriteLine("Welcome!");
            Run();
        }
    }
}
