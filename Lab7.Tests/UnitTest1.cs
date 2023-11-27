using Lab_7;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using static Lab_7.Program;

namespace Lab7.Tests
{
    [TestClass]
    public class Inst_account_Tests
    {
        [TestMethod]
        public void CorrectUser()
        {
            //arrange
            Inst_account account = new Inst_account();

            //act
            account.Username = "testUser";

            //assert
            Assert.AreEqual("testUser", account.Username);
        }

        [TestMethod]
        public void WrongUser()
        {
            //arrange
            Inst_account account = new Inst_account();

            //act
            account.Username = "‡·‚„‰";

            //assert
            Assert.AreEqual("", account.Username);
        }

        [TestMethod]
        public void WrongUser2()
        {
            //arrange
            Inst_account account = new Inst_account();

            //act
            account.Username = "+";

            //assert
            Assert.AreEqual("", account.Username);
        }

        [TestMethod]
        public void CorrectPassword()
        {
            //arrange
            Inst_account account = new Inst_account();

            //act
            account.Password = "1234567890";

            //assert
            Assert.AreEqual("1234567890", account.Password);
        }


        [TestMethod]
        public void WrongPassword()
        {
            //arrange
            Inst_account account = new Inst_account();

            //act
            account.Password = "aaa";

            //assert
            Assert.AreEqual("", account.Password);
        }

        [TestMethod]
        public void WrongPassword2()
        {
            //arrange
            Inst_account account = new Inst_account();

            //act
            account.Password = "...";

            //assert
            Assert.AreEqual("", account.Password);
        }

        [TestMethod]
        public void CorrectPhone()
        {
            //arrange
            Inst_account account = new Inst_account();

            //act
            account.Phone = 973499155;

            //assert
            Assert.AreEqual(973499155, account.Phone);
        }

        [TestMethod]
        public void WrongPhone()
        {
            //arrange
            Inst_account account = new Inst_account();

            //act
            account.Phone = 123;

            //assert
            Assert.AreEqual(0, account.Phone);
        }

        [TestMethod]
        public void CorrectPost()
        {
            //arrange
            Inst_account account = new Inst_account();

            //act
            account.Post = 7;

            //assert
            Assert.AreEqual(7, account.Post);
        }

        [TestMethod]
        public void WrongPost()
        {
            //arrange
            Inst_account account = new Inst_account();

            //act
            account.Post = -1;

            //assert
            Assert.AreEqual(0, account.Post);
        }

        [TestMethod]
        public void CorrectYear()
        {
            //arrange
            Inst_account account = new Inst_account();

            //act
            account.Year = 2023;

            //assert
            Assert.AreEqual(2023, account.Year);
        }

        [TestMethod]
        public void WrongYear()
        {
            //arrange
            Inst_account account = new Inst_account();

            //act
            account.Year = 1991;

            //assert
            Assert.AreEqual(0, account.Year);
        }


        [TestMethod]
        public void CorrectStatus()
        {
            //arrange
            Inst_account account = new Inst_account();

            //act
            account.Status = (Account_states)4;

            //assert
            Assert.AreEqual((Account_states)4, account.Status);
        }

        [TestMethod]
        public void WrongStatus()
        {
            //arrange
            Inst_account account = new Inst_account();

            //act
            account.Status = (Account_states)8888;

            //assert
            Assert.AreEqual((Account_states)0, account.Status);
        }

        [TestMethod]
        public void CorrectChangeUser()
        {
            //arrange
            Inst_account account = new Inst_account("qwerty", "1111111110", 5071234567, 4, 2020, (Account_states)3);

            //act
            string newUsername = "zxcvbnm";
            account.ChangeUsername(newUsername);

            //assert
            Assert.AreEqual(newUsername, account.Username);
        }

        [TestMethod]
        public void WrongChangeUser()
        {
            //arrange
            Inst_account account = new Inst_account("qwerty", "1111111110", 5071234567, 4, 2020, (Account_states)3);

            //act
            string newUsername = "˘Ï˘‡‚ÁÒÏ˘";
            account.ChangeUsername(newUsername);

            //assert
            Assert.AreEqual("qwerty", account.Username);
        }

        [TestMethod]
        public void CorrectChangePassword()
        {
            //arrange
            Inst_account account = new Inst_account("qwerty", "1111111110", 5071234567, 4, 2020, (Account_states)3);

            //act
            string newPassword = "55555555555";
            account.ChangePassword(newPassword);

            //assert
            Assert.AreEqual(newPassword, account.Password);
        }

        [TestMethod]
        public void WrongChangePassword()
        {
            //arrange
            Inst_account account = new Inst_account("qwerty", "1111111110", 5071234567, 4, 2020, (Account_states)3);

            //act
            string newPassword = "123";
            account.ChangePassword(newPassword);

            //assert
            Assert.AreEqual("1111111110", account.Password);
        }

        [TestMethod]
        public void CorrectNewPost()
        {
            //arrange
            Inst_account account = new Inst_account("qwerty", "1111111110", 5071234567, 4, 2020, (Account_states)3);

            //act
            int numberOfPosts = 6;
            int expected = 10;
            account.NewPost(numberOfPosts);

            //assert
            Assert.AreEqual(account.Post, expected);
        }

        [TestMethod]
        public void WrongNewPost()
        {
            //arrange
            Inst_account account = new Inst_account("qwerty", "1111111110", 5071234567, 4, 2020, (Account_states)3);

            //act
            int numberOfPosts = -22;
            account.NewPost(numberOfPosts);

            //assert
            Assert.AreEqual(4, account.Post);
        }

        [TestMethod]
        public void CorrectTryParse()
        {
            //arrange
            string input = "validUsername validPassword 123456789 5 2022 Public";

            //act
            bool result = Inst_account.TryParse(input, out Inst_account account);

            //assert
            Assert.IsTrue(result);
            Assert.IsNotNull(account);
            Assert.AreEqual("validUsername", account.Username);
            Assert.AreEqual("validPassword", account.Password);
            Assert.AreEqual(123456789, account.Phone);
            Assert.AreEqual(5, account.Post);
            Assert.AreEqual(2022, account.Year);
            Assert.AreEqual(Account_states.Public, account.Status);
        }

        [TestMethod]
        public void WrongTryParse()
        {
            //arrange
            string input = "Invalid Input";

            //act
            bool result = Inst_account.TryParse(input, out Inst_account account);

            //assert
            Assert.IsFalse(result);
            Assert.IsNull(account);
        }


        // “ÂÒÚ˚ Í 6 Î‡·Â

        [TestMethod]
        public void CorrectAdd()
        {
            //arrange
            List<Inst_account> accounts = new List<Inst_account>();
            string input = "qwerty 555555555 0971234545 5 2020 1";

            //act
            bool forAdd = Lab_7.Program.Add(input, ref accounts);

            //assert
            Assert.IsTrue(forAdd);
            Assert.AreEqual(1, accounts.Count);
            Assert.AreEqual(1, Inst_account.PublicCounter);
        }

        [TestMethod]
        public void WrongAdd()
        {
            //arrange
            List<Inst_account> accounts = new List<Inst_account>();
            string input = "";

            //act
            bool forAdd = Lab_7.Program.Add(input, ref accounts);

            //assert
            Assert.IsFalse(forAdd);
            Assert.AreEqual(0, accounts.Count);
        }


        [TestMethod]
        public void CorrectSearchAccount_User()
        {
            //arrange
            List<Inst_account> accounts = new List<Inst_account>();
            Inst_account acc = new Inst_account("qwerty", "555555555555", 0501234545, 5, 2020, Account_states.Personal);
            accounts.Add(acc);
            Inst_account foundAccount;
            string searchUsername = "qwerty";

            //act
            Inst_account res = Lab_7.Program.SearchAccount(accounts, searchUsername, out foundAccount);

            //assert
            Assert.IsNotNull(res);
            Assert.AreEqual(res, foundAccount);
        }


        [TestMethod]
        public void WrongSearchAccount_User()
        {
            //arrange
            List<Inst_account> accounts = new List<Inst_account>();
            Inst_account acc = new Inst_account("qwerty", "555555555555", 0501234545, 5, 2020, Account_states.Personal);
            accounts.Add(acc);
            Inst_account foundAccount;
            string searchUsername = "zxc";

            //act
            Lab_7.Program.SearchAccount(accounts, searchUsername, out foundAccount);

            //assert
            Assert.IsNull(foundAccount);
        }

        [TestMethod]
        public void CorrectSearchAccount_Password()
        {
            //arrange
            List<Inst_account> accounts = new List<Inst_account>();
            Inst_account acc = new Inst_account("qwerty", "555555555555", 0501234545, 5, 2020, Account_states.Personal);
            accounts.Add(acc);
            Inst_account foundAccount;
            float searchPost = 5;

            //act
            Inst_account res = Lab_7.Program.SearchAccount(accounts, searchPost, out foundAccount);

            //assert
            Assert.IsNotNull(res);
            Assert.AreEqual(res, foundAccount);
        }


        [TestMethod]
        public void WrongSearchAccount_Password()
        {
            //arrange
            List<Inst_account> accounts = new List<Inst_account>();
            Inst_account acc = new Inst_account("qwerty", "555555555555", 0501234545, 5, 2020, Account_states.Personal);
            accounts.Add(acc);
            Inst_account foundAccount;
            float searchPost = 10;

            //act
            Inst_account res = Lab_7.Program.SearchAccount(accounts, searchPost, out foundAccount);

            //assert
            Assert.IsNull(foundAccount);
        }

        [TestMethod]
        public void CorrectForDelete_Index()
        {
            //arrange
            List<Inst_account> accounts = new List<Inst_account>();
            Inst_account acc = new Inst_account("qwerty", "555555555555", 0501234545, 5, 2020, Account_states.Personal);
            accounts.Add(acc);
            int deleteIndex = 1;
            //act
            bool a = ForDelete(deleteIndex, ref accounts);

            //assert
            Assert.IsTrue(a);
            Assert.AreEqual(0, accounts.Count);
        }

        [TestMethod]
        public void WrongForDelete_Index()
        {
            //arrange
            List<Inst_account> accounts = new List<Inst_account>();
            Inst_account acc = new Inst_account("qwerty", "555555555555", 0501234545, 5, 2020, Account_states.Personal);
            accounts.Add(acc);
            int deleteIndex = 5;
            //act
            bool a = ForDelete(deleteIndex, ref accounts);
            //assert
            Assert.IsFalse(a);
            Assert.AreEqual(1, accounts.Count);
        }

        [TestMethod]
        public void CorrectForDelete_User()
        {
            //arrange
            List<Inst_account> accounts = new List<Inst_account>();
            Inst_account acc = new Inst_account("qwerty", "555555555555", 0501234545, 5, 2020, Account_states.Personal);
            accounts.Add(acc);
            string searchUsername = "qwerty";
            Inst_account foundAccount;
            //act
            bool a = ForDelete(ref accounts, searchUsername, out foundAccount);
            //assert
            Assert.IsTrue(a);
            Assert.AreEqual(0, accounts.Count);
        }

        [TestMethod]
        public void WrongForDelete_User()
        {
            //arrange
            List<Inst_account> accounts = new List<Inst_account>();
            Inst_account acc = new Inst_account("qwerty", "555555555555", 0501234545, 5, 2020, Account_states.Personal);
            accounts.Add(acc);
            string searchUsername = "aaa";
            Inst_account foundAccount;
            //act
            bool a = ForDelete(ref accounts, searchUsername, out foundAccount);
            //assert
            Assert.IsFalse(a);
            Assert.AreEqual(1, accounts.Count);
        }

        //

        const string txtFilePath2 = "E:\\”Õ»¬≈–—»“≈“\\2 ÍÛÒ\\ŒŒœ\\Lab7_√≥Í‡_program\\Lab7.Tests\\test.txt";
        const string jsonFilePath2 = "E:\\”Õ»¬≈–—»“≈“\\2 ÍÛÒ\\ŒŒœ\\Lab7_√≥Í‡_program\\Lab7.Tests\\test.json";

        [TestMethod]
        public void Correct_SaveToFileJson()
        {
            // Arrange
            List<Inst_account> accounts = new List<Inst_account>();

            Inst_account acc = new Inst_account("user111", "12345678790", 501235668, 1, 2022, Account_states.Public);

            accounts.Add(acc);
            
            // Act
            SaveToFileJson(accounts, jsonFilePath2);

            // Assert
            string fileContent = File.ReadAllText(jsonFilePath2);

            string expectedJson = "[\r\n  {\r\n    \"Username\": \"user111\",\r\n    \"Password\": \"12345678790\",\r\n    \"Phone\": 501235668,\r\n    \"Post\": 1,\r\n    \"Year\": 2022,\r\n    \"Status\": 1\r\n  }\r\n]";

            Assert.AreEqual(expectedJson, fileContent);
        }



        [TestMethod]
        public void Wrong_SaveToFileJson()
        {
            // Arrange
            List<Inst_account> accounts = new List<Inst_account>();

            Inst_account acc = new ("", "0", 0, 1, 2, Account_states.Public);
            accounts.Add(acc);
            
            // Act
            SaveToFileJson(accounts, jsonFilePath2);

            // Assert
            string fileContent = File.ReadAllText(jsonFilePath2);
            string expectedJson = null;
            Assert.AreNotEqual(expectedJson, fileContent);
        }


        [TestMethod]
        public void Correct_SaveToFileTxt()
        {
            // Arrange
            List<Inst_account> accounts = new List<Inst_account>();

            Inst_account acc = new Inst_account("user111", "12345678790", 501235668, 1, 2022, Account_states.Public);
            Inst_account acc2 = new Inst_account("user222", "55555555555", 987654321, 2, 2021, Account_states.Private);
            accounts.Add(acc);
            accounts.Add(acc2);

            // Act
            SaveToFileTxt(accounts, txtFilePath2);

            // Assert
            string fileContent = File.ReadAllText(txtFilePath2);
            Assert.IsTrue(fileContent.Contains(acc.ToString()));
            Assert.IsTrue(fileContent.Contains(acc2.ToString()));
        }

        [TestMethod]
        public void Wrong_SaveToFileTxt()
        {
            // Arrange
            List<Inst_account> accounts = new List<Inst_account>();

            Inst_account acc = new Inst_account("", "5", 9, 2, 1, Account_states.Private); 
            accounts.Add(acc);

            // Act
            SaveToFileTxt(accounts, txtFilePath2);

            // Assert
            string fileContent = File.ReadAllText(txtFilePath2);
            string expected = ""; //Ó˜≥ÍÛ˛ ˘Ó Ì≥˜Ó„Ó ÌÂ Á‡ÔË¯Â
            Assert.AreEqual(expected, fileContent);
        }


        const string txtFilePath3 = "E:\\”Õ»¬≈–—»“≈“\\2 ÍÛÒ\\ŒŒœ\\Lab7_√≥Í‡_program\\Lab7.Tests\\CorrectRead.txt";
        const string jsonFilePath3 = "E:\\”Õ»¬≈–—»“≈“\\2 ÍÛÒ\\ŒŒœ\\Lab7_√≥Í‡_program\\Lab7.Tests\\CorrectRead.json";

        [TestMethod]
        public void Correct_ReadFromJson()
        {
            // Arrange
            List<Inst_account> accounts = new List<Inst_account>();
            Inst_account expectedAccount = new Inst_account("qwerty", "1234567890", 501234577, 5, 2020, Account_states.Public);

            // Act
            ReadFromFileJson(ref accounts, jsonFilePath3);

            // Assert
            Assert.IsTrue(accounts.Count == 1); 

            Assert.AreEqual(expectedAccount.Username, accounts[0].Username);
            Assert.AreEqual(expectedAccount.Password, accounts[0].Password);
            Assert.AreEqual(expectedAccount.Phone, accounts[0].Phone);
            Assert.AreEqual(expectedAccount.Post, accounts[0].Post);
            Assert.AreEqual(expectedAccount.Year, accounts[0].Year);
            Assert.AreEqual(expectedAccount.Status, accounts[0].Status);
        }

        [TestMethod]
        public void Correct_ReadFromTxt()
        {
            // Arrange
            List<Inst_account> accounts = new List<Inst_account>();
            Inst_account expectedAccount = new Inst_account("qwerty", "1234567890", 501234577, 5, 2020, Account_states.Public);
            
            // Act
            ReadFromFileTxt(ref accounts, txtFilePath3);

            // Assert
            Assert.IsTrue(accounts.Count == 1);

            Assert.AreEqual(expectedAccount.Username, accounts[0].Username);
            Assert.AreEqual(expectedAccount.Password, accounts[0].Password);
            Assert.AreEqual(expectedAccount.Phone, accounts[0].Phone);
            Assert.AreEqual(expectedAccount.Post, accounts[0].Post);
            Assert.AreEqual(expectedAccount.Year, accounts[0].Year);
            Assert.AreEqual(expectedAccount.Status, accounts[0].Status);
        }

        const string txtFilePath4 = "E:\\”Õ»¬≈–—»“≈“\\2 ÍÛÒ\\ŒŒœ\\Lab7_√≥Í‡_program\\Lab7.Tests\\WrongRead.txt";
        const string jsonFilePath4 = "E:\\”Õ»¬≈–—»“≈“\\2 ÍÛÒ\\ŒŒœ\\Lab7_√≥Í‡_program\\Lab7.Tests\\WrongRead.json";

        [TestMethod]
        public void Wrong_ReadFromJson()
        {
            // Arrange
            List<Inst_account> accounts = new List<Inst_account>();

            // Act
            ReadFromFileJson(ref accounts, jsonFilePath4);

            // Assert
            Assert.AreEqual(0, accounts.Count);
        }

        [TestMethod]
        public void Wrong_ReadFromTxt()
        {
            // Arrange
            List<Inst_account> accounts = new List<Inst_account>();

            // Act
            ReadFromFileTxt(ref accounts, txtFilePath4);

            // Assert
            Assert.AreEqual(0, accounts.Count);
        }

    }
}