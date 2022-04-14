using Exiled.API.Interfaces;
using System.Collections.Generic;

namespace EntityCleanup
{
	public class Config : IConfig
	{
		public bool IsEnabled { get; set; } = true;

		public List<int> IgnoreItems { get; set; } = new List<int>();

		public int ItemCleanupInterval { get; set; } = 120;
		public int RagdollCleanupInterval { get; set; } = 120;
	}
}
