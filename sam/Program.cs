using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
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
        static public int minsd = 10;
        static public int maxsd = 21;
        static public int BOSSHP = 100;
        static public int BOSSminAttack = 15;
        static public int BOSSmaxAttack = 25;
        static public int action = 1;
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
                Console.WriteLine($"    ++++++    Начальные характеристики    ++++++    ");
                ShowStats();
                Console.WriteLine();
                Console.WriteLine();
                ProcessRoom();
            }
        }

        static void ProcessRoom()
        {
            while (roomNumber <= 14 && HP > 0)
            {
                int a = r.Next(1, 7);
                switch (a)
                {
                    case 1:
                        FightMonster(r.Next(20, 51), r.Next(5, 16));
                        break;
                    case 2:
                        OpenChest();
                        break;
                    case 3:
                        VisitMerchant();
                        break;
                    case 4:
                        VisitAltar();
                        break;
                    case 5:
                        MeetDarkMage();
                        break;
                    case 6:
                        Event();
                        break;
                }

            }
            if (roomNumber == 15 && HP > 0)
            {
                FightBoss();
            }
        }  

        static void FightMonster(int monsterHP, int monsterAttack)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"    ============    Комната {roomNumber}: Бой с монстром     ============    ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine($"На вашем пути появился монстр. У него {monsterHP} ХП и его сила атаки равна {monsterAttack}");
            Console.WriteLine();
            while (monsterHP > 0 && HP > 0)
            {
                Console.WriteLine($"Выберите действие:");
                Console.WriteLine($"1. Использовать меч ({minsd}-{maxsd-1})");
                Console.WriteLine($"2. Использовать лук (5-15), тратится одна стрела.");
                Console.WriteLine($"3. Использовать зелье (Восстанавливает 30 ХП)");
                int move = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                switch (move)
                {
                    case 1:
                        weapon = "Меч";
                        int swordDamage = r.Next(minsd, maxsd);
                        monsterHP -= swordDamage;
                        Console.WriteLine($"Вы использовали меч и нанесли удар монстру. Вы нанесли {swordDamage} урона, у противника осталось {monsterHP} ХП");
                        Console.WriteLine();
                        break;
                    case 2:
                        weapon = "Лук";
                        if (arows > 0)
                        {
                            int bowDamage = r.Next(5, 16);
                            monsterHP -= bowDamage;
                            Console.WriteLine($"Вы использовали лук и нанесли удар монстру. Вы нанесли {bowDamage} урона, у противника осталось {monsterHP} ХП");
                            arows -= 1;
                            Console.WriteLine($"Вы потратили одну стрелу и у вас осталось {arows} стрел(ы)");
                        }
                        else
                        {
                            Console.WriteLine($"Недастаточно стрел для выстрела, вы пропускаете ход.");
                            weapon = "Меч";
                        }
                        Console.WriteLine();
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
                            weapon = "Меч";
                        }
                        Console.WriteLine();
                        break;
                }
                if (monsterHP <= 0)
                {
                    Console.WriteLine($"Вы убили монстра и получили 10 монет");
                    gold += 10;
                    Console.WriteLine();
                    Console.WriteLine($"    ++++++    Ваши характеристики    ++++++    ");
                    ShowStats();
                    Console.WriteLine();
                    Console.WriteLine();
                    roomNumber++;
                    break;
                }
                if (weapon == "Меч")
                {
                    HP -= monsterAttack;
                    Console.WriteLine($"Ход противника, Монстр атакует вас на {monsterAttack}");
                    Console.WriteLine($"У вас осталось {HP} ХП");
                    Console.WriteLine();
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"    ============    Комната {roomNumber}: Сундук    ============    ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine($"Вы вошли в комнату и видете перед собой сундук. Хотите открыть сундук? Да/Нет");
            string open = Console.ReadLine().ToLower().Trim();
            Console.WriteLine();
            if (open == "да")
            {
                int chest = r.Next(1, 3);
                switch (chest)
                {
                    case 1:
                        Console.WriteLine($"Вам попался обычный сундук");
                        int loot1 = r.Next(1, 5);
                        switch (loot1)
                        {
                            case 1:
                                Console.WriteLine($"В сундуке вы нашли 10 монет");
                                gold += 10;
                                break;
                            case 2:
                                Console.WriteLine($"В сундуке вы нашли 1 зелье");
                                potion += 1;
                                break;
                            case 3:
                                Console.WriteLine($"В сундуке вы нашли 3 стрелы");
                                arows += 3;
                                break;
                            case 4:
                                Console.WriteLine($"Вам очень повезло, вам попался золотой сундук");
                                int ChestGold = r.Next(0, 11);
                                int ChestPotion = r.Next(0, 4);
                                int ChestArows = r.Next(0, 6);
                                Console.WriteLine($"В сундуке вы нашли: {ChestGold} монет, {ChestPotion} зелья {ChestArows} стрел(ы).");
                                gold += ChestGold;
                                potion += ChestPotion;
                                arows += ChestArows;
                                break;
                        }
                        break;
                    case 2:
                        Console.WriteLine($"Вам попался Проклятый сундук");
                        int loot2 = r.Next(1, 3);
                        switch (loot2)
                        {
                            case 1:
                                Console.WriteLine($"Вам повезло, вы нашли чистое золото");
                                gold += 10;
                                break;
                            case 2:
                                Console.WriteLine($"Вам не повезло, вы нашли проклятое золото");
                                gold += r.Next(10, 21);
                                MaxHP -= 10;
                                break;
                        }
                        break;
                }
                Console.WriteLine($"Вы забрали награду и идете дальше");
                Console.WriteLine();
                Console.WriteLine($"    ++++++    Ваши характеристики    ++++++    ");
                ShowStats();
                Console.WriteLine();
                Console.WriteLine();
                roomNumber++;
            }
            else if (open == "нет")
            {
                Console.WriteLine($"Не открыв сундук, вы покидаете комнату");
                Console.WriteLine();
                Console.WriteLine();
                roomNumber++;
            }
        }

        static void VisitMerchant()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"    ============    Комната {roomNumber}: Торговец    ============    ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine($"Вам повстречался торговец");
            Console.WriteLine($"Он предлогает купить вам 1 зелье за 10 монет или 3 стрелы за 5 монет.");
            Console.WriteLine();
            Console.WriteLine($"Хотите ли вы что-то преобрести? Да/Нет");
            string Merchant = Console.ReadLine().ToLower().Trim();
            Console.WriteLine();
            if (Merchant == "да")
            {
                int menu = 0;
                while (menu != 3)
                {
                    Console.WriteLine($"1) зелье за 10 монет");
                    Console.WriteLine($"2) 3 стрелы за 5 монет");
                    Console.WriteLine($"3) Выход");
                    Console.WriteLine();
                    Console.WriteLine($"Выберите товар");
                    menu = Convert.ToInt32(Console.ReadLine());
                    switch (menu)
                    {
                        case 1:
                            if (gold >= 10)
                            {
                                potion += 1;
                                gold -= 10;
                            }
                            else
                            {
                                Console.WriteLine($"Вам не хватает монет на зелье");
                            }
                            Console.WriteLine();
                            break;
                        case 2:
                            if (gold >= 5)
                            {
                                arows += 3;
                                gold -= 5;
                            }
                            else
                            {
                                Console.WriteLine($"Вам не хватает монет на стрелы");
                            }
                            Console.WriteLine();
                            break;
                        case 3:
                            Console.WriteLine($"Вы уходите от торговца");
                            Console.WriteLine();
                            Console.WriteLine($"    ++++++    Ваши характеристики    ++++++    ");
                            ShowStats();
                            Console.WriteLine();
                            Console.WriteLine();
                            roomNumber++;
                            break;
                    }
                }
            }
            else if (Merchant == "нет")
            {
                Console.WriteLine($"Вы решили пройти мимо");
                Console.WriteLine();
                Console.WriteLine();
                roomNumber++;
            }
        }

        static void VisitAltar()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"    ============    Комната {roomNumber}: Алтарь    ============    ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine($"Вам повстречался альтарь усиления");
            Console.WriteLine($"Вы можете пожертвовать 10 монет для усиления урона меча на 5 или восстановить 20 ХП");
            Console.WriteLine();
            Console.WriteLine($"Хотите ли вы подойти к алтарю? Да/Нет");
            string Altar = Console.ReadLine().ToLower().Trim();
            Console.WriteLine();
            if (Altar == "да")
            {
                int menu = 0;
                while (menu != 3)
                {
                    Console.WriteLine($"1) Усились меч");
                    Console.WriteLine($"2) Восстановить ХП");
                    Console.WriteLine($"3) Выход");
                    Console.WriteLine();
                    Console.WriteLine($"Сделайте выбор");
                    menu = Convert.ToInt32(Console.ReadLine());
                    switch (menu)
                    {
                        case 1:
                            if (gold >= 10)
                            {
                                minsd += 5;
                                maxsd += 5;
                                gold -= 10;
                            }
                            else
                            {
                                Console.WriteLine($"Вам не хватает монет для пожертвования");
                            }
                            Console.WriteLine();
                            break;
                        case 2:
                            if (gold >= 10)
                            {
                                HP += 20;
                                if (HP > MaxHP)
                                {
                                    HP = MaxHP;
                                }
                                gold -= 10;
                            }
                            else
                            {
                                Console.WriteLine($"Вам не хватает монет для пожертвования");
                            }
                            Console.WriteLine();
                            break;
                        case 3:
                            Console.WriteLine($"Вы отходите от алатря");
                            Console.WriteLine();
                            Console.WriteLine($"    ++++++    Ваши характеристики    ++++++    ");
                            ShowStats();
                            Console.WriteLine();
                            Console.WriteLine();
                            roomNumber++;
                            break;
                    }
                }
            }
            else if (Altar == "нет")
            {
                Console.WriteLine($"Вы решили пройти мимо");
                Console.WriteLine();
                Console.WriteLine();
                roomNumber++;
            }
        }

        static void MeetDarkMage()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"    ============    Комната {roomNumber}: Темный маг    ============    ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine($"Вам повстречался Темный маг");
            Console.WriteLine($"Маг предлагает совершить сделку: жертвуй 10 HP, чтобы получить 2 зелья и 5 стрел.");
            Console.WriteLine();
            Console.WriteLine($"Согласны ли вы? Да/Нет");
            string DarkMage = Console.ReadLine().ToLower().Trim();
            Console.WriteLine();
            if (DarkMage == "да")
            {
                if (HP <= 10)
                {
                    Console.WriteLine($"Вам не хватает ХП, Маг исчезает");
                    Console.WriteLine();
                    Console.WriteLine();
                    roomNumber++;
                }
                else
                {
                    HP -= 10;
                    potion += 2;
                    arows += 5;
                    Console.WriteLine($"Маг доволен сделкой");
                    Console.WriteLine($"Маг исчез, а вы продолжали путь");
                    Console.WriteLine();
                    Console.WriteLine($"    ++++++    Ваши характеристики    ++++++    ");
                    ShowStats();
                    Console.WriteLine();
                    Console.WriteLine();
                    roomNumber++;
                }
            }
            else if (DarkMage == "нет")
            {
                Console.WriteLine($"Вы решили пройти мимо");
                Console.WriteLine();
                Console.WriteLine();
                roomNumber++;
            }
        }

        static void Event()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"    ============    Комната {roomNumber}: Событие    ============    ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            int sob = r.Next(1, 4);
            switch (sob)
            {
                case 1:
                    Console.WriteLine($"Вам повстречался Голлум, который предлагает отгадать загадку");
                    int riddle = r.Next(1, 6);
                    string answer;
                    Console.WriteLine();
                    switch (riddle)
                    {
                        case 1:
                            Console.WriteLine($"«Не видать её корней, она выше тополей, вверх, вверх идёт, но не растёт»");
                            Console.WriteLine();
                            Console.WriteLine($"Ваш ответ:");
                            answer = Console.ReadLine().ToLower().Trim();
                            Console.WriteLine();
                            if (answer == "гора")
                            {
                                Console.WriteLine($"Голлум огорчился правильному ответу и дал вам 5 монет");
                                gold += 5;
                            }
                            else
                            {
                                Console.WriteLine($"Голлум посмеялся, плюнул вам в рожу и убежал");
                                HP -= 5;
                            }
                                break;
                        case 2:
                            Console.WriteLine($"«Без голоса кричит, без крыльев — а летает, и безо рта свистит, и без зубов кусает»");
                            Console.WriteLine($"Ваш ответ:");
                            answer = Console.ReadLine().ToLower().Trim();
                            Console.WriteLine();
                            if (answer == "ветер")
                            {
                                Console.WriteLine($"Голлум огорчился правильному ответу и дал вам 5 монет");
                                gold += 5;
                            }
                            else
                            {
                                Console.WriteLine($"Голлум посмеялся, плюнул вам в рожу и убежал");
                                HP -= 5;
                            }
                            break;
                        case 3:
                            Console.WriteLine($"«Её не увидать, в руках не подержать, не услышать ухом, не учуять нюхом. Царит над небесами, таится в каждой яме. Она была в начале и будет после всех. Любую жизнь кончает и убивает смех»");
                            Console.WriteLine($"Ваш ответ:");
                            answer = Console.ReadLine().ToLower().Trim();
                            Console.WriteLine();
                            if (answer == "тьма")
                            {
                                Console.WriteLine($"Голлум огорчился правильному ответу и дал вам 5 монет");
                                gold += 5;
                            }
                            else
                            {
                                Console.WriteLine($"Голлум посмеялся, плюнул вам в рожу и убежал");
                                HP -= 5;
                            }
                            break;
                        case 4:
                            Console.WriteLine($"«Не дышит, но живёт она, как смерть, нема и холодна. Не пьёт, хотя в воде сидит, в кольчуге, хоть и не звенит»");
                            Console.WriteLine($"Ваш ответ:");
                            answer = Console.ReadLine().ToLower().Trim();
                            Console.WriteLine();
                            if (answer == "рыба")
                            {
                                Console.WriteLine($"Голлум огорчился правильному ответу и дал вам 5 монет");
                                gold += 5;
                            }
                            else
                            {
                                Console.WriteLine($"Голлум посмеялся, плюнул вам в рожу и убежал");
                                HP -= 5;
                            }
                            break;
                        case 5:
                            Console.WriteLine($"«Пожирает всё кругом: зверя, птицу, лес и дом. Сталь сгрызёт, железо сгложет, крепкий камень уничтожит, власть его всего сильней, даже власти королей»");
                            Console.WriteLine($"Ваш ответ:");
                            answer = Console.ReadLine().ToLower().Trim();
                            Console.WriteLine();
                            if (answer == "время")
                            {
                                Console.WriteLine($"Голлум огорчился правильному ответу и дал вам 5 монет");
                                gold += 5;
                            }
                            else
                            {
                                Console.WriteLine($"Голлум посмеялся, плюнул вам в рожу и убежал");
                                HP -= 5;
                            }
                            break;
                    }
                    if (HP <= 0)
                    {
                        Console.WriteLine($"Вы умерли. ИГРА ОКОНЧЕНА");
                        EndGame(false);
                    }
                    break;
                case 2:
                    int lostHp = r.Next(5, 21);
                    Console.WriteLine($"Ловушка! вы потеряли {lostHp} ХП");
                    HP -= lostHp;
                    if (HP <= 0)
                    {
                        Console.WriteLine($"Вы умерли. ИГРА ОКОНЧЕНА");
                        EndGame(false);
                    }
                    break;
                case 3:
                    int reward = r.Next(1, 4);
                    switch (reward)
                    {
                        case 1:
                            int findGold = r.Next(1, 6);
                            Console.WriteLine($"вы нашли {findGold} монет");
                            gold += findGold;
                            break;
                        case 2:
                            Console.WriteLine($"вы нашли 1 зелье");
                            potion += 1;
                            break;
                        case 3:
                            int findArows = r.Next(1, 6);
                            Console.WriteLine($"вы нашли {findArows} стрелы");
                            arows += findArows;
                            break;
                    }
                            break;
            }
            Console.WriteLine($"Событие завершено");
            Console.WriteLine();
            Console.WriteLine($"    ++++++    Ваши характеристики    ++++++    ");
            ShowStats();
            Console.WriteLine();
            Console.WriteLine();
            roomNumber++;
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
            Console.WriteLine($"    ++++++    ХП = {HP}    ++++++    ");
            Console.WriteLine($"    ++++++    МаксХП = {MaxHP}    ++++++    ");
            Console.WriteLine($"    ++++++    Монеты = {gold}    ++++++    ");
            Console.WriteLine($"    ++++++    Зелья = {potion}    ++++++    ");
            Console.WriteLine($"    ++++++    Стрелы = {arows}    ++++++    ");
            Console.WriteLine($"    ++++++    Количество пройденных комнат = {roomNumber}    ++++++    ");
        }

        static void FightBoss()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"    ============    Комната {roomNumber}: БОСС   ============    ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine($"Перед вами последняя комната, вы заходите внутрь и видете перед собой Принцессу");
            Console.WriteLine($"Вы пытаетесь с ней заговорить, но она достает огронмый топор");
            Console.WriteLine($"У принцессы {BOSSHP} ХП и ее сила атаки равна {BOSSminAttack}-{BOSSmaxAttack}");
            Console.WriteLine();
            while (BOSSHP > 0 && HP > 0)
            {
                Console.WriteLine($"    ======    {action} Раунд    ======    ");
                Console.WriteLine();
                Console.WriteLine($"Выберите действие:");
                Console.WriteLine($"1. Использовать меч ({minsd}-{maxsd-1})");
                Console.WriteLine($"2. Использовать лук (5-15), тратится одна стрела.");
                Console.WriteLine($"3. Использовать зелье (Восстанавливает 30 ХП)");
                int move = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                switch (move)
                {
                    case 1:
                        weapon = "Меч";
                        int swordDamage = r.Next(minsd, maxsd);
                        BOSSHP -= swordDamage;
                        Console.WriteLine($"Вы использовали меч и нанесли удар Принцессе. Вы нанесли {swordDamage} урона, у Нее осталось {BOSSHP} ХП");
                        Console.WriteLine();
                        break;
                    case 2:
                        weapon = "Лук";
                        if (arows > 0)
                        {
                            int bowDamage = r.Next(5, 16);
                            BOSSHP -= bowDamage;
                            Console.WriteLine($"Вы использовали лук и нанесли удар Принцессе. Вы нанесли {bowDamage} урона, у Нее осталось {BOSSHP} ХП");
                            arows -= 1;
                            Console.WriteLine($"Вы потратили одну стрелу и у вас осталось {arows} стрел(ы)");
                        }
                        else
                        {
                            Console.WriteLine($"Недастаточно стрел для выстрела, вы пропускаете ход.");
                            weapon = "Лук";
                        }
                        Console.WriteLine();
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
                            weapon = "Меч";
                        }
                        Console.WriteLine();
                        break;
                }
                if (BOSSHP <= 0)
                {
                    Console.WriteLine($"Вы убили Принцессу");
                    EndGame(true);
                    break;
                }
                if (weapon == "Меч")
                {
                    int BOSSAttack = r.Next(BOSSminAttack, BOSSmaxAttack);
                    int doubleBOSSAttack = BOSSAttack * 2;
                    int doubleAttack = r.Next(0, 101);
                    if (doubleAttack >= 90)
                    {
                        Console.WriteLine($"Принцесса разозлилась и рубанула вас топором дважды ({doubleBOSSAttack} ХП)");
                        HP -= doubleBOSSAttack;
                        Console.WriteLine($"У вас осталось {HP} ХП");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine($"Принцесса наносит удар ({BOSSAttack} ХП)");
                        HP -= BOSSAttack;
                        Console.WriteLine($"У вас осталось {HP} ХП");
                        Console.WriteLine();
                    }
                }
                else if (weapon == "Лук")
                {
                    int BOSSAttack = r.Next(BOSSminAttack, BOSSmaxAttack);
                    int halfBOSSAttack = BOSSAttack / 2;
                    Console.WriteLine($"Принцесса кинула в вас неведимку, вы получили {halfBOSSAttack} ХП");
                    HP -= halfBOSSAttack;
                    Console.WriteLine($"У вас осталось {HP} ХП");
                    Console.WriteLine();
                }
                else if (weapon == "Зелье")
                { 

                }
                if (HP <= 0)
                {
                    Console.WriteLine($"Вы умерли. ИГРА ОКОНЧЕНА");
                    EndGame(false);
                }
                if (action % 3 == 0)
                {
                    int chans = r.Next(0, 101);
                    if (chans >= 75)
                    {
                        Console.WriteLine($"Принцесса надушилась и восстановила 10 ХП");
                        BOSSHP += 10;
                        Console.WriteLine();
                    }
                }
                action++; 
            }
        }

        static void EndGame(bool isWin)
        {
            if (isWin)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!  ПОЗДРАВЛЯЕМ ВЫ ПОБЕДИЛИ  !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine($"    ++++++    Ваше статистика     ++++++    ");
                ShowStats();
            }
            else
            {
                Console.WriteLine($"Игра окончена");
                Console.WriteLine();
                Console.WriteLine($"    ++++++    Ваше статистика    ++++++    ");
                ShowStats();
            }
        }
        
        static void Main(string[] args)
        {
            StartGame();
        }
    }
}
