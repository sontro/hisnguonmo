using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisService;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineType
{
    partial class HisMedicineTypeUpdate : BusinessBase
    {
        internal HisMedicineTypeUpdate()
            : base()
        {

        }

        internal HisMedicineTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDICINE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineTypeCheck checker = new HisMedicineTypeCheck(param);
                HisServiceCheck serviceChecker = new HisServiceCheck(param);
                HIS_MEDICINE_TYPE raw = null;
                HIS_SERVICE oldService = null;
                List<HIS_MEDICINE_TYPE_ACIN> deletes = null;
                List<HIS_MEDICINE_TYPE_ACIN> inserts = null;
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsValidActiveIngredient(data);
                valid = valid && checker.ExistsCode(data.MEDICINE_TYPE_CODE, data.ID);
                if (valid)
                {
                    //Thiet lap is_leaf cua dich vu
                    List<HIS_MEDICINE_TYPE> existChildren = new HisMedicineTypeGet().GetByParentId(data.ID);
                    if (IsNotNullOrEmpty(existChildren))
                    {
                        data.IS_LEAF = null;
                    }
                    else
                    {
                        data.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                    }

                    //neu co truyen vao HIS_SERVICE thi moi thuc hien update HIS_SERVICE
                    if (data.HIS_SERVICE != null)
                    {
                        data.HIS_SERVICE.SERVICE_CODE = data.MEDICINE_TYPE_CODE;
                        data.HIS_SERVICE.SERVICE_NAME = data.MEDICINE_TYPE_NAME;
                        data.TDL_SERVICE_UNIT_ID = data.HIS_SERVICE.SERVICE_UNIT_ID;//luu du thua du lieu
                        data.HIS_SERVICE.IS_LEAF = data.IS_LEAF;
                        if (!new HisServiceUpdate(param).Update(data.HIS_SERVICE))
                        {
                            throw new Exception("Cap nhat du lieu HIS_SERVICE that bai. Ket thuc xu ly. Rollback du lieu");
                        }
                    }
                    else
                    {
                        if (!serviceChecker.ExistsCode(data.MEDICINE_TYPE_CODE, data.SERVICE_ID))
                            throw new Exception("Ma dich vu da ton tai");
                    }

                    if (!DAOWorker.HisMedicineTypeDAO.Update(data))
                    {
                        throw new Exception("Cap nhat du lieu HIS_MEDICINE_TYPE that bai. Ket thuc xu ly. Rollback du lieu");
                    }


                    //set lai is_leaf = null cho dich vu parent
                    if (data.PARENT_ID.HasValue)
                    {
                        HIS_MEDICINE_TYPE parent = new HisMedicineTypeGet().GetById(data.PARENT_ID.Value);
                        HIS_SERVICE service = new HisServiceGet().GetById(parent.SERVICE_ID);
                        parent.IS_LEAF = null;
                        parent.HIS_SERVICE = service;
                        if (!new HisMedicineTypeUpdate(param).Update(parent))
                        {
                            throw new Exception("Cap nhat IS_LEAF du lieu parent that bai. Ket thuc xu ly. Rollback du lieu");
                        }
                    }
                    //set lai is_leaf = null cho dich vu parent cu
                    if (raw.PARENT_ID.HasValue)
                    {
                        List<HIS_MEDICINE_TYPE> children = new HisMedicineTypeGet().GetByParentId(raw.PARENT_ID.Value);
                        if (!IsNotNullOrEmpty(children))
                        {
                            HIS_MEDICINE_TYPE oldParent = new HisMedicineTypeGet().GetById(raw.PARENT_ID.Value);
                            HIS_SERVICE service = new HisServiceGet().GetById(oldParent.SERVICE_ID);
                            oldParent.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                            oldParent.HIS_SERVICE = service;
                            if (!new HisMedicineTypeUpdate(param).Update(oldParent))
                            {
                                throw new Exception("Cap nhat IS_LEAF du lieu oldParent that bai. Ket thuc xu ly. Rollback du lieu");
                            }
                        }
                    }

                    HisMedicineTypeLog.Run(data, raw, data.HIS_SERVICE, oldService, inserts, deletes, LibraryEventLog.EventLog.Enum.HisMedicineType_Sua);
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

        internal bool UpdateList(List<HIS_MEDICINE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineTypeCheck checker = new HisMedicineTypeCheck(param);
                HisServiceCheck serviceChecker = new HisServiceCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.ExistsCode(data.MEDICINE_TYPE_CODE, data.ID);
                    valid = valid && serviceChecker.ExistsCode(data.MEDICINE_TYPE_CODE, data.SERVICE_ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisMedicineTypeDAO.UpdateList(listData);
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
    }
}
