using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisSurgServiceType
{
    public class SurgServiceTypeADO : V_HIS_SURG_SERVICE_TYPE
    {
        public bool? IsLeaf { get; set; }

        //public SurgServiceTypeADO()
        //{
        //}

        //public SurgServiceTypeADO(V_HIS_SURG_SERVICE_TYPE _data)
        //{
        //    Inventec.Common.Mapper.DataObjectMapper.Map<SurgServiceTypeADO>(this, _data);
        //    IsLeaf = (_data.IS_LEAF == IMSys.DbConfig.HIS_RS.HIS_SURG_SERVICE_TYPE.IS_LEAF__TRUE);

        //}
    }
}
