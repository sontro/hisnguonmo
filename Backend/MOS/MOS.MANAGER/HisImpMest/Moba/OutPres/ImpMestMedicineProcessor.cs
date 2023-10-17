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

namespace MOS.MANAGER.HisImpMest.Moba.OutPres
{
    class ImpMestMedicineProcessor : BusinessBase
    {
        private HisImpMestMedicineCreate hisImpMestMedicineCreate;
        private HisExpMestMedicineIncreaseThAmount hisExpMestMedicineIncreaseThAmount;

        internal ImpMestMedicineProcessor()
            : base()
        {
            this.Init();
        }

        internal ImpMestMedicineProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestMedicineCreate = new HisImpMestMedicineCreate(param);
            this.hisExpMestMedicineIncreaseThAmount = new HisExpMestMedicineIncreaseThAmount(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, List<HisMobaPresSereServSDO> mobaPresMedicines, List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines, List<HIS_SERE_SERV> sereServMedicines, ref List<HIS_IMP_MEST_MEDICINE> hisImpMestMedicines)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(mobaPresMedicines))
                {
                    List<HIS_IMP_MEST_MEDICINE> listCreate = new List<HIS_IMP_MEST_MEDICINE>();

                    List<HIS_MEDICINE> hisMedicines = new HisMedicineGet().GetByIds(sereServMedicines.Select(s => s.MEDICINE_ID.Value).Distinct().ToList());

                    Dictionary<long, decimal> dicIncrease = new Dictionary<long, decimal>();

                    foreach (var mobaItem in mobaPresMedicines)
                    {
                        HIS_SERE_SERV sereServ = sereServMedicines.FirstOrDefault(o => o.ID == mobaItem.SereServId);
                        if (sereServ == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Khong lay duoc HIS_SERE_SERV theo id: " + mobaItem.SereServId);
                        }

                        //Kiem tra so luong kha dung thu hoi
                        if (sereServ.AMOUNT < mobaItem.Amount)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMobaImpMest_SoLuongYeuCauThuHoiVuotQuaSoLuongKhaDung);
                            return false;
                        }
                        HIS_EXP_MEST_MEDICINE expMestMedicine = null;
                        //Lay ra tat cac cac SereServ tuong uong voi ExpMestMedicine
                        if (sereServ.EXP_MEST_MEDICINE_ID.HasValue)
                        {
                            expMestMedicine = hisExpMestMedicines.Where(o => o.ID == sereServ.EXP_MEST_MEDICINE_ID.Value).FirstOrDefault();
                        }
                        else
                        {
                            expMestMedicine = hisExpMestMedicines
                                .Where(o => o.MEDICINE_ID == sereServ.MEDICINE_ID
                                    && o.IS_EXPEND == sereServ.IS_EXPEND
                                    && o.PATIENT_TYPE_ID == sereServ.PATIENT_TYPE_ID
                                    && o.IS_OUT_PARENT_FEE == sereServ.IS_OUT_PARENT_FEE
                                    )
                                .FirstOrDefault();
                        }

                        if (expMestMedicine == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                            throw new Exception("Khong lay duoc HIS_EXP_MEST_MEDICINE tuong ung voi HIS_SERE_SERV" + LogUtil.TraceData("sereServ", sereServ));
                        }

                        //Kiem tra so luong trong sereServ co du thu hoi hay khong
                        if (mobaItem.Amount > (expMestMedicine.AMOUNT - (expMestMedicine.TH_AMOUNT ?? 0)))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMobaImpMest_SoLuongYeuCauThuHoiVuotQuaSoLuongKhaDung);
                            return false;
                        }

                        //Cong vao tranh truong hop frontend gui len 2 dong cua cung 1 expMestMedicineID
                        sereServ.AMOUNT = sereServ.AMOUNT - mobaItem.Amount;

                        expMestMedicine.TH_AMOUNT = (expMestMedicine.TH_AMOUNT ?? 0) + mobaItem.Amount;
                        if (dicIncrease.ContainsKey(expMestMedicine.ID))
                        {
                            dicIncrease[expMestMedicine.ID] += mobaItem.Amount;
                        }
                        else
                        {
                            dicIncrease[expMestMedicine.ID] = mobaItem.Amount;
                        }

                        HIS_IMP_MEST_MEDICINE impMestMedicine = new HIS_IMP_MEST_MEDICINE();
                        impMestMedicine.AMOUNT = mobaItem.Amount;
                        impMestMedicine.MEDICINE_ID = expMestMedicine.MEDICINE_ID.Value;
                        impMestMedicine.IMP_MEST_ID = impMest.ID;
                        impMestMedicine.TH_EXP_MEST_MEDICINE_ID = expMestMedicine.ID;
                        impMestMedicine.PRICE = sereServ.PRICE;
                        impMestMedicine.VAT_RATIO = sereServ.VAT_RATIO;
                        listCreate.Add(impMestMedicine);
                    }
                    if (!this.hisImpMestMedicineCreate.CreateList(listCreate))
                    {
                        throw new Exception("Tao HIS_IMP_MEST_MEDICINE that bai");
                    }
                    if (!this.hisExpMestMedicineIncreaseThAmount.Run(dicIncrease))
                    {
                        throw new Exception("Update ThAmount cho HIS_EXP_MEST_MEDICINE  that bai");
                    }
                    hisImpMestMedicines = listCreate;
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
