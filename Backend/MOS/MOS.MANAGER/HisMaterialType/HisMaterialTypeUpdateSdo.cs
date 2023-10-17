using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisServicePaty;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMaterialType
{
    partial class HisMaterialTypeUpdate : BusinessBase
    {
        internal bool UpdateSdo(HisMaterialTypeSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMaterialTypeCheck checker = new HisMaterialTypeCheck(param);
                HIS_MATERIAL_TYPE raw = null;
                HIS_SERVICE oldService = null;
                HIS_SERVICE oldService1 = null;
                HisServiceCheck serviceChecker = new HisServiceCheck(param);
                valid = valid && checker.VerifyRequireField(data.HisMaterialType);
                valid = valid && IsGreaterThanZero(data.HisMaterialType.ID);
                valid = valid && checker.VerifyId(data.HisMaterialType.ID, ref raw);
                valid = valid && serviceChecker.VerifyId(data.HisMaterialType.SERVICE_ID, ref oldService1);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.HisMaterialType.MATERIAL_TYPE_CODE, data.HisMaterialType.ID);
                valid = valid && this.CheckHeinInfo(data.HisMaterialType);
                if (valid)
                {
                    //Thiet lap is_leaf cua dich vu
                    List<HIS_MATERIAL_TYPE> existChildren = new HisMaterialTypeGet().GetByParentId(data.HisMaterialType.ID);
                    if (IsNotNullOrEmpty(existChildren))
                    {
                        data.HisMaterialType.IS_LEAF = null;
                    }
                    else
                    {
                        data.HisMaterialType.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                    }
                    if (!data.HisMaterialType.IMP_UNIT_ID.HasValue)
                    {
                        data.HisMaterialType.IMP_UNIT_CONVERT_RATIO = null;
                    }
                    if (data.HisMaterialType.HIS_SERVICE != null)
                    {
                        data.HisMaterialType.TDL_SERVICE_UNIT_ID = data.HisMaterialType.HIS_SERVICE.SERVICE_UNIT_ID; //luu du thua du lieu
                    }

                    if (!DAOWorker.HisMaterialTypeDAO.Update(data.HisMaterialType))
                    {
                        throw new Exception("Cap nhat du lieu HIS_MATERIAL_TYPE that bai. Ket thuc xu ly. Rollback du lieu");
                    }

                    //neu co truyen vao HIS_SERVICE thi moi thuc hien update HIS_SERVICE
                    if (data.HisMaterialType.HIS_SERVICE != null)
                    {
                        HisServiceSDO serviceSdo = new HisServiceSDO();
                        serviceSdo.HisService = data.HisMaterialType.HIS_SERVICE;
                        serviceSdo.HisService.SERVICE_CODE = data.HisMaterialType.MATERIAL_TYPE_CODE;
                        serviceSdo.HisService.SERVICE_NAME = data.HisMaterialType.MATERIAL_TYPE_NAME;
                        serviceSdo.HisService.IS_LEAF = data.HisMaterialType.IS_LEAF;
                        serviceSdo.UpdateSereServ = data.UpdateSereServ;
                        if (!new HisServiceUpdate(param).UpdateSdo(serviceSdo, ref oldService))
                        {
                            throw new Exception("Cap nhat du lieu HIS_SERVICE that bai. Ket thuc xu ly. Rollback du lieu");
                        }
                    }
                    else
                    {
                        if (!serviceChecker.ExistsCode(data.HisMaterialType.MATERIAL_TYPE_CODE, data.HisMaterialType.SERVICE_ID))
                        {
                            throw new Exception("Ma dich vu da ton tai");
                        }
                    }

                    //set lai is_leaf = null cho dich vu parent
                    if (data.HisMaterialType.PARENT_ID.HasValue)
                    {
                        HIS_MATERIAL_TYPE parent = new HisMaterialTypeGet().GetById(data.HisMaterialType.PARENT_ID.Value);
                        if (parent.IS_LEAF.HasValue)
                        {
                            parent.IS_LEAF = null;
                            if (!new HisMaterialTypeUpdate(param).Update(parent))
                            {
                                throw new Exception("Cap nhat IS_LEAF du lieu parent that bai. Ket thuc xu ly. Rollback du lieu");
                            }
                        }
                    }
                    //set lai is_leaf = null cho dich vu parent cu
                    if (raw.PARENT_ID.HasValue && raw.PARENT_ID.Value != data.HisMaterialType.PARENT_ID)
                    {
                        List<HIS_MATERIAL_TYPE> children = new HisMaterialTypeGet().GetByParentId(raw.PARENT_ID.Value);
                        if (!IsNotNullOrEmpty(children))
                        {
                            HIS_MATERIAL_TYPE oldParent = new HisMaterialTypeGet().GetById(raw.PARENT_ID.Value);
                            oldParent.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                            if (!new HisMaterialTypeUpdate(param).Update(oldParent))
                            {
                                throw new Exception("Cap nhat IS_LEAF du lieu oldParent that bai. Ket thuc xu ly. Rollback du lieu");
                            }
                        }
                    }
                    result = true;

                    HisMaterialTypeLog.Run(data.HisMaterialType, raw, data.HisMaterialType.HIS_SERVICE, oldService1, data, LibraryEventLog.EventLog.Enum.HisMaterialType_Sua);
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


        private bool CheckHeinInfo(HIS_MATERIAL_TYPE data)
        {
            bool valid = true;
            try
            {
                if (data.HIS_SERVICE != null
                    && (String.IsNullOrWhiteSpace(data.HIS_SERVICE.HEIN_SERVICE_BHYT_CODE)
                    || String.IsNullOrWhiteSpace(data.HIS_SERVICE.HEIN_SERVICE_BHYT_NAME)))
                {
                    HisServicePatyFilterQuery sPatyFilter = new HisServicePatyFilterQuery();
                    sPatyFilter.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT;
                    sPatyFilter.SERVICE_ID = data.SERVICE_ID;
                    List<HIS_SERVICE_PATY> servicePatys = new HisServicePatyGet().Get(sPatyFilter);
                    if (IsNotNullOrEmpty(servicePatys))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMaterialType_VatTuCoChinhSachGiaBHYTYeuCauNhapDayDuThongTinBHYT);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
