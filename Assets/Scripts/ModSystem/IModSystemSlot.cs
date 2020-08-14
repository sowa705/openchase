using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IModSystemSlot
{
    void PartAdded(string Type,string Part);
    void PartNulled(string Type);
}