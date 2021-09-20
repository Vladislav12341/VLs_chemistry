using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text _textField;
    [SerializeField] private List<Button> _button;
    private List<string> _textInfo = new List<string>();
    private int iterator = -1;

    #region TextReader
    private List<string> GetText(string File_Name)
    {
        TextAsset data = (TextAsset)Resources.Load(File_Name);
        string[] tmp = data.text.Split('\n');
        _textInfo.AddRange(tmp);
        return _textInfo;
    }
    #endregion
    #region TurnPages
    private void CheckEndOfText(int clickcount)
    {
        if (clickcount == _textInfo.Count)
        {
            _textField.text = "";
            _textInfo.Clear();
            iterator = -1;
            ButtonActrivator();
        }
        else
        if (clickcount == -1)
        {
            _textField.text = "";
            _textInfo.Clear();
            iterator = -1;
            ButtonActrivator();
        }
    }
    public void Next()
    {
        iterator++;
        if (iterator != _textInfo.Count) //костыль
        {
            _textField.text = _textInfo[iterator];
        }
        CheckEndOfText(iterator);
    }
    public void Prev()
    {
        iterator--;
        if (iterator != _textInfo.Count && iterator >= 0) //костыль
        {
            _textField.text = _textInfo[iterator];
        }
        else
            CheckEndOfText(iterator);
    }
    #endregion 
    #region MainButtons
    public void Vvodnii_But()
    {
        GetText("ControlText");
        ButtonActrivator();
        Next();
    }
    public void Metod_But()
    {
        GetText("MetodText");
        ButtonActrivator();
        Next();
    }
    #endregion
    private void ButtonActrivator()
    {
        foreach (var buttons in _button)
        {
            if (buttons.IsActive())
            {
                buttons.gameObject.SetActive(false);
            }
            else
            {
                buttons.gameObject.SetActive(true);
            }
        }
    }
}
