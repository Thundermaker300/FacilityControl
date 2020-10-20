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
            if (Ply.Inventory.curItem == itemType)
            {
                return (true, true);
            }
            else
            {
                foreach (Inventory.SyncItemInfo item in Ply.Inventory.items)
                {
                    if (item.id == itemType)
                    {
                        return (true, false);
                    }
                }
            }
            return (false, false);
        }
    }
}
