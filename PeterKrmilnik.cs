using UnityEngine;

public class PeterKrmilnik : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;
    private bool isInDialog = false;

    public void Interact()
    {
        if (!isInDialog)
        {
            isInDialog = true;
            StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
            DialogManager.Instance.OnHideDialog += () => isInDialog = false;
        }
    }
}
