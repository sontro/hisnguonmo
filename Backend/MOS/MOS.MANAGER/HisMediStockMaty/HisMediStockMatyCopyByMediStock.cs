using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockMaty
{
    class HisMediStockMatyCopyByMediStock : BusinessBase
    {
        private List<HIS_MEDI_STOCK_MATY> recentMediStockMatys;

        internal HisMediStockMatyCopyByMediStock()
            : base()
        {

        }

        internal HisMediStockMatyCopyByMediStock(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisMestMatyCopyByMediStockSDO data, ref List<HIS_MEDI_STOCK_MATY> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyMediStockId);
                valid = valid && IsGreaterThanZero(data.PasteMediStockId);
                if (valid)
                {
                    List<HIS_MEDI_STOCK_MATY> newMestMatys = new List<HIS_MEDI_STOCK_MATY>();
                    List<HIS_MEDI_STOCK_MATY> oldMestMatys = new List<HIS_MEDI_STOCK_MATY>();
                    List<HIS_MEDI_STOCK_MATY> copyMestMatys = DAOWorker.SqlDAO.GetSql<HIS_MEDI_STOCK_MATY>("SELECT * FROM HIS_MEDI_STOCK_MATY WHERE MEDI_STOCK_ID = :param1", data.CopyMediStockId);
                    List<HIS_MEDI_STOCK_MATY> pasteMestMatys = DAOWorker.SqlDAO.GetSql<HIS_MEDI_STOCK_MATY>("SELECT * FROM HIS_MEDI_STOCK_MATY WHERE MEDI_STOCK_ID = :param1", data.PasteMediStockId);
                    if (!IsNotNullOrEmpty(copyMestMatys))
                    {
                        V_HIS_MEDI_STOCK stock = Config.HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == data.CopyMediStockId);
                        string name = stock != null ? stock.MEDI_STOCK_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMediStock_KhoChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyMestMatys");
                    }

                    foreach (HIS_MEDI_STOCK_MATY copyData in copyMestMatys)
                    {
                        HIS_MEDI_STOCK_MATY mestMaty = pasteMestMatys != null ? pasteMestMatys.FirstOrDefault(o => o.MATERIAL_TYPE_ID == copyData.MATERIAL_TYPE_ID) : null;
                        if (mestMaty != null)
                        {
                            mestMaty.ALERT_MAX_IN_STOCK = copyData.ALERT_MAX_IN_STOCK;
                            mestMaty.ALERT_MIN_IN_STOCK = copyData.ALERT_MIN_IN_STOCK;
                            mestMaty.IS_GOODS_RESTRICT = copyData.IS_GOODS_RESTRICT;
                            mestMaty.IS_PREVENT_MAX = copyData.IS_PREVENT_MAX;
                            oldMestMatys.Add(mestMaty);
                        }
                        else
                        {
                            mestMaty = new HIS_MEDI_STOCK_MATY();
                            mestMaty.MEDI_STOCK_ID = data.PasteMediStockId;
                            mestMaty.MATERIAL_TYPE_ID = copyData.MATERIAL_TYPE_ID;
                            mestMaty.ALERT_MAX_IN_STOCK = copyData.ALERT_MAX_IN_STOCK;
                            mestMaty.ALERT_MIN_IN_STOCK = copyData.ALERT_MIN_IN_STOCK;
                            mestMaty.IS_GOODS_RESTRICT = copyData.IS_GOODS_RESTRICT;
                            mestMaty.IS_PREVENT_MAX = copyData.IS_PREVENT_MAX;
                            newMestMatys.Add(mestMaty);
                        }
                    }
                    if (IsNotNullOrEmpty(newMestMatys))
                    {
                        if (!DAOWorker.HisMediStockMatyDAO.CreateList(newMestMatys))
                        {
                            throw new Exception("Khong tao duoc HIS_MEDI_STOCK_MATY");
                        }
                        this.recentMediStockMatys = newMestMatys;
                    }

                    if (IsNotNullOrEmpty(oldMestMatys))
                    {
                        if (!DAOWorker.HisMediStockMatyDAO.UpdateList(oldMestMatys))
                        {
                            throw new Exception("Khong sua duoc HIS_MEDI_STOCK_MATY");
                        }
                    }

                    result = true;
                    resultData = new List<HIS_MEDI_STOCK_MATY>();
                    if (IsNotNullOrEmpty(newMestMatys))
                    {
                        resultData.AddRange(newMestMatys);
                    }
                    if (IsNotNullOrEmpty(pasteMestMatys))
                    {
                        resultData.AddRange(pasteMestMatys);
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
                if (IsNotNullOrEmpty(this.recentMediStockMatys))
                {
                    if (!DAOWorker.HisMediStockMatyDAO.TruncateList(this.recentMediStockMatys))
                    {
                        Logging("Rollback HIS_MEDI_STOCK_MATY that bai. Kiem tra lai du lieu", LogType.Warn);
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
