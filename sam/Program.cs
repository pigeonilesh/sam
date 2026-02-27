using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace sam
{
    internal class Program
    {
        static public int HP;
        static public int MaxHP;
        static public int gold;
        static public int potion;
        static public int arows;
        static public string weapon;
        static public int roomNumber = 1;
        static public Random r = new Random();

        static void InitializeGame()
        {
            HP = 100;
            MaxHP = 100;
            gold = 10;
            potion = 2;
            arows = 5;
            roomNumber = 1;
        }

        static void StartGame()
        {
            Console.WriteLine("Хотите начать игру? Да/Нет");
            string start = Console.ReadLine();
            Console.WriteLine();
            if (start.ToLower() == "нет")
            {
                Console.WriteLine($"Завершение игры");
            }
            else if (start.ToLower() == "да")
            {
                InitializeGame();
                ProcessRoom(roomNumber);
                ShowStats();
            }
        }

        static void ProcessRoom(int roomNumber)
        {
            while (roomNumber <= 14 && HP > 0)
            {
                int a = r.Next(1, 2);
                switch (a)
                {
                    case 1:
                        FightMonster(r.Next(20, 51), r.Next(5, 16));
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                }
            }
        }  

        static void FightMonster(int monsterHP, int monsterAttack)
        {
            Console.WriteLine($"Комната {roomNumber}: Бой с монстром");
            Console.WriteLine();
            Console.WriteLine($"На вашем пути появился монстр. У него {monsterHP} ХП и его сила атаки равна {monsterAttack}");
            Console.WriteLine();
            while (monsterHP > 0 && HP > 0)
            {
                Console.WriteLine($"Выберите действие:");
                Console.WriteLine($"1. Использовать меч (10-20)");
                Console.WriteLine($"2. Использовать лук (5-15), тратится одна стрела.");
                Console.WriteLine($"3. Использовать зелье (Восстанавливает 30 ХП)");
                int move = Convert.ToInt32(Console.ReadLine());
                switch (move)
                {
                    case 1:
                        weapon = "Меч";
                        int swordDamage = r.Next(10, 21);
                        monsterHP -= swordDamage;
                        Console.WriteLine($"Вы использовали меч и нанесли удар монстру. Вы нанесли {swordDamage} урона, у противника осталось {monsterHP} ХП");
                        break;
                    case 2:
                        weapon = "Лук";
                        if (arows > 0)
                        {
                            int bowDamage = r.Next(5, 16);
                            monsterHP -= bowDamage;
                            Console.WriteLine($"Вы использовали лук и нанесли удар монстру. Вы нанесли {bowDamage} урона, у противника осталось {monsterHP} ХП");
                            arows -= 1;
                            Console.WriteLine($"Вы потратили одну стрелу и у вас осталось {arows} стрел");
                        }
                        else
                        {
                            Console.WriteLine($"Недастаточно стрел для выстрела, вы пропускаете ход.");
                        }
                        break;
                    case 3:
                        weapon = "Зелье";
                        if (potion > 0)
                        {
                            UsePotion();
                        }
                        else
                        {
                            Console.WriteLine($"Зелий не осталось, вы пропускаете ход");
                        }
                        break;
                }
                if (monsterHP <= 0)
                {
                    Console.WriteLine($"Вы убили монстра, вы получили 10 монет");
                    gold += 10;
                    Console.WriteLine($"Вы завершили комнату");
                    Console.WriteLine($"Ваши характеристики:");
                    ShowStats();
                    roomNumber++;
                    break;
                }
                if (weapon == "Меч")
                {
                    HP -= monsterAttack;
                    Console.WriteLine($"Ход противника, Монстр атакует вас на {monsterAttack}");
                    Console.WriteLine($"У вас осталось {HP} ХП");
                }
                else if (weapon == "Лук" || weapon == "Зелье")
                {

                }
                if (HP <= 0)
                {
                    Console.WriteLine($"Вы умерли. ИГРА ОКОНЧЕНА");
                    EndGame(false);
                }
            }
        }

        static void OpenChest()
        {

        }

        static void VisitMerchant()
        {

        }

        static void VisitAltar()
        {

        }

        static void MeetDarkMage()
        {

        }

        static void Event()
        {

        }

        static void UsePotion()
        {
            HP += 30;
            if (HP > MaxHP)
            {
                HP = MaxHP; 
            }
            Console.WriteLine($"Вы использовали зелье, Ваше здоровье составляет {HP} ХП");
            potion -= 1;
            if (potion < 0)
            {
                potion = 0;
            }
            Console.WriteLine($"У вас сталось {potion} зелий");
        }

        static void ShowStats()
        {
            Console.WriteLine($"ХП = {HP}");
            Console.WriteLine($"МаксХП = {MaxHP}");
            Console.WriteLine($"Монеты = {gold}");
            Console.WriteLine($"Зелья = {potion}");
            Console.WriteLine($"Стрелы = {arows}");
            Console.WriteLine($"Количество пройденных комнат = {roomNumber}");
        }

        static void FightBoss()
        {

        }

        static void EndGame(bool isWin)
        {
            if (isWin)
            {

            }
            else
            {
                Console.WriteLine($"Игра окончена");
                Console.WriteLine();
                Console.WriteLine($"Ваше статистика");
                ShowStats();
            }
        }
        
        static void Main(string[] args)
        {
            StartGame();
        }
    }
}
