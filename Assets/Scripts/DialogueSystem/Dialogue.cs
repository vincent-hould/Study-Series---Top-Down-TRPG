using System.Collections.Generic;

namespace TopDownTRPG
{
    public class Dialogue
    {
        public Dialogue(string actor, List<string> lines)
        {
            Actor = actor;
            Lines = lines;
        }

        public string Actor { get; private set; }
        public List<string> Lines { get; private set; }
    }
}
