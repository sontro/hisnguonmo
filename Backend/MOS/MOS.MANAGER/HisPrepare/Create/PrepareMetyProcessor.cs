using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPrepareMety;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPrepare.Create
{
    class PrepareMetyProcessor : BusinessBase
    {
        private HisPrepareMetyCreate hisPrepareMetyCreate;

        internal PrepareMetyProcessor(CommonParam param)
            : base(param)
        {
            this.hisPrepareMetyCreate = new HisPrepareMetyCreate(param);
        }

        internal bool Run(HIS_PREPARE hisPrepare, List<HIS_PREPARE_METY> prepareMetys, ref List<HIS_PREPARE_METY> medicines)
        {
            bool result = false;
            try
            {
                if (hisPrepare != null && IsNotNullOrEmpty(prepareMetys))
                {
                    prepareMetys.ForEach(o =>
                    {
                        o.PREPARE_ID = hisPrepare.ID;
                        o.APPROVAL_AMOUNT = null;
                        o.TDL_TREATMENT_ID = hisPrepare.TREATMENT_ID;
                    });

                    if (!this.hisPrepareMetyCreate.CreateList(prepareMetys))
                    {
                        throw new Exception("hisPrepareMetyCreate. Ket thuc nghiep vu");
                    }

                    medicines = prepareMetys;
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            try
            {
                this.hisPrepareMetyCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
