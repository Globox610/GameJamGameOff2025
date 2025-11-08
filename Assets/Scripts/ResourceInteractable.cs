using UnityEngine;

public class ResourceInteractable : MonoBehaviour, IInteractable
{

	[SerializeField] MaterialDataAsset materialDataAsset;
	BasicHookable hookable;
	public void Start()
	{
		hookable = GetComponent<BasicHookable>();
		hookable.OnHooked += () => { OnInteract(null); };
	}

	public void SetMaterial(MaterialDataAsset asset)
	{
		materialDataAsset = asset;
		GetComponent<MeshRenderer>().material.mainTexture = materialDataAsset.texture;
	}

	public void OnInteract(GameObject interactor)
	{
		Inventory.Instance.TryAddMaterial(materialDataAsset.material);
		Destroy(gameObject);
	}
}
