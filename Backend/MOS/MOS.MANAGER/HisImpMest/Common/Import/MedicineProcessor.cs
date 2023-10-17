using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMedicineBean.Update;
using MOS.MANAGER.HisMediStockMety;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Import
{
    class MedicineProcessor : BusinessBase
    {
        private HisMedicineBeanCreateSql hisMedicineBeanCreate;

        private List<long> medicineIds;

        internal MedicineProcessor(CommonParam param)
            : base(param)
        {
            this.hisMedicineBeanCreate = new HisMedicineBeanCreateSql(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, List<HIS_IMP_MEST_MEDICINE> hisImpMestMedicines, ref List<long> medicineTypeIds)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(hisImpMestMedicines))
                {
                    List<HIS_MEDICINE> hisMedicines = null;
                    // lay ve tat cac cac medicine phuc vu set cac truong TDL trong bean va update available amount
                    hisMedicines = new HisMedicineGet().GetByIds(hisImpMestMedicines.Select(s => s.MEDICINE_ID).ToList());
                    medicineTypeIds = hisMedicines.Select(s => s.MEDICINE_TYPE_ID).Distinct().ToList();
                    //Kiem tra xem co medicine nao bi khoa hay khong
                    if (hisMedicines.Exists(e => e.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                        throw new Exception("Ton tai lo thuoc dang bi khoa " + LogUtil.TraceData("hisMedicines", hisMedicines));
                    }
                    //Neu loai nhap la nhap NCC, nhap KK, nhap DK, nhap Khac => Update ImpTime trong MEDICINE
                    //Xet impTime phuc vu cho SetTdl medicineBean
                    if (HisImpMestContanst.TYPE_MUST_CREATE_PACKAGE_IDS.Contains(impMest.IMP_MEST_TYPE_ID))
                    {
                        hisMedicines.ForEach(o => o.IMP_TIME = impMest.IMP_TIME);
                        string updateMedicine = new StringBuilder().Append("UPDATE HIS_MEDICINE SET IMP_TIME = ").Append(impMest.IMP_TIME.Value).Append(", IS_PREGNANT = NULL").Append(" WHERE %IN_CLAUSE% ").ToString();
                        updateMedicine = DAOWorker.SqlDAO.AddInClause(hisImpMestMedicines.Select(s => s.MEDICINE_ID).ToList(), updateMedicine, "ID");
                        if (!DAOWorker.SqlDAO.Execute(updateMedicine))
                        {
                            throw new Exception("Update IMP_TIME trong HIS_MEDICINE that bai. Rollback du lieu");
                        }
                        this.medicineIds = hisImpMestMedicines.Select(s => s.MEDICINE_ID).ToList();
                    }

                    var Groups = hisImpMestMedicines.GroupBy(g => g.MEDICINE_ID).ToList();
                    List<HIS_MEDICINE_BEAN> toInserts = new List<HIS_MEDICINE_BEAN>();
                    foreach (var group in Groups)
                    {
                        List<HIS_IMP_MEST_MEDICINE> listByGroup = group.ToList();
                        HIS_MEDICINE_BEAN bean = new HIS_MEDICINE_BEAN();
                        bean.AMOUNT = listByGroup.Sum(s => s.AMOUNT);
                        bean.MEDICINE_ID = group.Key;
                        bean.MEDI_STOCK_ID = impMest.MEDI_STOCK_ID;
                        if (HisImpMestContanst.TYPE_MOBA_IDS.Contains(impMest.IMP_MEST_TYPE_ID))
                        {
                            bean.IS_TH = MOS.UTILITY.Constant.IS_TRUE;
                        }
                        else if (HisImpMestContanst.TYPE_CHMS_IDS.Contains(impMest.IMP_MEST_TYPE_ID))
                        {
                            bean.IS_CK = MOS.UTILITY.Constant.IS_TRUE;
                        }
                        HisMedicineBeanUtil.SetTdl(bean, hisMedicines.FirstOrDefault(o => o.ID == group.Key));

                        toInserts.Add(bean);
                    }

                    if (IsNotNullOrEmpty(toInserts) && !this.hisMedicineBeanCreate.Run(toInserts))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
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
                if (IsNotNullOrEmpty(this.medicineIds))
                {
                    string rollbackMedicineSql = DAOWorker.SqlDAO.AddInClause(this.medicineIds, "UPDATE HIS_MEDICINE SET IMP_TIME = NULL, IS_PREGNANT = 1 WHERE %IN_CLAUSE% ", "ID");
                    if (!DAOWorker.SqlDAO.Execute(rollbackMedicineSql))
                    {
                        LogSystem.Warn("Rollback HIS_MEDICINE that bai SQL: " + rollbackMedicineSql);
                    }
                    this.medicineIds = null;
                }
                this.hisMedicineBeanCreate.Rollback();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
