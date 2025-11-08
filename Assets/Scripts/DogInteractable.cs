using UnityEngine;

public class DogInteractable : MonoBehaviour, IInteractable
{
    public void OnInteract(GameObject interactor)
    {
        print("Woof I'm dog :(");
    }
}
