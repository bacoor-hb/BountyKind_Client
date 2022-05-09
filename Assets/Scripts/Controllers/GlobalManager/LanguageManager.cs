using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//
public class LanguageManager : MonoBehaviour
{
    public LanguageView languageView;
    //THE language manager
    LanguageReader langReader;

    //Game language
    Language lang = Language.English;

    //path of the file that the game is reading from
    string langFilePath = "Resources/menuSentences.xml";

    protected void Awake()
    {
        //Initialize and set a default language
        langReader =
            new LanguageReader(Path.Combine(Application.dataPath, langFilePath),
                lang.Value,
                false);
    }

    void Start()
    {
        languageView.Init();
        langReader.OnStartLoadingLanguageFile += OnStartLoadingLanguageFile;
        langReader.OnEndLoadingLanguageFile += OnEndLoadingLanguageFile;
        langReader.OnStartTranslating += OnStartTranslating;
        langReader.OnEndTranslating += OnEndTranslating;
    }

    public void SetLanguageFile(string filePath)
    {
        langReader
            .setLanguage(Path.Combine(Application.dataPath, filePath),
            lang.Value);
        langFilePath = filePath;
    }

    public string GetSentence(string sentenceName)
    {
        return langReader.getString(sentenceName);
    }

    //Set game language
    public void SetLanguage(Language _language)
    {
        langReader
            .setLanguage(Path.Combine(Application.dataPath, langFilePath),
            _language.Value);
        lang = _language;
    }

    //Get game language
    public Language GetLanguage()
    {
        return lang;
    }

    void OnStartLoadingLanguageFile()
    {
        Debug.Log("Loading language file...");
    }

    void OnEndLoadingLanguageFile()
    {
        Debug.Log("Language file loaded!");
    }

    void OnStartTranslating()
    {
        Debug.Log("Translating...");
    }

    void OnEndTranslating()
    {
        Debug.Log("Translation complete!");
    }
}
