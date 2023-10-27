using System;
using Inventec.Common.Logging;
using Inventec.A2Manager;
using System.Collections.Generic;
using ACS.LDP.Base;
using ACS.EFMODEL.DataModels;

namespace ACS.LDP.DataPriv
{
    public class AcsUserDataPriv : BaseDataPriv<ACS_EV_ACS_USER>
    {
        public AcsUserDataPriv()
            : base(DataCodeConstant.ACS_USER)
        {
        }

        public void VerifyGet(string moduleCode, List<System.Linq.Expressions.Expression<Func<ACS_EV_ACS_USER, bool>>> query)
        {
            string dataGrant = "";
            try
            {
                dataGrant = A2.DetermineDataGrant(moduleCode, DataType, DataGrantTypeCodeConstant.GET);
                UserData user = A2.GetUserData();
                if (user != null)
                {
                    switch (dataGrant)
                    {
                        case DataGrantCodeConstant.GET_ALL:
                            break;
                        case DataGrantCodeConstant.GET_CHILD:
                            query.Add(o => user.LIST_GROUP_CODE.Contains(o.GROUP_CODE));
                            break;
                        case DataGrantCodeConstant.GET_GROUP:
                            query.Add(o => user.GROUP_CODE == o.GROUP_CODE);
                            break;
                        case DataGrantCodeConstant.GET_MINE:
                            query.Add(o => user.USER_NAME == o.CREATOR);
                            break;
                        default:
                            query.Add(o => o.ID == NEGATIVE_ID);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error("Kiem tra phan quyen VerifyGet co exception." + LogUtil.TraceData(LogUtil.GetMemberName(() => moduleCode), moduleCode) + LogUtil.TraceData(LogUtil.GetMemberName(() => dataGrant), dataGrant), ex);
                query.Add(o => o.ID == NEGATIVE_ID);
            }
        }
    }
}
