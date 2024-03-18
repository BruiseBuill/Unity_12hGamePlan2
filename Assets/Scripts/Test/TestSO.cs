using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
	[CreateAssetMenu(fileName = "TestSO",menuName = "Test/TestSO")]
	public class TestSO : ScriptableObject
	{
		public int count;
	}
}