using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Drawer : MonoBehaviour, IInteractableObject
{
    [SerializeField]
    public Material outlineMaterial;

    [SerializeField]
    public GameObject objectRenderer;

    private Animator m_Animator;
    private bool isOpen = false;

    private Renderer rend;
    private Material[] originalMaterials;
    private bool isOutlined = false;

    void Start()
    {
        m_Animator = GetComponent<Animator>();

        rend = objectRenderer.GetComponent<Renderer>();

        if (rend != null)
        {
            originalMaterials = rend.materials;
        }
    }

    public void ToggleObject()
    {
        if (!isOpen)
        {
            m_Animator.SetTrigger("Open");
            isOpen = !isOpen;
        }
        else
        {
            m_Animator.SetTrigger("Close");
            isOpen = !isOpen;
        }
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
