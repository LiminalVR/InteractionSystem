using System.Collections;
using System.Collections.Generic;
using App.StateMachine;
using UnityEngine;
using UnityEngine.UI;

public class SequenceBranch : SequenceState
{
    public SequenceStateMachine SequenceStateMachine;
    public BranchOptionButton BranchOptionPrefab;
    public Transform BranchContainer;

    public ESequenceContinueType ContinueType;

    public string Header = "What would you pick?";
    public List<SequenceBranchOption> Options;
    public Text HeaderLabel;

    private SequenceBranchOption _selectedOption;
    private List<BranchOptionButton> _optionButtons = new List<BranchOptionButton>();

    public override void Entered(IState from)
    {
        base.Entered(from);

        HeaderLabel.text = Header;

        foreach (var sequenceBranchOption in Options)
        {
            var optionInstance = Instantiate(BranchOptionPrefab, BranchContainer);
            optionInstance.OnClick += OnOptionClicked;
            optionInstance.Bind(sequenceBranchOption);
            _optionButtons.Add(optionInstance);
        }
    }

    public override void Exited(IState from)
    {
        base.Exited(from);

        for (int i = 0; i < _optionButtons.Count; i++)
            Destroy(_optionButtons[i].gameObject);

        _optionButtons.Clear();
        _selectedOption = null;
    }

    public override IEnumerator Run()
    {
        yield return new WaitUntil(() => _selectedOption != null);

        switch (ContinueType)
        {
            case ESequenceContinueType.Add:
                SequenceStateMachine.Add(_selectedOption.SequenceStateMachine);
                break;

            case ESequenceContinueType.Continue:
                yield return _selectedOption.SequenceStateMachine.Run();
                break;
        }
    }

    private void OnOptionClicked(BranchOptionButton optionButton)
    {
        _selectedOption = optionButton.BranchOption;
    }
}