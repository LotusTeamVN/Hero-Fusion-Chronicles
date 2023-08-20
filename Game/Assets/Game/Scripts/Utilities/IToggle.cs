using UnityEngine;
using UnityEngine.UI;

public class IToggle<T> : MonoBehaviour where T : class
{
    [Header("Object Reference")]
    [SerializeField] protected CanvasGroup selectedBorder = null;

    public bool Selected { get; private set; }

    private Toggle toggle = null;
    public Toggle Toggle 
    {
        get
        {
            if (toggle == null)
                toggle = GetComponent<Toggle>();
            return toggle;
        }
    }

    public CanvasGroup CanvasGroup { get; private set; }

    public T Data { get; private set; }


    protected virtual void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();

        Toggle.group = GetComponentInParent<ToggleGroup>();
        Toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    public virtual void Initialized(T Data)
    {
        Toggle.isOn = false;
        this.Data = Data;
    }

    protected virtual void OnToggleValueChanged(bool value)
    {
        Selected = value;
        selectedBorder.alpha = value ? 1 : 0;
    }

    public void ToggleOff() => Toggle.isOn = false;
}
