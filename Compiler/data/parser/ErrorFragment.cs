using Compiler.domain.enums;

namespace Compiler.data.parser;

public class ErrorFragment
{
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
    public string Fragment { get; set; }
    public ErrorFragment(int startIndex, int endIndex, string fragment)
    {
        StartIndex = startIndex;
        EndIndex = endIndex;
        Fragment = fragment;
    }
    public void UpdateFragment(char c, int endIndex)
    {
        Fragment += $"{c}";
        EndIndex = endIndex;
    }
}