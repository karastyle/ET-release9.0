using System.Linq;

namespace ET.Server
{
    [FriendOf(typeof(PlayerComponent))]
    public static partial class PlayerComponentSystem
    {
        public static void Remove(this PlayerComponent self, long playerId)
        {
            Player player = self.GetChild<Player>(playerId);
            player?.Dispose();
        }
        
        public static Player GetByAccount(this PlayerComponent self,  string account)
        {
            foreach (Entity child in self.Children.Values)
            {
                Player player = child as Player;
                if (player.Account == account)
                {
                    return player;
                }
            }

            return null;
        }
    }
}