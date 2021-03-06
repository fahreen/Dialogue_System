using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[ExecuteAlways]
public class LanguageTool : EditorWindow
{
    [MenuItem("LocalizationTool/Edit")]
    public static void OpenWindow()
    {
        // EditorWindow.GetWindow<LanguageTool>("LanguageTool");
        //CreateInstance<LanguageTool>().Show();
        //open window
        GetWindow<LanguageTool>();
    }

    bool click = false;
    int selected;
    int tempSelected;
    int selected2 = 0;
    string CL = "NULL";
    string NewLang = "";
    string NewValue = "";
    Vector2 scrollPos;
    string newTextId = "";



    //SerializedObject so;
    //SerializedProperty CLS;
    // SerializedProperty currentLang;
    //SerializedProperty id;



    private void OnEnable()
    {
        // LangBoxManager.setAllLanguages();
        //  so = new SerializedObject(this);
        // currentLang = so.FindProperty("gridSize");

    }

    private void OnGUI()
    {
        //so.Update();
        LangBoxType[] gameTexts = Resources.FindObjectsOfTypeAll<LangBoxType>();
        GUILayout.Space(30);
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUILayout.Space(10);
            GUILayout.Label("MANAGE LANGUAGES:");


            GUILayout.Space(10);
            using (new GUILayout.HorizontalScope())
            {
                addLanguage(gameTexts);
            }

            GUILayout.Space(10);

            // use manager 

            setLanguage(gameTexts);
            GUILayout.Space(20);
            deleteLanguage(gameTexts);
        }

        GUILayout.Space(30);
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(850), GUILayout.Height(500));
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUILayout.Space(10);
            GUILayout.Label("MANAGE VOCABULARY:");

            displayLangboxes(gameTexts);
        }
        EditorGUILayout.EndScrollView();

        // so.ApplyModifiedProperties();
    }





    void setLanguageEmpty()
    {
        string[] LanguageOptions = new string[0];
        EditorGUILayout.Popup("Set Current Language: ", selected, LanguageOptions, EditorStyles.popup);
    }







    void setLanguage(LangBoxType[] gameTexts)
    {
        using (new GUILayout.HorizontalScope())
        {
            // get language option selected by the user
            for (int i = 0; i < gameTexts.Length; i++)
            {
                if (gameTexts[i].id == "manager")
                {
                    if (gameTexts[i].Languages.keyList.Count == 0)
                    {
                        EditorGUILayout.Popup("Set Current Language: ", selected, new string[0], EditorStyles.popup, GUILayout.Width(300));
                    }

                    else
                    {

                        int size = gameTexts[i].Languages.keyList.Count;
                        string[] LanguageOptions = new string[size];

                        // 
                        for (int j = 0; j < size; j++)
                        {
                            LanguageOptions[j] = gameTexts[i].Languages.keyList[j];
                        }

                        tempSelected = selected;
                        selected = EditorGUILayout.Popup("Set Current Language: ", selected, LanguageOptions, EditorStyles.popup, GUILayout.Width(300));


                        if (selected != tempSelected)
                        {

                            LangBoxManager.setAllLanguages();

                            // SceneView.RepaintAll();
                        }
                        // for each LangBox set type.current Lang to selected
                        foreach (LangBoxType t in gameTexts)
                        {
                            t.currentLang = LanguageOptions[selected];


                        }
                    }


                }
            }





            //int size = gameTexts[0].Languages.keyList.Count;

            // string[] LanguageOptions = new string[size];

            /*for (int i = 0; i < size; i++)
            {
                LanguageOptions[i] = gameTexts[0].Languages.keyList[i];
            }*/
            //   tempSelected = selected;
            // selected = EditorGUILayout.Popup("Set Current Language: ", selected, LanguageOptions, EditorStyles.popup, GUILayout.Width(300));


            /*
            if(selected != tempSelected)
            {
                //UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
                LangBoxManager.setAllLanguages();
                //SceneView.currentDrawingSceneView.Repaint();
                SceneView.RepaintAll();
            }
            // for each LangBox set type.current Lang to selected
            foreach (LangBoxType t in gameTexts)
            {
                t.currentLang = LanguageOptions[selected];
            }
            

            //CL = LanguageOptions[selected];*/
        }


    }



    // for each scriptable object, display properties
    void displayLangboxes(LangBoxType[] gameTexts)
    {
        GUILayout.Space(10);
        GUILayout.Label("Current Vocabulay: ");
        GUILayout.Space(5);


        foreach (LangBoxType t in gameTexts)
        {
            if (t.id != "manager")
            {
                using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    using (new GUILayout.HorizontalScope())
                    {
                        GUILayout.Label("ID:", GUILayout.Width(30));


                        // display id
                        GUILayout.Label(t.id, GUILayout.Width(100));





                        // display remove button
                        if (GUILayout.Button("Remove", GUILayout.Width(100)))
                        {
                            AssetDatabase.DeleteAsset("Assets/DialogSystem/SO/" + t.name + ".asset");
                        }
                    }
                    if (t.Languages.keyList.Count > 0)
                    {

                        click = EditorGUILayout.Foldout(click, "Show details:");
                        if (click)
                        {
                            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                            {

                                for (int i = 0; i < t.Languages.keyList.Count; i++)
                                {
                                    using (new GUILayout.HorizontalScope())
                                    {
                                        GUILayout.Label(t.Languages.keyList[i] + ":", GUILayout.Width(100));
                                        //GUILayout.Label(t.Languages.valueList[i],GUILayout.Width(200));

                                        t.Languages.valueList[i] = GUILayout.TextArea(t.Languages.valueList[i], GUILayout.Width(200), GUILayout.Height(50));
                                        EditorUtility.SetDirty(t);
                                        /*
                                        if ( GUILayout.Button("Change Value", GUILayout.Width(100)))
                                        {

                                           t.Languages.valueList[i] = NewValue;
                                        }*/
                                    }
                                }




                            }


                        };


                    }
                }
            }
        }
        GUILayout.Space(10);








        using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
        {
            newTextId = GUILayout.TextField(newTextId, GUILayout.Width(170));
            GUILayout.Space(15);
            if (GUILayout.Button("Add New Gametext", GUILayout.Width(310)) && newTextId.Length > 0)
            {
                LangBoxType t = ScriptableObject.CreateInstance<LangBoxType>();
                t.setvalues();
                t.id = newTextId;
                AssetDatabase.CreateAsset(t, "Assets/DialogSystem/SO/" + t.id + ".asset");


                /*
                LangBoxType t = ScriptableObject.CreateInstance<LangBoxType>();
                t.id = newTextId;

                LangBoxType[] gameTextsCopy = (LangBoxType[])gameTexts.Clone();
                for (int i = 0; i < gameTextsCopy.Length; i++)
                {
                    if (gameTextsCopy[i].id == "manager")
                    {



                        t.Languages = gameTexts[i].Languages;
                        t.currentLang = gameTexts[i].currentLang;
                        AssetDatabase.CreateAsset(t, "Assets/DialogSystem/SO/" + t.id + ".asset");
                    }
                }
                */






            }
        }



        GUILayout.Space(15);
        GUILayout.Space(15);
        //GUILayout.Button("Add Language", GUILayout.Width(310));
        // GUILayout.Space(15);
        // GUILayout.Button("Delete Language", GUILayout.Width(310));

    }

    void addLanguage(LangBoxType[] gameTexts)
    {
        NewLang = GUILayout.TextField(NewLang, GUILayout.Width(150));
        GUILayout.Space(5);
        if (GUILayout.Button("Add Language", GUILayout.Width(140)))
        {

            foreach (LangBoxType k in gameTexts)
            {

                k.Languages.AddElement(NewLang, "ENTER TEXT HERE");
                EditorUtility.SetDirty(k);

            }







        }



    }


    void deleteLanguage(LangBoxType[] gameTexts)
    {
        using (new GUILayout.HorizontalScope())
        {

            for (int i = 0; i < gameTexts.Length; i++)
            {
                if (gameTexts[i].id == "manager")
                {
                    int size = gameTexts[i].Languages.keyList.Count;
                    string[] LanguageOptions = new string[size];

                    for (int j = 0; j < size; j++)
                    {
                        LanguageOptions[j] = gameTexts[i].Languages.keyList[j];
                    }
                    selected2 = EditorGUILayout.Popup("Delete a Languages: ", selected2, LanguageOptions, EditorStyles.popup, GUILayout.Width(300));


                    if (GUILayout.Button("Delete", GUILayout.Width(100)))
                    {
                        foreach (LangBoxType p in gameTexts)
                        {
                            Debug.Log("Hello");
                            p.Languages.removeElement(LanguageOptions[selected2]);
                        }

                    }

                }










                //for each type in langType, add a language



            }



        }
    }


    void deleteLanguageEmpty()
    {

        using (new GUILayout.HorizontalScope())
        {
            string[] LanguageOptions = new string[0];
            EditorGUILayout.Popup("Delete a Language: ", selected, LanguageOptions, EditorStyles.popup);
            GUILayout.Button("Delete", GUILayout.Width(100));

        }

    }

}
