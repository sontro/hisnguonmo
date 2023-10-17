using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.HisMediStockMety;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.UnImport
{
   
    class MedicineProcessor : BusinessBase
    {
        private HisImpMestMedicineUpdate hisImpMestMedicineUpdate;
        private HisMedicineUpdate hisMedicineUpdate;
        private HisMedicineBeanSplit hisMedicineBeanSplit;
        private HisMedicineBeanUnimport hisMedicineBeanUnimport;

        internal MedicineProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestMedicineUpdate = new HisImpMestMedicineUpdate(param);
            this.hisMedicineUpdate = new HisMedicineUpdate(param);
            this.hisMedicineBeanSplit = new HisMedicineBeanSplit();
            this.hisMedicineBeanUnimport = new HisMedicineBeanUnimport(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, ref List<long> medicineTypeIds)
        {
            bool result = false;
            try
            {
                List<HIS_IMP_MEST_MEDICINE> hisImpMestMedicines = new HisImpMestMedicineGet().GetByImpMestId(impMest.ID);
                if (IsNotNullOrEmpty(hisImpMestMedicines))
                {
                    HisImpMestCheck checker = new HisImpMestCheck(param);
                    List<HIS_MEDICINE> hisMedicines = new List<HIS_MEDICINE>();
                    List<ExpMedicineSDO> medicineSDOs = new List<ExpMedicineSDO>();
                    var Groups = hisImpMestMedicines.GroupBy(g => g.MEDICINE_ID).ToList();
                    foreach (var group in Groups)
                    {
                        if (HisImpMestContanst.TYPE_MUST_CREATE_PACKAGE_IDS.Contains(impMest.IMP_MEST_TYPE_ID))
                        {
                            HIS_MEDICINE medicine = new HisMedicineGet().GetById(group.Key);
                            if (medicine == null)
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                                throw new Exception("MEDICINE_ID Invalid: " + group.Key);
                            }
                            if (medicine.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                            {
                                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                                throw new Exception("Lo thuoc dang bi khoa: " + LogUtil.TraceData("Medicine", medicine));
                            }
                            hisMedicines.Add(medicine);
                        }

                        if (!checker.CheckCorrectImpExpMedicine(group.Key, impMest.MEDI_STOCK_ID, impMest.ID))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ThuocVatTuDaDuocXuat);
                            throw new Exception("Du lieu nhap, xuat khong con tinh dung dan khi huy nhap. MedicineId: " + group.Key);
                        }
                        decimal totalAmount = group.Sum(s => s.AMOUNT);
                        if (totalAmount <= 0) continue;
                        ExpMedicineSDO sdo = new ExpMedicineSDO();
                        sdo.Amount = totalAmount;
                        sdo.MedicineId = group.Key;
                        medicineSDOs.Add(sdo);
                    }
                    List<HIS_MEDICINE_BEAN> hisMedicineBeans = null;
                    if (!this.hisMedicineBeanSplit.SplitByMedicine(medicineSDOs, impMest.MEDI_STOCK_ID, ref hisMedicineBeans))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ThuocVatTuDaDuocXuat);
                        throw new Exception("Khong tach du bean theo medicine, co the do khong du so luong " + LogUtil.TraceData("medicineSDOs", medicineSDOs));
                    }

                    if (!this.hisMedicineBeanUnimport.Run(hisMedicineBeans.Select(s => s.ID).ToList(), impMest.MEDI_STOCK_ID))
                    {
                        throw new Exception("Update IS_ACTIVE, MEDI_STOCK_ID cho MEDICINE_BEAN that bai");
                    }

                    if (IsNotNullOrEmpty(hisMedicines))
                    {
                        if (HisImpMestContanst.TYPE_MUST_CREATE_PACKAGE_IDS.Contains(impMest.IMP_MEST_TYPE_ID))
                        {
                            medicineTypeIds.AddRange(hisMedicines.Select(s => s.MEDICINE_TYPE_ID).Distinct().ToList());
                        }
                        Mapper.CreateMap<HIS_MEDICINE, HIS_MEDICINE>();
                        List<HIS_MEDICINE> medicineBefores = Mapper.Map<List<HIS_MEDICINE>>(hisMedicines);
                        hisMedicines.ForEach(o =>
                        {
                            o.IMP_TIME = null;
                            o.IS_PREGNANT = MOS.UTILITY.Constant.IS_TRUE;
                        });
                        if (!this.hisMedicineUpdate.UpdateList(hisMedicines, medicineBefores))
                        {
                            throw new Exception("Update HIS_MEDICINE that bai");
                        }
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                this.hisImpMestMedicineUpdate.RollbackData();
                this.hisMedicineBeanUnimport.Rollback();
                this.hisMedicineBeanSplit.RollBack();
                this.hisMedicineUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
