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
            silver.errReel = Values[0].Value as uint?;
            silver.errVacuum = Values[1].Value as uint?;
            silver.speed = Values[2].Value as double?;
            silver.position = Values[3].Value as double?;
            silver.length = Values[4].Value as double?;
            silver.tapeName = Values[5].Value as string;
            silver.runTimes = Values[6].Value as uint?;
            silver.runTimesSet = Values[7].Value as uint?;
            silver.lengthSet = Values[8].Value as double?;
            silver.procesStart = Values[9].Value as bool?;
           // Console.WriteLine(Values[9].Value as bool?);
        }

        private void ClientDa_UpdateOpcPldB(object sender, EventArgs e)
        {
            var Values = sender as OpcDaItemValue[];
            pldB.errReel = Values[0].Value as uint?;
            pldB.errVacuum = Values[1].Value as uint?;
            pldB.speed = Values[2].Value as double?;
            pldB.position = Values[3].Value as double?;
            pldB.length = Values[4].Value as double?;
            pldB.tapeName = Values[5].Value as string;
            pldB.runTimes = Values[6].Value as uint?;
            pldB.runTimesSet = Values[7].Value as uint?;
            pldB.lengthSet = Values[8].Value as double?;
            pldB.procesStart = Values[9].Value as bool?;
           // Console.WriteLine(Values[9].Value as bool?);
        }

        private void ClientDa_UpdateOpcPldA(object sender, EventArgs e)
        {
            var Values = sender as OpcDaItemValue[];
            pldA.errReel = Values[0].Value as uint?;
            pldA.errVacuum = Values[1].Value as uint?;
            pldA.speed = Values[2].Value as double?;
            pldA.position = Values[3].Value as double?;
            pldA.length = Values[4].Value as double?;
            pldA.tapeName = Values[5].Value as string;
            pldA.runTimes = Values[6].Value as uint?;
            pldA.runTimesSet = Values[7].Value as uint?;
            pldA.lengthSet = Values[8].Value as double?;
            pldA.procesStart = Values[9].Value as bool?;
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
            leap300.sideA = (bool)Values[5].Value;
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
            leap130.sideA = (bool)Values[5].Value;
            // Console.WriteLine(Values[0].Value as string);
        }
    }
}
