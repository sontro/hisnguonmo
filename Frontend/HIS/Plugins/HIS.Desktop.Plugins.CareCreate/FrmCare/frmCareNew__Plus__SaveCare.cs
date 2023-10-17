using AutoMapper;
using HIS.Desktop.LocalStorage.LocalData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.CareCreate
{
    public partial class CareCreate : HIS.Desktop.Utility.FormBase
    {
        private void SaveCareProcess()
        {
            try
            {
                MOS.EFMODEL.DataModels.HIS_CARE hisCare = new MOS.EFMODEL.DataModels.HIS_CARE();

                MOS.EFMODEL.DataModels.HIS_DHST hisDHST = new MOS.EFMODEL.DataModels.HIS_DHST();

                if (this.action == GlobalVariables.ActionEdit)
                {
                    Mapper.CreateMap<MOS.EFMODEL.DataModels.HIS_CARE, MOS.EFMODEL.DataModels.HIS_CARE>();
                    hisCare = Mapper.Map<MOS.EFMODEL.DataModels.HIS_CARE, MOS.EFMODEL.DataModels.HIS_CARE>(this.hisCareCurrent);

                    Mapper.CreateMap<MOS.EFMODEL.DataModels.HIS_DHST, MOS.EFMODEL.DataModels.HIS_DHST>();
                    hisDHST = Mapper.Map<MOS.EFMODEL.DataModels.HIS_DHST, MOS.EFMODEL.DataModels.HIS_DHST>(this.currentDhst);
                }
                ProcessDataCare(ref hisCare);
                SaveDataCare(hisCare);

                ProcessDataDHST(ref hisDHST);
                SaveDataDHST(hisDHST);
                           
                
                
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
