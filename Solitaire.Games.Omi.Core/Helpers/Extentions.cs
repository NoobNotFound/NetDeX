using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Games.Omi.Core.Helpers
{
    public static class Extentions
    {
        private static Random rng = new Random();

        public static void AddRange<T>(this ObservableCollection<T> cll, IEnumerable<T> items)
        {
            foreach (var item in items)
                cll.Add(item);
        }

        public static List<IPAddress> GetIPAddresses()
        {
            return NetworkInterface
             .GetAllNetworkInterfaces()
             .SelectMany(i => i.GetIPProperties().UnicastAddresses)
             .Where(a => a.Address.AddressFamily == AddressFamily.InterNetwork)
             .Select(a => a.Address)
             .ToList();
        }
        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }
        public static bool WriteToConsole = false;
        public static bool WriteToDebug = true;
        public static void WriteLine(string text)
        {
            if (WriteToConsole)
                Console.WriteLine(text);

            if (WriteToDebug)
                Debug.WriteLine(text);
        }
        public static int Remove<T>(this ObservableCollection<T> coll, Func<T, bool> condition)
        {
            var itemsToRemove = coll.Where(condition).ToList();

            foreach (var itemToRemove in itemsToRemove)
            {
                coll.Remove(itemToRemove);
            }

            return itemsToRemove.Count;
        }
        public static void Split<T>(this IEnumerable<T> array, int index, out T[] first, out T[] second)
        {
            first = array.Take(index).ToArray();
            second = array.Skip(index).ToArray();
        }
        public static string SerializeJSON<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static T? DeserializeJSON<T>(this string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }
    }
}
