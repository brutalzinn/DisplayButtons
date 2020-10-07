using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NthDeveloper.MultiLanguage
{
    enum TextFileLineType
    {
        Unknown,
        Empty,
        Comment,
        KeyValue,
        GroupHeader,
        FileHeader
    }
}
