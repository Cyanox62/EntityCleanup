using System.Collections.Generic;

namespace EntityCleanup
{
	public static class Config
	{
		internal static List<int> ignoreItems;

		internal static int itemCleanupInterval;
		internal static int ragdollCleanupInterval;

		internal static void Reload()
		{
			ignoreItems = Plugin.Config.GetIntList("ev_ignored_items");
			if (ignoreItems == null || ignoreItems.Count == 0) ignoreItems = new List<int>();

			itemCleanupInterval = Plugin.Config.GetInt("ec_item_cleanup_interval", 300);
			ragdollCleanupInterval = Plugin.Config.GetInt("ec_ragdoll_cleanup_interval", 300);
		}
	}
}
