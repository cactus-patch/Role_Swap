using System.ComponentModel;
using Exiled.API.Interfaces;
using PlayerRoles;

namespace RoleSwap
{
    class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        
        [Description("The role that will be swapped when a detained NTF escapes")]
        public RoleTypeId NTFEscape = RoleTypeId.ChaosConscript;
        
        [Description("The role that will be swapped when a detained Chaos Insurgency escapes")]
        public RoleTypeId CIEscape = RoleTypeId.NtfPrivate;
    }
}
