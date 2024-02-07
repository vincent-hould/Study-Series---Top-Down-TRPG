using UnityEngine;
using TopDownTRPG.Assets.Scripts.Framework;
using TopDownTRPG.Assets.Scripts.UISystem;

namespace TopDownTRPG
{
    public class UIManager : BaseMonoSingleton<UIManager>
    {
        [SerializeField] private DialogueBox DialogueBox;
        [SerializeField] private Header Header;
        [SerializeField] private ActionMenu ActionMenu;

        public IActionMenu GetActionMenu()
        {
            return ActionMenu;
        }

        public IHeader GetHeader()
        {
            return Header;
        }

        public IDialogueBox GetDialogueBox()
        {
            return DialogueBox;
        }
    }
}
