using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisService;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialType
{
    partial class HisMaterialTypeUpdate : BusinessBase
    {
        internal HisMaterialTypeUpdate()
            : base()
        {

        }

        internal HisMaterialTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MATERIAL_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMaterialTypeCheck checker = new HisMaterialTypeCheck(param);
                HisServiceCheck serviceChecker = new HisServiceCheck(param);
                HIS_MATERIAL_TYPE raw = null;

                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.MATERIAL_TYPE_CODE, data.ID);
                if (valid)
                {

                    //Thiet lap is_leaf cua dich vu
                    List<HIS_MATERIAL_TYPE> existChildren = new HisMaterialTypeGet().GetByParentId(data.ID);
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
                        data.HIS_SERVICE.SERVICE_CODE = data.MATERIAL_TYPE_CODE;
                        data.HIS_SERVICE.SERVICE_NAME = data.MATERIAL_TYPE_NAME;
                        data.HIS_SERVICE.IS_LEAF = data.IS_LEAF;
                        data.TDL_SERVICE_UNIT_ID = data.HIS_SERVICE.SERVICE_UNIT_ID;
                        if (!new HisServiceUpdate(param).Update(data.HIS_SERVICE))
                        {
                            throw new Exception("Cap nhat du lieu HIS_SERVICE that bai. Ket thuc xu ly. Rollback du lieu");
                        }
                    }
                    else
                    {
                        if (!serviceChecker.ExistsCode(data.MATERIAL_TYPE_CODE, data.SERVICE_ID))
                        {
                            throw new Exception("Ma dich vu da ton tai");
                        }
                    }

                    if (!DAOWorker.HisMaterialTypeDAO.Update(data))
                    {
                        throw new Exception("Cap nhat du lieu HIS_MATERIAL_TYPE that bai. Ket thuc xu ly. Rollback du lieu");
                    }

                    //set lai is_leaf = null cho dich vu parent
                    if (data.PARENT_ID.HasValue)
                    {
                        HIS_MATERIAL_TYPE parent = new HisMaterialTypeGet().GetById(data.PARENT_ID.Value);
                        HIS_SERVICE service = new HisServiceGet().GetById(parent.SERVICE_ID);
                        parent.IS_LEAF = null;
                        parent.HIS_SERVICE = service;

                        if (!new HisMaterialTypeUpdate(param).Update(parent))
                        {
                            throw new Exception("Cap nhat IS_LEAF du lieu parent that bai. Ket thuc xu ly. Rollback du lieu");
                        }
                    }
                    //set lai is_leaf = null cho dich vu parent cu
                    if (raw.PARENT_ID.HasValue && raw.PARENT_ID != data.PARENT_ID)
                    {
                        List<HIS_MATERIAL_TYPE> children = new HisMaterialTypeGet().GetByParentId(raw.PARENT_ID.Value);
                        if (!IsNotNullOrEmpty(children))
                        {
                            HIS_MATERIAL_TYPE oldParent = new HisMaterialTypeGet().GetById(raw.PARENT_ID.Value);
                            HIS_SERVICE service = new HisServiceGet().GetById(oldParent.SERVICE_ID);
                            oldParent.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                            oldParent.HIS_SERVICE = service;
                            if (!new HisMaterialTypeUpdate(param).Update(oldParent))
                            {
                                throw new Exception("Cap nhat IS_LEAF du lieu oldParent that bai. Ket thuc xu ly. Rollback du lieu");
                            }
                        }
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

        internal bool UpdateList(List<HIS_MATERIAL_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMaterialTypeCheck checker = new HisMaterialTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.ExistsCode(data.MATERIAL_TYPE_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisMaterialTypeDAO.UpdateList(listData);
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
