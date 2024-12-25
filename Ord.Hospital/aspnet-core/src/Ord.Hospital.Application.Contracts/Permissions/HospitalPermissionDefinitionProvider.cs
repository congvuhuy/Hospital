using Ord.Hospital.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Ord.Hospital.Permissions;

public class HospitalPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(HospitalPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(HospitalPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HospitalResource>(name);
    }
}
