using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace TorresHanoi
{
    class Program
    {
        static Dictionary<char, Stack<int>> towers;
        static int numDiscos = 5;

        static void Main(string[] args)
        {
            InitializeTowers();
            ShowTowers();

            while (!IsGameComplete())
            {
                Console.Write("Ingrese movimiento (ejemplo: AC para mover de torre A a torre C): ");
                string move = Console.ReadLine().ToUpper();

                if (move.Length == 2 && towers.ContainsKey(move[0]) && towers.ContainsKey(move[1]))
                {
                    char source = move[0];
                    char destination = move[1];

                    if (IsValidMove(source, destination))
                    {
                        MoveDisk(source, destination);
                        ShowTowers();
                    }
                    else
                    {
                        Console.WriteLine("Movimiento no válido. Inténtalo de nuevo.");
                    }
                }
                else
                {
                    Console.WriteLine("Entrada incorrecta. Por favor, ingrese un movimiento válido.");
                }
            }

            Console.WriteLine("¡Has completado el juego!");
            Console.ReadKey();
        }

        static void InitializeTowers()
        {
            towers = new Dictionary<char, Stack<int>>();

            for (char c = 'A'; c <= 'C'; c++)
            {
                towers[c] = new Stack<int>();
            }

            for (int i = numDiscos; i >= 1; i--)
            {
                towers['A'].Push(i);
            }
        }

        static void ShowTowers()
        {
            Console.Clear();

            Console.WriteLine("-----|-----|-----");
            for (int i = 1; i <= numDiscos; i++)
            {
                foreach (var tower in towers)
                {
                    char towerName = tower.Key;
                    Stack<int> disks = tower.Value;
                    int disk = disks.Count >= i ? disks.ToArray()[i - 1] : 0;
                    string diskDisplay = new string('X', disk);

                    Console.Write($"{towerName}: {diskDisplay.PadLeft(numDiscos)} ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("  A  |  B  |  C  ");
        }

        static bool IsValidMove(char source, char destination)
        {
            if (!towers.ContainsKey(source) || !towers.ContainsKey(destination))
            {
                return false;
            }

            if (towers[source].Count == 0)
            {
                return false;
            }

            if (towers[destination].Count == 0)
            {
                return true;
            }

            int sourceTopDisk = towers[source].Peek();
            int destinationTopDisk = towers[destination].Peek();

            return sourceTopDisk < destinationTopDisk;
        }

        static void MoveDisk(char source, char destination)
        {
            int diskToMove = towers[source].Pop();
            towers[destination].Push(diskToMove);
        }

        static bool IsGameComplete()
        {
            return towers['C'].Count == numDiscos;
        }
    }
}
