using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilityControl
{
    class API
    {
        public static List<Player> GetPlayers(string data)
        {
            if (data == "*")
            {
                return Player.List.ToList();
            }
            else if (data.Contains("%"))
            {
                string searchFor = data.Remove(0, 1);
                if (!Enum.TryParse(searchFor, true, out RoleType role))
                {
                    return new List<Player> { };
                }
                return Player.List.Where(Ply => Ply.Role == role).ToList();
            }
            else if (data.Contains("*"))
            {
                string searchFor = data.Remove(0, 1);
                ZoneType zone = (searchFor.ToLower() == "light" ? ZoneType.LightContainment : (searchFor.ToLower() == "heavy" ? ZoneType.HeavyContainment : (searchFor.ToLower() == "entrance" ? ZoneType.Entrance : (searchFor.ToLower() == "surface" ? ZoneType.Surface : ZoneType.Unspecified))));
                if (zone == ZoneType.Unspecified)
                {
                    return new List<Player> { };
                }
                return Player.List.Where(Ply => Ply.CurrentRoom.Zone == zone).ToList();
            }
            else
            {
                List<Player> returnValue = new List<Player> { };
                string[] IDs = data.Split((".").ToCharArray());
                foreach (string id in IDs)
                {
                    Player Ply = Player.Get(id);
                    if (Ply != null)
                    {
                        returnValue.Add(Ply);
                    }
                }
                return returnValue;
            }
        }

        public static (bool, bool) GetItemInInventory(Player Ply, ItemType itemType)
        {
            if (Ply.CurrentItem.Type == itemType)
            {
                return (true, true);
            }
            else
            {
                foreach (var item in Ply.Items)
                {
                    if (item.Type == itemType)
                    {
                        return (true, false);
                    }
                }
            }
            return (false, false);
        }
    }
}
