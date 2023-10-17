using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPrepare.Check;
using MOS.MANAGER.HisPrepareMety;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPrepare.Unapprove
{
    class PrepareMetyProcessor : BusinessBase
    {
        private HisPrepareMetyUpdate hisPrepareMetyUpdate;

        internal PrepareMetyProcessor(CommonParam param)
            : base(param)
        {
            this.hisPrepareMetyUpdate = new HisPrepareMetyUpdate(param);
        }

        internal bool Run(HIS_PREPARE prepare, List<HIS_PREPARE_METY> prepareMetys)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(prepareMetys))
                {
                    Mapper.CreateMap<HIS_PREPARE_METY, HIS_PREPARE_METY>();
                    List<HIS_PREPARE_METY> listBefore = Mapper.Map<List<HIS_PREPARE_METY>>(prepareMetys);
                    prepareMetys.ForEach(o => o.APPROVAL_AMOUNT = null);
                    if (!this.hisPrepareMetyUpdate.UpdateList(prepareMetys,listBefore))
                    {
                        throw new Exception("hisPrepareMetyUpdate. Ket thuc nghiep vu");
                    }
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
                this.hisPrepareMetyUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
