using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TitaniumAS.Opc.Client.Common;
using TitaniumAS.Opc.Client.Da;

namespace PLD.BOT.OPC
{
    class ClientDaOPC
    {
        public event EventHandler UpdateOpcLEAP130, UpdateOpcLEAP300, UpdateOpcPldA, UpdateOpcPldB, UpdateOpcSilver;
        public event Action errOpcLEAP130, errOpcLEAP300, errOpcPldA, errOpcPldB, errOpcSilver;
        private bool _errOpcLEAP130, _errOpcLEAP300, _errOpcPldA, _errOpcPldB, _errOpcSilver;
        OpcDaServer server;
        OpcDaGroup groupPldA, groupPldB, groupSilver, groupLeap130, groupLeap300;
        public ClientDaOPC()
        {
            Uri url = UrlBuilder.Build("Kepware.KEPServerEX.V6", "10.177.3.61");
            server = new OpcDaServer(url);
            server.Connect();
            Console.WriteLine(DateTime.Now + " :OPC is connect");

            groupLeap130 = server.AddGroup("LEAP130");
            groupLeap130.IsActive = true;
            var tagsLeap130 = new OpcDaItemDefinition[]
            {
            new OpcDaItemDefinition()
            {
                ItemId = "ModbusTCP.PLD_LEP130.OpMode",
                IsActive = true
            },
            new OpcDaItemDefinition()
            {
                ItemId = "ModbusTCP.PLD_LEP130.OpModeCode",
                IsActive = true
            },
            new OpcDaItemDefinition()
            {
                ItemId = "ModbusTCP.PLD_LEP130.Egy",
                IsActive = true
            },
             new OpcDaItemDefinition()
            {
                ItemId = "ModbusTCP.PLD_LEP130.Hv",
                IsActive = true
            },
              new OpcDaItemDefinition()
            {
                ItemId = "ModbusTCP.PLD_LEP130.CounterNewFill",
                IsActive = true
            },
                new OpcDaItemDefinition()
            {
                ItemId = "ModbusTCP.PLD_LEP130.SideA",
                IsActive = true
            },
            };
            groupLeap130.AddItems(tagsLeap130);
            Console.WriteLine(DateTime.Now +" Group LEAP130 is Add");

            groupLeap300 = server.AddGroup("LEAP300");
            groupLeap300.IsActive = true;
            var tagsLeap300 = new OpcDaItemDefinition[]
           {
            new OpcDaItemDefinition()
            {
                ItemId = "ModbusTCP.PLD_LEP300.OpMode",
                IsActive = true
            },
            new OpcDaItemDefinition()
            {
                ItemId = "ModbusTCP.PLD_LEP300.OpModeCode",
                IsActive = true
            },
            new OpcDaItemDefinition()
            {
                ItemId = "ModbusTCP.PLD_LEP300.Egy",
                IsActive = true
            },
            new OpcDaItemDefinition()
            {
                ItemId = "ModbusTCP.PLD_LEP300.Hv",
                IsActive = true
            },
            new OpcDaItemDefinition()
            {
                ItemId = "ModbusTCP.PLD_LEP300.CounterNewFill",
                IsActive = true
            },
            new OpcDaItemDefinition()
            {
                ItemId = "ModbusTCP.PLD_LEP300.SideA",
                IsActive = true
            },
           };
            groupLeap300.AddItems(tagsLeap300);
            Console.WriteLine(DateTime.Now + " Group LEAP300 is Add");

            groupPldA = server.AddGroup("PldA");
            groupPldA.IsActive = true;
            var tagsPldA = new OpcDaItemDefinition[]
            {
            new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.PLD_Set_A_PLC_01.Alarm.AlarmReel",
                IsActive = true
            },
            new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.PLD_Set_A_PLC_01.Alarm.AlarmVacuum",
                IsActive = true
            },
           
            new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.PLD_Set_A_PLC_01.Speed_m_h",
                IsActive = true
            },
            new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.PLD_Set_A_PLC_01.pos",
                IsActive = true
            },
            new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.PLD_Set_A_PLC_01.Left_Length_m",
                IsActive = true
            },
            new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.PLD_Set_A_PLC_01.Name",
                IsActive = true
            },
             new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.PLD_Set_A_PLC_01.RunTimes",
                IsActive = true
            },
              new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.PLD_Set_A_PLC_01.RunTimeSet",
                IsActive = true
            },
                new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.PLD_Set_A_PLC_01.LengthSetting",
                IsActive = true
            },
                new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.PLD_Set_A_PLC_01.PROCES_START",
                IsActive = true
            },
            };
            groupPldA.AddItems(tagsPldA);
            Console.WriteLine(DateTime.Now + " Group PldA is Add");

            groupPldB = server.AddGroup("PldB");
            groupPldB.IsActive = true;
            var tagsPldB = new OpcDaItemDefinition[]
            {
            new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.PLD_Set_B_PLC_01.Alarm.AlarmReel",
                IsActive = true
            },
            new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.PLD_Set_B_PLC_01.Alarm.AlarmVacuum",
                IsActive = true
            },
            new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.PLD_Set_B_PLC_01.Speed_m_h",
                IsActive = true
            },
            new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.PLD_Set_B_PLC_01.pos",
                IsActive = true
            },
            new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.PLD_Set_B_PLC_01.Left_Length_m",
                IsActive = true
            },
            new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.PLD_Set_B_PLC_01.Name",
                IsActive = true
            },
             new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.PLD_Set_B_PLC_01.RunTimes",
                IsActive = true
            },
              new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.PLD_Set_B_PLC_01.RunTimeSet",
                IsActive = true
            },
                new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.PLD_Set_B_PLC_01.LengthSetting",
                IsActive = true
            },
                new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.PLD_Set_B_PLC_01.PROCES_START",
                IsActive = true
            },
            };
            groupPldB.AddItems(tagsPldB);
            Console.WriteLine(DateTime.Now + " Group PldB is Add");

            groupSilver = server.AddGroup("Silver");
            groupSilver.IsActive = true;
            var tagsSilver = new OpcDaItemDefinition[]
            {
            new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.SPAD_SILVER_PLC01.Alarm.AlarmReel",
                IsActive = true
            },
            new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.SPAD_SILVER_PLC01.Alarm.AlarmVacuum",
                IsActive = true
            },
             new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.SPAD_SILVER_PLC01.HMI_Length_Monitor", //*1000
                IsActive = true
            },
              new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.SPAD_SILVER_PLC01.HMI_Lenth_Setting", //*1000
                IsActive = true
            },
                new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.SPAD_SILVER_PLC01.HMI_Tape_Speed_Monitor",//*10
                IsActive = true
            },
              new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.SPAD_SILVER_PLC03.Pos",
                IsActive = true
            },
                new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.SPAD_SILVER_PLC03.Name",
                IsActive = true
            },
                    new OpcDaItemDefinition()
            {
                ItemId = "FinsTCP.SPAD_SILVER_PLC03.PROCES_START",
                IsActive = true
            },
            };
            groupSilver.AddItems(tagsSilver);
            Console.WriteLine(DateTime.Now + " Group Silver is Add");

            var time = new Timer(1000);
            time.AutoReset = true;
            time.Enabled = true;
            time.Elapsed += Time_Elapsed; ;

        }

        private async void Time_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                var Values = groupLeap130.Read(groupLeap130.Items, OpcDaDataSource.Device);
                UpdateOpcLEAP130?.Invoke(Values, null);
                _errOpcLEAP130 = false;
            }
            catch
            {
                if (!_errOpcLEAP130)
                {
                    Console.WriteLine(DateTime.Now + " : Error from OPC read LEAP130");
                    errOpcLEAP130?.Invoke();
                    // LogWriter.WriteLog("", "", "Error from OPC read LEAP130", 0);
                    _errOpcLEAP130 = true;
                }
            }

            try
            {
                var Values = groupLeap300.Read(groupLeap300.Items, OpcDaDataSource.Device);
                UpdateOpcLEAP300?.Invoke(Values, null);
                _errOpcLEAP300 = false;
            }
            catch
            {
                if (!_errOpcLEAP300)
                {
                    Console.WriteLine(DateTime.Now + " : Error from OPC read LEAP300");
                    errOpcLEAP300?.Invoke();
                    // LogWriter.WriteLog("", "", "Error from OPC read LEAP300", 0);
                    _errOpcLEAP300 = true;
                }
            }

            try
            {
                var Values = groupPldA.Read(groupPldA.Items, OpcDaDataSource.Device);
                UpdateOpcPldA?.Invoke(Values, null);
                _errOpcPldA = false;
            }
            catch
            {
                if (!_errOpcPldA)
                {
                    Console.WriteLine(DateTime.Now + " : Error from OPC read PLD-A");
                    errOpcPldA?.Invoke();
                    // LogWriter.WriteLog("", "", "Error from OPC read PLD-A", 0);
                    _errOpcPldA = true;
                }
            }

            try
            {
                var Values = groupPldB.Read(groupPldB.Items, OpcDaDataSource.Device);
                UpdateOpcPldB?.Invoke(Values, null);
                _errOpcPldB = false;
            }
            catch
            {
                if (!_errOpcPldB)
                {
                    Console.WriteLine(DateTime.Now + " : Error from OPC read PLD-B");
                    errOpcPldB?.Invoke();
                    // LogWriter.WriteLog("", "", "Error from OPC read PLD-B", 0);
                    _errOpcPldB = true;
                }
            }

            try
            {
                var Values = groupSilver.Read(groupSilver.Items, OpcDaDataSource.Device);
                UpdateOpcSilver?.Invoke(Values, null);
                _errOpcSilver = false;
            }
            catch
            {
                if (!_errOpcSilver)
                {
                    Console.WriteLine(DateTime.Now + " : Error from OPC read PLD-A");
                    errOpcSilver?.Invoke();
                    // LogWriter.WriteLog("", "", "Error from OPC read PLD-A", 0);
                    _errOpcSilver = true;
                }
            }

        }
    }
}
