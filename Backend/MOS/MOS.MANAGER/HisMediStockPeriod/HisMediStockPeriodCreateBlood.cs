using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBlood;
using MOS.MANAGER.HisMestPeriodBlood;
using MOS.MANAGER.HisMestPeriodBlty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockPeriod
{
    partial class HisMediStockPeriodCreate : BusinessBase
    {
        private List<V_HIS_EXP_MEST_BLOOD> appliedExpMestBloods;
        private List<V_HIS_IMP_MEST_BLOOD> appliedImpMestBloods;

        private List<HIS_BLOOD> ListBlood;

        private HisMestPeriodBltyCreateSql hisMestPeriodBltyCreate;
        private HisMestPeriodBloodCreateSql hisMestPeriodBloodCreate;

        private void PrepareDataBlood(HIS_MEDI_STOCK_PERIOD data)
        {
            this.hisMestPeriodBltyCreate = new HisMestPeriodBltyCreateSql(param);
            this.hisMestPeriodBloodCreate = new HisMestPeriodBloodCreateSql(param);

            appliedExpMestBloods = this.GetAppliedExpMestBloods(data);
            appliedImpMestBloods = this.GetAppliedImpMestBloods(data);
            ListBlood = this.GetBloodInStock(data.MEDI_STOCK_ID);
        }

        private List<HIS_BLOOD> GetBloodInStock(long mediStockId)
        {
            HisBloodFilterQuery filter = new HisBloodFilterQuery();
            filter.MEDI_STOCK_ID = mediStockId;
            return new HisBloodGet().Get(filter);
        }

        /// <summary>
        /// lay danh sach chi tiet nhap da thuc nhap
        /// co thoi gian thuc nhap nho hon thoi gian chot ky
        /// </summary>
        /// <param name="mediStockId"></param>
        /// <returns></returns>
        private List<V_HIS_IMP_MEST_BLOOD> GetAppliedImpMestBloods(HIS_MEDI_STOCK_PERIOD data)
        {
            string query = "SELECT * FROM V_HIS_IMP_MEST_BLOOD IMPB WHERE IMPB.IS_DELETE = 0 ";
            query += " AND MEDI_STOCK_PERIOD_ID IS NULL AND MEDI_STOCK_ID = :param1 AND IMP_TIME < :param2 AND IMP_MEST_STT_ID = :param3";

            return DAOWorker.SqlDAO.GetSql<V_HIS_IMP_MEST_BLOOD>(query, data.MEDI_STOCK_ID, data.TO_TIME, IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT);
        }

        /// <summary>
        /// lay danh sach chi tiet xuat da thuc xuat het
        /// co thoi gian thuc xuat nho hon thoi gian chot ky
        /// </summary>
        /// <param name="mediStockId"></param>
        /// <returns></returns>
        private List<V_HIS_EXP_MEST_BLOOD> GetAppliedExpMestBloods(HIS_MEDI_STOCK_PERIOD data)
        {
            string query = "SELECT * FROM V_HIS_EXP_MEST_BLOOD EXPB WHERE MEDI_STOCK_PERIOD_ID IS NULL AND TDL_MEDI_STOCK_ID = :param1 ";
            query += " AND IS_DELETE = 0 AND EXP_TIME <= :param2 AND EXP_MEST_STT_ID = :param3";

            return DAOWorker.SqlDAO.GetSql<V_HIS_EXP_MEST_BLOOD>(query, data.MEDI_STOCK_ID, data.TO_TIME, IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE);
        }

        /// <summary>
        /// Tao du lieu HIS_MEST_PERIOD_BLTY
        /// </summary>
        private void ProcessMestPeriodBlty()
        {
            if (!IsNotNullOrEmpty(Config.HisBloodTypeCFG.DATA)) return;

            //Danh sach chot ky truoc
            List<HIS_MEST_PERIOD_BLTY> previousMestPeriodBlties = null;
            List<HIS_MEST_PERIOD_BLTY> hisMestPeriodBltys = new List<HIS_MEST_PERIOD_BLTY>();
            if (this.previousHisMediStockPeriod != null)
            {
                previousMestPeriodBlties = this.GetPreviousMestPeriodBlty();
            }

            foreach (HIS_BLOOD_TYPE bloodType in Config.HisBloodTypeCFG.DATA)
            {
                HIS_MEST_PERIOD_BLTY previous = null;
                if (previousMestPeriodBlties != null)
                {
                    previous = previousMestPeriodBlties.SingleOrDefault(o => o.BLOOD_TYPE_ID == bloodType.ID);
                }

                HIS_MEST_PERIOD_BLTY dto = new HIS_MEST_PERIOD_BLTY();
                dto.BLOOD_TYPE_ID = bloodType.ID;
                dto.MEDI_STOCK_PERIOD_ID = this.recentHisMediStockPeriod.ID;
                dto.BEGIN_AMOUNT = previous != null && previous.VIR_END_AMOUNT.HasValue ? previous.VIR_END_AMOUNT.Value : 0;
                dto.IN_AMOUNT = appliedImpMestBloods != null ? appliedImpMestBloods.Where(o => o.BLOOD_TYPE_ID == bloodType.ID).Count() : 0;
                dto.OUT_AMOUNT = appliedExpMestBloods != null ? appliedExpMestBloods.Where(o => o.BLOOD_TYPE_ID == bloodType.ID).Count() : 0;
                dto.INVENTORY_AMOUNT = ListBlood != null ? ListBlood.Where(o => o.BLOOD_TYPE_ID == bloodType.ID).Count() : 0;

                hisMestPeriodBltys.Add(dto);
            }

            if ((IsNotNullOrEmpty(hisMestPeriodBltys) && !this.hisMestPeriodBltyCreate.Run(hisMestPeriodBltys)))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
        }

        /// <summary>
        /// Tao du lieu HIS_MEST_PERIOD_BLOOD
        /// </summary>
        private void ProcessMestPeriodBlood()
        {
            if (IsNotNullOrEmpty(ListBlood))
            {
                List<HIS_MEST_PERIOD_BLOOD> hisMestPeriodBloods = new List<HIS_MEST_PERIOD_BLOOD>();
                foreach (var item in ListBlood)
                {
                    HIS_MEST_PERIOD_BLOOD dto = new HIS_MEST_PERIOD_BLOOD();
                    dto.BLOOD_ID = item.ID;
                    dto.MEDI_STOCK_PERIOD_ID = this.recentHisMediStockPeriod.ID;
                    hisMestPeriodBloods.Add(dto);
                }

                if ((IsNotNullOrEmpty(hisMestPeriodBloods) && !this.hisMestPeriodBloodCreate.Run(hisMestPeriodBloods)))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        private List<HIS_MEST_PERIOD_BLTY> GetPreviousMestPeriodBlty()
        {
            HisMestPeriodBltyFilterQuery filter = new HisMestPeriodBltyFilterQuery();
            filter.MEDI_STOCK_PERIOD_ID = this.previousHisMediStockPeriod.ID;
            return new HisMestPeriodBltyGet().Get(filter);
        }
    }
}
