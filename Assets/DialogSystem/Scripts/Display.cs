using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    namespace DialogSystem.Scripts
{
    
    public class Display : MonoBehaviour
    {
        public DialogBox start;
        public Text text;


        public Text t0;
        public Text t1;
        public Text t2;
        public Dropdown responses;

        private void Start()
        {
           
                text.text = start.currentText.Languages.valueList[0];


                if (start.options[0] != null)
                {
                    t0.text = start.options[0].Languages.valueList[0];
                }
                if (start.options[1] != null)
                {
                    t1.text = start.options[1].Languages.valueList[0];
                }
            
            /*
            if (start.options[2] != null)
            {
                t2.text = start.options[2].Languages.valueList[0];
            }*/

        }

        public void button1()
        {
            start.DynamicOutputs.GetType();
                Debug.Log(start.DynamicOutputs.GetType());
            
          // start.GetOutputPort(fieldName: "Current Text");
        }

        public void button2()
        {

        }

        public  void button3()
        {

        }



        private void Update()
        {

            





        }






    }
}
