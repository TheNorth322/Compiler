using System;
using System.IO;
using System.Windows;

namespace Compiler.utils;

public class LocalizationProvider : ILocalizationProvider
{
    private static LocalizationProvider _instance;
    private string _currentLocalizationCode;

    private LocalizationProvider(string currentLocalizationCode)
    {
        _currentLocalizationCode = currentLocalizationCode;
    }

    public static LocalizationProvider Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LocalizationProvider("ru-RU");
            }
            return _instance;
        }
    }

    public string CurrentLocalizationCode
    {
        get => _currentLocalizationCode;
        set
        {
            _currentLocalizationCode = value;
        }
    } 
    
    public string GetStringByCode(string code)
    {
        string path = $"C:\\Users\\gorshkov.2021\\RiderProjects\\Compiler\\Compiler\\locals\\{_currentLocalizationCode}.xaml";
        
        if (!Path.Exists(path))
            throw new FileNotFoundException("Localization file not found");

        ResourceDictionary myResourceDictionary = new ResourceDictionary();
        myResourceDictionary.Source = new Uri(path, UriKind.RelativeOrAbsolute);

        string? stringValue = myResourceDictionary[code] as string;

        if (stringValue == null)
            throw new ArgumentNullException(null, "String not found");

        return stringValue;
    }
}