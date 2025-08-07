using System;
using System.Collections.Generic;

namespace gaming_project
{
    class Program
    {
        static List<string> inventory = new List<string>() { "Web Shooter", "Health Pack", "Arc Reactor" };
        static int[] stats = new int[3] { 100, 50, 25 }; // Health, Energy, Strength
        static string heroName;
        static int xp = 0;
        static int level = 1;

        static Random rng = new Random();

        static void Main(string[] args)
        {
            Console.Write("Choose your Marvel Hero Name: ");
            heroName = Console.ReadLine()!;
            Console.WriteLine($"\n🦸 Welcome, {heroName}! The world needs saving.\n");

            bool running = true;
            while (running)
            {
                DisplayMenu();

                try
                {
                    Console.Write("Choose an option: ");
                    string input = Console.ReadLine()!;
                    int choice = int.Parse(input);

                    switch (choice)
                    {
                        case 1:
                            ViewInventory();
                            break;
                        case 2:
                            UseItem();
                            break;
                        case 3:
                            ViewStats();
                            break;
                        case 4:
                            TakeDamage();
                            break;
                        case 5:
                            CastAbility();
                            break;
                        case 6:
                            EncounterVillain();
                            break;
                        case 7:
                            Console.WriteLine("Exiting the game. Avengers, disassemble!");
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Try again.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid number.");
                }

                Console.WriteLine();
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("====== MARVEL HERO MENU ======");
            Console.WriteLine("1. View Inventory");
            Console.WriteLine("2. Use Item");
            Console.WriteLine("3. View Stats");
            Console.WriteLine("4. Take Damage");
            Console.WriteLine("5. Cast Ability");
            Console.WriteLine("6. Encounter Villain");
            Console.WriteLine("7. Exit Game");
        }

        static void ViewInventory()
        {
            Console.WriteLine("\n🧰 Your Hero Gear:");
            if (inventory.Count == 0)
            {
                Console.WriteLine("Your gear bag is empty!");
            }
            else
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {inventory[i]}");
                }
            }
        }

        static void UseItem()
        {
            ViewInventory();
            if (inventory.Count == 0) return;

            Console.Write("Enter the number of the gear to use: ");
            try
            {
                int itemIndex = int.Parse(Console.ReadLine()!) - 1;

                if (itemIndex >= 0 && itemIndex < inventory.Count)
                {
                    string item = inventory[itemIndex];
                    Console.WriteLine($"You activated {item}!");

                    if (item.ToLower().Contains("health"))
                        Heal(30);
                    else if (item.ToLower().Contains("reactor"))
                        Recharge(20);

                    inventory.RemoveAt(itemIndex);
                }
                else
                {
                    Console.WriteLine("That gear doesn't exist.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter a valid number.");
            }
        }

        static void ViewStats()
        {
            Console.WriteLine($"\n🧬 {heroName}'s Stats (Level {level})");
            Console.WriteLine($"❤️ Health: {stats[0]}");
            Console.WriteLine($"⚡ Energy: {stats[1]}");
            Console.WriteLine($"💪 Strength: {stats[2]}");
            Console.WriteLine($"⭐ XP: {xp}/100");
        }

        static void TakeDamage()
        {
            int damage = GetRandomDamage(15, 35);
            stats[0] -= damage;
            if (stats[0] < 0) stats[0] = 0;

            Console.WriteLine($"💥 You took {damage} damage!");
            Console.WriteLine($"❤️ Current Health: {stats[0]}");

            if (stats[0] == 0)
            {
                Console.WriteLine("☠️ You've been knocked out! S.H.I.E.L.D will recover your body...");
                Console.WriteLine("Exiting the game. Avengers, disassemble!");
                Console.WriteLine();
                Environment.Exit(0); //Ends
            }
        }

        static void Heal(int amount)
        {
            stats[0] += amount;
            Console.WriteLine($"🩹 Healed {amount} points. Health: {stats[0]}");
        }

        static void Recharge(int amount)
        {
            stats[1] += amount;
            Console.WriteLine($"🔋 Recharged {amount} energy. Energy: {stats[1]}");
        }

        static void CastAbility()
        {
            if (stats[1] >= 20)
            {
                stats[1] -= 20;
                int damage = stats[2] + rng.Next(5, 15);
                Console.WriteLine($"🌀 You cast a powerful ability, dealing {damage} damage to your enemy!");
                GainXP(30);
            }
            else
            {
                Console.WriteLine("⚠️ Not enough energy to use a special ability.");
            }
        }

        static void EncounterVillain()
        {
            string[] villains = { "Loki", "Ultron", "Red Skull", "Thanos" };
            string villain = villains[rng.Next(villains.Length)];
            Console.WriteLine($"👹 Villain Appeared: {villain}!");

            int villainDamage = GetRandomDamage(10, 25);
            TakeDamage();
            Console.WriteLine($"You fought {villain} and survived. You gain XP!");
            GainXP(50);
        }

        static void GainXP(int amount)
        {
            xp += amount;
            Console.WriteLine($"⭐ You gained {amount} XP. Total XP: {xp}");

            while (xp >= 100)
            {
                LevelUp();
                xp -= 100;
            }

            CheckForVictory(); // 👈 Win check added here
        }

        static void LevelUp()
        {
            level++;
            stats[2] += 5; // increase strength
            stats[0] += 20; // bonus health
            Console.WriteLine($"\n🎉 LEVEL UP! You are now Level {level}.");
            Console.WriteLine("💪 Strength and ❤️ Health increased!");
        }

        // 🔥 Victory check added!
        static void CheckForVictory()
        {
            if (level >= 5)
            {
                Console.WriteLine("\n🏆 VICTORY!");
                Console.WriteLine($"🎖️ {heroName}, you've reached Level {level} and saved the universe from evil!");
                Console.WriteLine("🛡️ The Avengers celebrate your triumph!");
                Environment.Exit(0);
            }
        }

        // Overloaded methods for random damage
        static int GetRandomDamage(int max)
        {
            return rng.Next(1, max + 1);
        }

        static int GetRandomDamage(int min, int max)
        {
            return rng.Next(min, max + 1);
        }
    }
}