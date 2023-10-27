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
using ACS.SDO;
using AutoMapper;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.MANAGER.Core.TokenSys.Authorize
{
    class AcsTokenAuthorizeBehavior : BeanObjectBase, IAcsTokenAuthorize
    {
        AcsTokenLoginSDO entity;
        ACS_USER user;
        List<long> appRoleIds;

        internal AcsTokenAuthorizeBehavior(CommonParam param, AcsTokenLoginSDO data)
            : base(param)
        {
            entity = data;
        }

        ACS.SDO.AcsAuthorizeSDO IAcsTokenAuthorize.Run()
        {
            ACS.SDO.AcsAuthorizeSDO result = null;
            try
            {
                if (Check())
                {
                    bool valid = (AcsUserCheckVerifyValidDataForAuthorize.Verify(param, ref user, entity.LOGIN_NAME));
                    if (valid)
                    {
                        valid = valid && CheckUserIsActive(user);
                        if (valid)
                        {
                            AcsTokenAuthenticationSDO authenticationSDO = new AcsTokenAuthenticationSDO();
                            authenticationSDO = Mapper.Map<ACS_USER, AcsTokenAuthenticationSDO>(user);
                            authenticationSDO.ApplicationCode = entity.APPLICATION_CODE;
                            authenticationSDO.AppVersion = entity.APP_VERSION;
                            this.appRoleIds = new AuthorizeRoleForUsers(param, authenticationSDO).RunWithoutApplicationRole();
                            if (this.appRoleIds != null && this.appRoleIds.Count > 0)
                            {
                                result = new AcsAuthorizeSDO();
                                result.IsFull = CheckIsAdminFull(this.appRoleIds);

                                Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => appRoleIds), appRoleIds)
                                     + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity.LOGIN_NAME), entity.LOGIN_NAME)
                                     + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => result.IsFull), result.IsFull));
                                result.ModuleInRoles = (result.IsFull ? GetFullModule() : GetFullModuleByRoles(this.appRoleIds));
                                result.ControlInRoles = (result.IsFull ? GetFullControl() : GetFullControlByRoles(this.appRoleIds));
                            }
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    else
                    {
                        result = null;
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return null;
        }

        private bool CheckIsAdminFull(List<long> roleIds)
        {
            try
            {
                AcsRoleFilterQuery filter = new AcsRoleFilterQuery();
                filter.IDs = roleIds;
                return new AcsRoleBO().Get<List<ACS_ROLE>>(filter).Any(o => o.IS_FULL == 1);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return false;
        }

        private List<V_ACS_MODULE> GetFullModule()
        {
            AcsModuleViewFilterQuery filter = new AcsModuleViewFilterQuery();
            filter.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE;
            filter.APPLICATION_CODE = entity.APPLICATION_CODE;
            var moduleFulls = new AcsModuleBO().Get<List<V_ACS_MODULE>>(filter);
            return moduleFulls;
        }

        private List<V_ACS_MODULE> GetFullModuleByRoles(List<long> roleIds)
        {
            List<V_ACS_MODULE> modules = new List<V_ACS_MODULE>();
            var mdiro = GetModuleByRoles(roleIds);
            var mdano = GetModuleAnonymous();
            if (mdiro != null && mdiro.Count > 0)
            {
                modules.AddRange(mdiro);
                modules = modules.Distinct().ToList();
            }
            if (mdano != null && mdano.Count > 0)
            {
                var modulelinks = modules.Select(o => o.MODULE_LINK).ToArray();
                var mdanoadds = (modulelinks != null && modulelinks.Count() > 0 ? mdano.Where(o => !modulelinks.Contains(o.MODULE_LINK)).ToList() : mdano);
                if (mdanoadds != null && mdanoadds.Count > 0)
                    modules.AddRange(mdanoadds);
            }
            modules = modules.Distinct().ToList();
            return modules;
        }

        private List<V_ACS_MODULE> GetModuleByRoles(List<long> roleIds)
        {
            List<V_ACS_MODULE> modules = null;
            AcsModuleRoleViewFilterQuery filter = new AcsModuleRoleViewFilterQuery();
            if (this.appRoleIds != null && this.appRoleIds.Count > 0)
                filter.APPLICATION_CODE = entity.APPLICATION_CODE;
            filter.ROLE_IDs = roleIds;
            var moduleRoles = new AcsModuleRoleBO().Get<List<V_ACS_MODULE_ROLE>>(filter);
            if (moduleRoles != null && moduleRoles.Count > 0)
            {
                List<long> listModuleIds = moduleRoles.Select(o => o.MODULE_ID).Distinct().ToList();
                AcsModuleViewFilterQuery moduleViewFilterQuery = new AcsModuleViewFilterQuery();
                moduleViewFilterQuery.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE;
                if (listModuleIds != null && listModuleIds.Count > 0)
                {
                    moduleViewFilterQuery.IDs = listModuleIds;
                }
                else
                {
                    moduleViewFilterQuery.ID = -1;//No result data
                }
                modules = new AcsModuleBO().Get<List<V_ACS_MODULE>>(moduleViewFilterQuery);
            }
            return modules;
        }

        private List<V_ACS_MODULE> GetModuleAnonymous()
        {
            AcsModuleViewFilterQuery moduleViewFilterQuery = new AcsModuleViewFilterQuery();
            moduleViewFilterQuery.IsAnonymous = true;
            moduleViewFilterQuery.APPLICATION_CODE = entity.APPLICATION_CODE;
            moduleViewFilterQuery.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE;
            return new AcsModuleBO().Get<List<V_ACS_MODULE>>(moduleViewFilterQuery);
        }

        private List<ACS_CONTROL> GetFullControl()
        {
            AcsControlFilterQuery filter = new AcsControlFilterQuery();
            filter.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE;
            //filter.APPLICATION_CODE = entity.APPLICATION_CODE;
            return new AcsControlBO().Get<List<ACS_CONTROL>>(filter);
        }

        private List<ACS_CONTROL> GetControlAnonymous()
        {
            AcsControlFilterQuery filter = new AcsControlFilterQuery();
            filter.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE;
            filter.IsAnonymous = true;
            return new AcsControlBO().Get<List<ACS_CONTROL>>(filter);
        }

        private List<ACS_CONTROL> GetControlRoleByRoles(List<long> roleIds)
        {
            List<ACS_CONTROL> controls = null;
            AcsControlRoleViewFilterQuery filter = new AcsControlRoleViewFilterQuery();
            filter.ROLE_IDs = roleIds;
            var controlRoles = new AcsControlRoleBO().Get<List<V_ACS_CONTROL_ROLE>>(filter);
            if (controlRoles != null)
            {
                List<long> listControlIds = controlRoles.Select(o => o.CONTROL_ID).Distinct().ToList();
                AcsControlFilterQuery controlViewFilterQuery = new AcsControlFilterQuery();
                controlViewFilterQuery.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE;
                if (listControlIds != null && listControlIds.Count > 0)
                {
                    controlViewFilterQuery.IDs = listControlIds;
                }
                else
                {
                    controlViewFilterQuery.ID = -1;//No result data
                }
                controls = new AcsControlBO().Get<List<ACS_CONTROL>>(controlViewFilterQuery);
            }
            return controls;
        }

        private List<ACS_CONTROL> GetFullControlByRoles(List<long> roleIds)
        {
            List<ACS_CONTROL> controls = new List<ACS_CONTROL>();
            List<ACS_CONTROL> controliros = GetControlRoleByRoles(roleIds);
            List<ACS_CONTROL> controlsAnonymous = GetControlAnonymous();
            if (controliros != null && controliros.Count > 0)
            {
                controls.AddRange(controliros);
                controls = controls.Distinct().ToList();
            }
            if (controlsAnonymous != null && controlsAnonymous.Count > 0)
            {
                var controlCodes = controls.Select(o => o.CONTROL_CODE).ToArray();
                var ctanoadds = (controlCodes != null && controlCodes.Count() > 0 ? controlsAnonymous.Where(o => !controlCodes.Contains(o.CONTROL_CODE)).ToList() : controlsAnonymous);
                if (ctanoadds != null && ctanoadds.Count > 0)
                    controls.AddRange(ctanoadds);
            }
            controls = controls.Distinct().ToList();
            return controls;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                if (entity == null) throw new ArgumentNullException("entity is null");

                entity.LOGIN_NAME = entity.LOGIN_NAME.Trim().ToLower();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Ham kiem tra tai khoan co bi tam khoa hay khong
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckUserIsActive(ACS_USER data)
        {
            bool valid = false;
            try
            {

                valid = (data != null && data.ID > 0 && data.IS_ACTIVE == IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__TRUE);
                if (!valid && data != null)
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_TaiKhoanDangBiTamKhoa);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
