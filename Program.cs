using System;
using AIS;
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
        private const int MutationProbability = 30;

        /// <summary>
        /// Критерий останова. Число поколений, на протяжении которых не происходило улучшения результата.
        /// </summary>
        private const int EndingGenerationPersistentAffinnity = 150;

        /// <summary>
        /// Количество антител в популяции, которые будут клонироваться.
        /// </summary>
        private const int BestAbForCloneAmount = 10;

        /// <summary>
        /// Число клонов одного антитела.
        /// </summary>
        private const int CloneAmount = 5;

        /// <summary>
        /// Число заменяемых худших антител.
        /// </summary>
        private const int WorstAmount = 2;

        static void Main(string[] args)
        {
            AISOptymize ais = new AISOptymize(PopulationAbSize);
            //ais.ShowPopulation(10);
            ais.Train(MutationProbability, EndingGenerationPersistentAffinnity, BestAbForCloneAmount, CloneAmount, WorstAmount);
            ais.ShowBestOfTheBest();

            Console.ReadLine();
        }
    }
}
