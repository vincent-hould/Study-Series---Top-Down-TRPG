
namespace TopDownTRPG.Assets.Scripts.UISystem
{
    public interface IDialogueBox
    {
        public void DisplayDialogueBox(string actor);

        public void PrintText(string text);

        public void HideDialogueBox();
    }
}
