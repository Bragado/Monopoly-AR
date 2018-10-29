using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePropertyCardMenu : MonoBehaviour {

    public delegate void Responde();
    public Responde done;

    public Text m_MyText;

    public void SetMessage(string message)
    {
        m_MyText.text = message;
    }


    public void Accept()
    {
        done();
    }
}
