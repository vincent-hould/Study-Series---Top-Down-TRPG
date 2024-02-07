using System;

namespace TopDownTRPG
{
    public sealed class ActionMenuItem
    {
        public string Label { get; private set; }
        public Action Callback { get; private set; }
        public bool Enabled { get; private set; }

        public ActionMenuItem(string label, Action callback, bool enabled = true)
        {
            Label = label;
            Callback = callback;
            Enabled = enabled;
        }
    }
}
