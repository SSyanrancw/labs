using System;
using System.Linq;

namespace Sopra.Labs.ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MostrarTablaMultiplicar();
            MostrarValores();
            CalcularValores();
            CalcularLetraDNI();
        }

        /// <summary>
        /// Muestra la tabla de multiplicar del número indicado
        /// </summary>
        static void MostrarTablaMultiplicar()
        {
            Console.Write("Introduzca el valor de la tabla de multiplicar deseada: ");
            string res = Console.ReadLine();

            int.TryParse(res, out int val);
            int max = 10;

            Console.WriteLine($"Tabla de multiplicar de {val}");
            Console.WriteLine();

            // Utilizando For
            for (int i = 0; i <= max; i++)
            {
                Console.WriteLine($"{val} * {i} = {val*i}");
            }

            Console.WriteLine();

            // Utilizando While
            int n = 0;
            while (n <= max)
            {
                Console.WriteLine($"{val} * {n} = {val * n}");
                n++;
            }

        }

        /// <summary>
        /// Se pide un valor inicial, un valor final y un valor de salto y se imprime
        /// la secuencia.
        /// </summary>
        static void MostrarValores()
        {
            bool valido = false;
            string ini = "";
            string fin = "", salto = "";
            int nIni = 0;
            int nFin = 0;
            int nSalto = 0;

            while (!valido)
            {
                // Se comrpueba para cada valor si es válido, si no se reitera el bucle
                Console.Write("Introduzca el valor inicial: ");
                ini = Console.ReadLine();

                // Valor inicial
                if (int.TryParse(ini, out nIni))
                {
                    Console.Write("Introduzca el valor final: ");
                    fin = Console.ReadLine();

                    // Valor final
                    if (int.TryParse(fin, out nFin))
                    {
                        Console.Write("Introduzca el valor de salto: ");
                        salto = Console.ReadLine();

                        // Valor de salto
                        if (int.TryParse(salto, out nSalto))
                        {
                            // Se comprueba si la secuencia es posible y se imprime la secuencia.
                            if(nIni < nFin && nSalto > 0)
                            {
                                valido = true;
                                for(int i = nIni; i <= nFin; i= i + nSalto)
                                {
                                    Console.WriteLine(i);
                                }
                            } else if(nIni > nFin && nSalto < 0) {
                                valido = true;
                                for (int i = nIni; i >= nFin; i = i + nSalto)
                                {
                                    Console.WriteLine(i);
                                }
                            }
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Se pide al usuario la cantidad de números y sus valores y se calcula max, min, media y suma
        /// </summary>
        static void CalcularValores()
        {
            bool valido = false;
            int cant = 0;
            int[] numeros;
            string res = "";

            while (!valido)
            {
                Console.Write("Introduzca la cantidad de números: ");
                res = Console.ReadLine();
                valido = int.TryParse(res, out cant);
            }

            numeros = new int[cant];

            for(int i = 0; i < cant; i++)
            {
                res = Console.ReadLine();
                if(!int.TryParse(res, out numeros[i]))
                {
                    i--;
                }
            }

            // MAX
            Console.WriteLine($"Valor máximo: {numeros.Max()}");
            // MIN
            Console.WriteLine($"Valor mínimo: {numeros.Min()}");
            // MEDIA
            Console.WriteLine($"Madia: {numeros.Average()}");
            // SUMA
            Console.WriteLine($"Suma: {numeros.Sum()}");
        }

        /// <summary>
        /// Calcular la letra correspondiente al número de DNI
        /// </summary>
        static void CalcularLetraDNI()
        {
            char[] letras = { 'T', 'R', 'W', 'A', 'G', 'M', 'Y', 'F', 'P', 'D', 'X', 'B', 'N', 'J', 'Z', 'S', 'Q', 'V', 'H', 'L', 'C', 'K', 'E' };

            bool valido = false;
            int num = 0;

            while (!valido)
            {
                Console.Write("Introduzca el número de DNI sin la letra: ");
                string res = Console.ReadLine();
                valido = int.TryParse(res, out num);
            }
            int pos = num % 23;
            Console.WriteLine($"La letra del DNI es: {letras[pos]}");
        }
    }
}
