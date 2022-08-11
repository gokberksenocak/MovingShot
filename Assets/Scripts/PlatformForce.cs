using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformForce : MonoBehaviour
{
    [SerializeField] private float angle;
    [SerializeField] private float force;

    private void OnCollisionEnter(Collision other)
    {
        other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(angle, 90, 0) * force, ForceMode.Force);
    }
}
