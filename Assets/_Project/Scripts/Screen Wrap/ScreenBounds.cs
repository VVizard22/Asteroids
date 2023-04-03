using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class ScreenBounds : MonoBehaviour
{
    //public Camera mainCamera;
    private BoxCollider2D boxCollider;

    // Offset for calculating the wrapping bounds
    [SerializeField]
    private float teleportOffset = .2f;
    [SerializeField]
    private float cornerOffset = 1f;

    private void Awake()
    {
        // Reset local scale of camera to avoid calculation bugs
        Camera.main.transform.localScale = Vector3.one;

        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }


    private void Start()
    {
        transform.position = Vector3.zero;
        UpdateBoundsSize();
    }

    public void UpdateBoundsSize()
    {
        // orthographicSize = half the size of the height of the screen.
        float ySize = Camera.main.orthographicSize * 2;

        // Width of the camera depends on the aspect ratio and the height
        Vector2 boxColliderSize = new Vector2(ySize * Camera.main.aspect + teleportOffset, ySize + teleportOffset);

        boxCollider.size = boxColliderSize;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.TryGetComponent<WrapTrigger>(out WrapTrigger wrapTrigger);

        if (!wrapTrigger)
            return;

        Vector2 newWrappedPosition = CalculateOutOfBounds(wrapTrigger.transform.position);
        wrapTrigger.WrapToPosition(newWrappedPosition);
    }

    // Method responsible of calculating in which axis and direction the object is out of bounds
    // And returns the new desired position
    private Vector2 CalculateOutOfBounds(Vector2 targetPosition)
    {
        bool xBoundResult = Mathf.Abs(targetPosition.x) > (Mathf.Abs(boxCollider.bounds.min.x) - cornerOffset);
        bool yBoundResult = Mathf.Abs(targetPosition.y) > (Mathf.Abs(boxCollider.bounds.min.y) - cornerOffset);

        if (xBoundResult && yBoundResult)
            return new Vector2(targetPosition.x, targetPosition.y) * -1;
        else if (xBoundResult)
            return new Vector2(targetPosition.x * -1, targetPosition.y);
        else if (yBoundResult)
            return new Vector2(targetPosition.x, targetPosition.y * -1);
        else
            return targetPosition;
    }
}
