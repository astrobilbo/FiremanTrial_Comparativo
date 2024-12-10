using UnityEngine;

namespace FiremanTrial.WithArchitecture
{
    public class Lid : InteractiveObject
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Transform initialPositionHolder;
        void GetLid()
        {
            meshRenderer.enabled = false;
        }

        void PlaceLid(Transform holder)
        {
            gameObject.transform.parent = holder.transform;
            gameObject.transform.position = Vector3.zero;
            meshRenderer.enabled = true;
        }

        void ReturnLidToInitialPosition()
        {
            gameObject.transform.parent = initialPositionHolder.transform;
            gameObject.transform.position = Vector3.zero;
            meshRenderer.enabled = true;
        }
    }
}
