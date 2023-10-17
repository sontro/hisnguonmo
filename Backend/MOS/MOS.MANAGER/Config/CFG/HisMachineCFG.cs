using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMachine;
using MOS.MANAGER.HisMestRoom;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisMachineCFG
    {
        private static List<HIS_MACHINE> data;
        public static List<HIS_MACHINE> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisMachineGet().Get(new HisMachineFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisMachineGet().Get(new HisMachineFilterQuery());
            data = tmp;
        }
    }
}
