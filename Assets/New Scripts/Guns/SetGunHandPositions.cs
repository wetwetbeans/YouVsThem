using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class SetGunHandPositions : MonoBehaviour
{
    [SerializeField] Transform leftHandTransform = null;
    [SerializeField] Transform rightHandTransform = null;
    [SerializeField] GameObject gunSlot1;
    [SerializeField] GameObject gunslot2;
    FullBodyBipedIK _fullBodyIK;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(gunSlot1.transform.childCount > 0 || gunslot2.transform.childCount > 0)
        {
            leftHandTransform = GetComponentInChildren<Handpositions>().leftHandPosition;
            rightHandTransform = GetComponentInChildren<Handpositions>().rightHandPosition;
            _fullBodyIK = GetComponentInChildren<FullBodyBipedIK>();
            SetHandPos();
        }


    }

    public void SetHandPos()
    {

        _fullBodyIK.solver.rightHandEffector.position = rightHandTransform.position;
        _fullBodyIK.solver.rightHandEffector.rotation = rightHandTransform.rotation;
        _fullBodyIK.solver.leftHandEffector.rotation = leftHandTransform.rotation;
        _fullBodyIK.solver.leftHandEffector.position = leftHandTransform.position;
    }
}

