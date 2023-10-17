using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPrepareMaty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPrepare.Approve.List
{
    class PrepareMatyProcessor : BusinessBase
    {
        private HisPrepareMatyUpdate hisPrepareMatyUpdate;

        internal PrepareMatyProcessor(CommonParam param)
            : base(param)
        {
            this.hisPrepareMatyUpdate = new HisPrepareMatyUpdate(param);
        }

        internal bool Run(List<HIS_PREPARE> listPrepare)
        {
            bool result = false;
            try
            {
                List<HIS_PREPARE_MATY> matys = new HisPrepareMatyGet().GetByPrepareIds(listPrepare.Select(s => s.ID).ToList());
                if (IsNotNullOrEmpty(matys))
                {
                    Mapper.CreateMap<HIS_PREPARE_MATY, HIS_PREPARE_MATY>();
                    List<HIS_PREPARE_MATY> listBefore = Mapper.Map<List<HIS_PREPARE_MATY>>(matys);
                    matys.ForEach(o => o.APPROVAL_AMOUNT = o.REQ_AMOUNT);
                    if (!this.hisPrepareMatyUpdate.UpdateList(matys, listBefore))
                    {
                        throw new Exception("hisPrepareMayUpdate. Ket thuc nghiep vu");
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
                this.hisPrepareMatyUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
