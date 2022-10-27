using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlinkingText : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI text;
    public float blinkFadeInTime = 0.5f;
    public float blinkStayTime = 0.8f;
    public float blinkFadeOutTime = 0.7f;
    public float _timeChecker = 0f;
    private Color _colour;

    // Start is called before the first frame update


    private void OnEnable()
    {
        transform.position = new Vector3 (transform.position.x, 700, transform.position.z);
        blinkFadeInTime = 1f;
        blinkStayTime = 1.3f;
        blinkFadeOutTime = 1.1f;
        _timeChecker = 0f;
        _colour = Color.white;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(0, -0.5f, 0);
        _timeChecker += Time.deltaTime;
        if (_timeChecker < blinkFadeInTime)
        {
            text.color = new Color(_colour.r, _colour.g, _colour.b, _timeChecker / blinkFadeInTime);
        }
        else if (_timeChecker < blinkFadeInTime + blinkStayTime)
        {
            text.color = new Color(_colour.r, _colour.g, _colour.b, 1);
        }
        else if (_timeChecker < blinkFadeInTime + blinkStayTime + blinkFadeOutTime)
        {
            text.color = new Color(_colour.r, _colour.g, _colour.b, 1 - (_timeChecker - (blinkFadeInTime + blinkStayTime)) / blinkFadeOutTime);
        }
    }
}
