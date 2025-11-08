using UnityEngine;

[CreateAssetMenu(fileName = "MaterialDataAsset", menuName = "Scriptable Objects/MaterialDataAsset")]
public class MaterialDataAsset : ScriptableObject
{
	[SerializeField] private ItemMaterial _material;
	public ItemMaterial material => _material;

	[SerializeField] private Color _color;
	public Color color => _color;
	[SerializeField] private Texture _texture;
	public Texture texture => _texture;
}

public enum ItemMaterial
{
	Wood,
	Rock,
	Metal,
}
