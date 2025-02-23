using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using UnityEngine;

namespace RoleSwap
{
    public static class Utils
    {
        public static Vector3 GetGlobalCords(RoomType roomType, Vector3 localPos)
        {
            var room = Room.Get(roomType);
            var rotation = room.Rotation;
            var roomPos = room.Position;
            if (Math.Abs(rotation.eulerAngles.y) < 1.0)
                return new Vector3(roomPos.x + localPos.x, roomPos.y + localPos.y, roomPos.z + localPos.z);
            if (Math.Abs(rotation.eulerAngles.y - 90f) < 1.0)
                return new Vector3(roomPos.x + localPos.z, roomPos.y + localPos.y, roomPos.z - localPos.x);
            if (Math.Abs(rotation.eulerAngles.y - 180f) < 1.0)
                return new Vector3(roomPos.x - localPos.x, roomPos.y + localPos.y, roomPos.z - localPos.z);
            if (Math.Abs(rotation.eulerAngles.y - 270f) < 1.0)
                return new Vector3(roomPos.x - localPos.z, roomPos.y + localPos.y, roomPos.z + localPos.x);
            return Vector3.zero;
        }
    }
}