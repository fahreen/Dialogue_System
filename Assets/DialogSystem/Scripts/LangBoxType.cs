using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CreateAssetMenu] // allow creation of the barrel types in your assests
[System.Serializable]
[ExecuteAlways]
public class LangBoxType : ScriptableObject
{
    //id 
    public string id;
    //current language
    public string currentLang;
    // Dictionary to store current languages
    public serializableDictionary Languages;
    //public LangBoxType manager;



    

    public void setvalues()
    {
        // set id 
        this.id = CreateRandomString();
        Debug.Log(this.id);

        // initilize serializable dictionary
        this.Languages = new serializableDictionary();

        // get manager
        LangBoxType[] gameTexts = Resources.FindObjectsOfTypeAll<LangBoxType>();
        LangBoxType[] gameTextsCopy = (LangBoxType[])gameTexts.Clone();

        for (int i = 0; i < gameTextsCopy.Length; i++)
        {
            if (gameTextsCopy[i].id == "manager")
            {


                // set current lang according to manager
                this.currentLang = gameTexts[i].currentLang;

                // set Language list according to manager
                int size = gameTexts[i].Languages.keyList.Count;
                for (int j = 0; j < size; j++)
                {
                    string k = gameTexts[i].Languages.keyList[j];
                    string v = gameTexts[i].Languages.valueList[j];
                    // fill Language dictionary
                    this.Languages.AddElement(k, v);
                }
            }
        }




    }


    private string CreateRandomString(int stringLength = 10)
    {
        int _stringLength = stringLength - 1;
        string randomString = "";
        string[] characters = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        for (int i = 0; i <= _stringLength; i++)
        {
            randomString = randomString + characters[UnityEngine.Random.Range(0, characters.Length)];
        }
        return randomString;
    }


    /*

    public void OnEnable()
    {

        LangBoxType[] gameTexts = Resources.FindObjectsOfTypeAll<LangBoxType>();
        LangBoxType[] gameTextsCopy = (LangBoxType[])gameTexts.Clone();

        for (int i = 0; i < gameTextsCopy.Length; i++)
        {
            if (gameTextsCopy[i].id == "manager")
            {
                Debug.Log("Manager");
                foreach (string lang in gameTexts[i].Languages.keyList)
                {
                    if (!this.Languages.keyList.Contains(lang))
                        this.Languages.AddElement(lang, "n/a");
                }
            }
        }





    }*/


    // create constructor that takes id

    // public  LangBoxType(){}



    // add itself to LangboxManager
    //this.id = this.name;
    // initilaize list with current lanuages

    //   LangBoxManager.allLangTypes.Add(this);




}
