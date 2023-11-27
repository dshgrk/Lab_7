using Lab_7;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Text;
using System.Text.Json;

namespace Lab_7
{
    public class Program
    {
        private const string TxtFilePath = "E:\\УНИВЕРСИТЕТ\\2 курс\\ООП\\Lab7_Гірка_program\\Lab_7\\file.txt";
        private const string JsonFilePath = "E:\\УНИВЕРСИТЕТ\\2 курс\\ООП\\Lab7_Гірка_program\\Lab_7\\file.json";

        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.GetEncoding(1251);
            Console.WriteLine("Введіть максимальну кількість об'єктів: ");
            int N = int.Parse(Console.ReadLine());
            List<Inst_account> accounts = new List<Inst_account>();

            while (true)
            {
                Console.WriteLine("1 – додати об’єкт\r\n2 – вивести на екран об’єкти\r\n3 – знайти об’єкт\r\n4 – видалити об’єкт\r\n" +
                    "5 – демонстрація поведінки\r\n6 - демонстрація роботи static методів\r\n7 - зберегти колекцію об’єктів у файлі\r\n8 – зчитати колекцію об’єктів з файлу\r\n9 - очистити колекцію об’єктів\r\n0 – вийти з програми");
                Console.WriteLine("Введіть пункт меню --> ");
                int input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 0:
                        Console.WriteLine("Роботу програми завершено");
                        Environment.Exit(0);
                        break;
                    case 1:
                        if (Inst_account.Counter < N)
                        {
                            Inst_account acc;
                            Console.WriteLine("Введіть такі характеристики класу через пробіл:\r\nНікнейм\r\nПароль\r\nНомер телефону\r\nКількість постів\r\nРік створення\r\nСтан акаунту(Public=1,Private=2,Business=3,Personal=4)");
                            string s = Console.ReadLine();

                            bool AddOrNot = Add(s, ref accounts);

                            if (AddOrNot)
                            {
                                Console.WriteLine("Об'єкт додано");
                            }
                            else
                            {
                                Console.WriteLine("Помилка при додаванні об'єкта");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Досягнуто максимальну кількість об'єктів.");
                        }
                        break;

                    case 2:
                        if (accounts.Count >= 1)
                        {
                            Console.WriteLine("Список об'єктів:");
                            foreach (var acc in accounts)
                            {
                                Console.WriteLine($"\r\nНікнейм: {acc.Username}, \r\nСтатус: {acc.Status}, \r\nПароль: {acc.Password}, \r\nНомер телефону: {acc.Phone}, \r\nК-сть постів: {acc.Post}, \r\nРік створення: {acc.Year}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Об'єкти не знайдено");
                        }

                        Console.WriteLine("\nКількість об'єктів = " + accounts.Count);

                        break;
                    case 3:
                        Console.WriteLine("Оберіть характеристику для пошуку: 1 - Нікнейм, 2 - Кількість постів");
                        int searchChoice = int.Parse(Console.ReadLine());
                        Inst_account foundAccount;

                        if (searchChoice == 1)
                        {
                            Console.WriteLine("Введіть нікнейм для пошуку: ");
                            string searchUsername = Console.ReadLine();
                            SearchAccount(accounts, searchUsername, out foundAccount);
                            if (foundAccount != null)
                            {
                                Console.WriteLine("Знайдений об'єкт: ");
                                Console.WriteLine($"\r\nНікнейм: {foundAccount.Username}, \n\rСтатус: {foundAccount.Status} , \r\nПароль:  {foundAccount.Password} ," +
                                $" \r\nНомер телефону:  {foundAccount.Phone} , \r\nК-сть постів:  {foundAccount.Post} , \r\nРік створення:  {foundAccount.Year}");
                            }
                            else Console.WriteLine("Акаунта з таким параметром не існує");
                        }
                        else if (searchChoice == 2)
                        {
                            Console.WriteLine("Введіть кількість постів для пошуку: ");
                            float searchPost = float.Parse(Console.ReadLine());
                            SearchAccount(accounts, searchPost, out foundAccount);
                            if (foundAccount != null)
                            {
                                Console.WriteLine("Знайдений об'єкт: ");
                                Console.WriteLine($"\r\nНікнейм: {foundAccount.Username}, \n\rСтатус: {foundAccount.Status} , \r\nПароль:  {foundAccount.Password} ," +
                                $" \r\nНомер телефону:  {foundAccount.Phone} , \r\nК-сть постів:  {foundAccount.Post} , \r\nРік створення:  {foundAccount.Year}");
                            }
                            else Console.WriteLine("Акаунта з таким параметром не існує");
                        }

                        break;
                    case 4:
                        Console.WriteLine("Виберіть спосіб видалення: 1 - за номером у списку, 2 - за ніком");
                        int deleteChoice = int.Parse(Console.ReadLine());

                        if (deleteChoice == 1)
                        {

                            Console.WriteLine("Введіть номер об'єкта для видалення (1 - N): ");
                            int deleteIndex = int.Parse(Console.ReadLine());

                            bool deleteOrNot = ForDelete(deleteIndex, ref accounts);

                            if (deleteOrNot)
                            {
                                Console.WriteLine("\nОб'єкт успішно видалено\n");
                            }
                            else Console.WriteLine("\nОб'єкт не вдалось видалити\n");
                        }
                        else if (deleteChoice == 2)
                        {
                            Console.WriteLine("Введіть нікнейм для пошуку: ");
                            string searchUsername = Console.ReadLine();

                            SearchAccount(accounts, searchUsername, out foundAccount);
                            if (foundAccount != null)
                            {
                                Console.WriteLine("Знайдений об'єкт: ");
                                Console.WriteLine($"\r\nНікнейм: {foundAccount.Username}, \n\rСтатус: {foundAccount.Status} , \r\nПароль:  {foundAccount.Password} ," +
                                $" \r\nНомер телефону:  {foundAccount.Phone} , \r\nК-сть постів:  {foundAccount.Post} , \r\nРік створення:  {foundAccount.Year}");
                            }
                            else Console.WriteLine("\nАкаунта з таким параметром не існує");

                            bool deleteOrNot2 = ForDelete(ref accounts, searchUsername, out foundAccount);

                            if (deleteOrNot2)
                            {
                                Console.WriteLine("\nОб'єкт успішно видалено\n");
                            }
                            else Console.WriteLine("\nОб'єкт не вдалось видалити\n");

                        }
                        else
                        {
                            Console.WriteLine("\nНекоректний вибір");
                        }
                        break;
                    case 5:
                        Console.WriteLine("Оберіть, над яким акаунтом хочете здійснити дію: ");
                        Inst_account selectedAccount = null;

                        Console.WriteLine("Оберіть характеристику для пошуку: 1 - Нікнейм, 2 - Кількість постів");
                        int searchChoice2 = int.Parse(Console.ReadLine());

                        if (searchChoice2 == 1)
                        {
                            Console.WriteLine("Введіть нікнейм для пошуку: ");
                            string searchUsername = Console.ReadLine();
                            selectedAccount = SearchAccount(accounts, searchUsername, out foundAccount);
                            if (foundAccount != null)
                            {
                                Console.WriteLine($"\r\nНікнейм: {foundAccount.Username}, \n\rСтатус: {foundAccount.Status} , \r\nПароль:  {foundAccount.Password} ," +
                                $" \r\nНомер телефону:  {foundAccount.Phone} , \r\nК-сть постів:  {foundAccount.Post} , \r\nРік створення:  {foundAccount.Year}");
                            }
                            else Console.WriteLine("Акаунта з таким параметром не існує");
                        }
                        else if (searchChoice2 == 2)
                        {
                            Console.WriteLine("Введіть кількість постів для пошуку: ");
                            float searchPost = float.Parse(Console.ReadLine());
                            selectedAccount = SearchAccount(accounts, searchPost, out foundAccount);
                            if (foundAccount != null)
                            {
                                Console.WriteLine($"\r\nНікнейм: {foundAccount.Username}, \n\rСтатус: {foundAccount.Status} , \r\nПароль:  {foundAccount.Password} ," +
                                $" \r\nНомер телефону:  {foundAccount.Phone} , \r\nК-сть постів:  {foundAccount.Post} , \r\nРік створення:  {foundAccount.Year}");
                            }
                            else Console.WriteLine("Акаунта з таким параметром не існує");
                        }

                        if (selectedAccount != null)
                        {
                            Console.WriteLine("Оберіть дію для цього акаунту: \r\n1 - Змінити нікнейм, \r\n2 - Змінити пароль, \r\n3 - Викласти новий пост");
                            int actionChoice = int.Parse(Console.ReadLine());

                            switch (actionChoice)
                            {
                                case 1:
                                    Console.Write("Введіть новий нікнейм - ");
                                    string newUsername = Console.ReadLine();
                                    selectedAccount.Username = newUsername;
                                    Console.WriteLine("Нікнейм змінено.");
                                    break;

                                case 2:
                                    Console.Write("Введіть новий пароль - ");
                                    string newPassword = Console.ReadLine();
                                    selectedAccount.Password = newPassword;
                                    Console.WriteLine("Пароль змінено");
                                    break;

                                case 3:
                                    Console.Write("Введіть кількість нових постів - ");
                                    string postInput = Console.ReadLine();

                                    byte byteNewPosts;
                                    int intNewPosts;
                                    float floatNewPosts;

                                    if (byte.TryParse(postInput, out byteNewPosts))
                                    {
                                        selectedAccount.NewPost(byteNewPosts);
                                        Console.WriteLine("Нові пости додано (byte)");
                                    }
                                    else if (int.TryParse(postInput, out intNewPosts))
                                    {
                                        selectedAccount.NewPost(intNewPosts);
                                        Console.WriteLine("Нові пости додано (int)");
                                    }
                                    else if (float.TryParse(postInput, out floatNewPosts))
                                    {
                                        selectedAccount.NewPost(floatNewPosts);
                                        Console.WriteLine("Нові пости додано (float)");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Введено некоректне значення");
                                    }
                                    break;

                                default:
                                    Console.WriteLine("Некоректний вибір дії");
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Аккаунт не знайдено");
                        }
                        break;

                    case 6:
                        Inst_account.PublicCount(Inst_account.PublicCounter);
                        break;

                    case 7:
                        Console.WriteLine("1 – зберегти у файл *.txt\r\n2 – зберегти у файл *.json");
                        int saveChoice = int.Parse(Console.ReadLine());
                        switch (saveChoice)
                        {
                            case 1:
                                SaveToFileTxt(accounts, TxtFilePath);
                                break;
                            case 2:
                                SaveToFileJson(accounts, JsonFilePath);
                                break;
                            default:
                                Console.WriteLine("Некоректний вибір");
                                break;
                        }
                        break;

                    case 8:
                        Console.WriteLine("1 – зчитати з файлу *.txt\r\n2 – зчитати з файлу *.json");
                        int loadChoice = int.Parse(Console.ReadLine());
                        switch (loadChoice)
                        {
                            case 1:
                                ReadFromFileTxt(ref accounts, TxtFilePath);
                                break;
                            case 2:
                                ReadFromFileJson(ref accounts, JsonFilePath);
                                break;
                            default:
                                Console.WriteLine("Некоректний вибір");
                                break;
                        }
                        break;

                    case 9:
                        accounts.Clear();
                        Console.WriteLine("Об'єкти очищено");
                        break;
                
                }
            }
        }

        //Методи які не використовують ввід чи вивід даних
        public static bool Add(string s, ref List<Inst_account> accounts)
        {
            Inst_account acc;
            if (Inst_account.TryParse(s, out acc))
            {
                if (!(string.IsNullOrEmpty(acc.Username) || string.IsNullOrEmpty(acc.Password) || acc.Phone == 0 || acc.Post == 0 || acc.Year == 0 || acc.Status == 0))
                {
                    accounts.Add(acc);

                    Inst_account.Counter++;

                    if (acc.Status == Account_states.Public)
                    {
                        Inst_account.PublicCounter++;
                    }

                    return true;
                }
            }
            return false;
        }


        public static Inst_account SearchAccount(List<Inst_account> accounts, string searchUsername, out Inst_account foundAccount)
        {
            foundAccount = accounts.FirstOrDefault(acc => acc.Username == searchUsername);
            if (foundAccount != null)
            {
                return foundAccount;
            }
            else
            {
                return foundAccount = null;
            }
        }


        public static Inst_account SearchAccount(List<Inst_account> accounts, float searchPost, out Inst_account foundAccount)
        {
            foundAccount = accounts.FirstOrDefault(acc => acc.Post == searchPost);
            if (foundAccount != null)
            {
                return foundAccount;
            }
            else
            {
                return foundAccount = null;
            }
        }

        public static bool ForDelete(int deleteIndex, ref List<Inst_account> accounts)
        {
            if (deleteIndex >= 0 && deleteIndex <= accounts.Count)
            {
                Inst_account foundAccount = accounts[deleteIndex - 1];
                accounts.Remove(foundAccount);
                Inst_account.Counter--;
                if (foundAccount.Status == Account_states.Public) Inst_account.PublicCounter--;

                return true;
            }
            return false;
        }

        public static bool ForDelete(ref List<Inst_account> accounts, string searchUsername, out Inst_account foundAccount)
        {
            Lab_7.Program.SearchAccount(accounts, searchUsername, out foundAccount);
            if (foundAccount != null)
            {
                accounts.Remove(foundAccount);
                Inst_account.Counter--;
                if (foundAccount.Status == Account_states.Public) Inst_account.PublicCounter--;
                return true;
            }
            else return false;
        }




        //

        public static void SaveToFileTxt(List<Inst_account> accounts, string path)
        {
            try
            {
                
                using (StreamWriter sw = new StreamWriter(path))
                {
                    foreach (var acc in accounts)
                    {
                        if (!(string.IsNullOrEmpty(acc.Username) || string.IsNullOrEmpty(acc.Password) || acc.Phone == 0 || acc.Post == 0 || acc.Year == 0 || acc.Status == 0))
                        {
                            sw.WriteLine(acc.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при збереженні у файл *.txt: {ex.Message}");
            }
        }

        public static void SaveToFileJson(List<Inst_account> accounts, string path)
        {
            try
            {
                bool a = false;
                string json = JsonSerializer.Serialize(accounts, new JsonSerializerOptions { WriteIndented = true });

                using (StreamWriter file = new StreamWriter(path))
                {
                    foreach (var acc in accounts)
                    {
                        if (!(string.IsNullOrEmpty(acc.Username) || string.IsNullOrEmpty(acc.Password) || acc.Phone == 0 || acc.Post == 0 || acc.Year == 0 || acc.Status == 0))
                        {
                            a = true;
                        }
                    }
                    if (a)
                    {
                        file.Write(json);
                    }
                    else Console.WriteLine("Невірні дані");

                }
                

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при збереженні у файл *.json: {ex.Message}");
            }
        }



        public static void ReadFromFileTxt(ref List<Inst_account> accounts, string path)
        {
            try
            {
                if (File.Exists(TxtFilePath))
                {
                    string[] lines = File.ReadAllLines(path);
                    foreach (var line in lines)
                    {
                        if (Inst_account.TryParse(line, out Inst_account acc))
                        {
                            if (!(string.IsNullOrEmpty(acc.Username) || string.IsNullOrEmpty(acc.Password) || acc.Phone == 0 || acc.Post == 0 || acc.Year == 0 || acc.Status == 0))
                            {
                                accounts.Add(acc);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Не вдалося десеріалізувати рядок");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Файл *.txt не існує.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при зчитуванні з файлу *.txt: {ex.Message}");
            }
        }


        public static void ReadFromFileJson(ref List<Inst_account> accounts, string path)
        {
            try
            {
                if (File.Exists(path)) // Исправлено: использование переменной path вместо JsonFilePath
                {
                    string json = File.ReadAllText(path);
                    List<Inst_account> loadedAccounts = JsonSerializer.Deserialize<List<Inst_account>>(json);

                    if (loadedAccounts != null)
                    {
                        foreach (var acc in loadedAccounts)
                        {
                            if (!(string.IsNullOrEmpty(acc.Username) || string.IsNullOrEmpty(acc.Password) || acc.Phone == 0 || acc.Post == 0 || acc.Year == 0 || acc.Status == 0))
                            {
                                accounts.Add(acc);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Помилка при десеріалізації з файлу *.json");
                    }
                }
                else
                {
                    Console.WriteLine("Файл *.json не існує.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при зчитуванні з файлу: {ex.Message}");
            }
        }



        public enum Account_states
        {
            Undefined = 0,
            Public,
            Private,
            Business,
            Personal
        }
    }
}

