using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskContract
{
    partial class HisKskContractUpdate : BusinessBase
    {
        private List<HIS_KSK_CONTRACT> beforeUpdateHisKskContractDTOs = new List<HIS_KSK_CONTRACT>();

        internal HisKskContractUpdate()
            : base()
        {

        }

        internal HisKskContractUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(KsKContractSDO data, ref HIS_KSK_CONTRACT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskContractCheck checker = new HisKskContractCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_KSK_CONTRACT raw = null;
                WorkPlaceSDO workPlace = null;
                valid = valid && checker.VerifyId(data.KskContract.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.KskContract.KSK_CONTRACT_CODE, data.KskContract.ID);
                valid = valid && checker.CheckEditCode(data.KskContract, raw);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                if (valid)
                {
                    data.KskContract.DEPARTMENT_ID = workPlace.DepartmentId;
                    if (!DAOWorker.HisKskContractDAO.Update(data.KskContract))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskContract_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskContract that bai." + LogUtil.TraceData("data", data.KskContract));
                    }
                    this.beforeUpdateHisKskContractDTOs.Add(raw);

                    if ((raw.IS_REQUIRED_APPROVAL.HasValue && raw.IS_REQUIRED_APPROVAL.Value == Constant.IS_TRUE)
                        && (!data.KskContract.IS_REQUIRED_APPROVAL.HasValue || data.KskContract.IS_REQUIRED_APPROVAL.Value != Constant.IS_TRUE))
                    {
                        string sql = "UPDATE HIS_SERVICE_REQ REQ SET REQ.TDL_KSK_IS_REQUIRED_APPROVAL = NULL WHERE (REQ.IS_DELETE IS NULL OR REQ.IS_DELETE <> 1) AND EXISTS (SELECT 1 FROM HIS_TREATMENT T WHERE (T.IS_PAUSE IS NULL OR T.IS_PAUSE <> 1) AND (T.IS_ACTIVE = 1) AND T.ID = REQ.TREATMENT_ID AND T.TDL_KSK_CONTRACT_ID = :param1)";

                        if (!DAOWorker.SqlDAO.Execute(sql, raw.ID))
                        {
                            throw new Exception("Sql: " + sql);
                        }
                    }
                    resultData = data.KskContract;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
            }
            return result;
        }



        internal bool UpdateList(List<HIS_KSK_CONTRACT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskContractCheck checker = new HisKskContractCheck(param);
                List<HIS_KSK_CONTRACT> listRaw = new List<HIS_KSK_CONTRACT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.KSK_CONTRACT_CODE, data.ID);
                }
                if (valid)
                {
                    this.beforeUpdateHisKskContractDTOs.AddRange(listRaw);
                    if (!DAOWorker.HisKskContractDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskContract_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskContract that bai." + LogUtil.TraceData("listData", listData));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisKskContractDTOs))
            {
                if (!new HisKskContractUpdate(param).UpdateList(this.beforeUpdateHisKskContractDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisKskContract that bai, can kiem tra lai." + LogUtil.TraceData("HisKskContractDTOs", this.beforeUpdateHisKskContractDTOs));
                }
            }
        }
    }
}
