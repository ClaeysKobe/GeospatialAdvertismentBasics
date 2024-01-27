using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 5f;

    void Update()
    {
        // Find the direction vector from the object to the player
        Vector3 directionToCamera = (Camera.main.transform.position - transform.position).normalized;

        // Calculate the rotation needed to face the player
        Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

        // Smoothly rotate the object towards the player
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }
}
