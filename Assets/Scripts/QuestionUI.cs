
    using System;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class QuestionUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private TMP_Text _textVariantFirst;
        [SerializeField] private TMP_Text _textVariantSecond;
        [SerializeField] private List<GameObject> _bgVariants;
        private QuestionItem _questionItem;

        private void Awake()
        {
            RoleBgVariant();
        }
        
        

        public void CreateUI(QuestionItem questionItem)
        {
            _questionItem = questionItem;
            _text.text = questionItem.QuestionText;
            _textVariantFirst.text = questionItem.VariantFirst;
            _textVariantSecond.text = questionItem.VariantSecond;
        }

        public void ButtonFirst()
        {
            FindObjectOfType<GameController>().HandleButtonVariantFirst(_questionItem);
            RoleBgVariant();
        }

        public void ButtonSecond()
        {
            FindObjectOfType<GameController>().HandleButtonVariantSecond(_questionItem);
            RoleBgVariant();
        }

        private void RoleBgVariant()
        {
            _bgVariants.ForEach(i=>i.SetActive(false));
            _bgVariants[Random.Range(0, _bgVariants.Count)].SetActive(true);
        }
    }
