using UnityEngine;

public class StartMenu : MonoBehaviour
{
    GameObject thisPanel;
    ZoomThrough mainMenuTransition;
    public GameObject nextPanel;

    private void Start()
    {
        thisPanel = this.gameObject;
        mainMenuTransition = GetComponent<ZoomThrough>();
    }

    void Update()
    {
        if(thisPanel.activeSelf && Input.anyKeyDown)
        {
            // TRANSITION CODE WILL GO IN THIS IF STATEMENT WHEN IT'S DONE!!
            // mainMenuTransition.ZoomThroughPanel(nextPanel); <-- Will look something like that

            Debug.Log("Beginning Program");           
            thisPanel.SetActive(false);
            nextPanel.SetActive(true);

        }
    }
}
