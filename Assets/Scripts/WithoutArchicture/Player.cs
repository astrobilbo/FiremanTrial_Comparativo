using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace FiremanTrial.WithoutArch
{
    public class Player : MonoBehaviour
    {
        public Extinguisher myExtinguisher;
        
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float moveSpeed = 5;
        [SerializeField] private float gravity = 10;
        [SerializeField] private float maxDistanceToTakeObjects = 15f;
        [SerializeField] private TextMeshProUGUI canInteractText;
        
        private Camera _camera;
        private float _targetRotation;
        private readonly Vector3 _middleScreen = new Vector3(0.5f, 0.5f, 0);


        private void Start()
        {
            if (characterController == null)
            {
                if (GetComponent<CharacterController>())
                {
                    characterController = GetComponent<CharacterController>();
                }
                else
                {
                    characterController = gameObject.AddComponent<CharacterController>();
                }
            }
            _camera = Camera.main;
        }

        private void Update()
        {
            //Change position
            var inputXAxis = Input.GetAxis("Horizontal");
            var inputYAxis = Input.GetAxis("Vertical");

            var forward = characterController.transform.TransformDirection(Vector3.forward);
            var right = characterController.transform.TransformDirection(Vector3.right);

            var moveDirection = (inputXAxis * right + inputYAxis * forward).normalized;
            moveDirection *= moveSpeed;
            moveDirection += gravity * Vector3.down;
            characterController.Move(moveDirection * Time.deltaTime);

            //Change rotation
            var xRotationAxes = Input.GetAxis("Mouse X");
            var yRotationAxes = -Input.GetAxis("Mouse Y");

            _targetRotation += yRotationAxes;
            _targetRotation = Mathf.Clamp(_targetRotation, -90, 90);

            _camera.transform.localRotation = Quaternion.Euler(_targetRotation, 0, 0);
            characterController.transform.Rotate(Vector3.up, xRotationAxes);

            //Interact with extinguisher
            if (myExtinguisher != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Primary action called.");
                    myExtinguisher.activeExtinguisher=true;
                }


                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("Primary action stopped.");
                    myExtinguisher.activeExtinguisher=false;
                }
            }

            //Raycast to find objects
            var ray = _camera.ViewportPointToRay(_middleScreen);
            var hits = Physics.RaycastAll(ray, maxDistanceToTakeObjects);
            var findSomethingOnHit = false;
            Debug.DrawRay(ray.origin, ray.direction * maxDistanceToTakeObjects, Color.red);

            foreach (var hit in hits)
            {
                if (hit.transform.CompareTag("Extinguisher"))
                {
                    var extinguisher = hit.transform.GetComponent<Extinguisher>();
                    Debug.Log("Extinguisher hit");
                    findSomethingOnHit = true;

                    if (extinguisher.CanBeTake())
                    {
                        if (canInteractText == null)
                        {
                            Debug.Log("TextMeshProUGUI dont referenced");
                        }
                        else
                        {
                            canInteractText.text = "Press E to Interact with the Extinguisher";
                        }

                        if (Input.GetKeyUp(KeyCode.E))
                        {
                            if (myExtinguisher != null)
                            {
                                myExtinguisher.activeExtinguisher=false;
                                myExtinguisher.RemoveFromHand();
                                myExtinguisher = null;
                            }

                            myExtinguisher = extinguisher;
                            myExtinguisher.MoveToHand(this);
                        }
                    }

                    break;
                }
                else if (hit.transform.CompareTag("Wall"))
                {
                    Debug.Log("Wall hit");
                    break;
                }
            }

            if (!findSomethingOnHit)
            {
                canInteractText.text = "";
            }
        }
    }
}