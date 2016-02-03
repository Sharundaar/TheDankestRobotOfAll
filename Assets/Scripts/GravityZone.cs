using UnityEngine;
using System.Collections;

public class GravityZone : MonoBehaviour {

    [SerializeField]
    private Vector3 m_GravityDirection;

	void OnTriggerEnter(Collider _collider)
    {
        GravityFirstPersonController controller = _collider.GetComponent<GravityFirstPersonController>();
        if(controller != null)
        {
            controller.SetGravity(Physics.gravity.magnitude * m_GravityDirection);
        }
    }

    void OnTriggerExit(Collider _collider)
    {
        GravityFirstPersonController controller = _collider.GetComponent<GravityFirstPersonController>();
        if (controller != null)
        {
            controller.SetGravity(Physics.gravity);
        }
    }
}
