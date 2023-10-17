using Inventec.Common.Logging;
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

namespace MOS.MANAGER.HisImpMest.Moba.Pres
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

        internal bool Run(HIS_IMP_MEST impMest, List<HisMobaPresMedicineSDO> mobaPresMedicines, List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines, List<HIS_SERE_SERV> hisSereServs, ref List<HIS_IMP_MEST_MEDICINE> hisImpMestMedicines, ref List<HIS_SERE_SERV> sereServUpdates)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(mobaPresMedicines))
                {
                    List<HIS_IMP_MEST_MEDICINE> listCreate = new List<HIS_IMP_MEST_MEDICINE>();
                    List<HIS_MEDICINE> hisMedicines = new HisMedicineGet().GetByIds(hisExpMestMedicines.Select(s => s.MEDICINE_ID.Value).Distinct().ToList());

                    Dictionary<long, decimal> dicIncrease = new Dictionary<long, decimal>();

                    foreach (var mobaItem in mobaPresMedicines)
                    {
                        HIS_EXP_MEST_MEDICINE expMestMedicine = hisExpMestMedicines
                            .FirstOrDefault(o => o.ID == mobaItem.ExpMestMedicineId);
                        if (expMestMedicine == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Khong lay duoc HIS_EXP_MEST_MEDICINE theo id: " + mobaItem.ExpMestMedicineId);
                        }

                        //Kiem tra so luong kha dung thu hoi
                        if ((expMestMedicine.AMOUNT - (expMestMedicine.TH_AMOUNT ?? 0)) < mobaItem.Amount)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMobaImpMest_SoLuongYeuCauThuHoiVuotQuaSoLuongKhaDung);
                            return false;
                        }

                        //Lay ra SereServ tuong ung voi ExpMestMedicine
                        List<HIS_SERE_SERV> sereServs = null;
                        if (hisSereServs.Exists(e => e.SERVICE_REQ_ID == expMestMedicine.TDL_SERVICE_REQ_ID && e.MEDICINE_ID.HasValue && e.EXP_MEST_MEDICINE_ID.HasValue))
                        {
                            sereServs = hisSereServs.Where(o => o.SERVICE_REQ_ID == expMestMedicine.TDL_SERVICE_REQ_ID && o.EXP_MEST_MEDICINE_ID == expMestMedicine.ID).ToList();
                        }
                        else
                        {
                            sereServs = hisSereServs
                                .Where(o => o.SERVICE_REQ_ID == expMestMedicine.TDL_SERVICE_REQ_ID.Value
                                && o.MEDICINE_ID == expMestMedicine.MEDICINE_ID
                                && o.IS_EXPEND == expMestMedicine.IS_EXPEND
                                && o.PATIENT_TYPE_ID == expMestMedicine.PATIENT_TYPE_ID
                                && o.IS_OUT_PARENT_FEE == expMestMedicine.IS_OUT_PARENT_FEE)
                                .ToList();
                        }

                        if (!IsNotNullOrEmpty(sereServs))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                            throw new Exception("Khong lay duoc HIS_SERE_SERV tuong ung voi HIS_EXP_MEST_MEDICINE" + LogUtil.TraceData("expMestMedicine", expMestMedicine));
                        }

                        //Oder theo so luong giam dan de sl SereServ update nho nhat
                        sereServs = sereServs.OrderByDescending(o => o.AMOUNT).ToList();

                        //Kiem tra so luong trong sereServs co du de thu hoi hay khong
                        if (mobaItem.Amount > sereServs.Sum(s => s.AMOUNT))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMobaImpMest_SoLuongYeuCauThuHoiVuotQuaSoLuongKhaDung);
                            return false;
                        }

                        //Cong vao tranh truong hop frontend gui len hai dong 1 exp_mest_medicine_id;
                        expMestMedicine.TH_AMOUNT = (expMestMedicine.TH_AMOUNT ?? 0) + mobaItem.Amount;

                        if (dicIncrease.ContainsKey(expMestMedicine.ID))
                        {
                            dicIncrease[expMestMedicine.ID] += mobaItem.Amount;
                        }
                        else
                        {
                            dicIncrease[expMestMedicine.ID] = mobaItem.Amount;
                        }

                        //Duyet update sereServ
                        decimal amount = mobaItem.Amount;
                        foreach (var ss in sereServs)
                        {
                            if (amount <= 0) break;

                            if (ss.AMOUNT <= 0)
                                continue;
                            if (ss.AMOUNT >= amount)
                            {
                                ss.AMOUNT -= amount;
                                sereServUpdates.Add(ss);
                                break;
                            }
                            else
                            {
                                ss.AMOUNT = 0;
                                amount -= ss.AMOUNT;
                                sereServUpdates.Add(ss);
                            }
                        }
                        HIS_IMP_MEST_MEDICINE impMestMedicine = new HIS_IMP_MEST_MEDICINE();
                        impMestMedicine.AMOUNT = mobaItem.Amount;
                        impMestMedicine.MEDICINE_ID = expMestMedicine.MEDICINE_ID.Value;
                        impMestMedicine.IMP_MEST_ID = impMest.ID;
                        impMestMedicine.TH_EXP_MEST_MEDICINE_ID = expMestMedicine.ID;
                        listCreate.Add(impMestMedicine);
                    }
                    if (!this.hisImpMestMedicineCreate.CreateList(listCreate))
                    {
                        throw new Exception("Tao HIS_IMP_MEST_MEDICINE that bai");
                    }
                    if (!this.hisExpMestMedicineIncreaseThAmount.Run(dicIncrease))
                    {
                        throw new Exception("Update ThAmount trong HIS_EXP_MEST_MEDICINE that bai");
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
            try
            {
                this.hisExpMestMedicineIncreaseThAmount.RollbackData();
                this.hisImpMestMedicineCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
