using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsControl;
using ACS.MANAGER.Core.AcsControl.Get;
using ACS.MANAGER.Core.AcsControlRole;
using ACS.MANAGER.Core.AcsControlRole.Get;
using ACS.MANAGER.Core.AcsModule;
using ACS.MANAGER.Core.AcsModule.Get;
using ACS.MANAGER.Core.AcsModuleRole;
using ACS.MANAGER.Core.AcsModuleRole.Get;
using ACS.MANAGER.Core.AcsRole;
using ACS.MANAGER.Core.AcsRole.Get;
using ACS.MANAGER.Core.Check;
using ACS.MANAGER.Token;
using ACS.SDO;
using AutoMapper;
using Inventec.Core;
using Inventec.Token.AuthSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.MANAGER.Core.TokenSys.SyncToken
{
    class SyncTokenInsertBehavior : BeanObjectBase, IAcsTokenSync
    {
        AcsTokenSyncInsertSDO entity;

        internal SyncTokenInsertBehavior(CommonParam param, AcsTokenSyncInsertSDO data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsTokenSync.Run()
        {
            bool result = false;
            try
            {
                TokenManager tokenManager = new TokenManager();
                if (Check())
                {
                    ExtraTokenData extraTokenData = new ExtraTokenData();
                    Inventec.Common.Mapper.DataObjectMapper.Map<ExtraTokenData>(extraTokenData, entity);
                    result = tokenManager.InsertTokenInRam(extraTokenData);
                    var tokenAlives = tokenManager.GetTokenDataInRamAlives();
                    if ((tokenAlives != null ? tokenAlives.Count : 0) != entity.TokenCount)
                    {
                        Inventec.Common.Logging.LogSystem.Debug("Da dong bo du lieu token moi them ve RAM cua cac ACS, tuy nhien kiem tra lai thay so luong token hien tai trong RAM cua ACS dang thuc hien dong bo va so luong token của ACS gui yeu cau dong bo khac nhau____so token trong RAM cua ACS dang chay=" + tokenAlives.Count + "____so token trong RAM cua ACS da gui yeu cau dong bo=" + entity.TokenCount);
                        //tokenManager.InitTokenInRamForStartApp();
                        //Inventec.Common.Logging.LogSystem.Debug("Thuc hien khoi tao lai du lieu token trong RAM lay tu cac token moi nhat hop le trong DB");
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                if (entity == null) throw new ArgumentNullException("entity is null");
                if (entity.User == null || String.IsNullOrEmpty(entity.TokenCode)) throw new ArgumentNullException("Token is null");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
