using CryptoExchanger.Shared.Models;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace CRM.Services
{
    public class TelegramService
    {
        public readonly TelegramSettings _telegramSettings;
        private DbService db;
        TelegramBotClient botClient;
        public TelegramService(IOptions<TelegramSettings> telegramSettings, DbService dbService)
        {
            _telegramSettings = telegramSettings.Value ?? throw new ArgumentException(nameof(telegramSettings));

            botClient = new TelegramBotClient(_telegramSettings.BotToken);
            //botClient.StartReceiving(UpdateHandler, ErrorHandler);
            db = dbService;
        }

        public async Task SendMessageToAdmin(string text)
        {
            var message = botClient.SendTextMessageAsync(
                chatId: _telegramSettings.AdminId,
                text: text);
        }
    }
}
