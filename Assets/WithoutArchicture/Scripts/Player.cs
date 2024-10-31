using TMPro;
using UnityEngine;

namespace FiremanTrial.WithoutArchitecture
{
    public class Player : MonoBehaviour
    {
        public Extinguisher myExtinguisher;
        private PanLid _panLid;
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float moveSpeed = 5;
        [SerializeField] private float gravity = 10;
        [SerializeField] private float maxDistanceToTakeObjects = 15f;
        [SerializeField] private TextMeshProUGUI canInteractText;
        [SerializeField] private QuestManager questManager;
        private Camera _camera;
        private float _targetRotation;
        private readonly Vector3 _middleScreen = new Vector3(0.5f, 0.5f, 0);
        private bool _canMove = true;
        [SerializeField] private Transform handlePosition;
        private float currentIKWeight = 0.0f;
        private bool isInteracting = false;
        private float reachStartTime = 0.5f; // Tempo de início do IK durante a animação
        private float reachEndTime = 1.0f; // Tempo de fim do IK durante a animação
        private float maxIKWeight = 1.0f; // Peso máximo do IK

        public bool CanMove
        {
            get { return _canMove; }
            set { _canMove = value; }
        }

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

            if (animator == null)
            {
                Debug.Log("animator is null", this);
            }

            _camera = Camera.main;
        }

        private void Update()
        {
            if (!_canMove)
            {
                return;
            }



            //Change position
            var inputXAxis = Input.GetAxis("Horizontal");
            var inputYAxis = Input.GetAxis("Vertical");
            var forward = characterController.transform.TransformDirection(Vector3.forward);
            var right = characterController.transform.TransformDirection(Vector3.right);
            var moveDirection = (inputXAxis * right + inputYAxis * forward).normalized;
            animator.SetFloat("Move", inputYAxis);
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
                    myExtinguisher.activeExtinguisher = true;
                }


                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("Primary action stopped.");
                    myExtinguisher.activeExtinguisher = false;
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
                            canInteractText.gameObject.SetActive(true);
                            canInteractText.text = "'E' -> Coletar o extintor.";
                        }

                        if (Input.GetKeyUp(KeyCode.E))
                        {
                            if (myExtinguisher != null)
                            {
                                myExtinguisher.activeExtinguisher = false;
                                myExtinguisher.RemoveFromHand();
                                myExtinguisher = null;
                            }

                            myExtinguisher = extinguisher;
                            myExtinguisher.MoveToHand(this);
                        }
                    }

                    break;
                }
                else if (hit.transform.CompareTag("Discharge"))
                {
                    var discharge = hit.transform.GetComponent<Discharge>();
                    findSomethingOnHit = true;
                    if (canInteractText == null)
                    {

                        Debug.Log("TextMeshProUGUI dont referenced");
                    }
                    else
                    {
                        canInteractText.gameObject.SetActive(true);
                        canInteractText.text = "'E' -> Dar descarga.";
                    }

                    if (Input.GetKeyUp(KeyCode.E))
                    {
                        discharge.Play();
                    }

                    break;
                }
                else if (hit.transform.CompareTag("PanLid"))
                {
                    var panLid = hit.transform.GetComponent<PanLid>();
                    findSomethingOnHit = true;
                    if (canInteractText == null)
                    {

                        Debug.Log("TextMeshProUGUI dont referenced");
                    }
                    else
                    {
                        canInteractText.gameObject.SetActive(true);
                        canInteractText.text = "'E' -> Pegar a tampa da panela.";
                    }

                    if (Input.GetKeyUp(KeyCode.E))
                    {
                        panLid.GoToUserInventory();
                        _panLid = panLid;
                    }

                    break;
                }
                else if (hit.transform.CompareTag("Fire"))
                {
                    var fire = hit.transform.GetComponent<Fire>();
                    if (_panLid == null)
                    {
                        break;
                    }

                    findSomethingOnHit = true;
                    if (canInteractText == null)
                    {

                        Debug.Log("TextMeshProUGUI dont referenced");
                    }
                    else
                    {
                        canInteractText.gameObject.SetActive(true);
                        canInteractText.text = "'E' -> Tampar a frigideira.";
                    }

                    if (Input.GetKeyUp(KeyCode.E))
                    {
                        _panLid.PlaceInPan();
                        fire.extinguishFire();
                    }

                    break;
                }

                else if (hit.transform.CompareTag("KitchenGas"))
                {
                    var gas = hit.transform.GetComponent<KitchenGas>();
                    if (!gas.Open())
                    {
                        break;
                    }

                    findSomethingOnHit = true;
                    if (canInteractText == null)
                    {

                        Debug.Log("TextMeshProUGUI dont referenced");
                    }
                    else
                    {
                        canInteractText.gameObject.SetActive(true);
                        canInteractText.text = "'E' -> Fechar o gás.";
                    }

                    if (Input.GetKeyUp(KeyCode.E))
                    {
                        gas.CloseGas();
                    }

                    break;
                }
                else if (hit.transform.CompareTag("Door") && !isInteracting)
                {
                    if (questManager.DoorIsOpen())
                    {
                        break;
                    }

                    Debug.Log("Door hit");
                    findSomethingOnHit = true;

                    if (canInteractText == null)
                    {
                        Debug.Log("TextMeshProUGUI dont referenced");
                    }
                    else
                    {
                        canInteractText.gameObject.SetActive(true);
                        canInteractText.text = "'E' -> Abrir a porta.";
                    }

                    if (Input.GetKeyUp(KeyCode.E))
                    {
                        isInteracting = true;
                        //animator.Play("Opening");
                    }

                    break;
                }
                else if (hit.transform.CompareTag("Wall"))
                {
                    break;
                }

                if (!findSomethingOnHit)
                {
                    canInteractText.text = "";
                    canInteractText.gameObject.SetActive(false);
                }

            }
        }

        void OnAnimatorIK(int layerIndex)
        {
            if (!animator)
            {
                Debug.LogWarning("Animator is missing");
                return;
            }

            if (!handlePosition)
            {
                Debug.LogWarning("HandlePosition is missing");
                return;
            }

            if (!isInteracting)
            {
                Debug.LogWarning("dont have interaction");
                return;
            }
            
                Debug.Log("IK change started");
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                float animTime = stateInfo.normalizedTime % 1.0f;
                Debug.Log("Anim time is:"+ animTime);
                if (isInteracting && animTime >= reachStartTime && animTime <= reachEndTime)
                {
                    float t = Mathf.InverseLerp(reachStartTime, reachEndTime, animTime);
                    currentIKWeight = Mathf.Lerp(0, maxIKWeight, t);
                    Debug.Log(currentIKWeight);
                }
                else
                {
                    currentIKWeight = 0.0f;
                    Debug.Log("Ik is 0");
                }

                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, currentIKWeight);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, currentIKWeight);

                if (currentIKWeight > 0)
                {
                    animator.SetIKPosition(AvatarIKGoal.RightHand, handlePosition.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, handlePosition.rotation);
                    Debug.Log("IK goal setledO");
                }

                if (animTime >= reachEndTime) isInteracting = false;
            
        }
    }
}