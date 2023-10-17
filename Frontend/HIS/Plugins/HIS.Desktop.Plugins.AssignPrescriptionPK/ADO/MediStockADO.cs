using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK.ADO
{
    public class MediStockADO : V_HIS_MEST_ROOM
    {
        public bool IsChecked { get; set; }

        public MediStockADO()
            : base()
        {
            IsChecked = false;
        }
        
        public MediStockADO(V_HIS_MEST_ROOM userRoom)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<MediStockADO>(this, userRoom);
            this.IsChecked = false;
        }
    }
}
