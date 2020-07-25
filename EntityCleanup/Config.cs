using Exiled.API.Interfaces;
using System.Collections.Generic;

namespace EntityCleanup
{
	public class Config : IConfig
	{
		public bool IsEnabled { get; set; } = true;

		public List<int> IgnoreItems = new List<int>();

		public int ItemCleanupInterval { get; set; } = 300;
		public int RagdollCleanupInterval { get; set; } = 300;
	}
}
