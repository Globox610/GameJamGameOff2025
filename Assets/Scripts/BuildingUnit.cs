using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class BuildingUnit : MonoBehaviour
{
	[SerializeField] List<WallInteractable> _walls;
	bool _isFinished = false;
	[SerializeField] GameObject _stairs;
	[SerializeField] BuildingUnit _buildingUnitPrefab;
	void Start()
	{
		foreach (var wall in _walls)
		{
			wall.OnCreated();
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (!_isFinished)
		{
			bool wallsFinished = true;
			foreach (var wall in _walls)
			{
				if (!wall.GetIsBuilt())
					wallsFinished = false;
			}
			if (wallsFinished)
			{
				_stairs.SetActive(true);
				var pos = transform.position;
				pos.y += 10.1f;
				Instantiate(_buildingUnitPrefab, pos, Quaternion.identity);
				_isFinished = true;
			}
		}
	}
}
