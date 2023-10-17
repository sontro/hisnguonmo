using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HistoryMaterial
{
    class MaterialADO : V_HIS_MATERIAL_TYPE
    {
        public string MATERIAL_TYPE_NAME_UNSIGN { get; set; }

        public MaterialADO(V_HIS_MATERIAL_TYPE data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<MaterialADO>(this, data);
                this.MATERIAL_TYPE_NAME_UNSIGN = Inventec.Common.String.Convert.UnSignVNese(data.MATERIAL_TYPE_NAME);
            }
        }
    }
}
