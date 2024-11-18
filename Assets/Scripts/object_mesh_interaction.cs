using UnityEngine;


public class ObjectMeshDeformerInput : MonoBehaviour
{
    public float force = 10f;
    public float forceOffset = 0.1f;

    void Update()
    {
        // Cast a ray downward from the object to the ground
        Ray downwardRay = new Ray(transform.position, -Vector3.up);
        RaycastHit hit;

        if (Physics.Raycast(downwardRay, out hit))
        {
            MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer>();
            if (deformer)
            {
                Vector3 point = hit.point;
                point += hit.normal * forceOffset; // Apply an offset along the normal
                deformer.AddDeformingForce(point, force);
            }
        }
    }

}