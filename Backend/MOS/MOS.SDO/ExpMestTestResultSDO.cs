using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class ExpMestTestResultSDO
    {
        public V_HIS_EXP_MEST ExpMest { get; set; }
        public List<V_HIS_EXP_MEST_MATERIAL> ExpMestMaterials { get; set; }
    }
}
