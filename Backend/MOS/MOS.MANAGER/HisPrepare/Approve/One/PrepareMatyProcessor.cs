using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPrepareMaty;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPrepare.Approve.One
{
    class PrepareMatyProcessor : BusinessBase
    {
        private HisPrepareMatyUpdate hisPrepareMatyUpdate;

        internal PrepareMatyProcessor(CommonParam param)
            : base(param)
        {
            this.hisPrepareMatyUpdate = new HisPrepareMatyUpdate(param);
        }

        internal bool Run(List<HisPrepareMatySDO> matySDOs, List<HIS_PREPARE_MATY> prepareMatys)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(matySDOs))
                {
                    Mapper.CreateMap<HIS_PREPARE_MATY, HIS_PREPARE_MATY>();
                    List<HIS_PREPARE_MATY> listBefore = Mapper.Map<List<HIS_PREPARE_MATY>>(prepareMatys);
                    foreach (HisPrepareMatySDO sdo in matySDOs)
                    {
                        HIS_PREPARE_MATY exist = prepareMatys.FirstOrDefault(o => o.ID == sdo.PrepareMatyId);
                        if (exist == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("sdo.PrepareMatyId invalid: " + sdo.PrepareMatyId);
                        }
                        exist.APPROVAL_AMOUNT = sdo.ApproveAmount;
                    }

                    if (!this.hisPrepareMatyUpdate.UpdateList(prepareMatys, listBefore))
                    {
                        throw new Exception("hisPrepareMatyUpdate. Ket thuc nghiep vu");
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
