using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MaterialType.ADO
{
    public class MaterialTypeADO : V_HIS_MATERIAL_TYPE
    {
        public bool IsChemicalSubstance { get; set; }
        public bool IsStopImp { get; set; }

        public MaterialTypeADO(V_HIS_MATERIAL_TYPE _data)
        {
            try
            {
                if (_data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<MaterialTypeADO>(this, _data);

                    this.IsChemicalSubstance = (_data.IS_CHEMICAL_SUBSTANCE == IMSys.DbConfig.HIS_RS.HIS_MATERIAL_TYPE.IS_CHEMICAL_SUBSTANCE);
                    this.IsStopImp = (_data.IS_STOP_IMP == IMSys.DbConfig.HIS_RS.HIS_MATERIAL_TYPE.IS_STOP_IMP__TRUE);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
