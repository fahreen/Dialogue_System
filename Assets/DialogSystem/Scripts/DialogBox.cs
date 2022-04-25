using System;
 using System.Collections.Generic;
using XNode;
using UnityEngine;

using UnityEditor;

namespace DialogSystem.Scripts
{
    [Serializable]
    //public struct Connection {}
    public class DialogBox: Node
    {
       


        //public List<LangBoxType> optionsData;

        private LangBoxType tempObject;

        [Input]
        public LangBoxType currentText;

        [Output(dynamicPortList = true)]
        public List<LangBoxType> options;


        public override object GetValue(NodePort port)
        {
            return null;
        }

        private void OnDestroy()
        {
            AssetDatabase.DeleteAsset("Assets/DialogSystem/SO/" + currentText.id + ".asset");
            
            for(int i =0; i< options.Count; i++)
            {
                AssetDatabase.DeleteAsset("Assets/DialogSystem/SO/" + options[i].id + ".asset");
            }
        }






        protected override void Init()
        {

            // declare parts for the node
            // set current dialog to new scriptable object, this automatically filled with manager values

            // tempObject = new LangBoxType();
            if (this.currentText == null){
                tempObject = ScriptableObject.CreateInstance<LangBoxType>();
                tempObject.setvalues();
                AssetDatabase.CreateAsset(tempObject, "Assets/DialogSystem/SO/" + tempObject.id + ".asset");
                this.currentText = tempObject;

                options = new List<LangBoxType>();
            }

            
        }



    }
}