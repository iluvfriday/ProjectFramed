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

    private Renderer rend;
    private Material[] originalMaterials;
    private bool isOutlined = false;

    [SerializeField]
    private Material outlineMaterial;

    void Start()
    {
        joint = GetComponent<HingeJoint>();
        rend = GetComponent<Renderer>();

        if (rend != null)
        {
            originalMaterials = rend.materials;
        }
    }

    public void ToggleDoor()
    {
        if (joint == null)
            return;

        JointSpring spring = new JointSpring
        {
            spring = springForce,
            damper = damper,
            targetPosition = isOpen ? closeAngle : openAngle,
        };

        joint.spring = spring;
        joint.useSpring = true;

        isOpen = !isOpen;
    }

    public void SetOutline(bool enable)
    {
        if (rend == null || outlineMaterial == null || isOutlined == enable)
            return;

        if (enable)
        {
            Material[] newMaterials = new Material[originalMaterials.Length + 1];
            for (int i = 0; i < originalMaterials.Length; i++)
            {
                newMaterials[i] = originalMaterials[i];
            }
            newMaterials[newMaterials.Length - 1] = outlineMaterial;

            rend.materials = newMaterials;
        }
        else
        {
            rend.materials = originalMaterials;
        }

        isOutlined = enable;
    }
}
