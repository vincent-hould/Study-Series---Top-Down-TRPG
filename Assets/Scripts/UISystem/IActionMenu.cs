using System;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownTRPG
{
    public interface IActionMenu
    {
        public void PromptForAction(Vector3 origin, List<ActionMenuItem> choices);

        public void HideMenu();
    }
}
