using UnityEngine;
//^My Directives

public class SettingsBackPulser : MonoBehaviour //My blueprint
{
    public float BACKBUTTONPulser = 0.9f; //Rate at which the BACK Text pulses
    public float maxScale = 1f; //MAX scale of the obj during BACKPulse
    public float minScale = 0.5f; //MIN scale of the obj during BACKPulse
    public float PULSERSPEED = 1f;

    private RectTransform rectTransform; //reference to the obj RectTransform

    void Awake() //my script instnace is being loaded
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update() //updating every frame
    {
        // the scale of the object between minScale and maxScale at a rate of BACKPulser !
        float scale = Mathf.Lerp(minScale, maxScale, Mathf.PingPong(Time.time * BACKBUTTONPulser *PULSERSPEED, 1));
        rectTransform.localScale = new Vector3(scale, scale, 1f); //applying the caculated scale to the obj
    }
}
