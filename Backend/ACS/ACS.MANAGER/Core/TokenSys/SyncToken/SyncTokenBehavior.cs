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
    class AcsTokenSyncBehavior : BeanObjectBase, IAcsTokenSync
    {
        List<AcsCredentialTrackingSDO> entity;

        internal AcsTokenSyncBehavior(CommonParam param, List<AcsCredentialTrackingSDO> data)
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
                    var tokenAlives = tokenManager.GetTokenDataInRamAlives();
                    if (tokenAlives == null)
                    {
                        tokenAlives = new List<AcsCredentialTrackingSDO>();
                    }

                    Inventec.Common.Logging.LogSystem.Debug("--------So luong token trong RAM truoc khi dong bo: " + tokenAlives.Count);
                    List<AcsCredentialTrackingSDO> tokenAdds = new List<AcsCredentialTrackingSDO>();

                    if (tokenAlives != null && tokenAlives.Count > 0)
                    {
                        tokenManager.RemoveTokenInRam(tokenAlives.Select(k => k.TokenCode).ToList());

                        Inventec.Common.Logging.LogSystem.Debug("--------Truong hop RAM da duoc khoi tao token==> clear tat ca du lieu token cu trong RAM");

                        List<ExtraTokenData> allTokens = (from m in entity
                                                          select new ExtraTokenData()
                                                          {
                                                              ExpireTime = m.ExpireTime,
                                                              LastAccessTime = m.LastAccessTime,
                                                              LoginAddress = m.LoginAddress,
                                                              LoginTime = m.LoginTime,
                                                              MachineName = m.MachineName,
                                                              RenewCode = m.RenewCode,
                                                              TokenCode = m.TokenCode,
                                                              ValidAddress = m.LoginAddress,
                                                              VersionApp = m.VersionApp,
                                                              AuthorSystemCode = "",
                                                              AuthenticationCode = "",
                                                              User = new Inventec.Token.Core.UserData()
                                                              {
                                                                  ApplicationCode = m.ApplicationCode,
                                                                  Email = m.Email,
                                                                  GCode = m.GCode,
                                                                  LoginName = m.LoginName,
                                                                  Mobile = m.Mobile,
                                                                  UserName = m.UserName
                                                              }
                                                          }).ToList();


                        tokenManager.InitTokenInRamForStartApp(allTokens);
                        Inventec.Common.Logging.LogSystem.Debug("--------So luong token sau khi sync: " + allTokens.Count);
                        Inventec.Common.Logging.LogSystem.Debug("--------Da chay xong dong bo du lieu token giua cac backend ACS, qua trinh sync ket thuc");
                    }
                    else
                    {
                        Inventec.Common.Logging.LogSystem.Debug("--------Truong hop RAM khong co du lieu token ==> thuc hien gọi vao DB de lay danh sach token moi nhat va luu vao RAM");
                        tokenManager.InitTokenInActiveRamForStartApp();
                    }

                    result = true;
                    return result;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return false;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                if (entity == null || entity.Count == 0) throw new ArgumentNullException("entity is null");

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
