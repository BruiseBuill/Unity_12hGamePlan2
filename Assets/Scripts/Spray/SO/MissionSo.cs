using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spray
{
    [CreateAssetMenu(fileName = "Mission", menuName = "Spray/Mission")]
    public class MissionSo : ScriptableObject
	{
		public int presentMission;
		public int unlockMission;
		public int maxMission;
	}
}