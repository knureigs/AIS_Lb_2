using System;
using AIS;
using Gray;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVS_Lb2
{
    class Program
    {
        /// <summary>
        /// Размер популяции антител (без учета клеток памяти).
        /// </summary>
        private const int PopulationAbSize = 100;

        /// <summary>
        /// Максимальная вероятность мутации.
        /// </summary>
        private const int MutationProbability = 50;

        /// <summary>
        /// Критерий останова. Число поколений, на протяжении которых не происходило улучшения результата.
        /// </summary>
        private const int EndingGenerationPersistentAffinnity = 15000;

        /// <summary>
        /// Количество антител в популяции, которые будут клонироваться.
        /// </summary>
        private const int BestAbForCloneAmount = 10;

        /// <summary>
        /// Число клонов одного антитела.
        /// </summary>
        private const int CloneAmount = 5;

        static void Main(string[] args)
        {
            GrayTest();


            AISOptymize ais = new AISOptymize(PopulationAbSize);
            //ais.ShowPopulation(10);
            ais.Train(MutationProbability, EndingGenerationPersistentAffinnity, BestAbForCloneAmount, CloneAmount, 2);
            ais.ShowBestOfTheBest();

            Console.ReadLine();
        }

        private static void GrayTest()
        {
            Byte i;
            Console.WriteLine("Table #1");
            Console.Write("Number\tBinary\tGrey\n");
            for (i = 0; i < 8; i++)
            {
                Console.Write(i + ": \t");
                Console.Write(GrayCode.BinStringFormat(Convert.ToString(i, 2)) + "\t");
                Console.Write(GrayCode.BinStringFormat(Convert.ToString(i ^ (i >> 1), 2)));
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Table #2");
            Console.Write("Grey\tBinary\tNumber\n");

            // заполняем массив строк кодами Грея
            String[] arrayCodeGrey = new String[8];
            for (i = 0; i < 8; i++)
                arrayCodeGrey[i] = GrayCode.BinStringFormat(Convert.ToString(i, 2));

            for (i = 0; i < 8; i++)
            {
                Console.Write(arrayCodeGrey[i] + ": \t");

                // берем элемент массива кодов Грея, конвертируем в числовое представление, 
                // преобразовываем в обычное число через функцию Grey2Bin, 
                // конвертируем обратно в форматированную строку
                Console.Write(GrayCode.BinStringFormat(Convert.ToString(GrayCode.Grey2Bin(Convert.ToByte(arrayCodeGrey[i], 2)), 2)) + "\t");
                Console.Write(GrayCode.Grey2Bin(Convert.ToByte(arrayCodeGrey[i], 2)));
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
