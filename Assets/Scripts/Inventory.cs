using System.Collections.Generic;
using UnityEditor.Rendering.Universal.ShaderGraph;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public static Inventory Instance;
	Dictionary<ItemMaterial, int> _materialValues;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		Instance = this;
		_materialValues = new();
	}

	public void TryAddMaterial(ItemMaterial material)
	{
		if (_materialValues.ContainsKey(material))
		{ 
			_materialValues[material]++;
			Debug.Log("Added one " +  material + " now its " + _materialValues[material]);
		}
		else
		{
			_materialValues.Add(material, 1);
			Debug.Log("Added one " + material + " now its " + _materialValues[material]);
		}
	}

	public bool TryRemoveMaterial(ItemMaterial material, int amount = 1)
	{
		if (_materialValues.ContainsKey(material))
		{
			if (_materialValues[material] >= amount)
			{
				_materialValues[material] -= amount;
				Debug.Log("Removed " + amount + " " + material + " now its " + _materialValues[material]);
				return true;
			}
			else
			{
				Debug.Log("Couldnt remove " + amount + " " + material + " because its only " + _materialValues[material]);
				return false;
			}
		}
		else
		{
			Debug.Log("Couldnt remove " + amount + " " + material + " because its didnt exist yet ");
			return false;
		}
	}


	// Update is called once per frame
	void Update()
	{

	}
}
