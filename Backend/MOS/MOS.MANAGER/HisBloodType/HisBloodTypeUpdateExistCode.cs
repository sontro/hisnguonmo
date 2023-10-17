using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBloodType
{
    partial class HisBloodTypeUpdate : BusinessBase
    {
        private List<HIS_BLOOD_TYPE> beforeUpdateHisBloodTypes = new List<HIS_BLOOD_TYPE>();

        internal HisBloodTypeUpdate()
            : base()
        {

        }

        internal HisBloodTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BLOOD_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBloodTypeCheck checker = new HisBloodTypeCheck(param);
                HisServiceCheck serviceChecker = new HisServiceCheck(param);
                HIS_BLOOD_TYPE raw = null;
                HIS_SERVICE oldService = null;
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.BLOOD_TYPE_CODE, data.ID);
                if (valid)
                {
                    //neu co truyen vao HIS_SERVICE thi moi thuc hien update HIS_SERVICE
                    if (data.HIS_SERVICE != null)
                    {
                        data.HIS_SERVICE.SERVICE_CODE = data.BLOOD_TYPE_CODE;
                        data.HIS_SERVICE.SERVICE_NAME = data.BLOOD_TYPE_NAME;
                        data.HIS_SERVICE.IS_LEAF = data.IS_LEAF;
                        data.TDL_SERVICE_UNIT_ID = data.HIS_SERVICE.SERVICE_UNIT_ID; //luu du thua du lieu
                        if (!new HisServiceUpdate(param).Update(data.HIS_SERVICE, ref oldService))
                        {
                            throw new Exception("Cap nhat du lieu HIS_SERVICE that bai. Ket thuc xu ly. Rollback du lieu");
                        }
                    }
                    else
                    {
                        if (!serviceChecker.ExistsCode(data.BLOOD_TYPE_CODE,data.SERVICE_ID))
                        {
                            throw new Exception("Ma dich vu da ton tai");
                        }
                    }
                    //Thiet lap is_leaf cua dich vu
                    List<HIS_BLOOD_TYPE> existChildren = new HisBloodTypeGet().GetByParentId(data.ID);
                    if (IsNotNullOrEmpty(existChildren))
                    {
                        data.IS_LEAF = null;
                    }
                    else
                    {
                        data.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                    }

                    if (!DAOWorker.HisBloodTypeDAO.Update(data))
                    {
                        throw new Exception("Cap nhat du lieu HIS_BLOOD_TYPE that bai. Ket thuc xu ly. Rollback du lieu");
                    }


                    //set lai is_leaf = null cho dich vu parent
                    if (data.PARENT_ID.HasValue)
                    {
                        HIS_BLOOD_TYPE parent = new HisBloodTypeGet().GetById(data.PARENT_ID.Value);
                        parent.IS_LEAF = null;
                        if (!new HisBloodTypeUpdate(param).Update(parent))
                        {
                            throw new Exception("Cap nhat IS_LEAF du lieu parent that bai. Ket thuc xu ly. Rollback du lieu");
                        }
                    }
                    //set lai is_leaf = null cho dich vu parent cu
                    if (raw.PARENT_ID.HasValue)
                    {
                        List<HIS_BLOOD_TYPE> children = new HisBloodTypeGet().GetByParentId(raw.PARENT_ID.Value);
                        if (!IsNotNullOrEmpty(children))
                        {
                            HIS_BLOOD_TYPE oldParent = new HisBloodTypeGet().GetById(raw.PARENT_ID.Value);
                            oldParent.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                            if (!new HisBloodTypeUpdate(param).Update(oldParent))
                            {
                                throw new Exception("Cap nhat IS_LEAF du lieu oldParent that bai. Ket thuc xu ly. Rollback du lieu");
                            }
                        }
                    }
                    result = true;

                    HisBloodTypeLog.Run(data, raw, data.HIS_SERVICE, oldService, LibraryEventLog.EventLog.Enum.HisBloodType_Sua);
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

        internal bool UpdateList(List<HIS_BLOOD_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBloodTypeCheck checker = new HisBloodTypeCheck(param);
                List<HIS_BLOOD_TYPE> listRaw = new List<HIS_BLOOD_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.BLOOD_TYPE_CODE, data.ID);
                }
                if (valid)
                {
                    this.beforeUpdateHisBloodTypes.AddRange(listRaw);
                    if (!DAOWorker.HisBloodTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBloodType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBloodType that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBloodTypes))
            {
                if (!new HisBloodTypeUpdate(param).UpdateList(this.beforeUpdateHisBloodTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisBloodType that bai, can kiem tra lai." + LogUtil.TraceData("HisBloodTypes", this.beforeUpdateHisBloodTypes));
                }
            }
        }
    }
}
