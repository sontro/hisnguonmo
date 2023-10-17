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

namespace MOS.MANAGER.HisServiceReq.Prescription.Subclinical.Create
{
    class HisExpMestProcessor : BusinessBase
    {
        private HisExpMestCreate hisExpMestCreate;

        internal HisExpMestProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
        }

        internal bool Run(List<HIS_SERVICE_REQ> serviceReqs, long? expMestReasonId, ref List<HIS_EXP_MEST> resultData)
        {
            try
            {
                if (IsNotNullOrEmpty(serviceReqs))
                {
                    var tmp = new List<HIS_EXP_MEST>();

                    //Tao thong tin exp_mest tu thong tin service_req
                    //Do don phong kham se truc tiep tao lenh chua khong tao y/c
                    //==> lay luon thong tin duyet phieu xuat theo thong tin tao y/c
                    foreach(HIS_SERVICE_REQ sr in serviceReqs)
                    {
                        //lay thong tin kho xuat dua vao execute_room_id cua service_req
                        V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ROOM_ID == sr.EXECUTE_ROOM_ID).FirstOrDefault();
                        HIS_EXP_MEST expMest = new HIS_EXP_MEST();
                        expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;

                        //neu ke vao tu truc thi loai don la don tu truc, nguoc lai, la don phong kham
                        expMest.EXP_MEST_TYPE_ID = mediStock.IS_CABINET == MOS.UTILITY.Constant.IS_TRUE ? 
                            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT : IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK;
                        expMest.SERVICE_REQ_ID = sr.ID;
                        expMest.MEDI_STOCK_ID = mediStock.ID;
                        expMest.EXP_MEST_REASON_ID = expMestReasonId;
                        tmp.Add(expMest);
                    }

                    if (!this.hisExpMestCreate.CreateList(tmp, serviceReqs))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                    resultData = tmp;
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
