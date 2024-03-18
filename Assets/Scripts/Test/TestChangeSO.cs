using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Test
{
	public class TestChangeSO : MonoBehaviour
	{
		[SerializeField] TestSO testSO;

		[ContextMenu("Add")]
		public void Add()
		{
			testSO.count++;
            EditorUtility.SetDirty(testSO);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
	}
}