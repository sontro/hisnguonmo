using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Handler.UnConfirm
{
    class MedicineMaterialProcessor : BusinessBase
    {
        internal MedicineMaterialProcessor()
            : base()
        {

        }

        internal MedicineMaterialProcessor(CommonParam param)
            : base()
        {
            
        }

        internal bool Run(HIS_IMP_MEST_MEDICINE impMestMedicine, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                string sqlDelete = String.Format("DELETE HIS_MEDICINE_MATERIAL WHERE MEDICINE_ID = {0}", impMestMedicine.MEDICINE_ID);
                sqls.Add(sqlDelete);
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
