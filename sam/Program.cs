using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace sam
{
    internal class Program
    {
        static public int HP;
        static public int MaxHp;
        static public int gold;
        static public int potion;
        static public int arows;
        static public string sword = "меч";
        static public string bow = "лук";
        static public int roomNumber = 1;
        static public Random r = new Random();

        static void InitializeGame()
        {
            HP = 100;
            MaxHp = 100;
            gold = 10;
            potion = 2;
            arows = 5;
        }

        static void StartGame()
        {
            Console.WriteLine("Хотите начать игру? Да/Нет");
            if (Console.ReadLine().ToLower() == "нет")
            {
                EndGame(false);
            }
            else if (Console.ReadLine().ToLower() == "да")
            {
                InitializeGame();
            }
        }

        static void ProcessRoom(int roomNumber)
        {
            while (roomNumber <= 14)
            {
                int a = r.Next(1, 7);
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
                roomNumber++;
            }
        }  

        static void FightMonster(int monsterHP, int monsterAttack)
        {
            Console.WriteLine($"Комната {roomNumber}: Бой с монстром");
            Console.WriteLine($"На ваем пути появился монстр. У него {monsterHP} ХП и его сила атаки равна {monsterAttack}");
            while (monsterHP > 0 || HP > 0)
            {

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

        }

        static void ShowStats()
        {

        }

        static void FightBoss()
        {

        }

        static void EndGame(bool isWin)
        {

        }
        
        static void Main(string[] args)
        {

        }
    }
}
