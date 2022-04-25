using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace DialogSystem.Scripts.Editor
{
    [CustomNodeEditor(typeof(DialogBox))]
    public class DialogNodeEditor : NodeEditor
    {
        int size;
        int tempSize;

        void draw(ReorderableList list)
        {
            list.elementHeightCallback = (int index) =>
            {
                return 60;
            };


            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var segment = serializedObject.targetObject as DialogBox;

                NodePort port = segment.GetPort("options " + index);

                segment.options[index].Languages.valueList[0] = GUI.TextArea(rect, segment.options[index].Languages.valueList[0]);
                for (int k = 0; k < segment.options.Count; k++){
                    EditorUtility.SetDirty(segment.options[k]);
                }
                
                
                
                
                if (port != null)
                {
                    Vector2 pos = rect.position + (port.IsOutput ? new Vector2(rect.width + 6, 0) : new Vector2(-36, 0));
                    NodeEditorGUILayout.PortField(pos, port);
                }
            };
        }

        private void  OnEnable()
        {
            var segment = serializedObject.targetObject as DialogBox;
            size = segment.options.Count;
        }

        private void check()
        {
            var segment = serializedObject.targetObject as DialogBox;
            tempSize = segment.options.Count;
            if(tempSize> size)
            {
                for (int i =0; i < tempSize; i++)
                {
                    if(segment.options[i] == null || (i > 0 && segment.options[i] == segment.options[i-1] ))
                    {
                        LangBoxType tempObject = ScriptableObject.CreateInstance<LangBoxType>();
                        tempObject.setvalues();
                        AssetDatabase.CreateAsset(tempObject, "Assets/DialogSystem/SO/" + tempObject.id + ".asset");
                        segment.options[i] = tempObject;
                    }

                    
                }
                
                size = tempSize;
            }

            if (tempSize < size)
            {
                size = tempSize;
            }


        }


        public override void OnBodyGUI()
        {
            serializedObject.Update();

            var segment = serializedObject.targetObject as DialogBox;

            NodeEditorGUILayout.PortField(segment.GetPort("currentText"));


            segment.currentText.Languages.valueList[0] = GUILayout.TextArea(segment.currentText.Languages.valueList[0], new GUILayoutOption[]
            


            {
                GUILayout.MinHeight(50),
                
            });

            EditorUtility.SetDirty(segment.currentText);
            
            //EditorUtility.SetDirty(segment.options);

            NodeEditorGUILayout.DynamicPortList(
                "options", 
                typeof(LangBoxType), 
                serializedObject, 
                NodePort.IO.Input, 
                Node.ConnectionType.Override, 
                Node.TypeConstraint.None,
                draw);
            
            
            foreach (XNode.NodePort dynamicPort in target.DynamicPorts) {
                if (NodeEditorGUILayout.IsDynamicPortListPort(dynamicPort)) continue;
                NodeEditorGUILayout.PortField(dynamicPort);
            }

            check();

            serializedObject.ApplyModifiedProperties();
        }


    }
}