using Exiled.API.Interfaces;
using System.Collections.Generic;

namespace EntityCleanup
{
	public class Config : IConfig
	{
		public bool IsEnabled { get; set; } = true;

		//public List<int> CleanupItems { get; set; } = new List<int>();

		//public int ItemCleanupInterval { get; set; } = 420;
		public int RagdollCleanupInterval { get; set; } = 120;
	}
}
