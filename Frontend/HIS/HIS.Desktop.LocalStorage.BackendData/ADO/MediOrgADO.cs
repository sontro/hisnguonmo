using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData.ADO
{
    public class MediOrgADO : MOS.EFMODEL.DataModels.HIS_MEDI_ORG
    {
        public MediOrgADO() { }
        public string MEDI_ORG_NAME_UNSIGNED { get; set; } 

        public MediOrgADO(MOS.EFMODEL.DataModels.HIS_MEDI_ORG data) 
        {
            try
            {
                //Inventec.Common.Mapper.DataObjectMapper.Map<MediOrgADO>(this, data);
                this.ADDRESS = data.ADDRESS;
                this.CREATE_TIME = data.CREATE_TIME;
                this.CREATOR = data.CREATOR;
                this.GROUP_CODE = data.GROUP_CODE;
                this.ID = data.ID;
                this.IS_ACTIVE = data.IS_ACTIVE;
                this.IS_DELETE = data.IS_DELETE;
                this.LEVEL_CODE = data.LEVEL_CODE;
                this.MEDI_ORG_CODE = data.MEDI_ORG_CODE;
                this.MEDI_ORG_NAME = data.MEDI_ORG_NAME;
                this.MODIFIER = data.MODIFIER;
                this.MODIFY_TIME = data.MODIFY_TIME;
                this.PROVINCE_CODE = data.PROVINCE_CODE;
                this.RANK_CODE = data.RANK_CODE;

                this.MEDI_ORG_NAME_UNSIGNED = Inventec.Common.String.Convert.UnSignVNese2(this.MEDI_ORG_NAME);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
