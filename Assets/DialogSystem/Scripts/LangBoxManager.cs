using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LangBoxManager : MonoBehaviour
{

    public static List<LangBox> allLangBoxes = new List<LangBox>();
    public static List<LangBoxType> allLangTypes = new List<LangBoxType>();

    [SerializeField]
    public static List<string> currentLanguages = new List<string>(); 

    public LangBoxType type;

 








    public static void setAllLanguages()
    {
        foreach (LangBox b in allLangBoxes)
        {

            b.TryWriteText();
        }
    }




}
