using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.Token;
using MOS.SDO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccinationExam.Register
{
    class ExpMestProcessor : BusinessBase
    {
        private HisExpMestCreate hisExpMestCreate;

        internal ExpMestProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
        }

        internal bool Run(List<HIS_VACCINATION> vaccinations, ref List<HIS_EXP_MEST> resultData)
        {
            try
            {
                if (IsNotNullOrEmpty(vaccinations))
                {
                    List<HIS_EXP_MEST> data = new List<HIS_EXP_MEST>();

                    //Tao thong tin exp_mest tu thong tin service_req
                    foreach (HIS_VACCINATION sr in vaccinations)
                    {
                        //lay thong tin kho xuat dua vao execute_room_id cua service_req
                        V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ROOM_ID == sr.EXECUTE_ROOM_ID).FirstOrDefault();
                        HIS_EXP_MEST expMest = new HIS_EXP_MEST();
                        expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                        expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__VACCIN;
                        expMest.VACCINATION_ID = sr.ID;
                        expMest.MEDI_STOCK_ID = mediStock.ID;
                        data.Add(expMest);
                    }

                    if (!this.hisExpMestCreate.CreateList(data, vaccinations))
                    {
                        return false;
                    }
                    resultData = data;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        internal void Rollback()
        {
            this.hisExpMestCreate.RollbackData();
        }
    }
}
