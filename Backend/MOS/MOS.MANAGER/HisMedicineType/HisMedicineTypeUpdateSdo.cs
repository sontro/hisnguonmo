using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMedicineTypeAcin;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisServicePaty;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.EventLogUtil;

namespace MOS.MANAGER.HisMedicineType
{
    partial class HisMedicineTypeUpdate : BusinessBase
    {
        internal bool UpdateSdo(HisMedicineTypeSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineTypeCheck checker = new HisMedicineTypeCheck(param);
                HIS_MEDICINE_TYPE raw = null;
                HIS_SERVICE oldService=null;
                HIS_SERVICE oldService1 = null;
                List<HIS_MEDICINE_TYPE_ACIN> deletes = null;
                List<HIS_MEDICINE_TYPE_ACIN> inserts = null;
                HisServiceCheck serviceChecker = new HisServiceCheck(param);
                valid = valid && checker.VerifyRequireField(data.HisMedicineType);
                valid = valid && IsGreaterThanZero(data.HisMedicineType.ID);
                valid = valid && checker.VerifyId(data.HisMedicineType.ID, ref raw);
                valid = valid && serviceChecker.VerifyId(data.HisMedicineType.SERVICE_ID, ref oldService1);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.HisMedicineType.MEDICINE_TYPE_CODE, data.HisMedicineType.ID);
                valid = valid && serviceChecker.ExistsCode(data.HisMedicineType.MEDICINE_TYPE_CODE, data.HisMedicineType.SERVICE_ID);
                valid = valid && this.CheckHeinInfo(data.HisMedicineType);
                if (valid)
                {
                    List<HIS_MEDICINE_TYPE_ACIN> medicineTypeAcins = data.HisMedicineType.HIS_MEDICINE_TYPE_ACIN.ToList();
                    data.HisMedicineType.HIS_MEDICINE_TYPE_ACIN = null;//set bang null de tranh update loi

                    //Thiet lap is_leaf cua dich vu
                    List<HIS_MEDICINE_TYPE> existChildren = new HisMedicineTypeGet().GetByParentId(data.HisMedicineType.ID);
                    if (IsNotNullOrEmpty(existChildren))
                    {
                        data.HisMedicineType.IS_LEAF = null;
                    }
                    else
                    {
                        data.HisMedicineType.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                    }

                    if (!data.HisMedicineType.IMP_UNIT_ID.HasValue)
                    {
                        data.HisMedicineType.IMP_UNIT_CONVERT_RATIO = null;
                    }

                    if (data.HisMedicineType.HIS_SERVICE != null)
                    {
                        data.HisMedicineType.TDL_SERVICE_UNIT_ID = data.HisMedicineType.HIS_SERVICE.SERVICE_UNIT_ID; //luu du thua du lieu
                    }

                    if (!DAOWorker.HisMedicineTypeDAO.Update(data.HisMedicineType))
                    {
                        throw new Exception("Cap nhat du lieu HIS_MEDICINE_TYPE that bai. Ket thuc xu ly. Rollback du lieu");
                    }

                    //neu co truyen vao HIS_SERVICE thi moi thuc hien update HIS_SERVICE
                    //update sau de co thong tin hoat chat BHYT
                    if (data.HisMedicineType.HIS_SERVICE != null)
                    {
                        HisServiceSDO serviceSdo = new HisServiceSDO();
                        serviceSdo.HisService = data.HisMedicineType.HIS_SERVICE;
                        serviceSdo.HisService.SERVICE_CODE = data.HisMedicineType.MEDICINE_TYPE_CODE;
                        serviceSdo.HisService.SERVICE_NAME = data.HisMedicineType.MEDICINE_TYPE_NAME;
                        serviceSdo.HisService.IS_LEAF = data.HisMedicineType.IS_LEAF;
                        serviceSdo.UpdateSereServ = data.UpdateSereServ;
                        if (!new HisServiceUpdate(param).UpdateSdo(serviceSdo, ref oldService))
                        {
                            throw new Exception("Cap nhat du lieu HIS_SERVICE that bai. Ket thuc xu ly. Rollback du lieu");
                        }
                    }

                    //set lai is_leaf = null cho dich vu parent
                    if (data.HisMedicineType.PARENT_ID.HasValue)
                    {
                        HIS_MEDICINE_TYPE parent = new HisMedicineTypeGet().GetById(data.HisMedicineType.PARENT_ID.Value);
                        if (parent.IS_LEAF.HasValue)
                        {
                            parent.IS_LEAF = null;
                            if (!DAOWorker.HisMedicineTypeDAO.Update(parent))
                            {
                                throw new Exception("Cap nhat IS_LEAF du lieu parent that bai. Ket thuc xu ly. Rollback du lieu");
                            }

                            HIS_SERVICE parentService = new HisServiceGet().GetById(parent.SERVICE_ID);
                            parentService.IS_LEAF = null;
                            if (!DAOWorker.HisServiceDAO.Update(parentService))
                            {
                                throw new Exception("Cap nhat IS_LEAF du lieu Service parent that bai.");
                            }
                        }
                    }
                    //set lai is_leaf = null cho dich vu parent cu
                    if (raw.PARENT_ID.HasValue && raw.PARENT_ID.Value != data.HisMedicineType.PARENT_ID)
                    {
                        List<HIS_MEDICINE_TYPE> children = new HisMedicineTypeGet().GetByParentId(raw.PARENT_ID.Value);
                        if (!IsNotNullOrEmpty(children))
                        {
                            HIS_MEDICINE_TYPE oldParent = new HisMedicineTypeGet().GetById(raw.PARENT_ID.Value);
                            oldParent.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                            if (!DAOWorker.HisMedicineTypeDAO.Update(oldParent))
                            {
                                throw new Exception("Cap nhat IS_LEAF du lieu oldParent that bai. Ket thuc xu ly. Rollback du lieu");
                            }

                            HIS_SERVICE oldParentService = new HisServiceGet().GetById(oldParent.SERVICE_ID);
                            oldParentService.IS_LEAF = null;
                            if (!DAOWorker.HisServiceDAO.Update(oldParentService))
                            {
                                throw new Exception("Cap nhat IS_LEAF du lieu Service parent that bai.");
                            }
                        }
                    }

                    this.ProcessMedicineTypeAcin(data.HisMedicineType.ID, medicineTypeAcins, ref deletes, ref inserts);

                    result = true;

                    HisMedicineTypeLog.Run(data.HisMedicineType, raw, data.HisMedicineType.HIS_SERVICE, oldService1, inserts, deletes, LibraryEventLog.EventLog.Enum.HisMedicineType_Sua);
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
        private static bool IsDiffShort(short? oldValue, short? newValue)
        {
            return (oldValue ?? -1) != (newValue ?? -1);
        }
        private bool ProcessMedicineTypeAcin(long medicineTypeId, List<HIS_MEDICINE_TYPE_ACIN> data, ref List<HIS_MEDICINE_TYPE_ACIN> deletes, ref List<HIS_MEDICINE_TYPE_ACIN> inserts)
        {
            try
            {
                List<HIS_MEDICINE_TYPE_ACIN> medicineTypeAcins = new HisMedicineTypeAcinGet().GetByMedicineTypeId(medicineTypeId);
                List<HIS_MEDICINE_TYPE_ACIN> toInserts = null;
                List<HIS_MEDICINE_TYPE_ACIN> toDeletes = null;

                //Cac ban ghi can insert la cac ban ghi co trong d/s gui len nhung ko co trong DB
                if (data != null)
                {
                    toInserts = data
                        .Where(o => medicineTypeAcins == null
                            || !medicineTypeAcins.Exists(t => t.ACTIVE_INGREDIENT_ID == o.ACTIVE_INGREDIENT_ID))
                        .Select(o => new HIS_MEDICINE_TYPE_ACIN
                        {
                            ACTIVE_INGREDIENT_ID = o.ACTIVE_INGREDIENT_ID,
                            MEDICINE_TYPE_ID = medicineTypeId
                        }).ToList();
                }

                //Cac ban ghi can delete la cac ban ghi co DB nhung ko co trong trong d/s gui len
                if (medicineTypeAcins != null)
                {
                    toDeletes = medicineTypeAcins
                        .Where(o => data == null
                            || !data.Exists(t => t.ACTIVE_INGREDIENT_ID == o.ACTIVE_INGREDIENT_ID))
                        .ToList();
                }

                if (IsNotNullOrEmpty(toInserts) && !new HisMedicineTypeAcinCreate().CreateList(toInserts))
                {
                    LogSystem.Error("Tao thong tin HIS_MEDICINE_TYPE_ACIN that bai");
                    return false;
                }
                if (IsNotNullOrEmpty(toDeletes) && !new HisMedicineTypeAcinTruncate().TruncateList(toDeletes))
                {
                    LogSystem.Error("Xoa thong tin HIS_MEDICINE_TYPE_ACIN that bai");
                    return false;
                }
                inserts = toInserts;
                deletes = toDeletes;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        private bool CheckHeinInfo(HIS_MEDICINE_TYPE data)
        {
            bool valid = true;
            try
            {
                if (String.IsNullOrWhiteSpace(data.ACTIVE_INGR_BHYT_CODE)
                    || String.IsNullOrWhiteSpace(data.ACTIVE_INGR_BHYT_NAME)
                    || (data.HIS_SERVICE != null && (String.IsNullOrWhiteSpace(data.HIS_SERVICE.HEIN_SERVICE_BHYT_NAME))))
                {
                    HisServicePatyFilterQuery sPatyFilter = new HisServicePatyFilterQuery();
                    sPatyFilter.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT;
                    sPatyFilter.SERVICE_ID = data.SERVICE_ID;
                    List<HIS_SERVICE_PATY> servicePatys = new HisServicePatyGet().Get(sPatyFilter);
                    if (IsNotNullOrEmpty(servicePatys))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMedicineType_ThuocCoChinhSachGiaBHYTYeuCauNhapDayDuThongTinBHYT);
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
