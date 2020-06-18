using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using PLD.BOT.BufferSpace;
using PLD.BOT.TelegramBot;
using System.Timers;
using System.Collections;

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
            bot.pldAErrorsGet += Bot_pldAErrorsGet;
            bot.pldBErrorsGet += Bot_pldBErrorsGet;
            bot.pldAStatusGet += Bot_pldAStatusGet;
            bot.pldBStatusGet += Bot_pldBStatusGet;
            bot.silverStatusGet += Bot_silverStatusGet;
            bot.silverErrorsGet += Bot_silverErrorsGet;
            var time = new Timer(10000);
            time.AutoReset = true;
            time.Enabled = true;
            time.Elapsed += Time_Elapsed;
        }
        private async void Time_Elapsed(object sender, ElapsedEventArgs e)
        {
            await Task.Run(() =>
            {
             errLeapScan(buff.leap130);
             errLeapScan(buff.leap300);
             statusLeapScan(buff.leap130);
             statusLeapScan(buff.leap300);
             errPldScan(buff.pldA);
             errPldScan(buff.pldB);
             statusPldScan(buff.pldA);
             statusPldScan(buff.pldB);
             statusSilverScan(buff.silver);
             errSilverScan(buff.silver);
             });
        }


        private void Bot_silverErrorsGet(object sender, EventArgs e)
        {
            var message = sender as Telegram.Bot.Types.Message;
            //Console.WriteLine(buff.leap300.OpModCode);
            var Msg = errSilverGet(buff.silver);
            GC.Collect();
            bot.SendMessage(message.Chat.Id, "Ошибки SILVER: \n" + Msg);
        }

        private void Bot_silverStatusGet(object sender, EventArgs e)
        {
            var message = sender as Telegram.Bot.Types.Message;
            var Msg = statusSilverGet(buff.silver);
            bot.SendMessage(message.Chat.Id, "Статус SILVER: \n" + Msg);
            GC.Collect();
        }

        private void Bot_pldBStatusGet(object sender, EventArgs e)
        {
            var message = sender as Telegram.Bot.Types.Message;
            var Msg = statusPldGet(buff.pldB);
            bot.SendMessage(message.Chat.Id, "Статус PLD-B: \n" + Msg);
            GC.Collect();
        }

        private void Bot_pldAStatusGet(object sender, EventArgs e)
        {
            var message = sender as Telegram.Bot.Types.Message;
            var Msg = statusPldGet(buff.pldA);
            bot.SendMessage(message.Chat.Id, "Статус PLD-A: \n" + Msg);
            GC.Collect();
        }

        private void Bot_pldBErrorsGet(object sender, EventArgs e)
        {
            var message = sender as Telegram.Bot.Types.Message;
            //Console.WriteLine(buff.leap300.OpModCode);
            var Msg = errPldGet(buff.pldB);
            GC.Collect();
            bot.SendMessage(message.Chat.Id, "Ошибки PLD-B: \n" + Msg);
        }

        private void Bot_pldAErrorsGet(object sender, EventArgs e)
        {
            var message = sender as Telegram.Bot.Types.Message;
            //Console.WriteLine(buff.leap300.OpModCode);
            var Msg = errPldGet(buff.pldA);
            GC.Collect();
            bot.SendMessage(message.Chat.Id, "Ошибки PLD-A: \n" + Msg);
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
                messageOut = "Ошибок нет" + "\n";
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
            if ((/*leap.Egy > leap.EgyOld + 0.2 ||*/ leap.Hv > leap.HvOld + 0.009 ) && (leap.Egy!=404 && leap.Hv != 404))
            {
                double CNF;
                string SCNF;
                CNF = (leap.counterNewFill / 1000000);
                SCNF = string.Format("{0:0.00}",CNF) + " MIO, ";
                bot.SendMessageChat(Name + " : " + "\n" + $"{SCNF}{leap.Hv} kV, {leap.Egy} mJ. {leap.OpModMsg}, {leap.OpModCode}");
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
                SCNF = string.Format("{0:0.00}", CNF) + " MIO, ";
                messageOut+= $"{SCNF}{leap.Hv} kV, {leap.Egy} mJ. {leap.OpModMsg}, {leap.OpModCode}";
                leap.HvOld = leap.Hv;
                leap.EgyOld = leap.Egy;
                return messageOut;
        }

        private string errPldGet(BufferSpace.PLD pld)
        {
            try
            {
                int i = 0;
                string Msg = "";
                BitArray bitArr = new BitArray(BitConverter.GetBytes(pld.errReel));
                if (bitArr[0])
                {
                    Msg += "Emergency stop from reel unit" + "\n";
                    i++;
                }
                if (bitArr[1])
                {
                    Msg += "Right reel servo driver warning" + "\n";
                    i++;
                }
                if (bitArr[2])
                {
                    Msg += "Left reel servo driver warning" + "\n";
                    i++;
                }
                if (bitArr[3])
                {
                    Msg += "Right tension unit over lord" + "\n";
                    i++;
                }
                if (bitArr[4])
                {
                    Msg += "Left tension unit over lord" + "\n";
                    i++;
                }
                if (bitArr[5])
                {
                    Msg += "Target servo driver warning" + "\n";
                    i++;
                }
                if (bitArr[6])
                {
                    Msg += "Optical box door open" + "\n";
                    i++;
                }

                if (bitArr[7])
                {
                    Msg += "Target NC unit error" + "\n";
                    i++;
                }
                if (bitArr[8])
                {
                    Msg += "Rotation window NC unit error" + "\n";
                    i++;
                }
                if (bitArr[9])
                {
                    Msg += "Laser NC unit error" + "\n";
                    i++;
                }
                if (bitArr[10])
                {
                    Msg += "Tape was cut" + "\n";
                    i++;
                }
                if (bitArr[11])
                {
                    Msg += "Scan unit disconnected" + "\n";
                    i++;
                }
                if (bitArr[12])
                {
                    Msg += "Vacuum system disconnected" + "\n";
                    i++;
                }
                if (bitArr[13])
                {
                    Msg += "PLD2 system disconnected" + "\n";
                    i++;
                }
                if (bitArr[14])
                {
                    Msg += "PLD1 system disconnected" + "\n";
                    i++;
                }
               
                var bitArr2 = new BitArray(BitConverter.GetBytes(pld.errVacuum));
                if (bitArr2[0])
                {
                    Msg += "Air pressure to low" + "\n";
                    i++;
                }
                if (bitArr2[1])
                {
                    Msg += "Trip a therminal for RP1" + "\n";
                    i++;
                }
                if (bitArr2[2])
                {
                    Msg += "TMP error" + "\n";
                    i++;
                }
                if (bitArr2[3])
                {
                    Msg += "Rough vacuum time out" + "\n";
                    i++;
                }
                if (bitArr2[4])
                {
                    Msg += "Vacuum system error" + "\n";
                    i++;
                }
                if (bitArr2[5])
                {
                    Msg += "Chamber pressure too higth" + "\n";
                    i++;
                }
                if (bitArr2[6])
                {
                    Msg += "APC motion error" + "\n";
                    i++;
                }

                //if (bitArr2[7])
                //{
                //    Msg += "Target NC unit error";
                //}
                //if (bitArr2[8])
                //{
                //    Msg += "Rotation window NC unit error";
                //}
                if (bitArr2[9])
                {
                    Msg += "Heater  coolant stopted" + "\n";
                    i++;
                }
                //if (bitArr2[10])
                //{
                //    Msg = "Tape was cut";
                //}
                if (bitArr2[11])
                {
                    Msg += "Heater power supply error" + "\n";
                    i++;
                }
                if (bitArr2[12])
                {
                    Msg += "Temp controller error" + "\n";
                    i++;
                }
                if (bitArr2[13])
                {
                    Msg += "Heater burnout" + "\n";
                    i++;
                }
                //if (bitArr2[14])
                //{
                //    Msg = "PLD1 system disconnected";
                //}
                if (bitArr2[15])
                {
                    Msg += "Emergency stop from vacuum unit"+"\n";
                    i++;
                }
                if (i>0)
                {
                    Msg += $"{i} оишбок";
                }
                else
                {
                    Msg += "Ошибок нет";
                }
                return Msg;
            }
            catch
            {
                return "Error";
            }
        }

        private void errPldScan(BufferSpace.PLD pld)
        {
            try
            {
                string Name="";
                if (pld is PldA)
                { 
                    Name = "PLD-A";
                }
                if (pld is PldB)
                { 
                    Name = "PLD-B";
                }
                if (pld is Silver)
                {
                    Name = "Silver";
                }

                BitArray bitArr = new BitArray(BitConverter.GetBytes(pld.errReel));
                if (bitArr[0])
                {
                    if(!pld.SendFlags[0])
                    {
                        bot.SendMessageChat( Name + " : " + "Emergency stop from reel unit" + "\n");
                        pld.SendFlags[0] = true;
                    }
                   
                }
                else
                {
                    pld.SendFlags[0] = false;
                }
                if (bitArr[1])
                {
                    if (!pld.SendFlags[1])
                    {
                        bot.SendMessageChat(Name + " : " + "Right reel servo driver warning" + "\n");
                        pld.SendFlags[1] = true;
                    }

                }
                else
                {
                    pld.SendFlags[1] = false;
                }
                if (bitArr[2])
                {
                    if (!pld.SendFlags[2])
                    {
                        bot.SendMessageChat(Name + " : " + "Left reel servo driver warning" + "\n");
                        pld.SendFlags[2] = true;
                    }

                }
                else
                {
                    pld.SendFlags[2] = false;
                }
                if (bitArr[3])
                {
                    if (!pld.SendFlags[3])
                    {
                        bot.SendMessageChat(Name + " : " + "Right tension unit over lord" + "\n");
                        pld.SendFlags[3] = true;
                    }

                }
                else
                {
                    pld.SendFlags[3] = false;
                }

                if (bitArr[4])
                {
                    if (!pld.SendFlags[4])
                    {
                        bot.SendMessageChat(Name + " : " + "Left tension unit over lord" + "\n");
                        pld.SendFlags[4] = true;
                    }

                }
                else
                {
                    pld.SendFlags[4] = false;
                }

                if (bitArr[5])
                {
                    if (!pld.SendFlags[5])
                    {
                        bot.SendMessageChat(Name + " : " + "Target servo driver warning" + "\n");
                        pld.SendFlags[5] = true;
                    }

                }
                else
                {
                    pld.SendFlags[5] = false;
                }

                if (bitArr[6])
                {
                    if (!pld.SendFlags[6])
                    {
                        bot.SendMessageChat(Name + " : " + "Optical box door open" + "\n");
                        pld.SendFlags[6] = true;
                    }

                }
                else
                {
                    pld.SendFlags[6] = false;
                }
                if (bitArr[7])
                {
                    if (!pld.SendFlags[7])
                    {
                        bot.SendMessageChat(Name + " : " + "Target NC unit error" + "\n");
                        pld.SendFlags[7] = true;
                    }
                }
                else
                {
                    pld.SendFlags[7] = false;
                }
                if (bitArr[8])
                {
                    if (!pld.SendFlags[8])
                    {
                        bot.SendMessageChat(Name + " : " + "Rotation window NC unit error" + "\n");
                        pld.SendFlags[8] = true;
                    }
                }
                else
                {
                    pld.SendFlags[8] = false;
                }
                if (bitArr[9])
                {
                    if (!pld.SendFlags[9])
                    {
                        bot.SendMessageChat(Name + " : " + "Laser NC unit error" + "\n");
                        pld.SendFlags[9] = true;
                    }
                }
                else
                {
                    pld.SendFlags[9] = false;
                }

                if (bitArr[10])
                {
                    if (!pld.SendFlags[10])
                    {
                        bot.SendMessageChat(Name + " : " + "Tape was cut" + "\n");
                        pld.SendFlags[10] = true;
                    }
                }
                else
                {
                    pld.SendFlags[10] = false;
                }

                if (bitArr[11])
                {
                    if (!pld.SendFlags[11])
                    {
                        bot.SendMessageChat(Name + " : " + "Scan unit disconnected" + "\n");
                        pld.SendFlags[11] = true;
                    }
                }
                else
                {
                    pld.SendFlags[11] = false;
                }

                if (bitArr[12])
                {
                    if (!pld.SendFlags[12])
                    {
                        bot.SendMessageChat(Name + " : " + "Vacuum system disconnected" + "\n");
                        pld.SendFlags[12] = true;
                    }
                }
                else
                {
                    pld.SendFlags[12] = false;
                }

                if (bitArr[13])
                {
                    if (!pld.SendFlags[13])
                    {
                        bot.SendMessageChat(Name + " : " + "PLD2 system disconnected" + "\n");
                        pld.SendFlags[13] = true;
                    }
                }
                else
                {
                    pld.SendFlags[13] = false;
                }

                if (bitArr[14])
                {
                    if (!pld.SendFlags[14])
                    {
                        bot.SendMessageChat(Name + " : " + "PLD1 system disconnected" + "\n");
                        pld.SendFlags[14] = true;
                    }
                }
                else
                {
                    pld.SendFlags[14] = false;
                }

                var bitArr2 = new BitArray(BitConverter.GetBytes(pld.errVacuum));



                if (bitArr2[0])
                {
                    if (!pld.SendFlags[15])
                    {
                        bot.SendMessageChat(Name + " : " + "Air pressure to low" + "\n");
                        pld.SendFlags[15] = true;
                    }
                }
                else
                {
                    pld.SendFlags[15] = false;
                }

                if (bitArr2[1])
                {
                    if (!pld.SendFlags[16])
                    {
                        bot.SendMessageChat(Name + " : " + "Trip a therminal for RP1" + "\n");
                        pld.SendFlags[16] = true;
                    }
                }
                else
                {
                    pld.SendFlags[16] = false;
                }

                if (bitArr2[2])
                {
                    if (!pld.SendFlags[17])
                    {
                        bot.SendMessageChat(Name + " : " + "TMP error" + "\n");
                        pld.SendFlags[17] = true;
                    }
                }
                else
                {
                    pld.SendFlags[17] = false;
                }

                if (bitArr2[3])
                {
                    if (!pld.SendFlags[18])
                    {
                        bot.SendMessageChat(Name + " : " + "Rough vacuum time out" + "\n");
                        pld.SendFlags[18] = true;
                    }
                }
                else
                {
                    pld.SendFlags[18] = false;
                }

                if (bitArr2[4])
                {
                    if (!pld.SendFlags[19])
                    {
                        bot.SendMessageChat(Name + " : " + "Vacuum system error" + "\n");
                        pld.SendFlags[19] = true;
                    }
                }
                else
                {
                    pld.SendFlags[19] = false;
                }
                if (bitArr2[5])
                {
                    if (!pld.SendFlags[20])
                    {
                        bot.SendMessageChat(Name + " : " + "Chamber pressure too higth" + "\n");
                        pld.SendFlags[20] = true;
                    }
                }
                else
                {
                    pld.SendFlags[20] = false;
                }

                if (bitArr2[6])
                {
                    if (!pld.SendFlags[21])
                    {
                        bot.SendMessageChat(Name + " : " + "APC motion error" + "\n");
                        pld.SendFlags[21] = true;
                    }
                }
                else
                {
                    pld.SendFlags[21] = false;
                }
                if (bitArr2[7])
                {
                    if (!pld.SendFlags[22])
                    {
                        bot.SendMessageChat(Name + " : " + "Target NC unit error" + "\n");
                        pld.SendFlags[22] = true;
                    }
                }
                else
                {
                    pld.SendFlags[22] = false;
                }
                if (bitArr2[8])
                {
                    if (!pld.SendFlags[23])
                    {
                        bot.SendMessageChat(Name + " : " + "Rotation window NC unit error" + "\n");
                        pld.SendFlags[23] = true;
                    }
                }
                else
                {
                    pld.SendFlags[23] = false;
                }
                if (bitArr2[9])
                {
                    if (!pld.SendFlags[24])
                    {
                        bot.SendMessageChat(Name + " : " + "Heater  coolant stopted" + "\n");
                        pld.SendFlags[24] = true;
                    }
                }
                else
                {
                    pld.SendFlags[24] = false;
                }
                if (bitArr2[10])
                {
                    if (!pld.SendFlags[25])
                    {
                        bot.SendMessageChat(Name + " : " + "Tape was cut" + "\n");
                        pld.SendFlags[25] = true;
                    }
                }
                else
                {
                    pld.SendFlags[25] = false;
                }

                if (bitArr2[11])
                {
                    if (!pld.SendFlags[26])
                    {
                        bot.SendMessageChat(Name + " : " + "Heater power supply error" + "\n");
                        pld.SendFlags[26] = true;
                    }
                }
                else
                {
                    pld.SendFlags[26] = false;
                }

                if (bitArr2[12])
                {
                    if (!pld.SendFlags[27])
                    {
                        bot.SendMessageChat(Name + " : " + "Temp controller error" + "\n");
                        pld.SendFlags[27] = true;
                    }
                }
                else
                {
                    pld.SendFlags[27] = false;
                }

                if (bitArr2[13])
                {
                    if (!pld.SendFlags[28])
                    {
                        bot.SendMessageChat(Name + " : " + "Heater burnout" + "\n");
                        pld.SendFlags[28] = true;
                    }
                }
                else
                {
                    pld.SendFlags[28] = false;
                }

                if (bitArr2[14])
                {
                    if (!pld.SendFlags[29])
                    {
                        bot.SendMessageChat(Name + " : " + "PLD1 system disconnected" + "\n");
                        pld.SendFlags[29] = true;
                    }
                }
                else
                {
                    pld.SendFlags[29] = false;
                }
                if (bitArr2[15])
                {
                    if (!pld.SendFlags[30])
                    {
                        bot.SendMessageChat(Name + " : " + "Emergency stop from vacuum unit" + "\n");
                        pld.SendFlags[30] = true;
                    }
                }
                else
                {
                    pld.SendFlags[30] = false;
                }

              
            }
            catch
            {

            }
        }

        private string statusPldGet(BufferSpace.PLD pld)
        {
            string Name = "";
            if (pld is PldA)
            {
                Name = "PLD-A";
            }
            if (pld is PldB)
            {
                Name = "PLD-B";
            }
            string msg = "";
            if (pld.procesStart)
            {

                msg += "Установка запущена\n";
                msg += "Текущая длинна: " + string.Format("{0:0.00}", pld.length) + " м" + "\n";
                msg += "Текущая скорость: " + string.Format("{0:0.00}", pld.speed) + " м/час" + "\n";
                msg += "Суммарная длина " + string.Format("{0:0.00}", pld.lengthSet * pld.runTimesSet) + " м" + "\n";
                msg += "Текущая лента: " + pld.tapeName + "\n";
                msg += "Текущая координата: " + string.Format("{0:0.00}", pld.position) + "\n";
                int min = Convert.ToInt32((pld.runTimesSet * pld.lengthSet - (pld.length + (pld.runTimes * pld.lengthSet))) * 60 / (pld.speed));
             
                int hour = min / 60;
                min %= 60;
                msg+="Процесс завершится через " + hour + " часов " + min + " минут" + "\n";
            }
            else
            {
                msg =  "Процесс остановлен \n";
            }
            return msg;

        }

        private string statusSilverGet(BufferSpace.Silver silver)
        {
            
               
            
            string msg = "";
            if (silver.procesStart)
            {

                msg += "Установка запущена\n";
                msg += "Текущая длинна: " + string.Format("{0:0.00}", silver.length) + " м" + "\n";
                msg += "Текущая скорость: " + string.Format("{0:0.00}", silver.speed) + " м/час" + "\n";
                msg += "Суммарная длина " + string.Format("{0:0.00}", silver.lengthSet ) + " м" + "\n";
                msg += "Текущая лента: " + silver.tapeName + "\n";
                msg += "Текущая координата: " + string.Format("{0:0.00}", silver.position )+ "\n";
                int min = Convert.ToInt32((silver.lengthSet - silver.length ) * 60 / (silver.speed));

                int hour = min / 60;
                min %= 60;
                msg += "Процесс завершится через " + hour + " часов " + min + " минут" + "\n";
            }
            else
            {
                msg =  "Процесс остановлен \n";
            }
            return msg;

        }
        private void statusPldScan(BufferSpace.PLD pld)
        {
            string Name = "";
            if (pld is PldA)
            {
                Name = "PLD-A";
            }
            if (pld is PldB)
            {
                Name = "PLD-B";
            }
            if (pld.procesStart != pld.SendFlags[32] && pld.procesStart)
            {

                if (!pld.SendFlags[31])
                {
                    bot.SendMessageChat(Name + " : " + "Процесс запущен" + "\n");
                    pld.SendFlags[31] = true;
                }
            }
            else
            {
                pld.SendFlags[31] = false;

            }
            if (pld.procesStart != pld.SendFlags[32] && !pld.procesStart)
            {

                if (!pld.SendFlags[33])
                {
                    bot.SendMessageChat(Name + " : " + "Процесс остановлен" + "\n");
                    pld.SendFlags[33] = true;
                }
            }
            else
            {
                pld.SendFlags[33] = false;

            }
            pld.SendFlags[32] = pld.procesStart;
            if (pld.procesStart  && pld.speed >0 )
            {
                
                int min = Convert.ToInt32((pld.runTimesSet * pld.lengthSet - (pld.length + (pld.runTimes * pld.lengthSet))) * 60 / (pld.speed));
                if (min < 11 && min > 0)
                {
                    if (!pld.SendFlags[34])
                    {
                        bot.SendMessageChat(Name + " : " + "Процесс завершится через " + min + " минут" + "\n");
                        pld.SendFlags[34] = true;
                    }
                }
                else
                {
                    pld.SendFlags[34] = false;

                }
            }


        }

        private void statusSilverScan(Silver silver)
        {
         
            if (silver.procesStart != silver.SendFlags[32] && silver.procesStart)
            {

                if (!silver.SendFlags[31])
                {
                    bot.SendMessageChat("SILVER: " + "Процесс запущен" + "\n");
                    silver.SendFlags[31] = true;
                }
            }
            else
            {
                silver.SendFlags[31] = false;

            }
            if (silver.procesStart != silver.SendFlags[32] && !silver.procesStart)
            {

                if (!silver.SendFlags[33])
                {
                    bot.SendMessageChat("SILVER: " + "Процесс остановлен" + "\n");
                    silver.SendFlags[33] = true;
                }
            }
            else
            {
                silver.SendFlags[33] = false;

            }
            silver.SendFlags[33] = silver.procesStart;

            int min = Convert.ToInt32((silver.lengthSet - silver.length ) * 60 / (silver.speed));
            if (min < 11 && min > 0)
            {
                if (!silver.SendFlags[34])
                {
                    bot.SendMessageChat("SILVER: " + "Процесс завершится через " + min + " минут" + "\n");
                    silver.SendFlags[34] = true;
                }
            }
            else
            {
                silver.SendFlags[34] = false;

            }


        }

        private void errSilverScan(Silver pld)
        {
            try
            {

                string Name = "SILVER";
                BitArray bitArr = new BitArray(BitConverter.GetBytes(pld.errReel));
                //if (bitArr[0])
                //{
                //    if (!pld.SendFlags[0])
                //    {
                //        bot.SendMessageChat(Name + " : " + "Emergency stop from reel unit" + "\n");
                //        pld.SendFlags[0] = true;
                //    }

                //}
                //else
                //{
                //    pld.SendFlags[0] = false;
                //}
                if (bitArr[1])
                {
                    if (!pld.SendFlags[1])
                    {
                        bot.SendMessageChat(Name + " : " + "Emargancy stop" + "\n");
                        pld.SendFlags[1] = true;
                    }

                }
                else
                {
                    pld.SendFlags[1] = false;
                }
                //if (bitArr[2])
                //{
                //    if (!pld.SendFlags[2])
                //    {
                //        bot.SendMessageChat(Name + " : " + "Left reel servo driver warning" + "\n");
                //        pld.SendFlags[2] = true;
                //    }

                //}
                //else
                //{
                //    pld.SendFlags[2] = false;
                //}
                if (bitArr[3])
                {
                    if (!pld.SendFlags[3])
                    {
                        bot.SendMessageChat(Name + " : " + "Servo driver left fault" + "\n");
                        pld.SendFlags[3] = true;
                    }

                }
                else
                {
                    pld.SendFlags[3] = false;
                }

                if (bitArr[4])
                {
                    if (!pld.SendFlags[4])
                    {
                        bot.SendMessageChat(Name + " : " + "Servo driver rigth fault" + "\n");
                        pld.SendFlags[4] = true;
                    }

                }
                else
                {
                    pld.SendFlags[4] = false;
                }

                if (bitArr[5])
                {
                    if (!pld.SendFlags[5])
                    {
                        bot.SendMessageChat(Name + " : " + "Left tension unit over load" + "\n");
                        pld.SendFlags[5] = true;
                    }

                }
                else
                {
                    pld.SendFlags[5] = false;
                }

                if (bitArr[6])
                {
                    if (!pld.SendFlags[6])
                    {
                        bot.SendMessageChat(Name + " : " + "Right tension unit over load" + "\n");
                        pld.SendFlags[6] = true;
                    }

                }
                else
                {
                    pld.SendFlags[6] = false;
                }
                //if (bitArr[7])
                //{
                //    if (!pld.SendFlags[7])
                //    {
                //        bot.SendMessageChat(Name + " : " + "Target NC unit error" + "\n");
                //        pld.SendFlags[7] = true;
                //    }
                //}
                //else
                //{
                //    pld.SendFlags[7] = false;
                //}
                //if (bitArr[8])
                //{
                //    if (!pld.SendFlags[8])
                //    {
                //        bot.SendMessageChat(Name + " : " + "Rotation window NC unit error" + "\n");
                //        pld.SendFlags[8] = true;
                //    }
                //}
                //else
                //{
                //    pld.SendFlags[8] = false;
                //}
                //if (bitArr[9])
                //{
                //    if (!pld.SendFlags[9])
                //    {
                //        bot.SendMessageChat(Name + " : " + "Laser NC unit error" + "\n");
                //        pld.SendFlags[9] = true;
                //    }
                //}
                //else
                //{
                //    pld.SendFlags[9] = false;
                //}

                if (bitArr[10])
                {
                    if (!pld.SendFlags[10])
                    {
                        bot.SendMessageChat(Name + " : " + "Tape was cut" + "\n");
                        pld.SendFlags[10] = true;
                    }
                }
                else
                {
                    pld.SendFlags[10] = false;
                }

                //if (bitArr[11])
                //{
                //    if (!pld.SendFlags[11])
                //    {
                //        bot.SendMessageChat(Name + " : " + "Scan unit disconnected" + "\n");
                //        pld.SendFlags[11] = true;
                //    }
                //}
                //else
                //{
                //    pld.SendFlags[11] = false;
                //}

                if (bitArr[12])
                {
                    if (!pld.SendFlags[12])
                    {
                        bot.SendMessageChat(Name + " : " + "Vacuum system disconnected" + "\n");
                        pld.SendFlags[12] = true;
                    }
                }
                else
                {
                    pld.SendFlags[12] = false;
                }

                //if (bitArr[13])
                //{
                //    if (!pld.SendFlags[13])
                //    {
                //        bot.SendMessageChat(Name + " : " + "PLD2 system disconnected" + "\n");
                //        pld.SendFlags[13] = true;
                //    }
                //}
                //else
                //{
                //    pld.SendFlags[13] = false;
                //}

                //if (bitArr[14])
                //{
                //    if (!pld.SendFlags[14])
                //    {
                //        bot.SendMessageChat(Name + " : " + "PLD1 system disconnected" + "\n");
                //        pld.SendFlags[14] = true;
                //    }
                //}
                //else
                //{
                //    pld.SendFlags[14] = false;
                //}

                var bitArr2 = new BitArray(BitConverter.GetBytes(pld.errVacuum));



                if (bitArr2[0])
                {
                    if (!pld.SendFlags[15])
                    {
                        bot.SendMessageChat(Name + " : " + "Air pressure to low" + "\n");
                        pld.SendFlags[15] = true;
                    }
                }
                else
                {
                    pld.SendFlags[15] = false;
                }

                if (bitArr2[1])
                {
                    if (!pld.SendFlags[16])
                    {
                        bot.SendMessageChat(Name + " : " + "Trip a therminal for RP1" + "\n");
                        pld.SendFlags[16] = true;
                    }
                }
                else
                {
                    pld.SendFlags[16] = false;
                }

                if (bitArr2[2])
                {
                    if (!pld.SendFlags[17])
                    {
                        bot.SendMessageChat(Name + " : " + "TMP error" + "\n");
                        pld.SendFlags[17] = true;
                    }
                }
                else
                {
                    pld.SendFlags[17] = false;
                }

                if (bitArr2[3])
                {
                    if (!pld.SendFlags[18])
                    {
                        bot.SendMessageChat(Name + " : " + "Rough vacuum time out" + "\n");
                        pld.SendFlags[18] = true;
                    }
                }
                else
                {
                    pld.SendFlags[18] = false;
                }

                if (bitArr2[4])
                {
                    if (!pld.SendFlags[19])
                    {
                        bot.SendMessageChat(Name + " : " + "Vacuum system error" + "\n");
                        pld.SendFlags[19] = true;
                    }
                }
                else
                {
                    pld.SendFlags[19] = false;
                }
                if (bitArr2[5])
                {
                    if (!pld.SendFlags[20])
                    {
                        bot.SendMessageChat(Name + " : " + "Chamber pressure too higth" + "\n");
                        pld.SendFlags[20] = true;
                    }
                }
                else
                {
                    pld.SendFlags[20] = false;
                }

                if (bitArr2[6])
                {
                    if (!pld.SendFlags[21])
                    {
                        bot.SendMessageChat(Name + " : " + "APC motion error" + "\n");
                        pld.SendFlags[21] = true;
                    }
                }
                else
                {
                    pld.SendFlags[21] = false;
                }
                if (bitArr2[7])
                {
                    if (!pld.SendFlags[22])
                    {
                        bot.SendMessageChat(Name + " : " + "Target NC unit error" + "\n");
                        pld.SendFlags[22] = true;
                    }
                }
                else
                {
                    pld.SendFlags[22] = false;
                }
                if (bitArr2[8])
                {
                    if (!pld.SendFlags[23])
                    {
                        bot.SendMessageChat(Name + " : " + "Rotation window NC unit error" + "\n");
                        pld.SendFlags[23] = true;
                    }
                }
                else
                {
                    pld.SendFlags[23] = false;
                }
                if (bitArr2[9])
                {
                    if (!pld.SendFlags[24])
                    {
                        bot.SendMessageChat(Name + " : " + "Heater  coolant stopted" + "\n");
                        pld.SendFlags[24] = true;
                    }
                }
                else
                {
                    pld.SendFlags[24] = false;
                }
                if (bitArr2[10])
                {
                    if (!pld.SendFlags[25])
                    {
                        bot.SendMessageChat(Name + " : " + "Tape was cut" + "\n");
                        pld.SendFlags[25] = true;
                    }
                }
                else
                {
                    pld.SendFlags[25] = false;
                }

                if (bitArr2[11])
                {
                    if (!pld.SendFlags[26])
                    {
                        bot.SendMessageChat(Name + " : " + "Heater power supply error" + "\n");
                        pld.SendFlags[26] = true;
                    }
                }
                else
                {
                    pld.SendFlags[26] = false;
                }

                if (bitArr2[12])
                {
                    if (!pld.SendFlags[27])
                    {
                        bot.SendMessageChat(Name + " : " + "Temp controller error" + "\n");
                        pld.SendFlags[27] = true;
                    }
                }
                else
                {
                    pld.SendFlags[27] = false;
                }

                if (bitArr2[13])
                {
                    if (!pld.SendFlags[28])
                    {
                        bot.SendMessageChat(Name + " : " + "Heater burnout" + "\n");
                        pld.SendFlags[28] = true;
                    }
                }
                else
                {
                    pld.SendFlags[28] = false;
                }

                if (bitArr2[14])
                {
                    if (!pld.SendFlags[29])
                    {
                        bot.SendMessageChat(Name + " : " + "PLD1 system disconnected" + "\n");
                        pld.SendFlags[29] = true;
                    }
                }
                else
                {
                    pld.SendFlags[29] = false;
                }
                if (bitArr2[15])
                {
                    if (!pld.SendFlags[30])
                    {
                        bot.SendMessageChat(Name + " : " + "Emergency stop from vacuum unit" + "\n");
                        pld.SendFlags[30] = true;
                    }
                }
                else
                {
                    pld.SendFlags[30] = false;
                }


            }
            catch
            {

            }
        }
        private string errSilverGet(BufferSpace.PLD pld)
        {
            try
            {
                int i = 0;
                string Msg = "";
                BitArray bitArr = new BitArray(BitConverter.GetBytes(pld.errReel));
                if (bitArr[0])
                {
                    Msg += "PLC 02 varning" + "\n";
                    i++;
                }
                if (bitArr[1])
                {
                    Msg += "Emergency stop" + "\n";
                    i++;
                }
                //if (bitArr[2])
                //{
                //    Msg += "Left reel servo driver warning" + "\n";
                //    i++;
                //}
                if (bitArr[3])
                {
                    Msg += "Servo driver left fault" + "\n";
                    i++;
                }
                if (bitArr[4])
                {
                    Msg += "Servo driver right fault" + "\n";
                    i++;
                }
                if (bitArr[5])
                {
                    Msg += "Left tension unit over load" + "\n";
                    i++;
                }
                if (bitArr[6])
                {
                    Msg += "Right tension unit over load" + "\n";
                    i++;
                }

                //if (bitArr[7])
                //{
                //    Msg += "Target NC unit error" + "\n";
                //    i++;
                //}
                //if (bitArr[8])
                //{
                //    Msg += "Rotation window NC unit error" + "\n";
                //    i++;
                //}
                //if (bitArr[9])
                //{
                //    Msg += "Laser NC unit error" + "\n";
                //    i++;
                //}
                if (bitArr[10])
                {
                    Msg += "Tape was cut" + "\n";
                    i++;
                }
                //if (bitArr[11])
                //{
                //    Msg += "Scan unit disconnected" + "\n";
                //    i++;
                //}
                if (bitArr[12])
                {
                    Msg += "Vacuum system disconnected" + "\n";
                    i++;
                }
                    //}
                    //if (bitArr[13])
                    //{
                    //    Msg += "PLD2 system disconnected" + "\n";
                    //    i++;
                    //}
                    //if (bitArr[14])
                    //{
                    //    Msg += "PLD1 system disconnected" + "\n";
                    //    i++;
                    //}

                    var bitArr2 = new BitArray(BitConverter.GetBytes(pld.errVacuum));
                if (bitArr2[0])
                {
                    Msg += "Air pressure to low" + "\n";
                    i++;
                }
                if (bitArr2[1])
                {
                    Msg += "Trip a therminal for RP1" + "\n";
                    i++;
                }
                if (bitArr2[2])
                {
                    Msg += "TMP error" + "\n";
                    i++;
                }
                if (bitArr2[3])
                {
                    Msg += "Rough vacuum time out" + "\n";
                    i++;
                }
                if (bitArr2[4])
                {
                    Msg += "Vacuum system error" + "\n";
                    i++;
                }
                if (bitArr2[5])
                {
                    Msg += "Chamber pressure too higth" + "\n";
                    i++;
                }
                if (bitArr2[6])
                {
                    Msg += "APC motion error" + "\n";
                    i++;
                }

                //if (bitArr2[7])
                //{
                //    Msg += "Target NC unit error";
                //}
                //if (bitArr2[8])
                //{
                //    Msg += "Rotation window NC unit error";
                //}
                if (bitArr2[9])
                {
                    Msg += "Heater  coolant stopted" + "\n";
                    i++;
                }
                //if (bitArr2[10])
                //{
                //    Msg = "Tape was cut";
                //}
                if (bitArr2[11])
                {
                    Msg += "Heater power supply error" + "\n";
                    i++;
                }
                if (bitArr2[12])
                {
                    Msg += "Temp controller error" + "\n";
                    i++;
                }

                if (bitArr2[13])
                {
                    Msg += "Heater burnout" + "\n";
                    i++;
                }
                //if (bitArr2[14])
                //{
                //    Msg = "PLD1 system disconnected";
                //}
                if (bitArr2[15])
                {
                    Msg += "Emergency stop from vacuum unit" + "\n";
                    i++;
                }
                if (i > 0)
                {
                    Msg += $"{i} оишбок";
                }
                else
                {
                    Msg += "Ошибок нет";
                }
                    return Msg;
                
            }
            catch
            {
                return "Error";
            }
        }
    }
}
