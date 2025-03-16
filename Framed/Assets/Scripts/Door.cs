using UnityEngine;

public class Door : MonoBehaviour
{
    private HingeJoint joint;
    private bool isOpen = false;

    [SerializeField]
    private float openAngle = 90f;

    [SerializeField]
    private float closeAngle = 0f;

    [SerializeField]
    private float springForce = 100f;

    [SerializeField]
    private float damper = 50f;

    void Start()
    {
        joint = GetComponent<HingeJoint>();
    }

    public void ToggleDoor()
    {
        if (joint == null)
            return;

        JointSpring spring = new JointSpring();
        spring.spring = springForce;
        spring.damper = damper;

        spring.targetPosition = isOpen ? closeAngle : openAngle;

        joint.spring = spring;
        joint.useSpring = true;

        isOpen = !isOpen;
    }
}
