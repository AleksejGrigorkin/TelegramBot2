// See https://aka.ms/new-console-template for more information
using Telegram.Bot;
using Telegram.Bot.Types;
#nullable disable
var botClient = new TelegramBotClient("5702574501:AAF2Cik8x2qkJCDUa8JfkIWF6xqsOgztC3Y");

var me = await botClient.GetMeAsync();
List<long> userIds = new List<long>();

long adminId = 0;
string pincode = "4312";

while (true)
{
    var updates = await botClient.GetUpdatesAsync();
    for (int i = 0; i < updates.Count(); i++)
    {
        switch (updates[i].Type)
        {
            case Telegram.Bot.Types.Enums.UpdateType.Message:
                {
                    HandleMessage(updates[i].Message);

                    updates = await botClient.GetUpdatesAsync(updates[^1].Id + 1);
                    break;
                }
        }
    }
}

async void HandleMessage(Message message)
{
    if(pincode == message.Text && adminId == 0)
    {
        adminId = message.From.Id;
        return;   
    }

    if(message.From.Id == adminId)
    {
        SendMessageToEveryone(message.Text);
        Console.WriteLine(message.From.FirstName);
        return;
    }



    if (!userIds.Contains(message.From.Id))
    {
        userIds.Add(message.From.Id);


    }
}

async void SendMessageToEveryone(string text)
{
    if(text == null){
        return;
    }
    foreach (long id in userIds)
    {
       await botClient.SendTextMessageAsync(id, text);
    }
}

void PrintList(List<long> list)
{
    foreach (long item in list)
    {
        Console.WriteLine(item);
    }
}
