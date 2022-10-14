using UnityEngine;

public class Escape : MonoBehaviour
{
    int ClickCount = 0;

    private void Awake()
    {
        var obj = FindObjectsOfType<Escape>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            ClickCount++;
            if (!IsInvoking("DoubleClick"))
                Invoke("DoubleClick", 1.0f);
        }
        else if (ClickCount == 2)
        {
            CancelInvoke("DoubleClick");
            Application.Quit();
        }
    }
    void DoubleClick()
    {
        ClickCount = 0;
    }
}
