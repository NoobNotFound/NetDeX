using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDeX.Games.Omi.Core
{
    public class Action(string code,string data,bool sendAll)
    {
        public string Code { get; private set; } = code;
        public string Data { get; private set; } = data;
        public bool SendAll { get; set; } = sendAll;
    }
}