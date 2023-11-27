using Lab_7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Lab_7.Program;

namespace Lab_7
{
    public class Inst_account
    {
        private string username; // Нікнейм користувача
        private string password; // Пароль від аккаунту
        private long phone; // Номер телефону до якого прив'язаний аккаунт
        private float post; // Кількість постів
        private int year; // Рік створення аккаунта
        private Account_states status; //стани акаунту

        private static int counter; //лічильник 

        public static int Counter
        {
            get { return counter; }
            set { counter = value; }
        }

        private static int publicCounter;

        public static int PublicCounter
        {
            get { return publicCounter; }
            set { publicCounter = value; }
        }

        public static void PublicCount(int PublicCounter)
        {
            Console.WriteLine($"Кількість Public акаунтів = {publicCounter}");
        }

        public static Inst_account Parse(string s)
        {
            Inst_account acc = new Inst_account();
            string[] parts;

            parts = s.Split(' ');

            try
            {
                acc.Username = parts[0];
                acc.Password = parts[1];
                acc.Phone = long.Parse(parts[2]);
                acc.Post = float.Parse(parts[3]);
                acc.Year = int.Parse(parts[4]);
                acc.Status = (Account_states)Enum.Parse(typeof(Account_states), parts[5]);
            }
            catch (FormatException ex)
            {
                throw new FormatException($"Помилка при розборі рядка: {ex.Message}", ex);
            }

            return acc;
        }

        public static bool TryParse(string s, out Inst_account acc)
        {
            acc = null;
            try
            {
                acc = Parse(s);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"{Username} {Password} {Phone} {Post} {Year} {(int)Status}";
        }

        public Inst_account()
        {
            username = "";
            password = "";
            phone = 0;
            post = 0;
            year = 0;
            status = Account_states.Undefined;
        }

        public Inst_account(string _username, string _password, long _phone, float _post, int _year, Account_states _status)
        {
            username = _username;
            password = _password;
            phone = _phone;
            post = _post;
            year = _year;
            status = _status;
        }

        public Inst_account(string _username, string _password) : this(_username, _password, 0, 0, 0, Account_states.Public)
        {
            //Викликаю власний конструктор для ініціалізації тільки двох полів, інщі поля залишаються за замовчуванням
        }

        public string Username
        {
            get { return username; }
            set
            {
                if (value.Length >= 1 && value.Length <= 30 && Regex.IsMatch(value, "^[a-zA-Z0-9_.]+$"))
                {
                    username = value;
                }
                else
                {
                    Console.WriteLine("Некоректна довжина нікнейма або недопустимі символи. Введіть нове значення.");
                }
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (value.Length >= 8 && Regex.IsMatch(value, @"^[a-zA-Z0-9]+$"))
                {
                    password = value;
                }
                else
                {
                    Console.WriteLine("Некоректна довжина паролю або недопустимі символи. Введіть нове значення.");
                }
            }
        }

        public long Phone
        {
            get { return phone; }
            set
            {
                string phoneStr = value.ToString();
                if (phoneStr.Length == 9 && Regex.IsMatch(phoneStr, @"^[0-9]+$"))
                {
                    phone = value;
                }
                else
                {
                    Console.WriteLine("Некоректний номер телефону. Введіть нове значення.");
                }
            }
        }


        public float Post
        {
            get { return post; }
            set
            {
                if (value >= 0)
                {
                    post = value;
                }
                else
                {
                    Console.WriteLine("Кількість постів не може бути від'ємною. Введіть нове значення.");
                }
            }
        }

        public int Year
        {
            get { return year; }
            set
            {
                if (value >= 2010 && value <= 2023)
                {
                    year = value;
                }
                else
                {
                    Console.WriteLine("Некоректний рік. Введіть коректний рік між 2010 і 2023. Введіть нове значення.");
                }
            }
        }

        public Account_states Status
        {
            get { return status; }
            set
            {
                if (Enum.IsDefined(typeof(Account_states), value))
                {
                    status = value;
                }
                else
                {
                    Console.WriteLine("Оберіть коректний статус зі списку.");
                }
            }
        }


        public void ChangeUsername(string newUsername)
        {
            Username = newUsername;
        }

        public void ChangePassword(string newPassword)
        {
            Password = newPassword;
        }

        public void NewPost(float numberOfPosts)
        {
            Post += numberOfPosts;
        }

        //перевантажені версії методу NewPost:
        public void NewPost(int numberOfPosts)
        {
            Post += numberOfPosts;
        }

        public void NewPost(byte numberOfPosts)
        {
            Post += numberOfPosts;
        }
    }
}
