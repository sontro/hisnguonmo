using MOS.EFMODEL.DataModels;
using System.Collections.Generic;
using System.Reflection;

namespace MOS.EFMODEL.Decorator
{
    public partial class DenyUpdateDecorator
    {
        private static void LoadHisExpMestMedicine()
        {
            List<string> pies = new List<string>();
            pies.Add("CREATOR");
            pies.Add("APP_CREATOR");
            pies.Add("CREATE_TIME");
            pies.Add("EXP_MEST_ID");//ko cho sua
            pies.Add("TDL_MEDICINE_TYPE_ID");//ko cho sua
            pies.Add("MEDICINE_ID");//ko cho sua

            properties[typeof(HIS_EXP_MEST_MEDICINE)] = pies;
        }
    }
}
