using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Moba.Sale
{
    class ImpMestMedicineProcessor : BusinessBase
    {
        private HisImpMestMedicineCreate hisImpMestMedicineCreate;
        private HisExpMestMedicineIncreaseThAmount hisExpMestMedicineIncreaseThAmount;

        internal ImpMestMedicineProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestMedicineIncreaseThAmount = new HisExpMestMedicineIncreaseThAmount(param);
            this.hisImpMestMedicineCreate = new HisImpMestMedicineCreate(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, List<HisMobaMedicineSDO> mobaMedicines, HIS_EXP_MEST expMest, ref List<HIS_IMP_MEST_MEDICINE> hisImpMestMedicines)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(mobaMedicines))
                {
                    List<HIS_IMP_MEST_MEDICINE> listCreate = new List<HIS_IMP_MEST_MEDICINE>();
                    List<HIS_EXP_MEST_MEDICINE> existedExpMestMedicines = new HisExpMestMedicineGet().GetExportedByExpMestId(expMest.ID);

                    Dictionary<long, decimal> dicIncrease = new Dictionary<long, decimal>();

                    if (!IsNotNullOrEmpty(existedExpMestMedicines))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Khong lay duoc HIS_EXP_MEST_MEDICINE theo ExpMestId: " + expMest.ID);
                    }
                    List<HIS_MEDICINE> hisMedicines = new HisMedicineGet().GetByIds(existedExpMestMedicines.Select(s => s.MEDICINE_ID.Value).ToList());
                    if (!IsNotNullOrEmpty(hisMedicines))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        throw new Exception("Khong lay duoc HIS_MEDICINE theo IDs");
                    }

                    var Groups = mobaMedicines.GroupBy(g => g.MedicineId).ToList();
                    foreach (var group in Groups)
                    {
                        List<HisMobaMedicineSDO> listByGroup = group.ToList();
                        decimal amount = listByGroup.Sum(s => s.Amount);
                        List<HIS_EXP_MEST_MEDICINE> listByMedicine = existedExpMestMedicines.Where(o => o.MEDICINE_ID == group.Key).ToList();
                        if (!IsNotNullOrEmpty(listByMedicine))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Khong lay duoc HIS_EXP_MEST_MEDICINE theo medicineId: " + group.Key);
                        }
                        listByMedicine = listByMedicine.OrderByDescending(o => (o.AMOUNT - (o.TH_AMOUNT ?? 0))).ToList();
                        HIS_MEDICINE medicine = hisMedicines.FirstOrDefault(o => o.ID == group.Key);
                        if (medicine == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                            throw new Exception("Khong lay duoc HIS_MEDICINE theo ID: " + group.Key);
                        }

                        decimal canMoba = listByMedicine.Sum(s => (s.AMOUNT - (s.TH_AMOUNT ?? 0)));
                        if (canMoba < amount)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMobaImpMest_SoLuongYeuCauThuHoiVuotQuaSoLuongKhaDung);
                            throw new Exception("Yeu cau thu hoi lon hon kha dung thu hoi");
                        }

                        foreach (var exp in listByMedicine)
                        {
                            if (amount <= 0) break;
                            if ((exp.AMOUNT - (exp.TH_AMOUNT ?? 0)) >= amount)
                            {
                                dicIncrease[exp.ID] = amount;
                                HIS_IMP_MEST_MEDICINE impMestMedicine = new HIS_IMP_MEST_MEDICINE();
                                impMestMedicine.AMOUNT = amount;
                                impMestMedicine.IMP_MEST_ID = impMest.ID;
                                impMestMedicine.MEDICINE_ID = exp.MEDICINE_ID.Value;
                                impMestMedicine.TH_EXP_MEST_MEDICINE_ID = exp.ID;
                                listCreate.Add(impMestMedicine);
                                break;
                            }
                            else
                            {
                                dicIncrease[exp.ID] = exp.AMOUNT - (exp.TH_AMOUNT ?? 0);
                                amount = amount - dicIncrease[exp.ID];
                                HIS_IMP_MEST_MEDICINE impMestMedicine = new HIS_IMP_MEST_MEDICINE();
                                impMestMedicine.AMOUNT = dicIncrease[exp.ID];
                                impMestMedicine.IMP_MEST_ID = impMest.ID;
                                impMestMedicine.MEDICINE_ID = exp.MEDICINE_ID.Value;
                                impMestMedicine.TH_EXP_MEST_MEDICINE_ID = exp.ID;
                                listCreate.Add(impMestMedicine);
                            }
                        }
                    }

                    if (!this.hisImpMestMedicineCreate.CreateList(listCreate))
                    {
                        throw new Exception("Tao HIS_IMP_MEST_MEDICINE that bai");
                    }
                    if (!this.hisExpMestMedicineIncreaseThAmount.Run(dicIncrease))
                    {
                        throw new Exception("Update ThAmount cho HIS_EXP_MEST_MEDICINE that bai");
                    }
                    hisImpMestMedicines = listCreate;
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        internal void Rollback()
        {
            this.hisImpMestMedicineCreate.RollbackData();
            this.hisExpMestMedicineIncreaseThAmount.RollbackData();
        }
    }
}
