using System;

namespace CSharp.Quizz.Common
{
    [Flags]
    public enum Category
    {
        ComputerScience = 1 << 0,
        CSharp = 1 << 1,
        DotNet = 1 << 2,
        DataStructure = 1 << 3,
        ProgrammingLogic = 1 << 4,
        ObjectOrientedProgamming = 1 << 5,
        VisualStudio = 1 << 6,
        PopularLibraries = 1 << 7
    }
}