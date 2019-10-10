using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gray
{
    static public class GrayCode
    {
        /// <summary>
        /// преобразование бинарной строки в форматированную на 3 разряда
        /// </summary>
        static public String BinStringFormat(String s)
        {
            if (s.Length == 3) return s;
            if (s.Length == 2) return "0" + s;
            return "00" + s;
        }
        /// <summary>
        ///  конвертер из кода грея в обычное число
        /// </summary>
        static public Byte Grey2Bin(Byte num)
        {
            Byte res = 0;
            while (num > 0)
            {
                res ^= num;
                num >>= 1;
            }
            return res;
        }

        public static string ConvertTo2(string num, int round = 5)
        {
            string result = ""; //Результат
            int left = 0; //Целая часть
            int right = 0; //Дробная часть
            string[] temp1 = num.Split(new char[] { '.', ',' }); //Нужна для разделения целой и дробной частей
            left = Convert.ToInt32(temp1[0]);
            //Проверяем имеется ли у нас дробная часть
            if (temp1.Count() > 1)
            {
                right = Convert.ToInt32(num.Split(new char[] { '.', ',' })[1]); //Дробная часть
            }
            //Алгоритм перевода целой части в двоичную систему
            while (true)
            {
                result += left % 2; //В ответ помещаем остаток от деления. В конце программы мы перевернём строку, так как в обратном порядке записываются остатки
                left = left / 2; //Так как Left целое число, то при делении например числа 2359 на 2, мы получим не 1179,5 а 1179
                if (left == 0)
                    break;
            }
            result = new string(result.ToCharArray().Reverse().ToArray()); //Реверсирование строки
            /*Не углублялся в ситуацию, но вдруг при реверсе появятся первые символы нули, а ведь их мы не пишем!
            Не знаю есть ли необходимость в этом цикле */
            while (true)
            {
                int i = 0;
                if (result[i] == '0')
                    result = result.Remove(i, 1);
                else break;
            }
            //Прокеряем есть ли у нас дробная часть, можно было бы проверить и так if(temp1.count()>1)
            if (right == 0)
                return result;

            //Добавляем разделить целой части от дробной
            result += '.';

            int count = right.ToString().Count(); // Нам нужно знать кол-во цифр, при превышении которого дописывается единичка

            for (int i = 0; i < round; i++)
            {
                /*Умножаем число на 2 и проверяем, стало ли оно больше по количеству цифр, если да,
                то в результат идёт "1" и первая цифра у right удаляется */
                right = right * 2;
                if (right.ToString().Count() > count)
                {
                    string buf = right.ToString();
                    buf = buf.Remove(0, 1);
                    right = Convert.ToInt32(buf);

                    result += '1';
                }
                else
                {
                    result += '0';
                }
            }
            return result;
        }
    }
}
