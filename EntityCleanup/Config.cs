namespace EntityCleanup
{
	public static class Config
	{
		internal static int itemCleanupInterval;
		internal static int ragdollCleanupInterval;

		internal static void Reload()
		{
			itemCleanupInterval = Plugin.Config.GetInt("ec_item_cleanup_interval", 10);
			ragdollCleanupInterval = Plugin.Config.GetInt("ec_ragdoll_cleanup_interval", 10);
		}
	}
}
