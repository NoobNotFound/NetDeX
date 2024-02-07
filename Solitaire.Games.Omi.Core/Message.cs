using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Games.Omi.Core
{
    public partial class Message : ObservableObject
    {
        public int Count {  get; set; }
        public DateTime Time {  get; set; } = DateTime.Now;
        public string ID => ipPort + Count;
        public string ipPort {  get; set; }

        [ObservableProperty]
        private string _Name;

        [ObservableProperty]
        private string _Content = "";

        public override string ToString()
        {
            return $"[{Time.ToShortTimeString()}] {Name} ({ipPort}): {Content}";
        }

    }
}
