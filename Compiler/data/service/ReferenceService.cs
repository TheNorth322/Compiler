using System.Diagnostics;
using Compiler.domain.repository;

namespace Compiler.data.service;

public class ReferenceService : IReferenceRepository
{
    public void CallReference(string localization)
    {
        string reference = $"reference-{localization}.html";
        Process.Start(new ProcessStartInfo(reference) { UseShellExecute = true });
    }

    public void CallProgramDescription(string localization)
    {
        string path = $"description-{localization}.html";
        Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
    }
}