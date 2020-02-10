using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRDSDH2
{
    class Program
    {
        static public int recordLenght = 7;
        static public int amountOfRecs = 0;
        static public List <Record>[] records = new List<Record>[10];
        static void Main(string[] args)
        {
            
            List<string> kolKey = new List<string>();
            for (int i = 0; i < records.Length; i ++)
            {
                records[i] = new List<Record>();
            }
            Console.Write("1.Добавить запись\n2.Найти запись\n3.Список ключей вызывающих колизии\n0.Закончить работу\nВыберите нужную функцию: ");
            string option = Console.ReadLine();
            while (!option.Equals("0"))
            {
                if (option.Equals("1"))
                {
                    addRecord();

                }


                if (option.Equals("2"))
                {
                    Console.Write("Введите ключ записи: ");
                    string newKey = Console.ReadLine();
                    int Index = getIndex(newKey);

                    bool done = false;
                    foreach (Record a in records[Index])
                    {
                        if (a.key.Equals(newKey))
                        {
                            done = true;
                            Console.WriteLine("Найденная запись: "+ a.data + "(Ключ: " + a.key + ", Индекс: " + Index + ")");

                            if (records[Index].Count > 1)
                            {
                                Console.Write("Колизия(" + Index + "): ");
                                foreach (Record b in records[Index])
                                {
                                    Console.Write(b.data + "(" + b.key + ") ");
                                }
                                Console.WriteLine("");
                            }
                        }
                    }
                    if (!done)
                    {
                        Console.WriteLine("Запись не найдена.");
                    }
                }

                if (option.Equals("3"))
                {
                    Console.Write("Ключи которые вызывают колизии: ");

                        foreach (List<Record> a in records)
                        {
                            if (a.Count > 1)
                            {
                                for (int j = 1; j < a.Count; j++)
                                {
                                    Console.Write(a[j].key + " ");
                                }
                            }
                        }
                    
                    Console.WriteLine("");
                }

                Console.Write("Введите нормер следующей функции: ");
                option = Console.ReadLine();
            }
        }

        static public int getIndex(string kkey)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(kkey);
            int index = ((bytes[0] + bytes[bytes.Length - 1]) * bytes.Length) % 10;
            return index;
        }

       
        
        static public void addRecord()
        {
            if (amountOfRecs < 10)
            {
                Console.Write("Введите запись: ");
                string tempData = Console.ReadLine();

                Console.Write("Введите ключ записи: ");
                string tempKey = Console.ReadLine();
                bool done = false;

                while (!done)
                {
                    done = true;
                    for (int i = 0; i < records.Length; i++)
                    {
                        if (records[i] != null)
                        {

                            foreach (Record b in records[i])
                            {
                                if (b.key.Equals(tempKey))
                                {
                                    done = false;
                                    Console.Write("Ключ занят. Введите новый ключ: ");
                                    tempKey = Console.ReadLine();
                                    break;
                                }
                            }

                        }
                        if (!done)
                        {
                            break;
                        }
                    }
                }

                int ind = getIndex(tempKey);
                Record newRecord = new Record(tempData, tempKey);
                records[ind].Add(newRecord);
                if (records[ind].Count > 1)
                {
                    Console.Write("Колизия("+ ind +"): ");
                    foreach (Record a in records[ind])
                    {
                        Console.Write(a.data + "(" + a.key + ") ");
                    }
                    Console.WriteLine("");
                }
                amountOfRecs++;
            }
            else
            {
                Console.WriteLine("Таблица заполнена.");
            }
        }
    }
}
