using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi
{
    public partial class ChatMessage
    {
        public int Id { get; set; }
        public int idEmployee { get; set; }
        public int IdChatRoom { get; set; }
        public string TextMessage { get; set; }
        public DateTime DateTime { get; set; }
        public string TakeMessage { get; set; }
    }
}
