using Inventec.Common.Repository;

namespace ACS.MANAGER.Base
{
    static class DAOWorker
    {
        internal static ACS.DAO.Sql.SqlDAO SqlDAO { get { return (ACS.DAO.Sql.SqlDAO)Worker.Get<ACS.DAO.Sql.SqlDAO>(); } }
        internal static ACS.DAO.AcsApplication.AcsApplicationDAO AcsApplicationDAO { get { return (ACS.DAO.AcsApplication.AcsApplicationDAO)Worker.Get<ACS.DAO.AcsApplication.AcsApplicationDAO>(); } }
        internal static ACS.DAO.AcsApplicationRole.AcsApplicationRoleDAO AcsApplicationRoleDAO { get { return (ACS.DAO.AcsApplicationRole.AcsApplicationRoleDAO)Worker.Get<ACS.DAO.AcsApplicationRole.AcsApplicationRoleDAO>(); } }
        internal static ACS.DAO.AcsControl.AcsControlDAO AcsControlDAO { get { return (ACS.DAO.AcsControl.AcsControlDAO)Worker.Get<ACS.DAO.AcsControl.AcsControlDAO>(); } }
        internal static ACS.DAO.AcsControlRole.AcsControlRoleDAO AcsControlRoleDAO { get { return (ACS.DAO.AcsControlRole.AcsControlRoleDAO)Worker.Get<ACS.DAO.AcsControlRole.AcsControlRoleDAO>(); } }
        internal static ACS.DAO.AcsCredentialData.AcsCredentialDataDAO AcsCredentialDataDAO { get { return (ACS.DAO.AcsCredentialData.AcsCredentialDataDAO)Worker.Get<ACS.DAO.AcsCredentialData.AcsCredentialDataDAO>(); } }
        internal static ACS.DAO.AcsModule.AcsModuleDAO AcsModuleDAO { get { return (ACS.DAO.AcsModule.AcsModuleDAO)Worker.Get<ACS.DAO.AcsModule.AcsModuleDAO>(); } }
        internal static ACS.DAO.AcsModuleGroup.AcsModuleGroupDAO AcsModuleGroupDAO { get { return (ACS.DAO.AcsModuleGroup.AcsModuleGroupDAO)Worker.Get<ACS.DAO.AcsModuleGroup.AcsModuleGroupDAO>(); } }
        internal static ACS.DAO.AcsModuleRole.AcsModuleRoleDAO AcsModuleRoleDAO { get { return (ACS.DAO.AcsModuleRole.AcsModuleRoleDAO)Worker.Get<ACS.DAO.AcsModuleRole.AcsModuleRoleDAO>(); } }
        internal static ACS.DAO.AcsRole.AcsRoleDAO AcsRoleDAO { get { return (ACS.DAO.AcsRole.AcsRoleDAO)Worker.Get<ACS.DAO.AcsRole.AcsRoleDAO>(); } }
        internal static ACS.DAO.AcsRoleBase.AcsRoleBaseDAO AcsRoleBaseDAO { get { return (ACS.DAO.AcsRoleBase.AcsRoleBaseDAO)Worker.Get<ACS.DAO.AcsRoleBase.AcsRoleBaseDAO>(); } }
        internal static ACS.DAO.AcsRoleUser.AcsRoleUserDAO AcsRoleUserDAO { get { return (ACS.DAO.AcsRoleUser.AcsRoleUserDAO)Worker.Get<ACS.DAO.AcsRoleUser.AcsRoleUserDAO>(); } }
        internal static ACS.DAO.AcsUser.AcsUserDAO AcsUserDAO { get { return (ACS.DAO.AcsUser.AcsUserDAO)Worker.Get<ACS.DAO.AcsUser.AcsUserDAO>(); } }
        internal static ACS.DAO.AcsToken.AcsTokenDAO AcsTokenDAO { get { return (ACS.DAO.AcsToken.AcsTokenDAO)Worker.Get<ACS.DAO.AcsToken.AcsTokenDAO>(); } }
        internal static ACS.DAO.AcsOtp.AcsOtpDAO AcsOtpDAO { get { return (ACS.DAO.AcsOtp.AcsOtpDAO)Worker.Get<ACS.DAO.AcsOtp.AcsOtpDAO>(); } }
        internal static ACS.DAO.AcsAuthenRequest.AcsAuthenRequestDAO AcsAuthenRequestDAO { get { return (ACS.DAO.AcsAuthenRequest.AcsAuthenRequestDAO)Worker.Get<ACS.DAO.AcsAuthenRequest.AcsAuthenRequestDAO>(); } }
        internal static ACS.DAO.AcsAuthorSystem.AcsAuthorSystemDAO AcsAuthorSystemDAO { get { return (ACS.DAO.AcsAuthorSystem.AcsAuthorSystemDAO)Worker.Get<ACS.DAO.AcsAuthorSystem.AcsAuthorSystemDAO>(); } }
        internal static ACS.DAO.AcsRoleAuthor.AcsRoleAuthorDAO AcsRoleAuthorDAO { get { return (ACS.DAO.AcsRoleAuthor.AcsRoleAuthorDAO)Worker.Get<ACS.DAO.AcsRoleAuthor.AcsRoleAuthorDAO>(); } }
        internal static ACS.DAO.AcsActivityType.AcsActivityTypeDAO AcsActivityTypeDAO { get { return (ACS.DAO.AcsActivityType.AcsActivityTypeDAO)Worker.Get<ACS.DAO.AcsActivityLog.AcsActivityLogDAO>(); } }
        internal static ACS.DAO.AcsActivityLog.AcsActivityLogDAO AcsActivityLogDAO { get { return (ACS.DAO.AcsActivityLog.AcsActivityLogDAO)Worker.Get<ACS.DAO.AcsActivityLog.AcsActivityLogDAO>(); } }
        internal static ACS.DAO.AcsAppOtpType.AcsAppOtpTypeDAO AcsAppOtpTypeDAO { get { return (ACS.DAO.AcsAppOtpType.AcsAppOtpTypeDAO)Worker.Get<ACS.DAO.AcsAppOtpType.AcsAppOtpTypeDAO>(); } }
        internal static ACS.DAO.AcsOtpType.AcsOtpTypeDAO AcsOtpTypeDAO { get { return (ACS.DAO.AcsOtpType.AcsOtpTypeDAO)Worker.Get<ACS.DAO.AcsOtpType.AcsOtpTypeDAO>(); } }
    }
}
