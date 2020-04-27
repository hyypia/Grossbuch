﻿using System;
using GrossbuchLibrary;

namespace Grossbuch
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank<Account> bank = new Bank<Account>("HausBank");
            bool alive = true;
            while(alive)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("1. Открыть счет \t 2. Вывести средства  \t 3. Добавить на счет");
                Console.WriteLine("4. Закрыть счет \t 5. Пропустить день \t 6. Выйти из программы");
                Console.WriteLine("Введите номер пункта:");
                Console.ForegroundColor = color;
                try
                {
                    int command = Convert.ToInt32(Console.ReadLine());
                    switch(command)
                    {
                        case 1:
                            OpenAccount(bank);
                            break;
                        case 2:
                            Withdraw(bank);
                            break;
                        case 3:
                            Put(bank);
                            break;
                        case 4:
                            CloseAccount(bank);
                            break;
                        case 5:
                            break;
                        case 6:
                            alive = false;
                            continue;
                    }
                    bank.CalculatePercentage();
                }
                catch(Exception ex)
                {
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }
            }
        }
        private static void OpenAccount(Bank<Account> bank)
        {
            Console.WriteLine("Укажите сумму для создания счета:");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Выберите тип счета: 1. До востребования 2. Депозит");
            AccountType accountType;

            int type = Convert.ToInt32(Console.ReadLine());

            if(type == 2)
            {
                accountType = AccountType.Deposit;
            }
            else
            {
                accountType = AccountType.Ordinary;
            }

            bank.Open(accountType, sum, AddSumHandler, WithdrawSumHandler,
                (o, e) => Console.WriteLine(e.Message), CloseAccountHandler, 
                OpenAccountHandler);
        }

        public static void Withdraw(Bank<Account> bank)
        {
            Console.WriteLine("Укажите сумму для снятия со счета:");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Введите Id счета:");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Withdraw(sum, id);
        }

        private static void Put(Bank<Account> bank)
        {
            Console.WriteLine("Укажите сумму внесения на счет:");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Введите Id счета:");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Put(sum, id);
        }

        private static void CloseAccount(Bank<Account> bank)
        {
            Console.WriteLine("Введите Id счета, который надо закрыть:");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Close(id);
        }

        private static void OpenAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        private static void AddSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        private static void WithdrawSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
            if(e.Sum > 0)
                Console.WriteLine("Сумма успешно снята с Вашего счета");
        }
        private static void CloseAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
