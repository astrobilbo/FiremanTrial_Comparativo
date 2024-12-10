using System.Collections.Generic;
using UnityEngine;

namespace FiremanTrial.WithArchitecture
{
    public static class MeshRendererHelper
    {
        public static List<MeshRenderer> GetAllMeshRenderers(GameObject root)
        {
            List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
            CollectMeshRenderers(root.transform, meshRenderers);
            return meshRenderers;
        }

        private static void CollectMeshRenderers(Transform current, List<MeshRenderer> meshRenderers)
        {
            MeshRenderer meshRenderer = current.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderers.Add(meshRenderer);
            }

            foreach (Transform child in current)
            {
                CollectMeshRenderers(child, meshRenderers);
            }
        }

        public static List<Material> GetAllMaterialInstances(List<MeshRenderer> meshRenderers)
        {
            HashSet<Material> uniqueMaterialInstances = new HashSet<Material>();

            foreach (var meshRenderer in meshRenderers)
            {
                if (meshRenderer != null && meshRenderer.sharedMaterials != null)
                {
                    Material[] materialInstances = meshRenderer.materials;

                    foreach (var materialInstance in materialInstances)
                    {
                        if (materialInstance != null)
                        {
                            uniqueMaterialInstances.Add(materialInstance);
                        }
                    }
                }
            }

            return new List<Material>(uniqueMaterialInstances);
        }


        public static void ApplyEmissionHighlight(List<MeshRenderer> meshRenderers, Color emissionColor)
        {
            Debug.Log("ApplyEmissionHighlight");
            foreach (var meshRenderer in meshRenderers)
            {
                if (meshRenderer != null)
                {
                    Material[] materials = meshRenderer.materials; 
                    foreach (var material in materials)
                    {
                        if (material != null && material.HasProperty("_EmissionColor"))
                        {
                            material.EnableKeyword("_EMISSION");
                            material.SetColor("_EmissionColor", emissionColor);
                        }
                    }
                }
            }
        }


        public static void RemoveEmissionHighlight(List<MeshRenderer> meshRenderers)
        {
            Debug.Log("RemoveEmissionHighlight");
            foreach (var meshRenderer in meshRenderers)
            {
                if (meshRenderer != null)
                {
                    Material[] materials = meshRenderer.materials; 
                    foreach (var material in materials)
                    {
                        if (material != null && material.HasProperty("_EmissionColor"))
                        {
                            material.SetColor("_EmissionColor", Color.black);
                            material.DisableKeyword("_EMISSION"); 
                        }
                    }
                }
            }
        }
    }
}