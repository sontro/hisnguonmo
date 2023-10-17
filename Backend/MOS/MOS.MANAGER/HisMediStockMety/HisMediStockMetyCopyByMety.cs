using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockMety
{
    class HisMediStockMetyCopyByMety : BusinessBase
    {
        private List<HIS_MEDI_STOCK_METY> recentMediStockMetys;

        internal HisMediStockMetyCopyByMety()
            : base()
        {

        }

        internal HisMediStockMetyCopyByMety(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisMestMetyCopyByMetySDO data, ref List<HIS_MEDI_STOCK_METY> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyMedicineTypeId);
                valid = valid && IsGreaterThanZero(data.PasteMedicineTypeId);
                if (valid)
                {
                    List<HIS_MEDI_STOCK_METY> newMestMetys = new List<HIS_MEDI_STOCK_METY>();
                    List<HIS_MEDI_STOCK_METY> oldMestMetys = new List<HIS_MEDI_STOCK_METY>();
                    List<HIS_MEDI_STOCK_METY> copyMestMetys = DAOWorker.SqlDAO.GetSql<HIS_MEDI_STOCK_METY>("SELECT * FROM HIS_MEDI_STOCK_METY WHERE MEDICINE_TYPE_ID = :param1", data.CopyMedicineTypeId);
                    List<HIS_MEDI_STOCK_METY> pasteMestMetys = DAOWorker.SqlDAO.GetSql<HIS_MEDI_STOCK_METY>("SELECT * FROM HIS_MEDI_STOCK_METY WHERE MEDICINE_TYPE_ID = :param1", data.PasteMedicineTypeId);
                    if (!IsNotNullOrEmpty(copyMestMetys))
                    {
                        HIS_MEDICINE_TYPE medicineType = Config.HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == data.CopyMedicineTypeId);
                        string name = medicineType != null ? medicineType.MEDICINE_TYPE_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMedicineType_ThuocChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyMestMetys");
                    }

                    foreach (HIS_MEDI_STOCK_METY copyData in copyMestMetys)
                    {
                        HIS_MEDI_STOCK_METY mestMety = pasteMestMetys != null ? pasteMestMetys.FirstOrDefault(o => o.MEDI_STOCK_ID == copyData.MEDI_STOCK_ID) : null;
                        if (mestMety != null)
                        {
                            mestMety.ALERT_MAX_IN_STOCK = copyData.ALERT_MAX_IN_STOCK;
                            mestMety.ALERT_MIN_IN_STOCK = copyData.ALERT_MIN_IN_STOCK;
                            mestMety.IS_GOODS_RESTRICT = copyData.IS_GOODS_RESTRICT;
                            mestMety.IS_PREVENT_MAX = copyData.IS_PREVENT_MAX;
                            oldMestMetys.Add(mestMety);
                        }
                        else
                        {
                            mestMety = new HIS_MEDI_STOCK_METY();
                            mestMety.MEDICINE_TYPE_ID = data.PasteMedicineTypeId;
                            mestMety.MEDI_STOCK_ID = copyData.MEDI_STOCK_ID;
                            mestMety.ALERT_MAX_IN_STOCK = copyData.ALERT_MAX_IN_STOCK;
                            mestMety.ALERT_MIN_IN_STOCK = copyData.ALERT_MIN_IN_STOCK;
                            mestMety.IS_GOODS_RESTRICT = copyData.IS_GOODS_RESTRICT;
                            mestMety.IS_PREVENT_MAX = copyData.IS_PREVENT_MAX;
                            newMestMetys.Add(mestMety);
                        }
                    }
                    if (IsNotNullOrEmpty(newMestMetys))
                    {
                        if (!DAOWorker.HisMediStockMetyDAO.CreateList(newMestMetys))
                        {
                            throw new Exception("Khong tao duoc HIS_MEDI_STOCK_METY");
                        }
                        this.recentMediStockMetys = newMestMetys;
                    }

                    if (IsNotNullOrEmpty(oldMestMetys))
                    {
                        if (!DAOWorker.HisMediStockMetyDAO.UpdateList(oldMestMetys))
                        {
                            throw new Exception("Khong sua duoc HIS_MEDI_STOCK_METY");
                        }
                    }

                    result = true;
                    resultData = new List<HIS_MEDI_STOCK_METY>();
                    if (IsNotNullOrEmpty(newMestMetys))
                    {
                        resultData.AddRange(newMestMetys);
                    }
                    if (IsNotNullOrEmpty(pasteMestMetys))
                    {
                        resultData.AddRange(pasteMestMetys);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
                resultData = null;
            }
            return result;
        }

        private void RollbackData()
        {
            try
            {
                if (IsNotNullOrEmpty(this.recentMediStockMetys))
                {
                    if (!DAOWorker.HisMediStockMetyDAO.TruncateList(this.recentMediStockMetys))
                    {
                        Logging("Rollback HIS_MEDI_STOCK_METY that bai. Kiem tra lai du lieu", LogType.Warn);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
