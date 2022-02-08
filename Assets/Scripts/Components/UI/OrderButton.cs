using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OrderButton : MonoBehaviour
{
    public string orderName;
    public string orderArgument;

    public void Start()
    {
        GetComponent<Button>().onClick.AddListener(Click);
    }

    public void Click()
    {
        Contexts.sharedInstance.input.ReplaceSelectedOrder(orderName, orderArgument);
    }
}
