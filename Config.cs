using System.ComponentModel;
using Exiled.API.Interfaces;
using PlayerRoles;

namespace RoleSwap
{
    public class Config : IConfig
    {
        [Description("Whether or not the plugin is enabled")]
        public bool IsEnabled { get; set; } = true;

        [Description("Whether or not debug messages will be shown")]
        public bool Debug { get; set; } = false;

        [Description("The role that will be swapped when a detained NTF escapes")]
        public RoleTypeId FFEscape { get; set; } = RoleTypeId.ChaosConscript;
        
        [Description("The role that will be swapped when a detained Chaos Insurgency escapes")]
        public RoleTypeId CIEscape { get; set; } = RoleTypeId.NtfPrivate;

        [Description("The Distance from the escape area that peope escape with")]
        public float EscapeDistance { get; set; } = 3f;
    }
}
