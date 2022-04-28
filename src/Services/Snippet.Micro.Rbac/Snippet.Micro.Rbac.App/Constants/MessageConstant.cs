namespace Snippet.Micro.Rbac.App.Constants
{
    public static class MessageConstant
    {
        #region Common

        public static readonly (string, string) EMPTYTUPLE = (string.Empty, string.Empty);
        public static readonly (string, string) SYSTEM_ERROR_001 = ("SYSTEM_ERROR_001", "发生系统错误！请联系管理员！");

        public static readonly (string, string) SYSTEM_COMMON_001 = ("SYSTEM_COMMON_001", "页码不能小于0！");
        public static readonly (string, string) SYSTEM_COMMON_002 = ("SYSTEM_COMMON_002", "页面大小不能小于0！");

        #endregion Common

        #region ElementController

        public static readonly (string, string) ELEMENT_INFO_0001 = ("ELEMENT_INFO_0001", "创建成功！");
        public static readonly (string, string) ELEMENT_INFO_0002 = ("ELEMENT_INFO_0002", "删除成功！");
        public static readonly (string, string) ELEMENT_INFO_0003 = ("ELEMENT_INFO_0003", "更新成功！");

        public static readonly (string, string) ELEMENT_ERROR_0001 = ("ELEMENT_ERROR_0001", "请输元素名称！");
        public static readonly (string, string) ELEMENT_ERROR_0002 = ("ELEMENT_ERROR_0002", "元素名称过长！");
        public static readonly (string, string) ELEMENT_ERROR_0003 = ("ELEMENT_ERROR_0003", "请选择元素类型！");
        public static readonly (string, string) ELEMENT_ERROR_0004 = ("ELEMENT_ERROR_0004", "请输入元素标识！");
        public static readonly (string, string) ELEMENT_ERROR_0005 = ("ELEMENT_ERROR_0005", "元素标识过长！");
        public static readonly (string, string) ELEMENT_ERROR_0006 = ("ELEMENT_ERROR_0006", "元素标识只允许数字字母下划线！");

        #endregion ElementController

        #region OrganizationController

        public static readonly (string, string) ORGANIZATION_INFO_0001 = ("ORGANIZATION_INFO_0001", "创建成功！");
        public static readonly (string, string) ORGANIZATION_INFO_0002 = ("ORGANIZATION_INFO_0002", "删除成功！");
        public static readonly (string, string) ORGANIZATION_INFO_0003 = ("ORGANIZATION_INFO_0003", "更新成功！");
        public static readonly (string, string) ORGANIZATION_INFO_0004 = ("ORGANIZATION_INFO_0004", "职位编辑成功！");

        public static readonly (string, string) ORGANIZATION_ERROR_0001 = ("ORGANIZATION_ERROR_0001", "请输入组织名称！");
        public static readonly (string, string) ORGANIZATION_ERROR_0002 = ("ORGANIZATION_ERROR_0002", "组织名称过长！");
        public static readonly (string, string) ORGANIZATION_ERROR_0003 = ("ORGANIZATION_ERROR_0003", "请输入数字字母！");
        public static readonly (string, string) ORGANIZATION_ERROR_0004 = ("ORGANIZATION_ERROR_0004", "组织编码重复！");
        public static readonly (string, string) ORGANIZATION_ERROR_0005 = ("ORGANIZATION_ERROR_0005", "职位编码重复！");
        public static readonly (string, string) ORGANIZATION_ERROR_0006 = ("ORGANIZATION_ERROR_0006", "职位名称重复！");

        #endregion OrganizationController

        #region RoleController

        public static readonly (string, string) ROLE_INFO_0001 = ("ROLE_INFO_0001", "保存成功！");
        public static readonly (string, string) ROLE_INFO_0002 = ("ROLE_INFO_0002", "删除成功！");
        public static readonly (string, string) ROLE_INFO_0004 = ("ROLE_INFO_0004", "状态设置成功！");

        public static readonly (string, string) ROLE_ERROR_0001 = ("ROLE_ERROR_0001", "请输入角色名！");
        public static readonly (string, string) ROLE_ERROR_0002 = ("ROLE_ERROR_0002", "角色名过长！");
        public static readonly (string, string) ROLE_ERROR_0003 = ("ROLE_ERROR_0003", "请输入角色代码！");
        public static readonly (string, string) ROLE_ERROR_0004 = ("ROLE_ERROR_0004", "角色代码过长！");
        public static readonly (string, string) ROLE_ERROR_0005 = ("ROLE_ERROR_0005", "角色代码只允许数字字母下划线！");
        public static readonly (string, string) ROLE_ERROR_0006 = ("ROLE_ERROR_0006", "备注过长！");
        public static readonly (string, string) ROLE_ERROR_0007 = ("ROLE_ERROR_0007", "角色名重复！");
        public static readonly (string, string) ROLE_ERROR_0008 = ("ROLE_ERROR_0008", "角色代码重复！");

        #endregion RoleController

        #region UserController

        public static readonly string USER_INFO_0001 = "操作成功！";
        public static readonly string USER_INFO_0002 = "删除成功！";
        public static readonly string USER_INFO_0003 = "密码设置成功！";
        public static readonly string USER_INFO_0004 = "成员添加成功！";
        public static readonly string USER_INFO_0005 = "成员移出成功！";

        public static readonly string USER_ERROR_0001 = "请选择成员！";
        public static readonly string USER_ERROR_0002 = "请输入用户名！";
        public static readonly string USER_ERROR_0003 = "用户名过长！";
        public static readonly string USER_ERROR_0004 = "用户名只允许数字字母！";
        public static readonly string USER_ERROR_0005 = "请输入姓名！";
        public static readonly string USER_ERROR_0006 = "姓名过长！";
        public static readonly string USER_ERROR_0007 = "请输入密码！";
        public static readonly string USER_ERROR_0008 = "密码过长！";
        public static readonly string USER_ERROR_0009 = "请输入确认密码！";
        public static readonly string USER_ERROR_0011 = "两次输入的密码不一致！";

        public static readonly string USER_ERROR_0012 = "用户名重复！";

        #endregion UserController

        #region JobController
        public static readonly (string, string) JOB_INFO_0001 = ("JOB_INFO_0001", "创建成功！");
        public static readonly (string, string) JOB_INFO_0002 = ("JOB_INFO_0002", "删除成功！");
        public static readonly (string, string) JOB_INFO_0003 = ("JOB_INFO_0003", "更新成功！");
        public static readonly (string, string) JOB_INFO_0004 = ("JOB_INFO_0004", "任务已启用！");
        public static readonly (string, string) JOB_INFO_0005 = ("JOB_INFO_0005", "任务已停止！");
        public static readonly (string, string) JOB_INFO_0006 = ("JOB_INFO_0006", "任务开始执行！");

        public static readonly (string, string) JOB_ERROR_0001 = ("JOB_ERROR_0001", "没有找到该任务！");

        #endregion UserController
    }
}