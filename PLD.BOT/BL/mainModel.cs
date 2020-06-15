using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using PLD.BOT.BufferSpace;
using PLD.BOT.TelegramBot;
using System.Timers;

namespace PLD.BOT.BL
{
    class mainModel
    {
        
        Buff buff;
        Bot bot;
        public mainModel()
        {
            buff = new Buff();
            bot = new Bot();
            bot.Leap130ErrorsGet += Bot_Leap130ErrorsGet;
            bot.Leap130StatusGet += Bot_Leap130StatusGet;
            bot.Leap300ErrorsGet += Bot_Leap300ErrorsGet;
            bot.Leap300StatusGet += Bot_Leap300StatusGet;
            var time = new Timer(1000);
            time.AutoReset = true;
            time.Enabled = true;
            time.Elapsed += Time_Elapsed;
        }

        private void Time_Elapsed(object sender, ElapsedEventArgs e)
        {
            errLeapScan(buff.leap130);
            errLeapScan(buff.leap300);
            statusLeapScan(buff.leap130);
            statusLeapScan(buff.leap300);
        }

        private void Bot_Leap300StatusGet(object sender, EventArgs e)
        {
            var message = sender as Telegram.Bot.Types.Message;
            var Msg = statusLeapGet(buff.leap300);
            bot.SendMessage(message.Chat.Id, "Статус LEAP300: \n" + Msg);
            GC.Collect();
        }

        private void Bot_Leap130StatusGet(object sender, EventArgs e)
        {
            var message = sender as Telegram.Bot.Types.Message;
            var Msg = statusLeapGet(buff.leap130);
            bot.SendMessage(message.Chat.Id, "Статус LEAP130: \n" + Msg);
            GC.Collect();
        }

        private void Bot_Leap300ErrorsGet(object sender, EventArgs e)
        {
            var message = sender as Telegram.Bot.Types.Message;
            Console.WriteLine(buff.leap300.OpModCode);
            var Msg =errLeapGet(buff.leap300);
            GC.Collect();
            bot.SendMessage(message.Chat.Id, "Ошибки LEAP300: \n" + Msg);
        }

        private void Bot_Leap130ErrorsGet(object sender, EventArgs e)
        {
            var message = sender as Telegram.Bot.Types.Message;
            Console.WriteLine(buff.leap130.OpModCode);
            var Msg = errLeapGet(buff.leap130);
            GC.Collect();
            bot.SendMessage(message.Chat.Id, "Ошибки LEAP130: \n" + Msg);

        }

        string errLeapGet(LEAP leap)
        {
            string messageOut="";
            if (leap.OpModCode != 124 && leap.OpModCode != 182 && leap.OpModCode != 0 )
            {
                //codOld = code;
                messageOut= $"{leap.OpModMsg}, Error code: {leap.OpModCode} " +"\n";
            };
            if (leap.OpModCode == 182)
            {

                messageOut = $"Не та смесь" + "\n";

                //stCod_old = stCod;

            }
            if (messageOut=="")
            {
                messageOut = "Нет ошибок" + "\n";
            }
            return messageOut;
        }
        void  errLeapScan(LEAP leap)
        {
            string Name;
            if (leap is LEAP130)
            {
                Name = "LEAP130";
            }
            else
            {
                Name = "LEAP300";
            }
            if (leap.OpModCode != 124 && leap.OpModCode != 182 && leap.OpModCode != 0)
            {
               
                if (leap.OpModCode != leap.OpModCodeOld)
                {
                    bot.SendMessageChat(Name + ":"+ "\n" + $"{leap.OpModMsg}, Error code: {leap.OpModCode} " + "\n");
                }
                    leap.OpModCodeOld = leap.OpModCode;


            };
            if (leap.OpModCode == 182)
            {
                if (leap.OpModCode != leap.OpModCodeOld)
                {
                    bot.SendMessageChat(Name+":" + "\n" + $"Не та смесь" + "\n");
                }
                leap.OpModCodeOld = leap.OpModCode;

                //stCod_old = stCod;

            }
           
        }
        void statusLeapScan(LEAP leap)
        {
            string Name;
            if (leap is LEAP130)
            {
                Name = "LEAP130";
            }
            else
            {
                Name = "LEAP300";
            }
            if (leap.Egy > leap.EgyOld + 0.2 || leap.Hv > leap.HvOld + 0.009)
            {
                double CNF;
                string SCNF;
                CNF = (leap.counterNewFill / 1000000);
                SCNF = CNF.ToString("#.##") + " MIO, ";
                bot.SendMessageChat(Name + ":" + "\n" + $"{SCNF}{leap.Hv} kV, {leap.Egy} mJ. {leap.OpModMsg}, {leap.OpModCode}");
                leap.HvOld = leap.Hv;
                leap.EgyOld = leap.Egy;
            }

        }
        string statusLeapGet(LEAP leap)
        {
            string messageOut = "";

            
                double CNF;
                string SCNF;
                CNF = (leap.counterNewFill/ 1000000);
                SCNF = CNF.ToString("#.##") + " MIO, ";
                messageOut+= $"{SCNF}{leap.Hv} kV, {leap.Egy} mJ. {leap.OpModMsg}, {leap.OpModCode}";
                leap.HvOld = leap.Hv;
                leap.EgyOld = leap.Egy;
                return messageOut;
        }
    }
}
