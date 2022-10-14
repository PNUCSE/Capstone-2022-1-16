using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    public bool isClient1Connect { get; set; }=false;
    public bool isClient2Connect { get; set; }=false;
    private bool isSceneChanged;
    void Update()
    {
        if (isSceneChanged)
            return;
        if(isClient1Connect&& isClient2Connect)
        {
            isSceneChanged=true;
            SceneManager.LoadScene("HMDTrackingScene");
        }
    }
}
