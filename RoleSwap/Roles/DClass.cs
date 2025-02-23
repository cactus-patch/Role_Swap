using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using System.Collections.Generic;

namespace RoleSwap.Roles
{
    public class DClass : CustomRole
    {
        public override uint Id { get; set; } = 801;
        public override RoleTypeId Role { get; set; } = RoleTypeId.ClassD;
        public override int MaxHealth { get; set; } = 100;
        public override string Name { get; set; } = "D-Class";
        public override string Description { get; set; } = "Escape from the facility.\nCooperate with the <color=#008F1e>Chaos Insurgency.</color>\nAvoid other teams.";
        public override string CustomInfo { get; set; } = "D-Class";
        public override bool IgnoreSpawnSystem { get; set; } = false;

        public override List<string> Inventory { get; set; } = new()
        {
            $"{ItemType.KeycardJanitor}"
        };
        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            RoomSpawnPoints = new List<RoomSpawnPoint>
            {
                new()
                {
                    Room = RoomType.LczClassDSpawn,
                    
                }
            }
        };
    }
}