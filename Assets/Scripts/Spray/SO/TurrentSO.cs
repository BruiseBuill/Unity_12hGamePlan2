using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spray
{
    [CreateAssetMenu(fileName = "TurrentSO", menuName = "Spray/Turrent")]
    public class TurrentSO : ScriptableObject
	{
		public List<EnemyType> enemyTypes = new List<EnemyType>();
		public List<Vector3> enemyPosList = new List<Vector3>();
		public List<Vector3> wallPosList = new List<Vector3>();
		public List<Vector3> wallScaleList = new List<Vector3>();
		public List<Vector3> wallOrientList = new List<Vector3>();
		public List<Vector3> foodPos = new List<Vector3>();
		public List<int> foodMass = new List<int>();
		
		public void SetData(Turrent[] turrents, Wall[] walls, Food[] food)
		{
			enemyTypes.Clear();
			enemyPosList.Clear();
			wallPosList.Clear();
			wallScaleList.Clear();
			wallOrientList.Clear();
			foodPos.Clear();
			foodMass.Clear();

			foreach (var item in turrents)
			{
				enemyTypes.Add(item.Type);
				enemyPosList.Add(item.Model.position);
			}
			foreach (var item in walls)
			{
				wallPosList.Add(item.Pos);
				wallScaleList.Add(item.Scale);
				wallOrientList.Add(item.Orient);
			}
			foreach(var f in food)
			{
				foodPos.Add(f.Pos);
				foodMass.Add(f.Mass);
			}
		}
	}
}