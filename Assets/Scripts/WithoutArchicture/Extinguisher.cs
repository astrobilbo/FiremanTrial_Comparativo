using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace FiremanTrial.WithoutArch
{
    public class Extinguisher : MonoBehaviour
    {
        public bool activeExtinguisher;
        public string[] fireClass;

        [SerializeField] private Vector3 handPosition;
        [SerializeField] private Vector3 handRotation;
        [SerializeField] private ParticleSystem ps;
        [SerializeField] private float initialContent;
        [SerializeField] private float contentFlow;
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        [SerializeField] private Slider slider;

        private Vector3 _startPosition;
        private Quaternion _startRotation;
        private Transform _startParent;
        private string _extinguisherStartTag;
        private Player _player;
        private float _currentContent;

        private void Start()
        {
            _extinguisherStartTag = tag;
            _startPosition = transform.position;
            _startRotation = transform.rotation;
            _startParent = transform.parent;
            _currentContent = initialContent;
            if (slider!=null)
            {
                slider.value =  (_currentContent*100/initialContent);    
            }

            if (textMeshProUGUI != null)
            {
                textMeshProUGUI.text = (_currentContent*100/initialContent).ToString("F")+"%";    
            }
        }

        private void FixedUpdate()
        {
            if (activeExtinguisher && _currentContent <= 0)
            {
                ps.Play();
                _currentContent -= contentFlow;

                var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                var hits = Physics.RaycastAll(ray, 15f);

                Debug.DrawRay(ray.origin, ray.direction * 15, Color.red);

                foreach (var hit in hits)
                {
                    if (hit.transform.CompareTag("Fire"))
                    {
                        Debug.Log("FirePoint hit");
                        var firePoint = hit.transform.GetComponent<Fire>();
                        firePoint.TryReduceFire(this);
                        break;
                    }
                    else if (hit.transform.CompareTag("Wall"))
                    {
                        Debug.Log("Wall hit");
                        break;
                    }
                }

                if (slider!=null)
                {
                    slider.value =  (_currentContent*100/initialContent);    
                }

                if (textMeshProUGUI != null)
                {
                    textMeshProUGUI.text = (_currentContent*100/initialContent).ToString("F")+"%";    
                }
            }
            else
            {
                RemoveFromHand();
            }
        }

        public bool CanBeTake()
        {
            return !_player;
        }

        public void MoveToHand(Player player)
        {
            if (!CanBeTake())
            {
                tag = player.tag;
                transform.parent = player.transform;
                transform.position = _startPosition;
                transform.rotation = _startRotation;
                _player = player;
            }
            else
            {
                Debug.Log("Can't take to hand of " + player.name +" because it is in "+_player.name + "'s hand.");
            }
        }

        public void RemoveFromHand()
        {
            ps.Stop();
            tag = _extinguisherStartTag;
            transform.parent = _startParent;
            transform.position = _startPosition;
            transform.rotation = _startRotation;
           
            _player.myExtinguisher = null;
            _player = null;
        }
    }
}