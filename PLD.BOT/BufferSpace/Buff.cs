using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PLD.BOT.OPC;
using TitaniumAS.Opc.Client.Da;

namespace PLD.BOT.BufferSpace
{
    class Buff
    {
        public Silver silver;
        public PldA pldA;
        public PldB pldB;
        public LEAP130 leap130;
        public LEAP300 leap300;
        ClientDaOPC clientDa;

        public Buff()
        {
            silver = new Silver();
            pldA = new PldA();
            pldB = new PldB();
            leap130 = new LEAP130();
            leap300 = new LEAP300();
            clientDa = new ClientDaOPC();
            clientDa.UpdateOpcLEAP130 += ClientDa_UpdateOpcLEAP130;
            clientDa.UpdateOpcLEAP300 += ClientDa_UpdateOpcLEAP300;
            clientDa.UpdateOpcPldA += ClientDa_UpdateOpcPldA;
            clientDa.UpdateOpcPldB += ClientDa_UpdateOpcPldB;
            clientDa.UpdateOpcSilver += ClientDa_UpdateOpcSilver;
        }

        private void ClientDa_UpdateOpcSilver(object sender, EventArgs e)
        {
            var Values = sender as OpcDaItemValue[];
            silver.errReel = (ushort)Values[0].Value;
            silver.errVacuum = (ushort)Values[1].Value;
            silver.speed = Convert.ToSingle((ushort)Values[4].Value)/ 100;
            silver.position = Convert.ToSingle(Values[5].Value);
            silver.length = Convert.ToSingle((uint)Values[2].Value)/1000;
            silver.tapeName = (string)Values[6].Value;
            //silver.runTimes = Convert.ToDouble((ushort)Values[6].Value);
            //silver.runTimesSet = Convert.ToDouble((ushort)Values[7].Value);
            silver.lengthSet = Convert.ToDouble((uint)Values[3].Value) / 1000;
            silver.procesStart = (bool)Values[7].Value;
            //Console.WriteLine(silver.lengthSet);
            //Console.WriteLine(silver.tapeName);
        }

        private void ClientDa_UpdateOpcPldB(object sender, EventArgs e)
        {
            var Values = sender as OpcDaItemValue[];
            pldB.errReel = (ushort)Values[0].Value;
            pldB.errVacuum = (ushort)Values[1].Value ;
            pldB.speed = Convert.ToSingle(Values[2].Value);
            pldB.position = Convert.ToSingle(Values[3].Value);
            pldB.length = Convert.ToSingle(Values[4].Value);
            pldB.tapeName = (string)Values[5].Value;
            pldB.runTimes =Convert.ToDouble((ushort)Values[6].Value);
            pldB.runTimesSet = Convert.ToDouble((ushort)Values[7].Value);
            pldB.lengthSet = Convert.ToDouble((int)Values[8].Value)/1000;
            pldB.procesStart = (bool)Values[9].Value;
           // Console.WriteLine(pldB.speed);
            //Console.WriteLine(pldB.tapeName);
            // Console.WriteLine(Values[9].Value as bool?);
        }

        private void ClientDa_UpdateOpcPldA(object sender, EventArgs e)
        {
            var Values = sender as OpcDaItemValue[];
            pldA.errReel = (ushort)Values[0].Value;
            pldA.errVacuum = (ushort)Values[1].Value;
            pldA.speed = Convert.ToSingle(Values[2].Value);
            pldA.position = Convert.ToSingle(Values[3].Value);
            pldA.length = Convert.ToSingle(Values[4].Value);
            pldA.tapeName = (string)Values[5].Value;
            pldA.runTimes = Convert.ToDouble((ushort)Values[6].Value);
            pldA.runTimesSet = Convert.ToDouble((ushort)Values[7].Value);
            pldA.lengthSet = Convert.ToDouble((int)Values[8].Value) / 1000;
            pldA.procesStart = (bool)Values[9].Value;
            //Console.WriteLine(Convert.ToSingle(Values[2].Value));
            //Console.WriteLine(pldA.speed);
            // Console.WriteLine(Values[9].Value as bool?);
        }

        private void ClientDa_UpdateOpcLEAP300(object sender, EventArgs e)
        {
            var Values = sender as OpcDaItemValue[];
            leap300.OpModMsg = Values[0].Value as string;
            leap300.OpModCode = (double)Values[1].Value ;
            leap300.Egy = (double)Values[2].Value;
            leap300.Hv = (double)Values[3].Value;
            leap300.counterNewFill = (double)Values[4].Value;
            leap300.sideA = Convert.ToBoolean((ushort)Values[5].Value);
           // Console.WriteLine(Values[0].Value as string);
        }

       
        private void ClientDa_UpdateOpcLEAP130(object sender, EventArgs e)
        {
            var Values = sender as OpcDaItemValue[];
            leap130.OpModMsg = Values[0].Value as string;
            leap130.OpModCode = (double)Values[1].Value;
            leap130.Egy = (double)Values[2].Value;
            leap130.Hv = (double)Values[3].Value;
            leap130.counterNewFill = (double)Values[4].Value;
            leap130.sideA = Convert.ToBoolean((ushort)Values[5].Value);
            // Console.WriteLine(Values[0].Value as string);
        }
    }
}
