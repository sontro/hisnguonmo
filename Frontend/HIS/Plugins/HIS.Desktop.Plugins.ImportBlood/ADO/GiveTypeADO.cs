using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportBlood.ADO
{
    public class GiveTypeADO
    {
        public long ID { get; set; }
        public string GiveType { get; set; }

        public static List<GiveTypeADO> ListGiveType
        {
            get
            {
                List<GiveTypeADO> result = new List<GiveTypeADO>();
                result.Add(new GiveTypeADO { ID = 1, GiveType = "Tình nguyện" });
                result.Add(new GiveTypeADO { ID = 2, GiveType = "Chuyên nghiệp" });
                result.Add(new GiveTypeADO { ID = 3, GiveType = "Người nhà" });
                result.Add(new GiveTypeADO { ID = 4, GiveType = "Tự thân" });
                return result;
            }
        }
    }
}
