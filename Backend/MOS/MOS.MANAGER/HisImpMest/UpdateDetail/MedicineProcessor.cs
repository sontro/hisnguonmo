using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.UpdateDetail
{
    class MedicineProcessor : BusinessBase
    {
        private HisImpMestMedicineUpdate hisImpMestMedicineUpdate;
        private HisMedicineUpdate hisMedicineUpdate;

        internal MedicineProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestMedicineUpdate = new HisImpMestMedicineUpdate(param);
            this.hisMedicineUpdate = new HisMedicineUpdate(param);
        }

        internal bool Run(HisImpMestUpdateDetailSDO data, HIS_IMP_MEST impMest, HisImpMestUpdateDetailLog logProcessor)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(data.ImpMestMedicines))
                {
                    List<HIS_IMP_MEST_MEDICINE> listRaw = new List<HIS_IMP_MEST_MEDICINE>();
                    List<HIS_MEDICINE> rawMedicines = new List<HIS_MEDICINE>();
                    HisImpMestMedicineCheck checker = new HisImpMestMedicineCheck(param);
                    HisMedicineCheck mediChecker = new HisMedicineCheck(param);
                    bool valid = true;
                    valid = valid && checker.VerifyIds(data.ImpMestMedicines.Select(s => s.Id).ToList(), listRaw);
                    valid = valid && checker.IsUnLock(listRaw);
                    valid = valid && mediChecker.VerifyIds(listRaw.Select(s => s.MEDICINE_ID).ToList(), rawMedicines);
                    valid = valid && mediChecker.IsUnLock(rawMedicines);
                    if (!valid)
                    {
                        return false;
                    }
                    Mapper.CreateMap<HIS_IMP_MEST_MEDICINE, HIS_IMP_MEST_MEDICINE>();
                    List<HIS_IMP_MEST_MEDICINE> beforeImpMestMedicines = Mapper.Map<List<HIS_IMP_MEST_MEDICINE>>(listRaw);
                    Mapper.CreateMap<HIS_MEDICINE, HIS_MEDICINE>();
                    List<HIS_MEDICINE> beforeMedicines = Mapper.Map<List<HIS_MEDICINE>>(rawMedicines);
                    if (!this.CheckDetail(data, impMest, listRaw, rawMedicines, logProcessor))
                    {
                        return false;
                    }

                    if (!this.hisImpMestMedicineUpdate.UpdateList(listRaw, beforeImpMestMedicines))
                    {
                        throw new Exception("hisImpMestMedicineUpdate. Ket thuc nghiep vu");
                    }

                    if (!this.hisMedicineUpdate.UpdateList(rawMedicines, beforeMedicines))
                    {
                        throw new Exception("hisMedicineUpdate. Ket thuc nghiep vu");
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

        private bool CheckDetail(HisImpMestUpdateDetailSDO data, HIS_IMP_MEST impMest, List<HIS_IMP_MEST_MEDICINE> listRaw, List<HIS_MEDICINE> rawMedicines, HisImpMestUpdateDetailLog logProcessor)
        {
            bool valid = true;
            try
            {
                if (listRaw.Exists(e => e.IMP_MEST_ID != impMest.ID))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai HIS_IMP_MEST_MEDICINE khong thuoc phieu nhap can sua");
                }

                if (valid)
                {
                    foreach (HisImpMestMedicineSDO sdo in data.ImpMestMedicines)
                    {
                        HIS_IMP_MEST_MEDICINE impMestMedi = listRaw.FirstOrDefault(o => o.ID == sdo.Id);
                        HIS_MEDICINE medicine = rawMedicines.FirstOrDefault(o => o.ID == impMestMedi.MEDICINE_ID);
                        HIS_MEDICINE_TYPE medicineType = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == medicine.MEDICINE_TYPE_ID);
                        HIS_MEDICINE_TYPE newMedicineType = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == sdo.MedicineTypeId);
                        if (!this.CheckExists(medicine, impMest, medicineType))
                        {
                            return false;
                        }

                        if ((impMestMedi.TDL_IMP_UNIT_ID.HasValue || newMedicineType.IMP_UNIT_ID.HasValue) && newMedicineType.ID != medicineType.ID)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_KhongChoPhepDoiLoaiThuocCoCauHinhDonViNhap);
                            throw new Exception("Khong cho phep doi loai thuoc co thong tin TDL_IMP_UNIT_ID");
                        }

                        logProcessor.GenerateLogMessage(medicine, medicineType, newMedicineType, sdo);
                        //impMestMedi.PRICE = sdo.ImpPrice;
                        impMestMedi.VAT_RATIO = sdo.ImpVatRatio;
                        impMestMedi.DOCUMENT_PRICE = sdo.DocumentPrice;
                        impMestMedi.TEMPERATURE = sdo.Temperature;
                        if (impMestMedi.TDL_IMP_UNIT_ID.HasValue)
                        {
                            impMestMedi.IMP_UNIT_PRICE = sdo.ImpPrice;
                            medicine.IMP_UNIT_PRICE = sdo.ImpPrice;
                            impMestMedi.PRICE = impMestMedi.IMP_UNIT_PRICE.Value / impMestMedi.TDL_IMP_UNIT_CONVERT_RATIO.Value;
                            medicine.IMP_PRICE = impMestMedi.PRICE.Value;
                        }
                        else
                        {
                            impMestMedi.PRICE = sdo.ImpPrice;
                            medicine.IMP_PRICE = sdo.ImpPrice;
                            impMestMedi.IMP_UNIT_PRICE = null;
                            medicine.IMP_UNIT_PRICE = null;
                        }
                        //impMestMedi.AMOUNT = sdo.Amount;
                        //medicine.AMOUNT = sdo.Amount;
                        //medicine.IMP_PRICE = sdo.ImpPrice;
                        medicine.IMP_VAT_RATIO = sdo.ImpVatRatio;
                        medicine.PACKAGE_NUMBER = sdo.PackageNumber;
                        medicine.EXPIRED_DATE = sdo.ExpireDate;
                        medicine.INTERNAL_PRICE = newMedicineType.INTERNAL_PRICE;
                        medicine.MEDICINE_TYPE_ID = newMedicineType.ID;
                        medicine.TDL_SERVICE_ID = newMedicineType.SERVICE_ID;
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

        private bool CheckExists(HIS_MEDICINE medicine, HIS_IMP_MEST impMest, HIS_MEDICINE_TYPE medicineType)
        {
            bool valid = true;
            try
            {
                List<V_HIS_EXP_MEST_MEDICINE> existsExps = new HisExpMestMedicineGet().GetViewByMedicineId(medicine.ID);
                if (IsNotNullOrEmpty(existsExps))
                {
                    List<string> expMestCodes = existsExps.Select(s => s.EXP_MEST_CODE).Distinct().ToList();
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ThuocDaThuocPhieuXuat, medicineType.MEDICINE_TYPE_NAME, String.Join(";", expMestCodes));
                    return false;
                }
                List<V_HIS_IMP_MEST_MEDICINE> existsImps = new HisImpMestMedicineGet().GetViewByMedicineId(medicine.ID);
                existsImps = existsImps != null ? existsImps.Where(o => o.IMP_MEST_ID != impMest.ID).ToList() : null;
                if (IsNotNullOrEmpty(existsImps))
                {
                    List<string> impMestCodes = existsImps.Select(s => s.IMP_MEST_CODE).Distinct().ToList();
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ThuocDaThuocPhieuNhap, medicineType.MEDICINE_TYPE_NAME, String.Join(";", impMestCodes));
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal void RollbackData()
        {
            try
            {
                this.hisMedicineUpdate.RollbackData();
                this.hisImpMestMedicineUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
