using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class TakeMaterialBeanListResultSDO
    {
        public TakeBeanSDO Request { get; set; }
        public List<HIS_MATERIAL_BEAN> Result { get; set; }
    }
}
