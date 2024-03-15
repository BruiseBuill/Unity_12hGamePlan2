using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BF.UI
{
    //Using extend-method to extend UIManager instead of rewriting UIManager
    //Also use extend-method to extend BasePanel
    //
    public class UIManager : Single<UIManager>
    {
        Dictionary<string, BasePanel> dic = new Dictionary<string, BasePanel>();

        public void Register(BasePanel panel)
        {
            dic.Add(panel.Name, panel);
        }
        public BasePanel GetPanel(string panelName)
        {
            if (dic.ContainsKey(panelName))
                return dic[panelName];
            else
                Debug.LogError($"No Panel:{panelName}");
            return null;
        }
        public void Unregister(BasePanel panel)
        {
            dic.Remove(panel.Name);
        }
        private void OnDisable()
        {
            foreach(var pair in dic)
            {
                pair.Value.Close();
            }
            dic.Clear();
        }

        public void OpenPanel(string panelName)
        {
            GetPanel(panelName).Open();
        }
        public void ClosePanel(string panelName)
        {
            GetPanel(panelName).Close();
        }
        /*
        public void ChangeImage(string panelName, string imageName, Sprite content)
        {
            GetPanel(panelName).ChangeImage(imageName, content);
        }
        public void ChangeText(string panelName, string textName, string content)
        {
            GetPanel(panelName).ChangeText(textName, content);
        }*/
    }
}

