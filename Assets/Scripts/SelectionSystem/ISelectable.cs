
namespace TopDownTRPG
{
    public interface ISelectable
    {
        public bool CanBeSelected();

        public void SetSelected(bool selected = true);
    }
}
