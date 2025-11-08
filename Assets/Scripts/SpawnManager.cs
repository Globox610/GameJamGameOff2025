using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public static SpawnManager instance;
	[SerializeField] List<ResourceSpawner> spawners;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		instance = this;
	}

	public void Spawn()
	{
		foreach (var spawner in spawners)
		{
			spawner.Spawn();
		}
	}
}
