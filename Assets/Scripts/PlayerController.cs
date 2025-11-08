using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    GameObject HookVisual;

    public static PlayerController instance;
    public Action<Vector3, Vector3> OnInteractClicked;

    CharacterController characterController;
    Transform camTransform;

    [SerializeField]
    float _mouseSensitivity = 0.5f;

    [SerializeField]
    float _moveSpeed = 5.0f;

    [SerializeField]
    float _jumpForce = 25.0f;

    [SerializeField]
    float _gravityForce = -20.0f;

    [SerializeField]
    float InteractRange = 5;

    [SerializeField]
    float HookRange = 30;

    float _camRotX = 0;

    Vector3 _velocity = Vector3.zero;
    float _gravityVel = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        characterController = GetComponent<CharacterController>();
        camTransform = transform.Find("Camera");
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void TryInteract(Vector3 pos, Vector3 rot)
    {
        var hits = Physics.RaycastAll(pos, rot, InteractRange);
        foreach (var hit in hits)
        {

            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.OnInteract(gameObject);
            }

            //if (hit.transform.gameObject.CompareTag("Wall"))
            //{
            //    var wallObject = hit.transform.GetComponent<WallObject>();
            //    wallObject.Build();
            //}
        }
    }

    IEnumerator doHook()
    {
        RaycastHit[] hits = Physics.RaycastAll(camTransform.position, camTransform.forward, HookRange);

        if (hits.Length < 0)
            yield return 0;

        Vector3 hitPoint = hits[0].point;

        Collider[] colliders = Physics.OverlapSphere(hitPoint, 4.0f);



        HookVisual.SetActive(true);

        float progress = 0.0f;

        bool flag = false;

        while(progress < 1.0f)
        {

            Vector3 p0 = transform.position;
            Vector3 p1 = transform.position + (hitPoint - transform.position) * 2;
            Vector3 p2 = transform.position;

            HookVisual.transform.position = Tweens.BezierLerp(p0, p1, p2, progress);

            if (progress > 0.5f && !flag)
            {
                foreach (Collider col in colliders)
                {
                    IHookable hookable = col.GetComponent<IHookable>();
                    if (hookable == null)
                        continue;

                    hookable.OnHook(gameObject);
                }
                flag = true;
            }

            progress += Time.deltaTime * 3;

            yield return null;
        }


        HookVisual.SetActive(false);

        yield return 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(doHook());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract(camTransform.position, camTransform.forward);
            //OnInteractClicked(camTransform.position, camTransform.forward);
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 move = (transform.right * input.x + transform.forward * input.y).normalized * _moveSpeed;
        print(_velocity.y);
        Vector3 motion = new Vector3(move.x, 0f, move.z);
        motion += Vector3.up * _velocity.y;
        characterController.Move(motion * Time.fixedDeltaTime);

        bool grounded = (characterController.collisionFlags & CollisionFlags.Below) != 0;

        Vector2 mouseDir = Input.mousePositionDelta * _mouseSensitivity;

        transform.Rotate(0, mouseDir.x, 0);
        _camRotX += mouseDir.y;
        _camRotX = Mathf.Clamp(_camRotX, -90, 90);
        camTransform.localRotation = Quaternion.Euler(-_camRotX, 0, 0);



        if (grounded && _velocity.y < 0f)
        {
            // Apply small downward force to help character stick to the ground
            _velocity.y = -1f;
        }
        else
        {

        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {

            _velocity.y = _jumpForce;
        }


        _velocity.y += _gravityForce * Time.fixedDeltaTime;
        if (_velocity.y < -10.0f) _velocity.y = -10f;


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wave"))
        {
            string currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);
        }
    }

}
