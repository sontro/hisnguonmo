using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMedicineBean.Handle
{
    /// <summary>
    /// Huy thuc xuat
    /// - Tao cac bean moi theo thong tin trong exp_mest_medicine va gan vao kho
    /// </summary>
    class HisMedicineBeanUnexport : BusinessBase
    {
        private HisMedicineBeanCreateSql hisMedicineBeanCreate;

        internal HisMedicineBeanUnexport()
            : base()
        {
            this.hisMedicineBeanCreate = new HisMedicineBeanCreateSql(param);
        }

        internal HisMedicineBeanUnexport(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.hisMedicineBeanCreate = new HisMedicineBeanCreateSql(param);
        }

        internal bool Run(List<HIS_EXP_MEST_MEDICINE> expMestMedicines, long mediStockId)
        {
            try
            {
                if (IsNotNullOrEmpty(expMestMedicines))
                {
                    List<long> medicineIds = expMestMedicines.Select(o => o.MEDICINE_ID.Value).ToList();
                    List<HIS_MEDICINE> hisMedicines = new HisMedicineGet().GetByIds(medicineIds);

                    List<HIS_MEDICINE_BEAN> toInserts = new List<HIS_MEDICINE_BEAN>();

                    foreach (HIS_EXP_MEST_MEDICINE exp in expMestMedicines)
                    {
                        HIS_MEDICINE_BEAN bean = new HIS_MEDICINE_BEAN();
                        bean.AMOUNT = exp.AMOUNT;
                        bean.MEDICINE_ID = exp.MEDICINE_ID.Value;
                        bean.MEDI_STOCK_ID = mediStockId;
                        bean.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;
                        bean.EXP_MEST_MEDICINE_ID = exp.ID;
                        HisMedicineBeanUtil.SetTdl(bean, hisMedicines.FirstOrDefault(o => o.ID == exp.MEDICINE_ID));
                        toInserts.Add(bean);
                    }

                    if (!this.hisMedicineBeanCreate.Run(toInserts))
                    {
                        throw new Exception("Tao bean de huy thuc xuat that bai");
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal void Rollback()
        {
            this.hisMedicineBeanCreate.Rollback();
        }
    }
}
