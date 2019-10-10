using System;

namespace AIS
{
    class AISOptymize
    {
        private AntyBody[] abPopulation;
        public int PopulationAbSize = 100;

        public AISOptymize(int populationAbSize)
        {
            this.PopulationAbSize = populationAbSize;
            abPopulation = new AntyBody[populationAbSize];
            for (int i = 0; i < populationAbSize; i++)
            {
                abPopulation[i] = new AntyBody(i);
                abPopulation[i].SetAffinity();
                //Console.WriteLine("AntyBody #" + i + " has parameters: x1=" + population[i].x1 + " and x2=" + population[i].x2 + " and affinity: " + population[i].result);
            }
            OrderPopulation();
        }

        public void ShowPopulation(int amount)
        {
            if (amount > PopulationAbSize)
                amount = PopulationAbSize;

            for (int i = 0; i < amount; i++)
            {
                Console.WriteLine("AntyBody #" + i + " has parameters: x1=" + abPopulation[i].x1 + " and x2=" + abPopulation[i].x2 + " and affinity: " + abPopulation[i].result);
            }
            Console.WriteLine("\n");
        }

        private void OrderPopulation()
        {
            for (int i = 0; i < PopulationAbSize; i++)
                for (int j = 0; j < i; j++)
                {
                    AntyBody temp;
                    if (abPopulation[i].result > abPopulation[j].result)
                    {
                        temp = abPopulation[j];
                        abPopulation[j] = abPopulation[i];
                        abPopulation[i] = temp;
                    }
                }
        }

        internal void Train(int minProb, int genNotChange, int amountBest, int cloneAmount, int worstAmount)
        {
            int GenNotChangeCurrent = 0;
            int gen = 0;
            double afflast = 0;
            double affNew = 0;
            //for (int gen = 0; gen < 10000; gen++)
            while (GenNotChangeCurrent < genNotChange)
            {
                // формирование нового поколения антител, путем клонирования некоторого количества лучших антител, 
                // мутации клонов, и если клон лучше оригинала - заменить оригинал на его чуть измененную копию.
                for (int i = 0; i < amountBest; i++)
                {
                    AntyBody abForClone = new AntyBody(abPopulation[i]);
                    for (int j = 0; j < cloneAmount; j++)
                    {
                        AntyBody clone = abForClone.Clone();
                        clone.Mutate(i + gen, minProb);
                        clone.SetAffinity();

                        if (clone.result > abPopulation[i].result)
                        {
                            abPopulation[i] = clone;
                        }
                    }
                }

                for(int i=0;i< worstAmount;i++)
                {
                    abPopulation[PopulationAbSize - 1 - i] = new AntyBody(i);
                    abPopulation[PopulationAbSize - 1 - i].SetAffinity();
                }

                // снова отсортировать популяцию, поскольку лучшая часть антител могла существенно измениться.
                OrderPopulation();

                // получаем среднее значение аффинности - оно должно с каждым поколением становится все лучше и лучше - иначе что-то не так!!!
                //affNew = GetAvgAffinity();

                Console.WriteLine("Generation " + gen + " Best Antybody " + abPopulation[0].ToString() + " Average affinnity : " + GetAvgAffinity(amountBest).ToString("F2"));
                // приращение текущего числа поколений, на которых аффинность (лучшая или средняя по популяции) не менялась - это важно для выхода из цикла. 
                // лучше смотреть не по средней аффинности популяции, а по изменению аффинности лучшего антитела - она сойдется однозначно быстрее, т.е. за меньшее число поколений.
                affNew = abPopulation[0].result;
                if (afflast == affNew)
                    GenNotChangeCurrent++;
                else
                    GenNotChangeCurrent = 0;

                gen++;
                afflast = affNew;
            }
        }

        /// <summary>
        /// Возьмем среднюю аффинность, но не по всем подряд, а только по лучшим - остальные у нас все равно не участвуют.
        /// </summary>
        /// <param name="amountBest"></param>
        /// <returns></returns>
        private double GetAvgAffinity(int amountBest)
        {
            double sum = abPopulation[0].result;
            for (int i = 1; i < amountBest; i++)
            {
                sum += abPopulation[i].result;
            }
            return sum / PopulationAbSize;

        }

        internal void ShowBestOfTheBest()
        {
            Console.WriteLine("The Best Antybody: " + abPopulation[0]);
        }
    }
}
