using UnityEngine;

public class FrogInteractable : MonoBehaviour, IInteractable
{
    public void OnInteract(GameObject interactor)
    {
        print("I am frog!");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
