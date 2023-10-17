using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockMety
{
    class HisMediStockMetyImport : BusinessBase
    {
        private HisMediStockMetyCreate hisMediStockMetyCreate = null;
        private HisMediStockMetyUpdate hisMediStockMetyUpdate = null;

        internal HisMediStockMetyImport()
            : base()
        {
            this.Init();
        }

        internal HisMediStockMetyImport(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisMediStockMetyCreate = new HisMediStockMetyCreate(param);
            this.hisMediStockMetyUpdate = new HisMediStockMetyUpdate(param);
        }

        internal bool Run(List<HIS_MEDI_STOCK_METY> listData, ref List<HIS_MEDI_STOCK_METY> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediStockMetyCheck checker = new HisMediStockMetyCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                valid = valid && checker.CheckDuplicate(listData);
                foreach (HIS_MEDI_STOCK_METY data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    List<HIS_MEDI_STOCK_METY> creates = new List<HIS_MEDI_STOCK_METY>();
                    List<HIS_MEDI_STOCK_METY> updates = new List<HIS_MEDI_STOCK_METY>();
                    List<HIS_MEDI_STOCK_METY> befores = new List<HIS_MEDI_STOCK_METY>();

                    List<HIS_MEDI_STOCK_METY> listOld = new HisMediStockMetyGet().GetByMediStockIds(listData.Select(s => s.MEDI_STOCK_ID).Distinct().ToList());

                    foreach (HIS_MEDI_STOCK_METY data in listData)
                    {
                        HIS_MEDI_STOCK_METY old = listOld != null ? listOld.FirstOrDefault(o => o.MEDI_STOCK_ID == data.MEDI_STOCK_ID && o.MEDICINE_TYPE_ID == data.MEDICINE_TYPE_ID) : null;
                        if (old != null)
                        {
                            data.ID = old.ID;
                            updates.Add(data);
                            befores.Add(old);
                        }
                        else
                        {
                            creates.Add(data);
                        }
                    }

                    if (IsNotNullOrEmpty(creates) && !this.hisMediStockMetyCreate.CreateList(creates))
                    {
                        throw new Exception("hisMediStockMetyCreate. Ket thuc nghiep vu");
                    }


                    if (IsNotNullOrEmpty(updates) && !this.hisMediStockMetyUpdate.UpdateList(creates, befores))
                    {
                        throw new Exception("hisMediStockMetyUpdate. Ket thuc nghiep vu");
                    }

                    result = true;
                    resultData = listData;
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void Rollback()
        {
            try
            {
                this.hisMediStockMetyUpdate.RollbackData();
                this.hisMediStockMetyCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
