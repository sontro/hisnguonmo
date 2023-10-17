using HIS.Desktop.Plugins.AssignPrescriptionKidney.ADO;
using System.Collections.Generic;

namespace HIS.Desktop.Plugins.AssignPrescriptionKidney.Base
{
    public class MediMatyProcessor
    {
        internal static List<MediMatyTypeADO> MakeMaterialSingleStent(MediMatyTypeADO material)
        {
            List<MediMatyTypeADO> list = new List<MediMatyTypeADO>();
            if (material.IsStent == true && material.AMOUNT > 1)
            {
                decimal remain = material.AMOUNT ?? 0;
                while (remain > 0)
                {
                    MediMatyTypeADO s = new MediMatyTypeADO(material);
                    s.AMOUNT = remain > 1 ? 1 : remain;
                    s.IntructionTimeSelecteds = material.IntructionTimeSelecteds;
                    list.Add(s);
                    remain = remain - (s.AMOUNT ?? 0);
                }
            }
            else
            {
                list.Add(material);
            }
            return list;
        }
    }
}
