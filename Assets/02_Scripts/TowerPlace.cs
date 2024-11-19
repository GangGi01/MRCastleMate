using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TowerPlace : MonoBehaviour
{
    public ObjectPool arrowPool;
    public Transform snapPoint;
    private GameObject currentArrow;
    private GameObject currentTower;
    private XRGrabInteractable towerInteractable;
    private bool isInPlacementArea = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TOWER"))
        {
            currentArrow = arrowPool.GetObject(snapPoint.position);
            currentTower = other.gameObject;
            towerInteractable = currentTower.GetComponent<XRGrabInteractable>();
            isInPlacementArea = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TOWER"))
        {
            if (currentArrow != null)
            {
                arrowPool.ReturnObject(currentArrow);

            }
            currentTower = null;
            currentArrow = null;
            towerInteractable = null;
            isInPlacementArea = false;
        }
    }

    void Update()
    {
        if (isInPlacementArea && towerInteractable != null && !towerInteractable.isSelected)
        {
            currentTower.transform.position = snapPoint.position;
            arrowPool.ReturnObject(currentArrow);
            currentArrow = null;
            isInPlacementArea = false;
        }
    }

}
