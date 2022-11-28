using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChatApi
{
    /// <summary>
    /// Логика взаимодействия для ChatRoomWindow.xaml
    /// </summary>
    public partial class ChatRoomWindow : Window
    {
        List<ChatMessage> chatMessages = new List<ChatMessage>();
        public ChatRoomWindow()
        {
            InitializeComponent();
            Title = ChatSelection.chatRoom.Topic;
            GetMessage();
        }

        private async void GetMessage()
        {
            HttpResponseMessage httpResponseMessage = await MainWindow.httpClient.GetAsync("http://localhost:50203/api/ChatMessages");
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            chatMessages = JsonConvert.DeserializeObject<List<ChatMessage>>(content);
            MessageList.ItemsSource = chatMessages.Where(i => i.IdChatRoom == ChatSelection.chatRoom.id);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChatSelection c = new ChatSelection();
            c.Show();
            Close();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var message = new ChatMessage
            {
                IdChatRoom = ChatSelection.chatRoom.id,
                DateTime = DateTime.Now,
                TextMessage = MessageTb.Text,
                idEmployee = MainWindow.employee.id
            };

            HttpContent httpContent =
            new StringContent(JsonConvert
            .SerializeObject(message), Encoding.UTF8, "application/json");
            HttpResponseMessage sendmessage = await MainWindow.httpClient.PostAsync("http://localhost:50203/api/ChatMessages", httpContent);
            MessageTb.Text = string.Empty;
            GetMessage();
        }
    }
}
