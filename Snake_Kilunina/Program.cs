using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;

namespace Snake_Kilunina
{
    class Program
    {
        public static List<Leaders> Leaders = new List<Leaders>();
        public static List<ViewModelUserSettings> remoteIPAddres = new List<ViewModelUserSettings>();
        public static List<ViewModelGames> ViewModelGame = new List<ViewModelGames>();
        public static int LocalPort = 5001;
        public static int Speed = 15;
        static void Main(string[] args)
        {

        }

        private static void Send()
        {
            foreach (ViewModelUserSettings User in remoteIPAddres)
            {
                UdpClient sender = new UdpClient();
                IPEndPoint endPoint = new IPEndPoint(
                    IPAddress.Parse(User.IpAddress),
                    int.Parse(User.Port));

                try
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(
                        ViewModelGame.Find(x => x.IdSnake == User.IdSnake)));
                    sender.Send(bytes, bytes.Length, endPoint);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Отправил данные пользователю: {User.IpAddress}:{User.Port}");
                }
                catch (Exception exp)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Возникло исключение: " + exp.Message );
                }
                finally
                {
                    sender.Close();
                }
            }
        }
    }
}
