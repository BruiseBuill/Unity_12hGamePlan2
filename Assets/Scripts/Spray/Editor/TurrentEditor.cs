using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Spray
{
	public class TurrentEditor : MonoBehaviour
	{
        [SerializeField] TurrentSO towerSo;

        [ContextMenu("Save")]
        public void Save()
        {
            var towers = FindObjectsOfType<Turrent>();
            var walls = FindObjectsOfType<Wall>();
            var food = FindObjectsOfType<Food>();
            towerSo.SetData(towers, walls, food);
            EditorUtility.SetDirty(towerSo);

            //AssetDatabase.CreateAsset(tower, "Assets/So/Tower/" + name + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [ContextMenu("Load")]
        public void Load()
        {
            for (int i = 0; i < towerSo.enemyPosList.Count; i++)
            {
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Spray/" + towerSo.enemyTypes[i].ToString() + ".prefab");
                var tower = Instantiate(prefab, towerSo.enemyPosList[i], Quaternion.identity);
                tower.name = towerSo.enemyTypes[i].ToString();
            }
            for (int i = 0; i < towerSo.wallPosList.Count; i++) 
            {
                var wallPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Spray/Wall.prefab");
                var go = Instantiate(wallPrefab, towerSo.wallPosList[i], Quaternion.identity);
                go.transform.localScale = towerSo.wallScaleList[i];
                go.transform.up = towerSo.wallOrientList[i];
            }
            for(int i=0;i< towerSo.foodPos.Count; i++)
            {
                var foodPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Spray/Food.prefab");
                var go = Instantiate(foodPrefab, towerSo.foodPos[i], Quaternion.identity);
            }
        }
    }
}