using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    // Oluşturulacak objeyi tutacak değişken
    public GameObject selectedObject;
    public GameObject objectPrefab;
    private GameObject dragObject;
    public GameObject confirmWindow;
    
    public int price;
    

    // Raycast ile tespit edilecek nokta
    private Vector3 mousePosition;
    private RaycastHit hit;

    public Button confirmButton;
    public Button cancelButton;

    public void OnBeginDrag(PointerEventData eventData)
    {
        MoneyManager moneyManager=FindObjectOfType<MoneyManager>();
        if (moneyManager.money>=price)
        {
            dragObject = Instantiate(selectedObject, eventData.position, selectedObject.transform.rotation);
        }
        
    }
    public void OnDrag(PointerEventData eventData)
{
    // Mouse'un pozisyonunu al
    mousePosition = Input.mousePosition;

    // Mouse'un pozisyonunu dünya koordinatlarına çevir
    Ray ray = Camera.main.ScreenPointToRay(mousePosition);

    if (selectedObject.tag == "Obstacle")
{
    int layerMask = 1 << LayerMask.NameToLayer("Path");
    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
    {
        dragObject.transform.position = hit.point;
    }
}
else
{
    int layerMask = 1 << LayerMask.NameToLayer("Ground");
    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
    {
        dragObject.transform.position = hit.point;
    }
}


}

public void OnEndDrag(PointerEventData eventData)
{
    MoneyManager moneyManager=FindObjectOfType<MoneyManager>();
    if (moneyManager.money>=price)
    {
        confirmWindow.SetActive(true);
    }
    

}


public void confirm()
{
    MoneyManager moneyManager = FindObjectOfType<MoneyManager>();
    moneyManager.SpendMoney(price);
    Instantiate(objectPrefab, dragObject.transform.position, dragObject.transform.rotation);
    
    confirmWindow.SetActive(false);
    Destroy(dragObject);
}
public void cancel()
{
    Destroy(dragObject);
    confirmWindow.SetActive(false);
}
}
