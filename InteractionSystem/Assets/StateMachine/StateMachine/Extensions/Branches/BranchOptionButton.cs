using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BranchOptionButton : MonoBehaviour, IPointerClickHandler
{
    public Action<BranchOptionButton> OnClick;
    public Text Label;

    public SequenceBranchOption BranchOption { get; private set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(this);
    }

    public void Bind(SequenceBranchOption sequenceBranchOption)
    {
        BranchOption = sequenceBranchOption;

        Label.text = sequenceBranchOption.Text;
    }
}