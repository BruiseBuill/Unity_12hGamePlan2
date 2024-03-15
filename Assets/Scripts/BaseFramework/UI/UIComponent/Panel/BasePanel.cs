using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.MaterialProperty;

namespace BF.UI
{
    public class BasePanel : MonoBehaviour
    {
        /*
        [Header("可以通过GetText找到的Text")]
        [SerializeField] protected Text[] texts;
        protected Dictionary<string, int> textDic = new Dictionary<string, int>();
        //
        [Header("可以通过GetImage找到的Image")]
        [SerializeField] protected Image[] images;
        protected Dictionary<string, int> imageDic = new Dictionary<string, int>();
        //
        [Header("可以通过GetText找到的TextMeshPro")]
        [SerializeField] protected TextMeshProUGUI[] textPros;
        protected Dictionary<string, int> textProDic = new Dictionary<string, int>();
        //
        

        protected virtual void Awake()
        {
            for (int i = 0; i < texts.Length; i++) 
            {
                textDic.Add(texts[i].gameObject.name, i);
            }
            for(int i = 0;i < images.Length; i++)
            {
                imageDic.Add(images[i].gameObject.name, i);
            }
            for (int i = 0; i < textPros.Length; i++)
            {
                textProDic.Add(textPros[i].gameObject.name, i);
            }
            UIManager.Instance().Register(this);
        }
        protected virtual void Start()
        {
           
        }


        public Text GetText(string TextName)
        {
            if (textDic.ContainsKey(TextName))
            {
                return texts[textDic[TextName]];   
            }
            else
            {
                Debug.LogError($"No Text: {TextName}");
                return null;    
            }
        }
        public Image GetImage(string ImageName)
        {
            if (imageDic.ContainsKey(ImageName))
            {
                return images[imageDic[ImageName]];
            }
            else
            {
                Debug.LogError($"No Image: {ImageName}");
                return null;
            }
        }
        public TextMeshProUGUI GetTextPro(string TextName)
        {
            if (textProDic.ContainsKey(TextName))
            {
                return textPros[textProDic[TextName]];
            }
            else
            {
                Debug.LogError($"No Text: {TextName}");
                return null;
            }
        }
        public virtual void ChangeText(string TextName,string content)
        {
            GetText(TextName).text = content;
        }
        public virtual void ChangeImage(string imageName,Sprite content)
        {
            GetImage(imageName).sprite = content;
        }
        */
        public virtual string Name { get => gameObject.name; }
        protected virtual void Awake()
        {
            UIManager.Instance().Register(this);
        }
        public virtual void Open()
        {
            gameObject.SetActive(true);
        }
        public virtual void Close()
        {
            gameObject.SetActive(false);
        }
    }
}