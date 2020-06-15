using MihaZupan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace PLD.BOT.TelegramBot
{
    class Bot
    {
        public event EventHandler Leap130StatusGet;
        public event EventHandler Leap130ErrorsGet;
        public event EventHandler Leap300StatusGet;
        public event EventHandler Leap300ErrorsGet;
        public event EventHandler pldAStatusGet;
        public event EventHandler pldAErrorsGet;
        public event EventHandler pldBStatusGet;
        public event EventHandler pldBErrorsGet;
        public event EventHandler silverStatusGet;
        public event EventHandler silverErrorsGet;
        private static HttpToSocks5Proxy proxy;
        private static ITelegramBotClient botClient;
        readonly int _chatID = -341650278;


        public Bot()
        {
            proxy = new HttpToSocks5Proxy("80.211.202.168", 1080, "administrator", "Pld-2019");
            botClient = new TelegramBotClient("1058252871:AAHp7N0IipnLKyHvl4Bf4Os-rPFz8Fec7hQ", proxy) { Timeout = TimeSpan.FromSeconds(10) };
            try
            {

                var me = botClient.GetMeAsync().Result;
                botClient.StartReceiving();
                Console.WriteLine($"ID { me.Id} ame: { me.FirstName}" + " Telegram instal");
                botClient.OnMessage += BotClient_OnMessage;
                botClient.OnCallbackQuery += BotClient_OnCallbackQuery;
                BotButton(_chatID);

            }
            catch (Exception ex)
            {
                // LogWriter.WriteLog("", "", "Telegram instal" + ex.Message, 0);
            }
        }
        public async void SendMessageChat(string Msg)
        {
            try
            {
                await botClient.SendTextMessageAsync(_chatID, Msg);
            }
            catch
            { /*LogWriter.WriteLog("", "", "Error from Send message : Telegram Bot", 0);*/ }
        }
        public async void SendMessage(long ChatID, string Msg)
        {
            try
            {
                await botClient.SendTextMessageAsync(ChatID, Msg);
            }
            catch
            { /*LogWriter.WriteLog("", "", "Error from Send message : Telegram Bot", 0);*/ }
        }
        private async void BotClient_OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Message;

            if (e.CallbackQuery.Data == "callback1")
            {
                await botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id, "Проверяем статус установки");
                Leap130StatusGet?.Invoke(message, null);
            }

            if (e.CallbackQuery.Data == "callback2")
            {
                await botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id, "Проверяем  установку на ошибки");
                Leap130ErrorsGet?.Invoke(message, null);

            }

            if (e.CallbackQuery.Data == "callback3")
            {
                await botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id, "Проверяем статус установки");
                Leap300StatusGet?.Invoke(message, null);
            }

            if (e.CallbackQuery.Data == "callback4")
            {
                await botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id, "Проверяем  установку на ошибки");
                Leap300ErrorsGet?.Invoke(message, null);
            }
            if (e.CallbackQuery.Data == "callback5")
            {
                await botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id, "Проверяем статус установки");
                pldAStatusGet?.Invoke(message, null);
            }
            if (e.CallbackQuery.Data == "callback6")
            {
                await botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id, "Проверяем  установку на ошибки");
                pldAErrorsGet?.Invoke(message, null);
            }
            if (e.CallbackQuery.Data == "callback7")
            {
                await botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id, "Проверяем статус установки");
                pldBStatusGet?.Invoke(message, null);
            }
            if (e.CallbackQuery.Data == "callback8")
            {
                await botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id, "Проверяем  установку на ошибки");
                pldBErrorsGet?.Invoke(message, null);

            }
            if (e.CallbackQuery.Data == "callback9")
            {
                await botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id, "Проверяем статус установки");
                silverStatusGet?.Invoke(message, null);
            }
            if (e.CallbackQuery.Data == "callback10")
            {
                await botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id, "Проверяем  установку на ошибки");
                silverErrorsGet?.Invoke(message, null);
            }



        }
        private static async void BotButton(long ID)
        {

            try
            {
                var keyboard = new ReplyKeyboardMarkup
                {
                    Keyboard = new[] {
                                                new[] // row 1
                                                {
                                                    new KeyboardButton("/LEAP300"),
                                                    new KeyboardButton("/LEAP130")
                                                },
                                                new[] // row 2
                                                {
                                                    new KeyboardButton("/PLD-A"),
                                                    new KeyboardButton("/PLD-B")
                                                },
                                                new[] // row 3
                                                {
                                                    new KeyboardButton("/SILVER")

                                                },
                                            },
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(ID, "Выберите установку \n ", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, keyboard);
            }
            catch (Exception)
            {


            }

        }
        private static async void SysButtonLEAP130(long ID)
        {
            var keyboard = new InlineKeyboardMarkup(
                                                    new InlineKeyboardButton[][]
                                                    {
                                                            // First row
                                                            new InlineKeyboardButton[] {
                                                                // First column
                                                                new InlineKeyboardButton(){Text="Статус", CallbackData ="callback1" },

                                                                // Second column
                                                                new InlineKeyboardButton(){Text="Ошибки", CallbackData ="callback2" },
                                                            },
                                                    }

                                                );


            await botClient.SendTextMessageAsync(ID, "LEAP130: Выберете команду", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, keyboard);
        }
        private static async void SysButtonLEAP300(long ID)
        {
            var keyboard = new InlineKeyboardMarkup(
                                                    new InlineKeyboardButton[][]
                                                    {
                                                            // First row
                                                            new InlineKeyboardButton[] {
                                                                // First column
                                                                new InlineKeyboardButton(){Text="Статус", CallbackData ="callback3" },

                                                                // Second column
                                                                new InlineKeyboardButton(){Text="Ошибки", CallbackData ="callback4" },
                                                            },
                                                    }

                                                );


            await botClient.SendTextMessageAsync(ID, "LEAP300: Выберете команду", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, keyboard);
        }
        private static async void SysButtonPldA(long ID)
        {
            var keyboard = new InlineKeyboardMarkup(
                                                    new InlineKeyboardButton[][]
                                                    {
                                                            // First row
                                                            new InlineKeyboardButton[] {
                                                                // First column
                                                                new InlineKeyboardButton(){Text="Статус", CallbackData ="callback5" },

                                                                // Second column
                                                                new InlineKeyboardButton(){Text="Ошибки", CallbackData ="callback6" },
                                                            },
                                                    }

                                                );


            await botClient.SendTextMessageAsync(ID, "PLD-A: Выберете команду", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, keyboard);
        }
        private static async void SysButtonPldB(long ID)
        {
            var keyboard = new InlineKeyboardMarkup(
                                                    new InlineKeyboardButton[][]
                                                    {
                                                            // First row
                                                            new InlineKeyboardButton[] {
                                                                // First column
                                                                new InlineKeyboardButton(){Text="Статус", CallbackData ="callback7" },

                                                                // Second column
                                                                new InlineKeyboardButton(){Text="Ошибки", CallbackData ="callback8" },
                                                            },
                                                    }

                                                );


            await botClient.SendTextMessageAsync(ID, "PLD-B: Выберете команду", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, keyboard);
        }
        private static async void SysButtonSilver(long ID)
        {
            var keyboard = new InlineKeyboardMarkup(
                                                    new InlineKeyboardButton[][]
                                                    {
                                                            // First row
                                                            new InlineKeyboardButton[] {
                                                                // First column
                                                                new InlineKeyboardButton(){Text="Статус", CallbackData ="callback9" },

                                                                // Second column
                                                                new InlineKeyboardButton(){Text="Ошибки", CallbackData ="callback10" },
                                                            },
                                                    }

                                                );


            await botClient.SendTextMessageAsync(ID, "SILVER: Выберете команду", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, keyboard);
        }
        private static void BotClient_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var text = e?.Message?.Text;
            Console.WriteLine(e?.Message?.Text);
            if (text == null)
            {
                return;
            }
            Console.WriteLine(e.Message.Chat.Id);
            if (text == "/system")
            {
                BotButton(e.Message.Chat.Id);

            }
            if (text.ToUpper() == "/LEAP130")
            {

                SysButtonLEAP130(e.Message.Chat.Id);
            }
            if (text.ToUpper() == "/LEAP300")
            {

                SysButtonLEAP300(e.Message.Chat.Id);
            }
            if (text.ToUpper() == "/PLD-A")
            {

                SysButtonPldA(e.Message.Chat.Id);
            }
            if (text.ToUpper() == "/PLD-B")
            {

                SysButtonPldB(e.Message.Chat.Id);
            }
            if (text.ToUpper() == "/SILVER")
            {

                SysButtonSilver(e.Message.Chat.Id);
            }


        }
    }
}
        
