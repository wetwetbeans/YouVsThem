using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activatechild : MonoBehaviour
{
    [SerializeField] GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
        yield return new WaitForSeconds(0.2f);
        child.SetActive(true);
    }
}
