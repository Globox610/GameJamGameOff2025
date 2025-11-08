using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
	[SerializeField] ResourceInteractable resourcePrefab;
	[SerializeField] List<MaterialDataAsset> assets;
	[SerializeField] int minObjects;
	[SerializeField] int maxObjects;

	[SerializeField] float minY;
	[SerializeField] float maxY;
	[SerializeField] float minX;
	[SerializeField] float maxX;
	[SerializeField] float minZ;
	[SerializeField] float maxZ;

	public void Spawn()
	{
		Random.InitState((int)System.DateTime.Now.Ticks + 69);
		int numToSpawn = Random.Range(minObjects, maxObjects + 1);
		for (int i = 0; i < numToSpawn; i++) {
			Vector3 spawnPos = GetRandomPosition(i) + transform.position;
			var obj = Instantiate(resourcePrefab, spawnPos, Quaternion.identity);
			obj.SetMaterial(assets[Random.Range(0, assets.Count)]);
		}
	}

	Vector3 GetRandomPosition(int num)
	{
		Random.InitState((int)System.DateTime.Now.Ticks + num);
		float x = Random.Range(minX, maxX);
		float y = Random.Range(minY, maxY);
		float z = Random.Range(minZ, maxZ);
		return new Vector3(x, y, z);
	}

}
