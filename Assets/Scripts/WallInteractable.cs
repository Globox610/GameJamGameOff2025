using UnityEngine;

public class WallInteractable : MonoBehaviour, IInteractable
{
	bool _isBuilt;
	[SerializeField] MeshRenderer _meshRenderer;
	[SerializeField] Material _materialBuilt;
	[SerializeField] Material _materialDefault;
	[SerializeField] MaterialDataAsset _asset;

	public void OnInteract(GameObject interactor)
	{
		Build();
		Debug.Log(_asset.material);
	}

	public void OnCreated()
	{
		_meshRenderer.material = _materialDefault;
		_meshRenderer.material.mainTexture = _asset.texture;
		_isBuilt = false;
	}

	public void Build()
	{
		if (!Inventory.Instance.TryRemoveMaterial(_asset.material, 2))
			return;
		_meshRenderer.material = _materialBuilt;
		_meshRenderer.material.mainTexture = _asset.texture;
		_isBuilt = true;
	}

	public bool GetIsBuilt()
	{
		return _isBuilt;
	}
}
